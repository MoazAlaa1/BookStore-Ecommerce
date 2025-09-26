using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Bl
{
    public interface ISliders
    {
        public List<TbSlider> GetAll();
        public TbSlider GetById(int id);
        public bool Save(TbSlider slider,string userId);
        public bool Delete(int id);
    }

    public class ClsSliders : ISliders
    {
        BookStoreContext context;
        public ClsSliders(BookStoreContext ctx)
        {
            context = ctx;
        }
        public List<TbSlider> GetAll()
        {
            try
            {
                var sliders = context.TbSliders.Where(a=>a.CurrentState==1).ToList();
                return sliders;
            }
            catch
            {
                return new List<TbSlider>();
            }
        }

        public TbSlider GetById(int id)
        {
            try
            {
                var slider = context.TbSliders.FirstOrDefault(a => a.SliderId == id && a.CurrentState==1);
                return slider;
            }
            catch
            {
                return new TbSlider();
            }
        }

        public bool Save(TbSlider slider,string userId)
        {
            try
            {
                slider.CurrentState = 1;
                if (slider.SliderId == 0)
                {
                    slider.CreatedBy = userId;
                    slider.CreatedDate = DateTime.Now;
                    context.TbSliders.Add(slider);
                }
                else
                {
                    slider.UpdatedBy = userId;
                    slider.UpdatedDate = DateTime.Now;
                    context.Entry(slider).State = EntityState.Modified;
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
                var slider = GetById(id);
                slider.CurrentState = 0;
                context.Entry(slider).State = EntityState.Modified;
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
