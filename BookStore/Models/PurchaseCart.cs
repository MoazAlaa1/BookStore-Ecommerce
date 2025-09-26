namespace BookStore.Models
{
    public class PurchaseCart
    {
        public PurchaseCart()
        {
            lstBooks = new List<PurchaseCartBook>();
        }
        public int invoiceId { get; set; }
        public List<PurchaseCartBook> lstBooks { get; set; }
        public int SupplierId { get; set; }
        public decimal Total { get; set; }
        public bool isNew { get; set; } = true;
    }
}
