using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class VwPurchaseInvoiceBook
    {
        public int InvoiceBookId { get; set; }
        public int BookId { get; set; }
        public int InvoiceId { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int CurrentState { get; set; }
        public int SupplierId { get; set; }
        public string? Note { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public string SupplierName { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal PurchasePrice { get; set; }

    }
}
