using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace ConsoleApplication7.ShopModels
{
    class Display
    {
        [Key]
        public int mobileId { get; set; }
        public string name { get; set; }
        public double rate { get; set; }
        public double minPrice { get; set; }
        public double maxPrice { get; set; }

        public byte[] imageBinary { get; set; }
        public string imageType { get; set; }

        public string diffrent { get; set; }
        public string mainDescription { get; set; }
        public string diemensions { get; set; }
        public string interfaces { get; set; }
        public string technical { get; set; }
        public string constructive { get; set; }
        public string under { get; set; }

        public Display()
        { 
        
        }

        public Display(string name, double rate, double minPrice
            , double maxPrice, string picturePath, Dictionary<string, IEnumerable<string>> description)
        {
            this.name = name;
            this.rate = rate;
            this.minPrice = minPrice;
            this.maxPrice = maxPrice;

            using(FileStream fs = File.Open(picturePath,FileMode.Open))
            using (MemoryStream ms = new MemoryStream())
            {
                fs.CopyTo(ms);
                imageBinary = ms.GetBuffer();
                imageType = picturePath.Substring(picturePath.LastIndexOf("."), picturePath.Length - picturePath.LastIndexOf("."));
            }

            diffrent=Util.TakeOption(description, "Общая информация");
            mainDescription = Util.TakeOption(description, "Основные");
            diemensions = Util.TakeOption(description, "Габариты и вес");
            interfaces = Util.TakeOption(description, "Интерфейсы");
            technical = Util.TakeOption(description, "Технические характеристики");
            constructive = Util.TakeOption(description, "Конструктивные характеристики");
            under = Util.TakeOption(description, "Подставка и габариты");
           
        }

    }
}
