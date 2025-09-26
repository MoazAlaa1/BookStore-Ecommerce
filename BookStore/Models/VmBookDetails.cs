namespace BookStore.Models
{
    public class VmBookDetails
    {
        public VmBookDetails() 
        {
            Book = new VwBook();
            lstRelatedBooks = new List<TbBook>();
        }
        public VwBook Book { get; set; }
        public List<TbBook> lstRelatedBooks { get; set; }
        public decimal PriceDiscounted { get; set; }
        
    }
}
