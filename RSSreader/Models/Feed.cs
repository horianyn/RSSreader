using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RSSreader.Models 
{
    public class Feed
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }
    }
}