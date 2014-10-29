using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApplication7.ShopModels
{
    class Photo
    {
        [Key]
        public int pohtoId { get; set; }
        public string name { get; set; }
        public double rate { get; set; }
        public double minPrice { get; set; }
        public double maxPrice { get; set; }

        public byte[] imageBinary { get; set; }
        public string imageType { get; set; }

        public string diffrent { get; set; }
        public string mainDescription { get; set; }
        public string construction { get; set; }
        public string diemensions { get; set; }
        public string matrix { get; set; }
        public string display { get; set; }
        public string objectiveGlass { get; set; }
        public string pictures { get; set; }
        public string flash { get; set; }
        public string shutter { get; set; }
        public string shootingMode { get; set; }
        public string audio { get; set; }
        public string memory { get; set; }
        public string navigation { get; set; }
        public string interfaces { get; set; }
        public string battery { get; set; }

        public Photo()
        { 
        
        }

         public Photo(string name, double rate, double minPrice
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

            diffrent = Util.TakeOption(description, "Общая информация");
            mainDescription = Util.TakeOption(description, "Основные");
            construction = Util.TakeOption(description, "Конструкция");
            diemensions = Util.TakeOption(description, "Габариты и вес");
            display = Util.TakeOption(description, "Экран");
            matrix = Util.TakeOption(description, "Матрица");
            objectiveGlass = Util.TakeOption(description, "Объектив");
            pictures = Util.TakeOption(description, "Работа с изображением");
            flash = Util.TakeOption(description, "Вспышка");
            shutter = Util.TakeOption(description, "Экспозиция и затвор");
            shootingMode = Util.TakeOption(description, "Режимы съемки");
            audio = Util.TakeOption(description, "Работа со звуком");
            memory = Util.TakeOption(description, "Память");
            navigation = Util.TakeOption(description, "Навигация");
            interfaces = Util.TakeOption(description, "Интерфейсы");
            battery = Util.TakeOption(description, "Аккумулятор");
        }
    }
}
