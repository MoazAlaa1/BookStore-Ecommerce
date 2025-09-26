namespace BookStore.Models
{
    public class VmDashboard
    {
        public List<VwSalesInvoice> lstSales { get; set; } = new List<VwSalesInvoice>();
        public decimal TotalSales { get; set; } = 0;
        public int TotalOrder { get; set; } = 0;
        public decimal TotalPurchases { get; set; } = 0;
        public decimal TotalEarnings { get; set; } = 0;
        public int TotalCustomer { get; set; } = 0;
    }
}
