namespace SenseIt.Data.Models
{
    public class ShoppingCart
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public string SessionId { get; set; }
    }
}
