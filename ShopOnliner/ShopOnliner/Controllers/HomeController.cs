using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnliner.Models;
using ShopOnliner.Services;
using Ninject;
namespace ShopOnliner.Controllers
{
    public class HomeController : Controller
    {
        private IService service;
        int i = 0;

        public HomeController(IService service)
        {
            this.service = service;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Catalog(string type, int page)
        {
            var items = service.GetPageItems(type, page);
            var lastPage = service.LastPage(type);
            if (!items.Any())
                return HttpNotFound();
            var links = AddLinks(page, lastPage);
            ViewBag.title = Url.Action(null);
            return View(
                new CatalogPageView {PageItems=items, Links=links, Type=type});
        }

        [HttpPost]
        public ActionResult Catalog(string type, int page, string word)
        {
            return RedirectToAction("Search", new { page = 1, type = type, word = word });
        }

        public ActionResult Search(string type, int page, string word)
        {
            var items = service.GetPageItemsFromSearch(page, type ,word);
            var lastPage = service.LastPageOfSearch(type, word);
            if (!items.Any())
                return HttpNotFound();
            var links = AddLinks(page, lastPage);
            return View("Catalog",
                new CatalogPageView { PageItems = items, Links = links, Type = type });
            
        }

        [HttpPost]
        public ActionResult Search(string type, int page, string oldword , string word)
        {
            return RedirectToAction("Search", new { page = 1, type = type, word = word });
        }

        


        


        private  Dictionary<string,string> AddLinks(int page, int lastPage)
        {
            var result = new Dictionary<string, string>();
            var url = Url.Action(null).Substring(0, Url.Action(null).LastIndexOf("/"));
            result.Add("<<", url + "/1/");
            if (page - 1 > 0) result.Add("назад", url + '/' + (page - 1) + '/');
            if (page + 1 <= lastPage) result.Add ("вперед", url + '/' + (page + 1) + '/');
            result.Add(">>", url + '/' + lastPage + '/');
            return result;
        }

        public ActionResult MainCatalog()
        {
            return View();
        }

        public ActionResult Info(int id)
        {
            ViewData.Model = service.FindItem(id);
            return View();
        }

        public FileContentResult ShowImage(int id)
        {
            var ItemToShow = service.FindItem(id);
            return File(ItemToShow.imageBinary, ItemToShow.imageType);
            
        }

        public ActionResult Invalid(string route)
        {
            return HttpNotFound();
        }

    }
}
