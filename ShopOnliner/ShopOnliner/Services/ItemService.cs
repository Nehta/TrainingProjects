using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShopOnliner.Models;

namespace ShopOnliner.Services
{
    public class ItemService:IService
    {
        public  const int ITEMS_PER_PAGE=70;

        private ItemsEntities context = new ItemsEntities();

        public IEnumerable<Item> GetPageItems(string type, int page)
        {
            return context.Items.Where(x => x.type == type).OrderBy(x => x.name).Skip(ITEMS_PER_PAGE * (page - 1)).Take(ITEMS_PER_PAGE);
        }

        public IEnumerable<string> GetMenu()
        {
            return context.Items.Select(x => x.type).Distinct();
        }

        public IEnumerable<string> GetAttributes(string type)
        {
            return context.Categories.Where(x => x.Category1!=null && x.Category1.Item.type == type && (x.value.Contains("Да ")||x.value.Contains("Нет "))).Select(x => x.name).Take(5);
        }


        public IEnumerable<Item> GetPageItemsFromSearch(string type, SearchModel searchModel )
        {
            int page = searchModel.SearchPage;
            return Search(searchModel, type).
                OrderBy(x => x.name).Skip(ITEMS_PER_PAGE * (page - 1)).Take(ITEMS_PER_PAGE);
        }

        public Item FindItem(int id)
        {
            return context.Items.Find(id);
        }

        public int LastPageOfSearch( string type, SearchModel searchModel)
        {
            return (Search(searchModel, type).Count() - 1) / ITEMS_PER_PAGE + 1;
        }

        public int LastPage(string type)
        {
            return (context.Items.Where(x => x.type == type).Count() - 1) / ITEMS_PER_PAGE + 1;
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
            return rateFilter.Where(x=>!searchModel.checkbox_1 || hasAttribute(searchModel.attr_1, x))
                             .Where(x => !searchModel.checkbox_2 || hasAttribute(searchModel.attr_2, x))
                             .Where(x => !searchModel.checkbox_3 || hasAttribute(searchModel.attr_3, x))
                             .Where(x=>!searchModel.checkbox_4 || hasAttribute(searchModel.attr_4, x))
                             .Where(x=>!searchModel.checkbox_5 || hasAttribute(searchModel.attr_5, x));
        }

        private bool hasAttribute(string attr, Item item)
        {
            foreach (var category in item.Categories)
                foreach (var subcategory in category.Categories1)
                    if (subcategory.name == attr && !subcategory.value.Contains("Нет"))
                        return true;
            return false;
        }
        
    }
}