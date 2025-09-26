using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BL
{
    public interface IPublisher
    {
        public List<TbPublish> GetAll();
        public TbPublish GetById(int id);
        public Task<bool> Save(TbPublish model);
        public bool Delete(int id);

    }
    public class ClsPublisher : IPublisher
    {
        BookStoreContext context;
        public ClsPublisher(BookStoreContext ctx)
        {
            context = ctx;
        }
        public List<TbPublish> GetAll()
        {
            try
            {
                //return context.TbPublishes.Where(a => a.CurrentState == 1).OrderBy(a => a.Publisher).ToList();
                return context.TbPublishes.FromSqlRaw("select * from TbPublish where CurrentState = 1").ToList();
            }
            catch
            {
                return new List<TbPublish>();
            }

        }
        public TbPublish GetById(int id)
        {
            try
            {
                return context.TbPublishes.Where(a => a.PublishId == id && a.CurrentState == 1).FirstOrDefault();
            }
            catch
            {
                return new TbPublish();
            }
        }
        public async Task<bool> Save(TbPublish model)
        {
            try
            {
                if (model.PublishId != 0)
                {
                    context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                else
                {
                    model.CurrentState = 1;
                    context.TbPublishes.Add(model);
                }
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var Publisher = GetById(id);
                Publisher.CurrentState = 0;
                context.Entry(Publisher).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
