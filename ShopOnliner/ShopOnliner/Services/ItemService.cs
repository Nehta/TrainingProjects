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

        public IEnumerable<Item> GetPageItemsFromSearch(string type, SearchModel searchModel )
        {
            int page = searchModel.SearchPage;
            return Search(searchModel, type).
                OrderBy(x=>x.name).Skip(10 * (page - 1)).Take(10);
        }

        public Item FindItem(int id)
        {
            return context.Items.Find(id);
        }

        public int LastPageOfSearch( string type, SearchModel searchModel)
        {
            return (Search(searchModel,type).Count() - 1) / 10 + 1;
        }

        public int LastPage(string type)
        {
            return (context.Items.Where(x => x.type == type).Count() - 1) / 10 + 1;
        }

        private IEnumerable<Item> Search(SearchModel searchModel, string type)
        {
            var items = context.Items.Where(x=>x.type==type);
            IEnumerable<Item> nameFilter;
            IEnumerable<Item> minPriceFilter;
            IEnumerable<Item> maxPriceFilter;
            IEnumerable<Item> rateFilter;

            if (searchModel.Name != null)
                nameFilter = items.Where(x => x.name.Contains(searchModel.Name));
            else nameFilter = items;
            if (searchModel.MinPrice != null)
                minPriceFilter = nameFilter.Where(x => x.minPrice >= searchModel.MinPrice && x.minPrice!=0);
            else minPriceFilter = nameFilter;
            if (searchModel.MaxPrice != null)
                maxPriceFilter = minPriceFilter.Where(x => x.minPrice <= searchModel.MaxPrice && x.minPrice != 0);
            else maxPriceFilter = minPriceFilter;
            if (searchModel.Rate != null)
                rateFilter = maxPriceFilter.Where(x => x.rate >= searchModel.Rate);
            else rateFilter = maxPriceFilter;
            return rateFilter;
        }
        
    }
}