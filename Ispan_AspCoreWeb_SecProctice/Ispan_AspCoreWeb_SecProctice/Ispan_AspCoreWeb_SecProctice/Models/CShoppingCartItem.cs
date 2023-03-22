namespace Ispan_AspCoreWeb_SecProctice.Models
{
    public class CShoppingCartItem
    {
        public int fProductId { get; set; }
        public int fCount { get; set; }
        public decimal fPrice { get; set; }
        public decimal 小計 { get { return this.fCount * this.fPrice; } }
        public Product product { get; set; }
    }
}
