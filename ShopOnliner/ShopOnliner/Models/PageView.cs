using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopOnliner.Models
{
    public class PageView
    {
        public IQueryable<Item> PageItems;
        public int NextPage;
        public int PrevPage;
        public int LastPage;
        public string Type;
    }
}