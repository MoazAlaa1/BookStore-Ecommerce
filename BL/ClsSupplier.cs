using BookStore.Models;

namespace BookStore.BL
{
    public interface ISupplier
    {
        public List<TbSupplier> GetAll();
        public TbSupplier GetById(int id);
        public Task<bool> Save(TbSupplier model);
        public bool Delete(int id);

    }
    public class ClsSupplier : ISupplier
    {
        BookStoreContext context;
        public ClsSupplier(BookStoreContext ctx)
        {
            context = ctx;
        }
        public List<TbSupplier> GetAll()
        {
            try
            {
                return context.TbSuppliers.Where(a => a.CurrentState == 1).ToList();
            }
            catch
            {
                return new List<TbSupplier>();
            }

        }
        public TbSupplier GetById(int id)
        {
            try
            {
                return context.TbSuppliers.Where(a => a.SupplierId == id && a.CurrentState == 1).FirstOrDefault();
            }
            catch
            {
                return new TbSupplier();
            }
        }
        public async Task<bool> Save(TbSupplier model)
        {
            try
            {
                if (model.SupplierId != 0)
                {
                    context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                else
                {
                    model.CurrentState = 1;
                    context.TbSuppliers.Add(model);
                }
                context.SaveChanges();
                return true;
            }
            catch 
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var supplier = GetById(id);
                supplier.CurrentState = 0;
                context.Entry(supplier).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
