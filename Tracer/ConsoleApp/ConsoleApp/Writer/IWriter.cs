using TracerLibrary.Model;
using TracerLibrary.Serialization;

namespace ConsoleApp.Writer
{
    public interface IWriter
    {
        void Print(TraceResult traceResult);

        void SetTraceSerializer(ITraceSerializer traceSerializer);
    }
}