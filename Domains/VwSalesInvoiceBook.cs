using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class VwSalesInvoiceBook
    {
        public int InvoiceBookId { get; set; }

        public int BookId { get; set; }

        public int InvoiceId { get; set; }

        public int Qty { get; set; }

        public decimal Price { get; set; }
        public DateTime InvoiceDate { get; set; }

        public decimal TotalPrice { get; set; }

        public int CurrentState { get; set; }
        public string CustomerId { get; set; } = "1";

        public string? Note { get; set; }

        public int CustomerDeliverId { get; set; }

        public int DeliveryManId { get; set; }

        public string CreatedBy { get; set; } = null!;

        public string DeliveryState { get; set; } = null!;
        public string CutomerName { get; set; } = "";
        public string Adress { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public int GovernorateId { get; set; }
        public string GovernorateName { get; set; } = null!;
        public decimal DeliveryPrice { get; set; }
        public string DeliveryManName { get; set; } = null!;
        public string Title { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public int DiscountId { get; set; }
        public int DiscountPercent { get; set; }
        public int BookQty { get; set; }
    }
}


