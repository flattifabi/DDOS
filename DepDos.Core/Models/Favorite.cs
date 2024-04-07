using System.Xml.Serialization;

namespace DepDos.Core.Models;

public class Favorite
{
    [XmlAttribute]
    public string FavoriteName { get; set; }
    [XmlAttribute]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [XmlElement]
    public List<string> WebAddresses { get; set; } = new List<string>();
}