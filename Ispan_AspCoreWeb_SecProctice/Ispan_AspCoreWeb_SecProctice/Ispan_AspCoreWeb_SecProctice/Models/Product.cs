using System;
using System.Collections.Generic;

namespace Ispan_AspCoreWeb_SecProctice.Models
{
    public partial class Product
    {
        public int FId { get; set; }
        public string? FName { get; set; }
        public int? FQty { get; set; }
        public decimal? FCost { get; set; }
        public decimal? FPrice { get; set; }
        public string? FPhotoPath { get; set; }
    }
}
