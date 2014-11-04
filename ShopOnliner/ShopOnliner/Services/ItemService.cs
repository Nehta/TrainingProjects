using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopOnliner.Models;

namespace ShopOnliner.Services
{
    public class ItemService:IService
    {
        private ItemsEntities context = new ItemsEntities();

        public IQueryable<Item> GetPageItems(string type, int page)
        {
            return context.Items.Where(x => x.type == type).OrderBy(x=>x.name).Skip(10 * (page - 1)).Take(10);
        }

        public Item FindItem(int id)
        {
            return context.Items.Find(id);
        }

        public int LastPage(string type)
        {
           return (context.Items.Where(x => x.type == type).Count() - 1) / 10 + 1;
        }
    }
}