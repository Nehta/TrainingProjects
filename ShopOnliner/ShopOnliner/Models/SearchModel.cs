using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnliner.Models
{
    public class SearchModel
    {
        public string Name { get; set;}
        public int? MinPrice{get; set;}
        public int? MaxPrice { get; set;}
        public int? Rate { get; set; }
        public int SearchPage { get; set; }
    }
}