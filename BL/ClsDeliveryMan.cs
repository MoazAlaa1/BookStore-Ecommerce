using BookStore.Models;

namespace BookStore.BL
{
        public interface IDeliveryMan
        {
            public List<TbDeliveryMan> GetAll();
            public TbDeliveryMan GetById(int id);
            public Task<bool> Save(TbDeliveryMan model);
            public bool Delete(int id);
            public int GetDeliverManId();

        }
        public class ClsDeliveryMan : IDeliveryMan
        {
            BookStoreContext context;
            public ClsDeliveryMan(BookStoreContext ctx)
            {
                context = ctx;
            }
            public List<TbDeliveryMan> GetAll()
            {
                try
                {
                    return context.TbDeliveryMen.Where(a => a.CurrentState == 1).ToList();
                }
                catch
                {
                    return new List<TbDeliveryMan>();
                }

            }
            public TbDeliveryMan GetById(int id)
            {
                try
                {
                    return context.TbDeliveryMen.Where(a => a.DeliveryManId == id && a.CurrentState == 1).FirstOrDefault();
                }
                catch
                {
                    return new TbDeliveryMan();
                }
            }
            public async Task<bool> Save(TbDeliveryMan model)
            {
                try
                {

                    if (model.DeliveryManId != 0)
                    {
                        context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    }
                    else
                    {
                        model.CurrentState = 1;
                        context.TbDeliveryMen.Add(model);
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
                    var deliveryMan = GetById(id);
                    deliveryMan.CurrentState = 0;
                    context.Entry(deliveryMan).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }

            }
            public int GetDeliverManId()
            {
                return context.TbDeliveryMen.Where(a=>a.Status == 1).FirstOrDefault().DeliveryManId;
            }
        }
    }
