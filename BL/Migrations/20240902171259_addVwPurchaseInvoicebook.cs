using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BL.Migrations
{
    /// <inheritdoc />
    public partial class addVwPurchaseInvoicebook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Create view VwPurchaseInvoiceBooks
as
SELECT        dbo.TbPurchaseInvoiceBook.InvoiceBookId, dbo.TbPurchaseInvoiceBook.BookId, dbo.TbPurchaseInvoiceBook.InvoiceId, dbo.TbPurchaseInvoiceBook.Qty, dbo.TbPurchaseInvoiceBook.Price, 
                         dbo.TbPurchaseInvoice.InvoiceDate, dbo.TbPurchaseInvoice.TotalPrice, dbo.TbPurchaseInvoice.CurrentState, dbo.TbPurchaseInvoice.SupplierId, dbo.TbPurchaseInvoice.Note, dbo.TbSuppliers.SupplierName, 
                         dbo.TbBooks.SalesPrice, dbo.TbBooks.PurchasePrice, dbo.TbBooks.Title, dbo.TbBooks.ImageName
FROM            dbo.TbPurchaseInvoiceBook INNER JOIN
                         dbo.TbPurchaseInvoice ON dbo.TbPurchaseInvoiceBook.InvoiceId = dbo.TbPurchaseInvoice.PurchaseInvoiceId INNER JOIN
                         dbo.TbBooks ON dbo.TbPurchaseInvoiceBook.BookId = dbo.TbBooks.BookId INNER JOIN
                         dbo.TbSuppliers ON dbo.TbPurchaseInvoice.SupplierId = dbo.TbSuppliers.SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
