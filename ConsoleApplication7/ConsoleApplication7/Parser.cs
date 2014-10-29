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

        private bool isYesOption(HtmlNode option)
        {
            foreach (var i in option.Descendants("img"))
                if (i.GetAttributeValue("alt", "") == "Да")
                    return true;
            return false;
        }

        private bool isNoOption(HtmlNode option)
        {
            foreach (var i in option.Descendants("img"))
                if (i.GetAttributeValue("alt", "") == "Нет")
                    return true;
            return false;
        }

        private string DeleteLeadingWhiteSpaces(string s)
        { 
            while (s[0]==' ' || s[0]=='\r' || s[0]=='\n' || s[0]=='\t')
                s=s.Substring(1, s.Length-1);
            return s;
        }

        private string DeleteTags(HtmlNode target)
        {
            string result = "";
            foreach (var i in target.ChildNodes)
                if (i.Name != "img") 
                {
                    var j = i;
                    while (j.FirstChild != null)
                        j = j.FirstChild;
                    result+=j.WriteTo();
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

        public string ExtractPrice()
        {
            var price = ExtractClasses("pphead", "table", page.DocumentNode)
                .First().Descendants("table").FirstOrDefault();
            if (price == null) return "нет в продаже";
            
            while (price.FirstChild.NextSibling != null)
                price = price.FirstChild.NextSibling;
            if (price.FirstChild != null) price = price.FirstChild;
            
            return DeleteLeadingWhiteSpaces(price.WriteTo());
        }

        public int ExtractMinPrice()
        {
            string price = ExtractPrice();
            if (price == "нет в продаже")
                return -1;
            int i = 0;
            int result = 0;
            while (Convert.ToInt32(price[i]) < Convert.ToInt32('0') || Convert.ToInt32(price[i]) > Convert.ToInt32('9')) i++;
            while (i<price.Length && Convert.ToInt32(price[i]) >= Convert.ToInt32('0') && Convert.ToInt32(price[i]) <= Convert.ToInt32('9'))
            {
                result = result * 10 + Convert.ToInt32(price[i]-48);
                i++;
            }
            return result;
        }

        public int ExtractMaxPrice()
        {
            string price = ExtractPrice();
            if (price == "нет в продаже")
                return -1;
            int i = price.Length-1;
            int result = 0;
            int n = 1;
            while (Convert.ToInt32(price[i]) < Convert.ToInt32('0') || Convert.ToInt32(price[i]) > Convert.ToInt32('9')) i--;
            while (i>=0 && Convert.ToInt32(price[i]) >= Convert.ToInt32('0') && Convert.ToInt32(price[i]) <= Convert.ToInt32('9'))
            {
                result = result + (Convert.ToInt32(price[i] - 48)*n);
                i--;
                n = n * 10;
            }
            return result;
        }

        public Dictionary<string,IEnumerable<string>> ExtractDescription()  
        {                            
            HtmlNode node = page.DocumentNode;
            var sectionNodes= ExtractClasses("pdsection","tr", page.DocumentNode);
            
            int lastIndex = 0;
            int index = 0;
            string section="";
            var result = new Dictionary<string,IEnumerable<string>>();
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

        public IEnumerable<string> ExtractOptions(int begSection, int endSection)
        {
            HtmlDocument sectionContent = new HtmlDocument();
            sectionContent.LoadHtml(page.DocumentNode.WriteTo().Substring(begSection, endSection-begSection));
            var options = ExtractClasses("pline2", "tr", sectionContent.DocumentNode);
            foreach (var option in options)
            {
                string value = "";
                if (isNoOption(option)) value += "Нет";
                else if (isYesOption(option)) value += "Да ";
                var key = ExtractClasses("par-link", "a", option).First().FirstChild.WriteTo();
                key = DeleteLeadingWhiteSpaces(key);
                var valueNode = option.LastChild.PreviousSibling;
                value+=DeleteLeadingWhiteSpaces(DeleteTags(valueNode)).Replace("&nbsp;"," ");
                yield return key + ":" + value;
            }
        }
     
        
    }
}
