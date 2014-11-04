using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ConsoleApplication7.ItemModels
{
    class ItemContext: DbContext
    {
        public ItemContext(string connString)
            : base(connString)
        {

        }
        public DbSet<Item> items {get; set;}
        public DbSet<Category> categories { get; set; }
    }
}
