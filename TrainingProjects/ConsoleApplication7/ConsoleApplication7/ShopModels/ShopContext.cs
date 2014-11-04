using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ConsoleApplication7.ShopModels
{
    class ShopContext : DbContext
    {
        public ShopContext(string connString)
            : base(connString)
        {

        }
        public DbSet<Mobile> mobiles { get; set; }
        public DbSet<Photo> photos { get; set; }
        public DbSet<Projector> projectors { get; set; }
        public DbSet<TablePC> tablePCs { get; set; }
        public DbSet<Display> displays { get; set; }
    }
}

