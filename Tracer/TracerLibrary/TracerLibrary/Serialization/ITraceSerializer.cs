using TracerLibrary.Model;

namespace TracerLibrary.Serialization
{
    public interface ITraceSerializer
    {
        string Serialize(TraceResult traceResult);
        TraceResult Deserialize(string traceResult);
        string GetFileExtension();
    }
}