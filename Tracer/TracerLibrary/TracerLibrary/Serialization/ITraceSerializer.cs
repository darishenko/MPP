using TracerLibrary.Model;

namespace TracerLibrary.Serialization.Interface
{
    public interface ITraceSerializer
    {
        string Serialize(TraceResult traceResult);
        TraceResult Deserialize(string traceResult);
        string GetFileExtension();
    }
}