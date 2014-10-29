using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication7.ShopModels
{
    class Util
    {
        public  static string TakeOption(Dictionary<string, IEnumerable<string>> description, string name)
        {
            string result = "";
            if (description.ContainsKey(name))
                foreach (var option in description[name])
                    result = result + option + "\n";
            return result;
        }
    }
}
