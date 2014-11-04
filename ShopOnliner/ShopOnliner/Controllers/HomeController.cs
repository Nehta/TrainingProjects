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
        private IKernel AppKernel; 

        public HomeController()
        {
            AppKernel = new StandardKernel(new ServiceNinjectModule());
            this.service = AppKernel.Get<IService>();
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
            return View(
                new PageView {PageItems=items, NextPage=page+1, PrevPage= page-1, Type= type, LastPage=lastPage});
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
