using BookStore.Models;

namespace BookStore.Models
{
    public class VmHomePages
    {
        public VmHomePages()
        {
            lstAllBooks = new List<TbBook>();
            lstRecommendedBooks = new List<TbBook>();
            lstFreeDelivery = new List<TbBook>();
            lstNewBooks = new List<TbBook>();
            lstCategories = new List<TbCategory>();
            lstSliders = new List<TbSlider>();
        }
        public List<TbBook> lstAllBooks { get; set; }
        public List<TbBook> lstRecommendedBooks { get; set; }
        public List<TbBook> lstFreeDelivery { get; set; }
        public List<TbBook> lstNewBooks { get; set; }
        public List<TbCategory> lstCategories { get; set; }
        public List<TbSlider> lstSliders { get; set; }
    }
}
