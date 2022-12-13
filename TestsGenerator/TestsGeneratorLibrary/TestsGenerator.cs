using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace TestsGenerator
{
    public class TestGenerator
    {
        private readonly List<string> fileNames;
        private readonly int maxProcessTasksCount;
        private readonly int maxReadFilesCount;
        private readonly int maxWriteFilesCount;

        public TestGenerator(List<string> fileNames, int maxReadableCount, int maxProcessCount, int maxWritableCount)
        {
            this.fileNames = new List<string>(fileNames);
            maxReadFilesCount = maxReadableCount;
            maxProcessTasksCount = maxProcessCount;
            maxWriteFilesCount = maxWritableCount;
        }

        public async Task Generate(AsyncFileWriter writer)
        {
            var linkOptions = new DataflowLinkOptions
            {
                PropagateCompletion = true
            };
            var maxReadableFilesTasks = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = maxReadFilesCount
            };
            var maxProcessableTasks = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = maxProcessTasksCount
            };
            var maxWritableTasks = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = maxWriteFilesCount
            };

            var asyncFileReader = new AsyncFileReader();
            var templateGenerator = new TemplateClassGenerator();

            var readingBlock = new TransformBlock<string, string>(asyncFileReader.Read, maxReadableFilesTasks);

            var processingBlock = new TransformBlock<string, List<TestClassInformation>>
                (templateGenerator.GetTemplate, maxProcessableTasks);

            var writingBlock = new ActionBlock< List<TestClassInformation> >
                (classTemplate => writer.Write(classTemplate), maxWritableTasks);

            readingBlock.LinkTo(processingBlock, linkOptions);                     
            processingBlock.LinkTo(writingBlock, linkOptions);

            foreach (var filePath in fileNames) 
                readingBlock.Post(filePath);

            readingBlock.Complete();

            await writingBlock.Completion;
        }
    }
}