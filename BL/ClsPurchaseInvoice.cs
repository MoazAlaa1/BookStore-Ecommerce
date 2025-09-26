using BookStore.Models;
using Domains;
using Microsoft.EntityFrameworkCore;


namespace BookStore.Bl
{
    public interface IPurchaseInvoice
    {

        public TbPurchaseInvoice GetById(int id);

        public bool Save(TbPurchaseInvoice book,List<TbPurchaseInvoiceBook> lstbooks, bool isNew);

        public bool Delete(int id);
        public List<TbPurchaseInvoice> GetAll();
        public List<VwPurchaseInvoice> GetAllVw();
        //public VwPurchaseInvoice GetVwById(int id);
    }
    public class ClsPurchaseInvoice : IPurchaseInvoice
    {
        BookStoreContext context;
        IPurchaseInvoiceBooks ClsPurchaseInvoiceBooks;
        public ClsPurchaseInvoice(BookStoreContext ctx,IPurchaseInvoiceBooks invoicebooks)
        {
            context = ctx;
            ClsPurchaseInvoiceBooks = invoicebooks;
        }
        public List<TbPurchaseInvoice> GetAll()
        {
            try
            {
                return context.TbPurchaseInvoices.Where(a => a.CurrentState == 1).ToList();
            }
            catch
            {
                return new List<TbPurchaseInvoice>();
            }

        }
        public List<VwPurchaseInvoice> GetAllVw()
        {
            try
            {
                return context.VwPurchaseInvoices.Where(a=>a.CurrentState == 1).ToList();
            }
            catch
            {
                return new List<VwPurchaseInvoice>();
            }
        }
        public TbPurchaseInvoice GetById(int id)
        {
            try
            {
                var book = context.TbPurchaseInvoices.Where(a => a.PurchaseInvoiceId == id && a.CurrentState==1).FirstOrDefault();
                if (book != null)
                    return book;
                else
                    return new TbPurchaseInvoice();                
            }
            catch 
            {
                return new TbPurchaseInvoice();
            }
        }

        public bool Save(TbPurchaseInvoice book,List<TbPurchaseInvoiceBook> lstbooks, bool isNew)
        {
            using var transaction = context.Database.BeginTransaction();
            try
            {
                book.CurrentState = 1;
                if (isNew)
                {
                    book.InvoiceDate= DateTime.Now;
                    context.TbPurchaseInvoices.Add(book);
                }
                else
                {
                    book.UpdatedBy = "Admin";
                    book.UpdatedDate = DateTime.Now;
                    context.Entry(book).State = EntityState.Modified;
                }
                context.SaveChanges();
                ClsPurchaseInvoiceBooks.Save(lstbooks, book.PurchaseInvoiceId, isNew);
                transaction.Commit();
                return true;
            }
            catch 
            {
                transaction.Rollback();
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var book = context.TbPurchaseInvoices.Where(a => a.PurchaseInvoiceId == id).FirstOrDefault();
                if (book != null)
                {
                    book.CurrentState = 0;
                    context.Entry(book).State= EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
        
        //public VwPurchaseInvoice GetVwById(int id)
        //{
        //    try
        //    {
        //        return context.VwPurchaseInvoices.Where(a => a.InvoiceId == id).FirstOrDefault();
        //    }
        //    catch
        //    {
        //        return new VwPurchaseInvoice();
        //    }

        //}
    }
}
