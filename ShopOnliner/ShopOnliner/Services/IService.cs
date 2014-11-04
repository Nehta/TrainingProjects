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
        IQueryable<Item> GetPageItems(string type, int page);
        Item FindItem(int id);
        int LastPage(string type);
    }
}
