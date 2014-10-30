using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace ConsoleApplication7.ShopModels
{
    class TablePC
    {
        [Key]
        public int tablePCId { get; set; }
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
        public string sensors { get; set; }
        public string display { get; set; }
        public string navigation { get; set; }
        public string interfaces { get; set; }
        public string battery { get; set; }
        public string processor { get; set; }
        public string data { get; set; }
        public string cameraAndAudio { get; set; }
        public string functionality { get; set; }

        public TablePC()
        { 
        
        }

        public TablePC(string name, double rate, double minPrice
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
            construction = Util.TakeOption(description, "Конструкция");
            diemensions = Util.TakeOption(description, "Габариты и вес");
            sensors = Util.TakeOption(description, "Датчики");
            display = Util.TakeOption(description, "Экран");
            navigation = Util.TakeOption(description, "Навигация");
            interfaces = Util.TakeOption(description, "Интерфейсы");
            battery = Util.TakeOption(description, "Аккумулятор");
            processor = Util.TakeOption(description, "Процессор и чипсет");
            data = Util.TakeOption(description, "Хранение данных");
            cameraAndAudio = Util.TakeOption(description, "Камера и звук");
            functionality = Util.TakeOption(description, "Функциональность");
        }
    }
}
