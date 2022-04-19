using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RSSreader.Models
{
    public class Article
    {
        public int Id { get; set; }

        public string GuId { get; set; }

        public string Title { get; set; }

        public string Link { get; set; }
        [Display(Name = "Date of Publish")]
        public DateTime PublishDate { get; set; }

        public string Description { get; set; }

        public bool IsRead { get; set; }

        public bool IsDeleted { get; set; } = false;

        public int FeedId { get; set; }

        public Feed Feed { get; set; }

    }
}