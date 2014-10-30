using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace ConsoleApplication7.ItemModels
{
    class Item
    {
        [Key]
        public int mobileId { get; set; }
        [MaxLength(200)]
        public string type { get; set; }
        [MaxLength(200)]
        public string name { get; set; }
        public double rate { get; set; }
        public int minPrice { get; set; }
        public int  maxPrice { get; set; }

        public byte[] imageBinary { get; set; }
        [MaxLength(10)]
        public string imageType { get; set; }
        public virtual  ICollection<Category> categories {get; set;}


        public Item()
        { 
        
        }

        public Item(string type, string name, double rate, int minPrice, int maxPrice, string picturePath)
        {
            this.type = type;
            this.name = name;
            this.rate = rate;
            this.minPrice = minPrice;
            this.maxPrice = maxPrice;

            imageType = picturePath.Substring(picturePath.LastIndexOf("."), picturePath.Length - picturePath.LastIndexOf("."));
            using (FileStream fs = File.Open(picturePath, FileMode.Open))
            using (MemoryStream ms = new MemoryStream())
            {
                fs.CopyTo(ms);
                imageBinary = ms.GetBuffer();
            }
            categories = new List<Category>();
        }
    }
}
