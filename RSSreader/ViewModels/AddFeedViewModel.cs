using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RSSreader.ViewModels
{
    public class AddFeedViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Link { get; set; }
    }
}