using BookStore.Models;
using Microsoft.AspNetCore.Identity;
namespace BookStore.Bl
{
    public interface IPages
    {
        public List<TbPages> GetAll();
        public TbPages GetById(int id);
        public bool Save(TbPages page, string userId);
        public bool Delete(int id);
    }

    public class ClsPages : IPages
    {
        BookStoreContext context;
       
        public ClsPages(BookStoreContext ctx)
        {
            context = ctx;
        }
        public List<TbPages> GetAll()
        {
            try
            {
                var pages = context.TbPages.Where(a=>a.CurrentState==1).ToList();
                return pages;
            }
            catch
            {
                return new List<TbPages>();
            }
        }

        public TbPages GetById(int id)
        {
            try
            {
                var page = context.TbPages.FirstOrDefault(a => a.PageId == id && a.CurrentState==1);
                return page;
            }
            catch
            {
                return new TbPages();
            }
        }

        public bool Save(TbPages page,string userId)
        {
            try
            {
                page.CurrentState = 1;
                if (page.PageId == 0)
                {
                    page.CreatedBy = userId;
                    page.CreatedDate = DateTime.Now;
                    context.TbPages.Add(page);
                }
                else
                {
                    page.UpdatedBy = userId;
                    page.UpdatedDate = DateTime.Now;
                    context.Entry(page).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
                var page = GetById(id);
                page.CurrentState = 0;
                context.Entry(page).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
