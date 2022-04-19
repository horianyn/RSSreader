using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace RSSreader.DTOs
{   
    [XmlRoot(ElementName = "channel")]
    public class FeedXmlDTO
    {
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "description")]
        public string Description { get; set; }

        [XmlElement(ElementName = "item")]
        public List<ArticleXmlDTO> Articles { get; set; }
    }

    public class ArticleXmlDTO
    {
        [XmlElement(ElementName = "guid")]
        public string GuId { get; set; }

        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "link")]
        public string Link { get; set; }

        [XmlElement(ElementName = "pubDate")]
        public string PublishDate { get; set; }

        [XmlElement(ElementName = "description")]
        public string Description { get; set; }
    }
}