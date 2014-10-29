using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


using ConsoleApplication7.ShopModels;

namespace ConsoleApplication7
{
    class Program
    {
        public static void AddMobile()
        {
            ShopContext mc = new ShopContext("ShopDB");
            for (int i = 1; i <= 2020; i++)
            {
                Parser parser = new Parser("mobile/"+((i-1)/500+1)+"/"+i+".html");
                Mobile mobile = new Mobile(parser.ExtractName(), parser.ExtractRating(), parser.ExtractMinPrice(),
                                            parser.ExtractMaxPrice(), "mobile/pic" + ((i - 1) / 500 + 1) + "/" + i + ".jpg", parser.ExtractDescription());
                mc.mobiles.Add(mobile);
            }
            mc.SaveChanges();
        }

        public static void AddPhoto()
        {
            ShopContext mc = new ShopContext("ShopDB");
            for (int i = 1; i <= 1395; i++)
            {
                Parser parser = new Parser("photo/" + ((i - 1) / 500 + 1) + "/" + i + ".html");
                Photo photo = new Photo(parser.ExtractName(), parser.ExtractRating(), parser.ExtractMinPrice(),
                                            parser.ExtractMaxPrice(), "photo/pic" + ((i - 1) / 500 + 1) + "/" + i + ".jpg", parser.ExtractDescription());
                mc.photos.Add(photo);
            }
            mc.SaveChanges();
        }

        public static void AddDisplay()
        {
            ShopContext mc = new ShopContext("ShopDB");
            for (int i = 1; i <= 2257; i++)
            {
                Parser parser = new Parser("display/" + ((i - 1) / 500 + 1) + "/" + i + ".html");
                Display display = new Display(parser.ExtractName(), parser.ExtractRating(), parser.ExtractMinPrice(),
                                            parser.ExtractMaxPrice(), "display/pic" + ((i - 1) / 500 + 1) + "/" + i + ".jpg", parser.ExtractDescription());
                mc.displays.Add(display);
            }
            mc.SaveChanges();
        }

        public static void AddTablePC()
        {
            ShopContext mc = new ShopContext("ShopDB");
            for (int i = 1; i <= 2197; i++)
            {
                Parser parser = new Parser("tabletpc/" + ((i - 1) / 500 + 1) + "/" + i + ".html");
                TablePC tablepc = new TablePC(parser.ExtractName(), parser.ExtractRating(), parser.ExtractMinPrice(),
                                            parser.ExtractMaxPrice(), "tabletpc/pic" + ((i - 1) / 500 + 1) + "/" + i + ".jpg", parser.ExtractDescription());
                mc.tablePCs.Add(tablepc);
            }
            mc.SaveChanges();
        }

        public static void AddProjectors()
        {
            ShopContext mc = new ShopContext("ShopDB");
            for (int i = 1; i <= 1017; i++)
            {
                Parser parser = new Parser("projectors/" + ((i - 1) / 500 + 1) + "/" + i + ".html");
                Projector projector = new Projector(parser.ExtractName(), parser.ExtractRating(), parser.ExtractMinPrice(),
                                            parser.ExtractMaxPrice(), "projectors/pic" + ((i - 1) / 500 + 1) + "/" + i + ".jpg", parser.ExtractDescription());
                mc.projectors.Add(projector);
            }
            mc.SaveChanges();
        }

        static void Main(string[] args)
        {
          
        }

         
    }
}
