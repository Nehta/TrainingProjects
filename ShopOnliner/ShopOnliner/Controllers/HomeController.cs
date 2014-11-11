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

        public HomeController(IService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Catalog(string type, int page, SearchModel model)
        {
            if (this.Request.QueryString.HasKeys())
                return Search(type, 1, model);
           
            var items = service.GetPageItems(type, page);
            var lastPage = service.LastPage(type);
            if (!items.Any())
                return HttpNotFound();
            var links = AddLinks(page, lastPage);
            return View(
                new CatalogPageView {PageItems=items, Links=links, Type=type});
        }

        public ActionResult Search(string type, int page, SearchModel model)
        {
            var items = service.GetPageItemsFromSearch(type , model);
            var lastPage = service.LastPageOfSearch(type, model);
            if (!items.Any())
                return  View("BadSearch");
            var links = AddSearchLinks(lastPage, model, type, page);
           return View(
                new CatalogPageView { PageItems = items, Links = links, Type = type });
            
        }

        private Dictionary<string, string> AddSearchLinks(int lastPage, SearchModel model, string type, int page)
        {
            var result = new Dictionary<string, string>();
    
            result.Add("<<", Url.Action("Catalog", "Home", new { type = type, page = page , 
                Name = model.Name, MinPrice=model.MinPrice, MaxPrice=model.MaxPrice, Rate=model.Rate, SearchPage=1}));

           
            if (model.SearchPage - 1 > 0) result.Add("назад", Url.Action("Catalog", "Home", new { type = type, page = page, 
             Name = model.Name, MinPrice=model.MinPrice, MaxPrice=model.MaxPrice, Rate=model.Rate, SearchPage=model.SearchPage-1}));
      
            if (model.SearchPage + 1 <= lastPage) result.Add("вперед", Url.Action("Catalog", "Home", new { type = type, page = page,
             Name = model.Name, MinPrice=model.MinPrice, MaxPrice=model.MaxPrice, Rate=model.Rate, SearchPage=model.SearchPage+1}));
   
            result.Add(">>", Url.Action("Catalog", "Home", new { type = type, page = page, 
             Name = model.Name, MinPrice=model.MinPrice, MaxPrice=model.MaxPrice, Rate=model.Rate,SearchPage=lastPage}));
        
            return result;
        }

        private  Dictionary<string,string> AddLinks(int page, int lastPage)
        {
            var result = new Dictionary<string, string>();
            var url = Url.Action(null).Substring(0, Url.Action(null).LastIndexOf("/"));
            
            result.Add("<<", url + "/1");
            if (page - 1 > 0) result.Add("назад", url + '/' + (page - 1));
            if (page + 1 <= lastPage) result.Add ("вперед", url + '/' + (page + 1));
            result.Add(">>", url + '/' + lastPage);
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
