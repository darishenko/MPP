using System.IO;
using TracerLibrary.Model;
using TracerLibrary.Serialization.Interface;

namespace Application.Writer
{
    public class FileWriter : IWriter
    {
        private readonly string _fileName;

        public FileWriter(ITraceSerializer traceSerializer, string fileName)
        {
            _traceSerializer = traceSerializer;
            _fileName = fileName;
        }

        private ITraceSerializer _traceSerializer { get; set; }

        public void Print(TraceResult traceResult)
        {
            var fileName = _fileName + _traceSerializer.GetFileExtension();
            var result = traceResult.ToString();
            if (_traceSerializer != null) result = _traceSerializer.Serialize(traceResult);

            File.WriteAllText(fileName, result);
        }

        public void SetTraceSerializer(ITraceSerializer traceSerializer)
        {
            _traceSerializer = traceSerializer;
        }
    }
}