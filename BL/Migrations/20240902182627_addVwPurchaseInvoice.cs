using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BL.Migrations
{
    /// <inheritdoc />
    public partial class addVwPurchaseInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"create view VwPurchaseInvoices
as
SELECT        dbo.TbPurchaseInvoice.PurchaseInvoiceId, dbo.TbPurchaseInvoice.InvoiceDate, dbo.TbPurchaseInvoice.TotalPrice, dbo.TbPurchaseInvoice.CurrentState, dbo.TbPurchaseInvoice.SupplierId, dbo.TbPurchaseInvoice.Note, 
                         dbo.TbSuppliers.SupplierName
FROM            dbo.TbPurchaseInvoice INNER JOIN
                         dbo.TbSuppliers ON dbo.TbPurchaseInvoice.SupplierId = dbo.TbSuppliers.SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
