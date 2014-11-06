using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnliner.Models
{
    public class CatalogPageView
    {
        public IEnumerable<Item> PageItems;
        public Dictionary<string, string> Links;
        public string Type;
    }
}