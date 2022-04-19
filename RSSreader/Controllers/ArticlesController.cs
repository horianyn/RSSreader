using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RSSreader.Models;

namespace RSSreader.Controllers
{
    public class ArticlesController : Controller
    {
        private RssDbContext _context = new RssDbContext();

        public ActionResult Index(int? id, DateTime? fromDate, DateTime? toDate)
        {
            if (fromDate == null)
                fromDate = DateTime.MinValue;


            var articles = _context.Articles
                .Where(a => !a.IsDeleted && a.PublishDate > fromDate)
                .OrderByDescending(a => a.PublishDate)
                .ToList();
            
            if(id != null && id != 0)
            {
                articles = articles.Where(a => a.FeedId == id).ToList();
                TempData["feed"] = _context.Feeds.Find(id).Name;
            }


            if (toDate > fromDate)
            {
                articles = articles.Where(a => a.PublishDate <= toDate).ToList();
                TempData["toDate"] = toDate;
            }

            TempData["id"] = id;
            TempData["fromDate"] = fromDate;

            return View(articles);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
