using BookStore.Bl;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface ICustomerInvoiceInfo
    {
        public bool Save(TbCustomerDeliverInfo info, TbSalesInvoice invoice, List<TbSalesInvoiceBook> lstInvoiceBook,bool isNew);
    }
    public class ClsCustomerInvoiceInfo:ICustomerInvoiceInfo
    {
        BookStoreContext context;
        ISalesInvoice ClsSalesInvoice;
        public ClsCustomerInvoiceInfo(BookStoreContext ctx, ISalesInvoice salesInvoice) 
        {
            context = ctx;
            ClsSalesInvoice = salesInvoice;
        }
        public bool Save (TbCustomerDeliverInfo info,TbSalesInvoice invoice,List<TbSalesInvoiceBook>lstInvoiceBook,bool isNew)
        {
            using var transaction = context.Database.BeginTransaction();
            try
            {
                if(isNew == true )
                {
                    context.TbCustomerDeliverInfos.Add(info);
                }
                else
                {
                    context.Entry(info).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                context.SaveChanges();
                ClsSalesInvoice.Save(invoice,info.CustomerDeliverId, lstInvoiceBook,isNew);
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }
    }
}
