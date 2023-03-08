using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ispan_AspNetWeb_firstPractice.Models
{
    public class CCustomerEntity
    {
        public int fId { get; set; }
        public string fName { get; set; }

        public string fPhone { get; set; }

        public string fEmail { get; set; }

        public string fAddress { get; set; }

        public string fPassword { get; set; }

        public override string ToString() 
        {
            return $"fId:{this.fId},fName:{this.fName},fPhone:{this.fPhone},fEmail:{this.fEmail},fAddress:{this.fAddress},fPassword:{this.fPassword}";
        }
    }
}