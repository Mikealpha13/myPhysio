using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPhysio.Domain.Configuration
{
    public class ProductDataSource
    {
       public List<Products> Products { get; set; }
    }
    public class Products
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string productImage { get; set; }
        public string logoPath { get; set; }
        public string discountedPrice { get; set; }
        public string currency { get; set; }
        public string orignalPrice { get; set; }
        public string cataegory { get; set; }
        public string active { get; set; }

    }
}
