using BookStore.BL;
using BookStore.Models;
using Domains;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Bl
{
    public interface ISalesInvoiceBooks
    {
        public List<VwSalesInvoiceBook> GetAll();
        public List<TbSalesInvoiceBook> GetSalesInvoiceId(int id);
        public List<VwSalesInvoiceBook> GetlstSalesInvoiceBook(int invoiceId);

        public bool Save(IList<TbSalesInvoiceBook> Books, int salesInvoiceId, bool isNew);
    }

    public class ClsSalesInvoiceBook : ISalesInvoiceBooks
    {
        BookStoreContext context;
        IBook oClsBook;
        public ClsSalesInvoiceBook(BookStoreContext ctx, IBook book)
        {
            context = ctx;
            oClsBook = book;
        }
        public List<VwSalesInvoiceBook> GetAll()
        {
            try 
	        {	        
		        return context.VwSalesInvoiceBooks.Where(a=>a.CurrentState == 1).ToList();
	        }
	        catch 
	        {
                return new List<VwSalesInvoiceBook>();
	        }
        }
        public List<TbSalesInvoiceBook> GetSalesInvoiceId(int id)
        {
            try
            {
                var Books = context.TbSalesInvoiceBooks.Where(a => a.InvoiceId == id).ToList();
                if (Books == null)
                    return new List<TbSalesInvoiceBook>();
                else
                    return Books;
            }
            catch 
            {
                return new List<TbSalesInvoiceBook>();
            }
        }
        public List<VwSalesInvoiceBook> GetlstSalesInvoiceBook(int invoiceId)
        {
            try
            {
                return context.VwSalesInvoiceBooks.Where(a=>a.InvoiceId== invoiceId).ToList();
            }
            catch
            {
                return new List<VwSalesInvoiceBook>();
            }
        }
        public bool Save(IList<TbSalesInvoiceBook> Books, int salesInvoiceId, bool isNew)
        {
            List<TbSalesInvoiceBook> dbInvoiceBooks;
            if (isNew == true)
            {
                dbInvoiceBooks = GetSalesInvoiceId(Books[0].InvoiceId);
            }
            else
            {
                dbInvoiceBooks = GetSalesInvoiceId(salesInvoiceId);
            }

            foreach (var interfaceBooks in Books)
            {
                var dbObject = dbInvoiceBooks.Where(a => (a.InvoiceBookId == interfaceBooks.InvoiceBookId) && (a.BookId==interfaceBooks.BookId)).FirstOrDefault();
                if (dbObject != null)
                {
                    dbObject.InvoiceId = salesInvoiceId;
                    context.Entry(dbObject).State = EntityState.Modified;
                    if (interfaceBooks.Qty > dbObject.Qty)
                    {
                        oClsBook.UpdateBookQty(interfaceBooks.BookId, -(interfaceBooks.Qty - dbObject.Qty));
                    }
                    else if (interfaceBooks.Qty < dbObject.Qty)
                    {
                        oClsBook.UpdateBookQty(interfaceBooks.BookId, (dbObject.Qty - interfaceBooks.Qty));
                    }
                }
                else
                {
                    interfaceBooks.InvoiceBookId = 0;
                    interfaceBooks.InvoiceId = salesInvoiceId;
                    context.TbSalesInvoiceBooks.Add(interfaceBooks);
                    oClsBook.UpdateBookQty(interfaceBooks.BookId, -interfaceBooks.Qty);
                }
            }

            foreach (var Book in dbInvoiceBooks)
            {
                var interfaceObject = Books.Where(a => (a.InvoiceBookId == Book.InvoiceBookId) && (a.BookId==Book.InvoiceId)).FirstOrDefault();
                if (interfaceObject == null)
                {
                    context.TbSalesInvoiceBooks.Remove(Book);
                    oClsBook.UpdateBookQty(Book.BookId, Book.Qty);
                }
            }

            context.SaveChanges();
            return true;
        }
    }
}
