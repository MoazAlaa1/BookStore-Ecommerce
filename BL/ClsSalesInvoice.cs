using BookStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BL;


namespace BookStore.Bl
{
    public interface ISalesInvoice
    {

        public TbSalesInvoice GetById(int id);

        public bool Save(TbSalesInvoice book,int id ,List<TbSalesInvoiceBook> lstbooks, bool isNew);

        public bool Delete(int id);
        public List<TbSalesInvoice> GetAll();
        public List<VwSalesInvoice> GetAllData();
        public VwSalesInvoice GetVwById(int id);
    }
    public class ClsSalesInvoice : ISalesInvoice
    {
        BookStoreContext context;
        ISalesInvoiceBooks ClsSalesInvoiceBooks;
        public ClsSalesInvoice(BookStoreContext ctx,ISalesInvoiceBooks invoicebooks)
        {
            context = ctx;
            ClsSalesInvoiceBooks = invoicebooks;
        }

        public TbSalesInvoice GetById(int id)
        {
            try
            {
                var book = context.TbSalesInvoices.Where(a => a.InvoiceId == id).FirstOrDefault();
                if (book == null)
                    return new TbSalesInvoice();
                else
                    return book;
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public bool Save(TbSalesInvoice book,int id ,List<TbSalesInvoiceBook> lstbooks, bool isNew)
        {
            try
            {
                book.CurrentState = 1;
                if (isNew)
                {
                    book.CustomerDeliverId = id;
                    context.TbSalesInvoices.Add(book);
                }
                else
                {
                    book.UpdatedBy = "Admin";
                    book.UpdatedDate = DateTime.Now;
                    book.CustomerDeliverId = id;
                    context.Entry(book).State = EntityState.Modified;
                }
                context.SaveChanges();
                ClsSalesInvoiceBooks.Save(lstbooks, book.InvoiceId, isNew);

                return true;
            }
            catch 
            {
                throw new Exception();
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var book = context.TbSalesInvoices.Where(a => a.InvoiceId == id).FirstOrDefault();
                if (book != null)
                {
                    //context.TbSalesInvoices.Remove(book);
                    book.CurrentState= 0;
                    context.Entry(book).State = EntityState.Modified;
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
        public List<VwSalesInvoice> GetAllData()
        {
            try
            {
                return context.VwSalesInvoices.Where(a => a.CurrentState == 1).ToList();
            }
            catch
            {
                return new List<VwSalesInvoice>();
            }

        }
        public List<TbSalesInvoice> GetAll()
        {
            try
            {
                return context.TbSalesInvoices.Where(a => a.CurrentState == 1).ToList();
            }
            catch
            {
                return new List<TbSalesInvoice>();
            }

        }
        public VwSalesInvoice GetVwById(int id)
        {
            try
            {
                return context.VwSalesInvoices.Where(a => a.InvoiceId == id).FirstOrDefault();
            }
            catch
            {
                return new VwSalesInvoice();
            }

        }
    }
}
