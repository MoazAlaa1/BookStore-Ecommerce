using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BL.Migrations
{
    /// <inheritdoc />
    public partial class addVwSalesInvoiceBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
create view VwSalesInvoiceBooks
as
SELECT        dbo.TbSalesInvoiceBook.InvoiceBookId, dbo.TbSalesInvoiceBook.BookId, dbo.TbSalesInvoiceBook.InvoiceId, dbo.TbSalesInvoiceBook.Qty, dbo.TbSalesInvoiceBook.Price, dbo.TbSalesInvoice.InvoiceDate,
                         dbo.TbSalesInvoice.TotalPrice, dbo.TbSalesInvoice.CurrentState, dbo.TbSalesInvoice.CustomerId, dbo.TbSalesInvoice.CustomerDeliverId, dbo.TbSalesInvoice.DeliveryManId, dbo.TbSalesInvoice.CreatedBy,
                         dbo.TbSalesInvoice.DeliveryState, dbo.TbDeliveryMan.DeliveryManName, dbo.TbCustomerDeliverInfo.CutomerName, dbo.TbCustomerDeliverInfo.Adress, dbo.TbCustomerDeliverInfo.PhoneNumber,
                         dbo.TbCustomerDeliverInfo.GovernorateId, dbo.TbGovernorates.GovernorateName, dbo.TbGovernorates.DeliveryPrice, dbo.TbBooks.Title, dbo.TbBooks.SalesPrice, dbo.TbBooks.PurchasePrice, dbo.TbBooks.DiscountId,
                         dbo.TbDiscount.DiscountPercent, dbo.TbSalesInvoice.Note
FROM            dbo.TbSalesInvoiceBook INNER JOIN
                         dbo.TbSalesInvoice ON dbo.TbSalesInvoiceBook.InvoiceId = dbo.TbSalesInvoice.InvoiceId INNER JOIN
                         dbo.TbCustomerDeliverInfo ON dbo.TbSalesInvoice.CustomerDeliverId = dbo.TbCustomerDeliverInfo.CustomerDeliverId INNER JOIN
                         dbo.TbBooks ON dbo.TbSalesInvoiceBook.BookId = dbo.TbBooks.BookId INNER JOIN
                         dbo.TbDiscount ON dbo.TbBooks.DiscountId = dbo.TbDiscount.DiscountId INNER JOIN
                         dbo.TbDeliveryMan ON dbo.TbSalesInvoice.DeliveryManId = dbo.TbDeliveryMan.DeliveryManId INNER JOIN
                         dbo.TbGovernorates ON dbo.TbCustomerDeliverInfo.GovernorateId = dbo.TbGovernorates.GovernorateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
