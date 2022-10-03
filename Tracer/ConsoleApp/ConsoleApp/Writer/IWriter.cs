using TracerLibrary.Model;
using TracerLibrary.Serialization.Interface;

namespace Application.Writer
{
    public interface IWriter
    {
        void Print(TraceResult traceResult);

        void SetTraceSerializer(ITraceSerializer traceSerializer);
    }
}