namespace BookStore.Models
{
    public class VmCustomerInvoiceInfo
    {
        public VmCustomerInvoiceInfo()
        {
            ShoppingCart = new ShoppingCart();
            CustomerDetails = new TbCustomerDeliverInfo();
        }
        public ShoppingCart ShoppingCart { get; set; }
        public TbCustomerDeliverInfo CustomerDetails { get; set; }
    }
}
