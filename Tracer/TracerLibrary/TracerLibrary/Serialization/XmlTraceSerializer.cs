using System.IO;
using System.Xml.Serialization;
using TracerLibrary.Model;
using TracerLibrary.Serialization.Interface;

namespace TracerLibrary.Serialization.Serializer
{

    public class XmlTraceSerializer : ITraceSerializer
    {
        public string Serialize(TraceResult traceResult)
        {
            using var memoryStream = new MemoryStream();
            new XmlSerializer(typeof(TraceResult)).Serialize(memoryStream, traceResult);
            memoryStream.Position = 0;
            return new StreamReader(memoryStream).ReadToEnd();
        }

        public TraceResult? Deserialize(string traceResult)
        {
            using var reader = new StringReader(traceResult);
            return (TraceResult) new XmlSerializer(typeof(TraceResult)).Deserialize(reader)!;
        }

        public string GetFileExtension()
        {
            return ".xml";
        }
    }
}