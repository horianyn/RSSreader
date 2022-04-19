using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RSSreader.Models
{
    public class RssDbContext : DbContext
    {
        public RssDbContext()
            :base("DefaultConnection")
        {
        }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Feed> Feeds { get; set; }
    }
}