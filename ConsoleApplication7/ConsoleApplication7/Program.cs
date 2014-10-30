using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.Entity;
using System.Collections;



using ConsoleApplication7.ShopModels;
using ConsoleApplication7.ItemModels;

namespace ConsoleApplication7
{
    class Program
    {

        public static void AddItem(string filePath, string picturePath, string type, ItemContext ic=null)
        {
            var parser = new Parser(filePath);
            
            var item = new Item(type, parser.ExtractName(), parser.ExtractRating(), 
                parser.ExtractMinPrice(), parser.ExtractMaxPrice(), picturePath);

            foreach (var description in parser.ExtractDescription())
            {
                var category = new Category(description.Key);
                foreach (var option in description.Value)
                {
                    var subcategory = new Category(option.Key, option.Value);
                    category.subCategories.Add(subcategory);
                }
                item.categories.Add(category);
            }
            if (ic == null)
            {
                ic = new ItemContext("ItemDB");
                ic.items.Add(item);
                ic.SaveChanges();
            }
            else ic.items.Add(item);
        }

        static void Main(string[] args)
        {
            
            using (var context = new ItemContext("ItemDB"))
            {
                var dirs = new List<string>(Directory.EnumerateDirectories(Directory.GetCurrentDirectory()));
                var i = 0;
                foreach (var dir in dirs)
                {
                    var subDirs = new List<string>(Directory.EnumerateDirectories(dir))
                        .Where(x => !x.Contains("pic"));

                    foreach (var subdir in subDirs)
                    {
                        var files = Directory.EnumerateFiles(subdir);
                        foreach (var file in files)
                        {
                             AddItem(dir.Split('\\').Last() +'\\'+subdir.Split('\\').Last() + '\\' + file.Split('\\').Last(),
                                     dir.Split('\\').Last() + "\\pic" + subdir.Split('\\').Last() + '\\' + file.Split('\\').Last().Replace(".html", ".jpg"),
                                     dir.Split('\\').Last(),
                                     context);
                             Console.WriteLine(++i);
                        }

                    }
                }
                context.SaveChanges();
            }
            
        }

         
    }
}
