using Newtonsoft.Json;
using TracerLibrary.Model;
using TracerLibrary.Serialization.Interface;

namespace TracerLibrary.Serialization.Serializer
{

    public class JsonTraceSerializer : ITraceSerializer
    {
        public string Serialize(TraceResult traceResult)
        {
            return JsonConvert.SerializeObject(traceResult, Formatting.Indented);
        }

        public TraceResult Deserialize(string traceResult)
        {
            return JsonConvert.DeserializeObject<TraceResult>(traceResult);
        }

        public string GetFileExtension()
        {
            return ".json";
        }
    }
}