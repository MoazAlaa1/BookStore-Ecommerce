using BookStore.Models;
namespace BookStore.Bl
{
    public interface ISettings
    {
        public TbSettings GetAll();
        public bool Save(TbSettings setting);
    }

    public class ClsSettings : ISettings
    {
        BookStoreContext context;
        public ClsSettings(BookStoreContext ctx)
        {
            context = ctx;
        }
        public TbSettings GetAll()
        {
            try
            {
                var settings = context.TbSettings.FirstOrDefault();
                return settings;
            }
            catch
            {
                return new TbSettings();
            }
        }

        public bool Save(TbSettings setting)
        {
            try
            {
                if(setting.Id == 0)
                {
                    context.TbSettings.Add(setting);
                }
                else
                {
                    context.Entry(setting).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
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
