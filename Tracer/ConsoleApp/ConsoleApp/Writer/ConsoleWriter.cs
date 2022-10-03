using System;
using TracerLibrary.Model;
using TracerLibrary.Serialization;

namespace ConsoleApp.Writer
{
    public class ConsoleWriter : IWriter
    {
        private ITraceSerializer _traceSerializer;

        public ConsoleWriter(ITraceSerializer traceSerializer)
        {
            _traceSerializer = traceSerializer;
        }

        public void Print(TraceResult traceResult)
        {
            var result = traceResult.ToString();
            if (_traceSerializer != null) result = _traceSerializer.Serialize(traceResult);

            Console.WriteLine(result);
        }

        public void SetTraceSerializer(ITraceSerializer traceSerializer)
        {
            _traceSerializer = traceSerializer;
        }
    }
}