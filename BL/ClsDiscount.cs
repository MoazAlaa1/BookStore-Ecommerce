using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BL
{
        public interface IDiscount
        {
            public List<TbDiscount> GetAll();
            public TbDiscount GetById(int id);
            public Task<bool> Save(TbDiscount model);
            public bool Delete(int id);

        }
        public class ClsDiscount : IDiscount
        {
            BookStoreContext context;
            public ClsDiscount(BookStoreContext ctx)
            {
                context = ctx;
            }
            public List<TbDiscount> GetAll()
            {
                try
                {
                return context.TbDiscounts.OrderBy(a => a.DiscountPercent).Where(a=>a.CurrentState==1).ToList();
                //return context.TbDiscounts.FromSqlRaw("select * from TbDiscount order By DiscountPercent").ToList();
            }
                catch
                {
                    return new List<TbDiscount>();
                }

            }
            public TbDiscount GetById(int id)
            {
                try
                {
                    return context.TbDiscounts.Where(a => a.DiscountId == id).FirstOrDefault();
                }
                catch
                {
                    return new TbDiscount();
                }
            }
            public async Task<bool> Save(TbDiscount model)
            {
                try
                {
                    model.CurrentState = 1;
                    if (model.DiscountId != 0)
                    {
                        context.Entry(model).State = EntityState.Modified;
                    }
                    else
                    {
                        context.TbDiscounts.Add(model);
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
                    var Discount = GetById(id);
                    Discount.CurrentState = 0;
                    context.Entry(Discount).State = EntityState.Modified;
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
