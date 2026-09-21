using System.Xml.Serialization;

namespace API.Hypermedia
{
    public class HypermediaLink
    {
        [XmlAnyAttribute]
        public string Rel { get; set; } = string.Empty;

        [XmlAnyAttribute]
        public string Href { get; set; } = string.Empty;

        [XmlAnyAttribute]
        public string Type { get; set; } = "application/json";

        [XmlAnyAttribute]
        public string Action { get; set; } = string.Empty;

    }
}
