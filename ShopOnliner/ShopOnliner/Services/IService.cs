using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopOnliner.Models;

namespace ShopOnliner.Services
{
    public interface IService
    {
        IEnumerable<string> GetMenu();
        IEnumerable<Item> GetPageItems(string type, int page);
        IEnumerable<Item> GetPageItemsFromSearch(string type, SearchModel searchModel);
        IEnumerable<string> GetAttributes(string type);
        Item FindItem(int id);
        int LastPage(string type);
        int LastPageOfSearch(string type, SearchModel searchModel);
 
    }
}
