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

        public bool checkbox_1 { get; set; }
        public bool checkbox_2 { get; set; }
        public bool checkbox_3 { get; set; }
        public bool checkbox_4 { get; set; }
        public bool checkbox_5 { get; set; }

        public string attr_1 { get; set; }
        public string attr_2 { get; set; }
        public string attr_3 { get; set; }
        public string attr_4 { get; set; }
        public string attr_5 { get; set; }
    }
}