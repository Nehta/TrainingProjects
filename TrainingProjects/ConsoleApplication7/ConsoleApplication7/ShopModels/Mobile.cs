using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace ConsoleApplication7.ShopModels
{
    class Mobile
    {
        [Key]
        public int mobileId { get; set; }
        public string name { get; set; }
        public double rate { get; set; }
        public double minPrice { get; set; }
        public double maxPrice { get; set; }

        public byte[] imageBinary{get; set;}
        public string imageType{get; set;}

        public string diffrent { get; set; }
        public string mainDescription { get; set; }
        public string construction { get; set; }
        public string diemensions { get; set; }
        public string sensors { get; set; }
        public string notebook { get; set; }
        public string shortMesseges { get; set; }
        public string display { get; set; }
        public string objectiveGlass { get; set; }
        public string pictures { get; set; }
        public string flash { get; set; }
        public string audio { get; set; }
        public string memory { get; set; }
        public string navigation { get; set; }
        public string callFunctions { get; set; }
        public string dataTransmission { get; set; }
        public string interfaces { get; set; }
        public string battery { get; set; }

        public string AddOption(Dictionary<string, IEnumerable<string>> description, string name)
        {
            string result="";
            if (description.ContainsKey(name))
                foreach (var option in description[name])
                    result = result + option + "\n";
            return result;
        }

        public Mobile()
        { 
        
        }

        public Mobile(string name, double rate, double minPrice
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
            notebook = Util.TakeOption(description, "Телефонная книга и органайзер");
            shortMesseges = Util.TakeOption(description, "Короткие сообщения");
            display = Util.TakeOption(description, "Экран");
            objectiveGlass = Util.TakeOption(description, "Объектив");
            pictures = Util.TakeOption(description, "Работа с изображением");
            flash = Util.TakeOption(description, "Вспышка");
            audio = Util.TakeOption(description, "Работа со звуком");
            memory = Util.TakeOption(description, "Память");
            navigation = Util.TakeOption(description, "Навигация");
            callFunctions = Util.TakeOption(description, "Функции вызова и персонализация");
            dataTransmission = Util.TakeOption(description, "Передача данных");
            interfaces = Util.TakeOption(description, "Интерфейсы");
            battery = Util.TakeOption(description, "Аккумулятор");
        }
    }

}

 