using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BL
{
        public interface IAuthor
        {
            public List<TbAuthor> GetAll();
            public TbAuthor GetById(int id);
            public bool Save(TbAuthor model);
            public bool Delete(int id);
        }
        public class ClsAuthor : IAuthor
        {
            BookStoreContext context;
            public ClsAuthor(BookStoreContext ctx)
            {
                context = ctx;
            }
            public List<TbAuthor> GetAll()
            {
                try
                {
                //return context.TbAuthors.Where(a => a.CurrentState == 1 ).OrderBy(a => a.AuthorName).ToList();
                     return context.TbAuthors.FromSqlRaw("select * from TbAuthor").ToList();
                }
                catch
                {
                    return new List<TbAuthor>();
                }

            }
            public TbAuthor GetById(int id)
            {
                try
                {
                    return context.TbAuthors.Where(a => a.AuthorId == id && a.CurrentState == 1).FirstOrDefault();
                }
                catch
                {
                    return new TbAuthor();
                }
            }
            public bool Save(TbAuthor model)
            {
                try
                {

                    if (model.AuthorId != 0)
                    {
                        context.Entry(model).State = EntityState.Modified;
                    }
                    else
                    {
                        model.CurrentState = 1;
                        context.TbAuthors.Add(model);
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
                    var Author = GetById(id);
                    Author.CurrentState = 0;
                    context.Entry(Author).State = EntityState.Modified;
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
