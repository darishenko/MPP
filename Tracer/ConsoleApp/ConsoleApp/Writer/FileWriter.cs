using System;
using System.IO;
using TracerLibrary.Model;
using TracerLibrary.Serialization.Interface;

namespace Application.Writer
{

    public class FileWriter : IWriter
    {
        private ITraceSerializer _traceSerializer { get; set; }
        private String _fileName;

        public FileWriter(ITraceSerializer traceSerializer, String fileName)
        {
            _traceSerializer = traceSerializer;
            _fileName = fileName;
        }

        public void Print(TraceResult traceResult)
        {
            String fileName = _fileName + _traceSerializer.GetFileExtension();
            var result = traceResult.ToString();
            if (_traceSerializer != null)
            {
                result = _traceSerializer.Serialize(traceResult);
            }

            File.WriteAllText(fileName, result);
        }

        public void SetTraceSerializer(ITraceSerializer traceSerializer)
        {
            _traceSerializer = traceSerializer;
        }
    }
}