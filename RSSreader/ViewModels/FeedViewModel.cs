using RSSreader.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RSSreader.ViewModels 
{
    public class FeedViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Title { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        [Display( Name = "Articles in this Feed")]
        public int ArticlesCount { get; set; }

        public static explicit operator FeedViewModel(Feed feed)
        {
            var feedViewModel = new FeedViewModel
            {
                Id = feed.Id,
                Name = feed.Name,
                Title = feed.Title,
                Link = feed.Link,
                Description = feed.Description,
                ArticlesCount = 0
            };

            return feedViewModel;
        }
    }
}