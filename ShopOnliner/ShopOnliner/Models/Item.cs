//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopOnliner.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Item
    {
        public Item()
        {
            this.Categories = new HashSet<Category>();
        }
    
        public int mobileId { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public double rate { get; set; }
        public int minPrice { get; set; }
        public int maxPrice { get; set; }
        public byte[] imageBinary { get; set; }
        public string imageType { get; set; }
    
        public virtual ICollection<Category> Categories { get; set; }
    }
}