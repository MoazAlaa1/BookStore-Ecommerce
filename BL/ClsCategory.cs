using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BL
{
    public interface ICategory
    {
        public List<TbCategory> GetAll();
        public TbCategory GetById(int id);
        public Task<bool> Save(TbCategory model);
        public bool Delete(int id);

    }
    public class ClsCategory: ICategory
    {
        BookStoreContext context;
        public ClsCategory(BookStoreContext ctx)
        {
            context = ctx;
        }
        public List<TbCategory> GetAll()
        {
            try
            {
                //return context.TbCategories.Where(a=>a.CurrentState==1).OrderBy(a => a.CategoryName).ToList();
                return context.TbCategories.FromSqlRaw("select * from TbCategories where Currentstate = 1").ToList();
            }
            catch
            {
                return new List<TbCategory>();
            }
            
        }
        public TbCategory GetById(int id)
        {
            try
            {
                return context.TbCategories.Where(a => a.CategoryId == id && a.CurrentState==1).FirstOrDefault();
            }
            catch
            {
                return new TbCategory();
            }
        }
        public async Task<bool> Save (TbCategory model)
        {
            try
            {
                if (model.CategoryId != 0)
                {
                    model.UpdatedBy = "1";
                    model.UpdatedDate = DateTime.Now;
                   context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                else
                {
                    model.CreatedDate = DateTime.Now;
                    model.CreatedBy = "1";
                    model.CurrentState = 1;
                  context.TbCategories.Add(model);
                }
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete (int id)
        {
            try
            {
                var category = GetById(id);
                category.CurrentState = 0;
                context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
