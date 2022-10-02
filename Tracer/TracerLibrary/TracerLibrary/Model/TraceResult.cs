using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TracerLibrary.Model
{

    [Serializable]
    public class TraceResult
    {
        [XmlElement(ElementName = "Thread")] public List<ThreadInfo> ThreadsInfo { get; init; } = new();
    }
}