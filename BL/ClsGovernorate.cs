using BookStore.Models;

namespace BookStore.BL
{
    public interface IGovernorate
    {
        public List<TbGovernorate> GetAll();
        public TbGovernorate GetById(int id);
        public Task<bool> Save(TbGovernorate model);
        public bool Delete(int id);

    }
    public class ClsGovernorate : IGovernorate
    {
        BookStoreContext context;
        public ClsGovernorate(BookStoreContext ctx)
        {
            context = ctx;
        }
        public List<TbGovernorate> GetAll()
        {
            try
            {
                return context.TbGovernorates.Where(a => a.CurrentState == 1).ToList();
            }
            catch
            {
                return new List<TbGovernorate>();
            }

        }
        public TbGovernorate GetById(int id)
        {
            try
            {
                return context.TbGovernorates.Where(a => a.GovernorateId == id && a.CurrentState == 1).FirstOrDefault();
            }
            catch
            {
                return new TbGovernorate();
            }
        }
        public async Task<bool> Save(TbGovernorate model)
        {
            try
            {

                if (model.GovernorateId != 0)
                {
                    context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                else
                {
                    model.CurrentState = 1;
                    context.TbGovernorates.Add(model);
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
                var governorate = GetById(id);
                governorate.CurrentState = 0;
                context.Entry(governorate).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
