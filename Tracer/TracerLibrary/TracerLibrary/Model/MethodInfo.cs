using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TracerLibrary.Model
{

    [Serializable]
    public record MethodInfo
    {
        [XmlAttribute] public string? Name { get; init; }
        [XmlAttribute] public string Class { get; init; }
        [XmlAttribute] public long ElapsedMills { get; init; }
        [XmlElement(ElementName = "Method")] public List<MethodInfo> Methods { get; init; } = new();
    }
}