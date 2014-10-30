using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApplication7.ItemModels
{
    class Category
    {
        [Key]
        public int CategoryID { get; set; }
        [MaxLength(200)]
        public string name {get; set;}
        [MaxLength(5000)]
        public string value { get; set;}
        public virtual ICollection<Category> subCategories { get; set; }

        public Category()
        { 
        
        }

        public Category( string name, string value=null)
        {
            this.name = name;
            this.value = value;
            subCategories = new List<Category>();
        }

        
    }
}
