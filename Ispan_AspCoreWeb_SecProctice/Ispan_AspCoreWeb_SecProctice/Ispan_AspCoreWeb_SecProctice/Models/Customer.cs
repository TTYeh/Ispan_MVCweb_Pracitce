using System;
using System.Collections.Generic;

namespace Ispan_AspCoreWeb_SecProctice.Models
{
    public partial class Customer
    {
        public int FId { get; set; }
        public string? FName { get; set; }
        public string? FPhone { get; set; }
        public string? FEmail { get; set; }
        public string? FAddress { get; set; }
        public string? FPassword { get; set; }
    }
}
