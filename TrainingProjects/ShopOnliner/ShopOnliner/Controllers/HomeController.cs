using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopOnliner.Models;

namespace ShopOnliner.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Catalog(string type, int page)
        {
            var context = new ItemsEntities();
            var items = context.Items.Where(x => x.type == type);
            if (!items.Any())
                return HttpNotFound();
            ViewData.Model = items.OrderBy(x=>x.name).Skip(10 * (page - 1)).Take(10);
            ViewBag.prevPage = page - 1;
            ViewBag.nextPage = page + 1;
            ViewBag.LastPage = (context.Items.Where(x => x.type == type).Count()-1)/10+1;
            ViewBag.type = type;
            return View();
        }

        public ActionResult MainCatalog()
        {
            return View();
        }

        public ActionResult Info(int id)
        {
            var context = new ItemsEntities();
            ViewData.Model = context.Items.Find(id);
            return View();
        }

        public FileContentResult ShowImage(int id)
        {
            using (var context = new ItemsEntities())
            {
                var ItemToShow = context.Items.Find(id);
                return File(ItemToShow.imageBinary, ItemToShow.imageType);
            }
        }

        public ActionResult Invalid(string route)
        {
            return HttpNotFound();
        }

    }
}
