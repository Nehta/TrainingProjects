using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace ConsoleApplication7
{
    public class Parser
    {
        private HtmlDocument page;
        private  Dictionary<string, double>ratePictures = new Dictionary<string, double>()
            {
                {"http://catalog.onliner.by/pic/starsbig_1.gif",1}, {"http://catalog.onliner.by/pic/starsbig_h.gif",0.5},
                {"http://catalog.onliner.by/pic/starsbig_2.gif",2}, {"http://catalog.onliner.by/pic/starsbig_1h.gif",1.5},
                {"http://catalog.onliner.by/pic/starsbig_3.gif",3}, {"http://catalog.onliner.by/pic/starsbig_2h.gif",2.5},
                {"http://catalog.onliner.by/pic/starsbig_4.gif",4}, {"http://catalog.onliner.by/pic/starsbig_3h.gif",3.5},
                {"http://catalog.onliner.by/pic/starsbig_5.gif",5}, {"http://catalog.onliner.by/pic/starsbig_4h.gif",4.5}
            };
        
        public Parser(string path)
        {
            page = new HtmlDocument();
            page.Load(path, Encoding.UTF8);
            string dom = page.DocumentNode.WriteTo();
            page = new HtmlDocument();
            page.LoadHtml(dom);
        }

        private IEnumerable<HtmlNode> ExtractClasses(string classname, string tagname, HtmlNode target)
        {
            return target.Descendants(tagname)
            .Where(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains(classname));
        }

        private string GetImagesAlts(HtmlNode textNode)
        {
            string result="";
            foreach (var i in textNode.Descendants("img"))
            {
                string alt = i.GetAttributeValue("alt", "");
                if (alt != "") result += alt + " ";
            }
            return result;
        }
        
        private double ExtractNumberOfPictureUrl(string srcValue)
        {
            if (srcValue == "no") return -1;
            else if (ratePictures.ContainsKey(srcValue))
                return ratePictures[srcValue];
            else return -1;
        }

        public string ExtractName()
        {
            var classProductH1 = ExtractClasses("product_h1", "td", page.DocumentNode).First(); 
            return classProductH1.Descendants("h1")
                  .First().FirstChild.WriteTo();
        }

       

        public double ExtractRating()
        {
            var classRate = ExtractClasses("pprate", "div",page.DocumentNode).First();
            var ratePicture = classRate.Descendants("img").FirstOrDefault();
            if (ratePicture != null)
                return ExtractNumberOfPictureUrl(ratePicture.GetAttributeValue("src", "not"));
            else return -1;
        }

        public string[] ExtractPrice()
        {
            var price = ExtractClasses("pphead", "table", page.DocumentNode)
                .First().Descendants("table").FirstOrDefault();
            if (price == null) return new string[] { "0", "0" };
            
            while (price.FirstChild.NextSibling != null)
                price = price.FirstChild.NextSibling;
            if (price.FirstChild != null) price = price.FirstChild;
            return price.WriteTo().Trim(new Char[]{' ','\n','\r','\t'}).Split('-');
        }

        public int PriceToInt( string price)
        {
            int i = 0;
            string result = "";
            while (Convert.ToInt32(price[i]) < Convert.ToInt32('0') || Convert.ToInt32(price[i]) > Convert.ToInt32('9')) i++;
            while (i < price.Length && Convert.ToInt32(price[i]) >= Convert.ToInt32('0') && Convert.ToInt32(price[i]) <= Convert.ToInt32('9'))
            {
                result += price[i];
                i++;
            }
            return Convert.ToInt32(result);
        }

        public int ExtractMinPrice()
        {
            return PriceToInt(ExtractPrice().First());
        }

        public int ExtractMaxPrice()
        {
            return PriceToInt(ExtractPrice().Last());
        }

        public Dictionary<string,Dictionary<string, string>> ExtractDescription()  
        {                            
            HtmlNode node = page.DocumentNode;
            var sectionNodes= ExtractClasses("pdsection","tr", page.DocumentNode);
            
            int lastIndex = 0;
            int index = 0;
            string section="";
            var result = new Dictionary<string,Dictionary<string,string>>();
            foreach (var sectionNode in sectionNodes)
            {
                lastIndex = index;
                index = sectionNode.StreamPosition;
                if (lastIndex!=0)
                    result.Add(section,ExtractOptions(lastIndex, index));
                section=sectionNode.FirstChild.FirstChild.WriteTo();
            }
            result.Add(section,ExtractOptions(lastIndex, index));
            return result;
        }

        public Dictionary<string, string> ExtractOptions(int begSection, int endSection)
        {
            HtmlDocument sectionContent = new HtmlDocument();
            sectionContent.LoadHtml(page.DocumentNode.WriteTo().Substring(begSection, endSection-begSection));
            var result = new Dictionary<string, string>();
            var options = ExtractClasses("pline2", "tr", sectionContent.DocumentNode);
            foreach (var option in options)
            {
                var key = ExtractClasses("par-link", "a", option).First().FirstChild.WriteTo().Trim(new Char[]{' ','\n','\r','\t'});;
                
                var valueNode = option.LastChild.PreviousSibling;
                string value = "";
                value += GetImagesAlts(valueNode);
                value += valueNode.InnerText.Replace("&nbsp;", " ").Trim(new Char[] { ' ', '\n', '\r', '\t' }); 
                if (!result.ContainsKey(key)) 
                    result.Add(key, value);
            }
            return result;
        }
     
        
    }
}
