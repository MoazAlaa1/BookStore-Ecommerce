using BookStore.BL;
using BookStore.Models;
using Domains;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Bl
{
    public interface IPurchaseInvoiceBooks
    {
        public List<TbPurchaseInvoiceBook> GetPurchaseInvoiceBookId(int id);

        public bool Save(IList<TbPurchaseInvoiceBook> Books, int salesInvoiceId, bool isNew);
        public VwPurchaseInvoiceBook GetById(int id);
        public List<VwPurchaseInvoiceBook> GetlstPurchaseInvoiceBook(int invoiceId);

    }

    public class ClsPurchaseInvoiceBook : IPurchaseInvoiceBooks
    {
        BookStoreContext context;
        IBook oClsBook;
        public ClsPurchaseInvoiceBook(BookStoreContext ctx, IBook book)
        {
            context = ctx;
            oClsBook = book;
        }

        public List<TbPurchaseInvoiceBook> GetPurchaseInvoiceBookId(int id)
        {
            try
            {
                var Books = context.TbPurchaseInvoiceBooks.Where(a => a.InvoiceId == id).ToList();
                if (Books == null)
                    return new List<TbPurchaseInvoiceBook>();
                else
                    return Books;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public bool Save(IList<TbPurchaseInvoiceBook> Books, int PurchaseInvoiceId, bool isNew)
        {
            List<TbPurchaseInvoiceBook> dbInvoiceBooks;
            if (isNew == true)
            {
                dbInvoiceBooks = GetPurchaseInvoiceBookId(Books[0].InvoiceId);
            }
            else
            {
                dbInvoiceBooks = GetPurchaseInvoiceBookId(PurchaseInvoiceId);
            }

            foreach (var interfaceBooks in Books)
            {
                var dbObject = dbInvoiceBooks.Where(a => a.InvoiceBookId == interfaceBooks.InvoiceBookId).FirstOrDefault();
                if (dbObject != null)
                {
                    interfaceBooks.InvoiceId = PurchaseInvoiceId;
                    context.Entry(dbObject).State = EntityState.Modified;
                    if (interfaceBooks.Qty > dbObject.Qty)
                    {
                        oClsBook.UpdateBookQty(interfaceBooks.BookId, (interfaceBooks.Qty - dbObject.Qty));
                    }
                    else if (interfaceBooks.Qty < dbObject.Qty)
                    {
                        oClsBook.UpdateBookQty(interfaceBooks.BookId, -(dbObject.Qty- interfaceBooks.Qty));
                    }
                }

                else
                {
                    interfaceBooks.InvoiceId = PurchaseInvoiceId;
                    context.TbPurchaseInvoiceBooks.Add(interfaceBooks);
                    oClsBook.UpdateBookQty(interfaceBooks.BookId, interfaceBooks.Qty);
                }
            }

            foreach (var Book in dbInvoiceBooks)
            {
                var interfaceObject = Books.Where(a => a.InvoiceBookId == Book.InvoiceBookId).FirstOrDefault();
                if (interfaceObject == null)
                {
                    context.TbPurchaseInvoiceBooks.Remove(Book);
                    oClsBook.UpdateBookQty(Book.BookId, -Book.Qty);
                }
            }
            context.SaveChanges();
            return true;
        }
        public VwPurchaseInvoiceBook GetById(int id)
        {
            try
            {
                return context.VwPurchaseInvoiceBooks.Where(a => a.InvoiceId == id).FirstOrDefault();
            }
            catch 
            {
                return new VwPurchaseInvoiceBook();
            }
        }
        public List<VwPurchaseInvoiceBook> GetlstPurchaseInvoiceBook(int invoiceId)
        {
            try
            {
                return context.VwPurchaseInvoiceBooks.Where(a=>a.InvoiceId==invoiceId).ToList();
            }
            catch 
            {
                return new List<VwPurchaseInvoiceBook>();
            }
        }
    }
}
