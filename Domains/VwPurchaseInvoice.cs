using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class VwPurchaseInvoice
    {
        public int PurchaseInvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int CurrentState { get; set; }
        public int SupplierId { get; set; }
        public string? Note { get; set; }
        public string SupplierName { get; set; }
    }
}
