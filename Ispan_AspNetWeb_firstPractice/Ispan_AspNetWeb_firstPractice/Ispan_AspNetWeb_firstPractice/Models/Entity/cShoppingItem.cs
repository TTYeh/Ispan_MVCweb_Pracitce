using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ispan_AspNetWeb_firstPractice.Models.Entity
{
    public class cShoppingItem
    {
        public int fProductId { get; set; }
        public int fCount { get; set; }
        public decimal fPrice { get; set; }
        public decimal 小計 { get { return this.fCount * this.fPrice; } }
        public Products product { get; set; }

    }

}