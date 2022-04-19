using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using RSSreader.DTOs;
using RSSreader.Models;
using RSSreader.ViewModels;

namespace RSSreader.Controllers
{
    public class FeedsController : Controller
    {
        private RssDbContext _context = new RssDbContext();

        public ActionResult Index(string searchString)
        {
            var feeds = new List<Feed>();
            if (!String.IsNullOrEmpty(searchString)) 
            {
                TempData["searchString"] = searchString;
                feeds = _context.Feeds.Where(f => f.Name.Contains(searchString)).ToList();
            }
            else 
            {
                feeds = _context.Feeds.ToList();
            }

            var feedsViewModel = new List<FeedViewModel>();

            foreach (var feed in feeds)
            {
                feedsViewModel.Add((FeedViewModel)feed);
            }

            return View(feedsViewModel);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var feed = _context.Feeds.Find(id);

            if (feed == null)
                return HttpNotFound();

            var feedViewModel = (FeedViewModel)feed;
            feedViewModel.ArticlesCount = _context.Articles.Count();

            return View(feedViewModel);
        }

        public ActionResult Reload(int? id)
        {
            if (id == null && id == 0)
                return RedirectToAction("Index", "Articles");

            var feed = _context.Feeds.Find(id);

            var feedXml = GetFeedFromXML(feed.Link);

            if(feedXml.Articles != null)
                PopulateArticles(feed.Id, feedXml.Articles);

            return RedirectToAction("Index", "Articles", new { id = feed.Id });
        }

        public ActionResult Create(AddFeedViewModel newFeed)
        {
            if (!ModelState.IsValid)
                return View(newFeed);
            
            var feedXml = GetFeedFromXML(newFeed.Link);
            
            if(feedXml.Title == null && feedXml.Articles == null)
            {
                TempData["Error"] = "Check the link, please.";
                return View(newFeed);
            }

            if (_context.Feeds.Select(f => f.Link).Contains(newFeed.Link))
            {
                TempData["Error"] = "Feed with such link already exist.";
                return View(newFeed);
            }

            var feed = new Feed()
            {
                Name = newFeed.Name,
                Title = feedXml.Title,
                Link = newFeed.Link,
                Description = feedXml.Description
            };

            _context.Feeds.Add(feed);
            _context.SaveChanges();

            PopulateArticles(feed.Id, feedXml.Articles);


            return RedirectToAction("Index", "Articles", new { id = feed.Id });
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Feed feed = _context.Feeds.Find(id);

            if (feed == null)
                return HttpNotFound();

            return View(feed);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string yes, string no)
        {
            if (!string.IsNullOrEmpty(no))
                return RedirectToAction("Index");

            Feed feed = _context.Feeds.Find(id);

            _context.Feeds.Remove(feed);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private FeedXmlDTO GetFeedFromXML(string url)
        {
            XmlSerializer serialiser = new XmlSerializer(typeof(FeedXmlDTO));

            WebRequest request = WebRequest.Create(url);

            var feed = new FeedXmlDTO();

            try
            {
                using (XmlReader reader = XmlReader.Create(url))
                {
                    reader.ReadStartElement();
                    feed = (FeedXmlDTO)serialiser.Deserialize(reader);
                }
            }
            catch (Exception)
            {
                return feed;
            }

            return feed; 
        }

        private void PopulateArticles(int feedId, List<ArticleXmlDTO> articles)
        { 
            foreach (var item in articles)
            {
                var article = new Article
                {
                    GuId = item.GuId,
                    Title = item.Title,
                    Link = item.Link,
                    PublishDate = DateTime.Parse(item.PublishDate),
                    Description = item.Description,
                    FeedId = feedId
                };

                if(!_context.Articles
                            .Where(a => a.FeedId == feedId)
                            .Select(a => a.GuId)
                            .Contains(article.GuId))
                    _context.Articles.Add(article);
            }
            _context.SaveChanges();
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
