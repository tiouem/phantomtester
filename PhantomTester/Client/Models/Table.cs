using System.Collections.Generic;
using System.Xml.Serialization;

namespace Client.Models
{

    [XmlRoot(ElementName = "Table")]
    public class Table
    {
        [XmlElement(ElementName = "Book")]
        public string Book { get; set; }

        [XmlElement(ElementName = "BookTitle")]
        public string BookTitle { get; set; }

        [XmlElement(ElementName = "Chapter")]
        public string Chapter { get; set; }

        [XmlElement(ElementName = "Verse")]
        public string Verse { get; set; }

        [XmlElement(ElementName = "BibleWords")]
        public string BibleWords { get; set; }
    }

    [XmlRoot(ElementName = "NewDataSet")]
    public class NewDataSet
    {
        [XmlElement(ElementName = "Table")]
        public List<Table> Table { get; set; }
    }
}





