

namespace Ispan_AspCoreWeb_SecProctice.Models
{
    public class CProductWrap
    {
        private Product _product;
   
        public Product product {
            get { return _product; }
            set { _product = value; }
        }
        public CProductWrap()
        {
            _product = new Product();
            this.photo = null;
        }

        public int FId {
            get { return _product.FId; }
            set { _product.FId = value; }
        }
        public string? FName {
            get { return _product.FName; }
            set { _product.FName = value; }
        }
        public int? FQty
        {
            get { return _product.FQty; }
            set { _product.FQty = value; }
        }
        public decimal? FCost
        {
            get { return _product.FCost; }
            set { _product.FCost = value; }
        }
        public decimal? FPrice
        {
            get { return _product.FPrice; }
            set { _product.FPrice = value; }
        }
        public string? FPhotoPath
        {
            get { return _product.FPhotoPath; }
            set { _product.FPhotoPath = value; }
        }

        public IFormFile? photo { get; set;
        
        
        }




    }
}
