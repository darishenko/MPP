#nullable enable
using System.IO;
using System.Threading;
using Application.Writer;
using ConsoleApp.Example;
using TracerLibrary.Logic;
using TracerLibrary.Serialization.Interface;
using TracerLibrary.Serialization.Serializer;

namespace ConsoleApp
{
    public class Program
    {
        private static void Main()
        {
            var program = new Program();
            var thread = new Thread(program.Method);
            var tracer = new MainTracer();

            var foo = new Foo(tracer);
            foo.MyMethod();
            thread.Start(tracer);
            thread.Join();

            var traceResult = tracer.GetTraceResult();
            ITraceSerializer xmlSerializer = new XmlTraceSerializer();
            ITraceSerializer jsonSerializer = new JsonTraceSerializer();

            IWriter consoleTraceResultPrinter = new ConsoleWriter(xmlSerializer);
            consoleTraceResultPrinter.Print(traceResult);
            consoleTraceResultPrinter.SetTraceSerializer(jsonSerializer);
            consoleTraceResultPrinter.Print(traceResult);

            var resultDir = new DirectoryInfo(@"..\..\..\..\..\ResultFiles");
            PathWorker.CreateDirectory(resultDir.FullName);

            var dataPath = Path.Combine(resultDir.FullName, "result");
            IWriter fileTraceResultPrinter = new FileWriter(xmlSerializer, dataPath);
            fileTraceResultPrinter.Print(traceResult);
            fileTraceResultPrinter.SetTraceSerializer(jsonSerializer);
            fileTraceResultPrinter.Print(traceResult);
        }

        private void Method(object? o)
        {
            var tracer = (MainTracer) o!;
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
            Thread.Sleep(100);
        }
    }
}