using TracerLibrary.Model;

namespace TracerLibrary.Logic
{

    public interface ITracer
    {
        void StartTrace();

        void StopTrace();

        TraceResult GetTraceResult();
    }
}