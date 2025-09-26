using BookStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookStore.BL
{
    public interface IBook
    {
        public List<VwBook> GetAllVwBooks();
        public VwBook GetBookById(int id);
        public List<TbBook> GetBooksData(int? categoryId);
        public List<TbBook> GetRelatedBooks(int bookId);
        public List<TbBook> GetAll();
        public TbBook GetById(int id);
        public List<TbBook> GetByAutherId(int id);
        public List<TbBook> Search(string target);
        public decimal GetPriceAfterDiscount(int bookId);
        public bool Save(TbBook book,string userId);
        public Task<bool> Delete(int id);
        public bool UpdateBookQty(int bookId,int Quantity);
    }
    public class ClsBook: IBook
    {
        public BookStoreContext context;
        public ClsBook(BookStoreContext ctx)
        {

            context = ctx;

        }
        public VwBook GetBookById(int id)
        {
            try
            {
                return context.VwBooks.Where(a => (a.BookId == id) && a.CurrentState == 1 && a.Qty > 0).FirstOrDefault()
;
            }
            catch
            {
                return new VwBook();
            }
            
        }
        public List<VwBook> GetAllVwBooks()
        {
            try
            {
                return context.VwBooks.Where(a=>a.CurrentState == 1).ToList();
            }
            catch 
            {
                return new List<VwBook>();
            }
        }
        public List<TbBook> GetAll()
        {
            try
            {
                return context.TbBooks.Where(a => a.CurrentState == 1 && a.Qty > 0).Take(200).ToList();
            }
            catch
            {
                return new List<TbBook>();
            }
        }
        public List<TbBook> GetBooksData(int? categoryId)
        {
            try
            {
                //return context.VwBooks.Where(a => (a.CategoryId == categoryId || categoryId == null || categoryId == 0) && a.CurrentState == 1).ToList();
                if (categoryId != null)
                {
                    return context.TbBooks.FromSqlRaw($"select * from VwBooks where CategoryId = {categoryId} and CurrentState = 1 ").ToList();
                }
                else
                {
                    return context.TbBooks.FromSqlRaw("select * from VwBooks where CurrentState = 1 and Qty > 0 ").Take(200).ToList();
                }

            }
            catch
            {
                return new List<TbBook>();
            }

        }
        public List<TbBook> GetRelatedBooks(int bookId)
        {
            try
            {
                var book = GetById(bookId);
                return context.TbBooks.Where(a => (a.CategoryId == book.CategoryId) && (a.CurrentState == 1) && (a.Qty > 0)  && (a.BookId != bookId)).ToList();
;
            }
            catch
            {
                return new List<TbBook>();
            }
            
        }
        public TbBook GetById(int bookId)
        {
            try
            {
                var book = context.TbBooks.FirstOrDefault(a => a.BookId == bookId && a.CurrentState == 1);
                return book;
            }
            catch
            {
                return new TbBook();
            }

        }
        public List<TbBook> GetByAutherId(int id)
        {
            try
            {
                return context.TbBooks.Where(a => a.AuthorId == id).ToList();
            }
            catch
            {

                return new List<TbBook>();
            }
        }
        public List<TbBook> Search(string target)
        {
            try
            {
                return context.TbBooks.Where(a => a.Title.Contains(target) || a.Isbn.Contains(target)).ToList();
            }
            catch (Exception)
            {
                return new List<TbBook>();
            }
        }
        public decimal GetPriceAfterDiscount(int bookId)
        {
            try
            {
                var book = GetBookById(bookId);
                decimal result = book.SalesPrice - ((book.SalesPrice * (decimal)(book.DiscountPercent??=0)) / 100);
                return Math.Round(result, 2);
            }
            catch
            {

                return new decimal();
            }
           
        }
        public bool Save(TbBook book,string userId)
        {
            try
            {
                if (book.BookId == 0)
                {

                    book.CreatedBy = userId;
                    book.CurrentState = 1;
                    book.CreatedDate = DateTime.Now;
                    book.Qty = 0;
                    context.TbBooks.Add(book);
                }
                else
                {
                    book.UpdateBy = userId;
                    book.UpdateDate = DateTime.Now;
                    context.Entry(book).State = EntityState.Modified;
                }
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                var book = GetById(id);
                book.CurrentState = 0;
                context.Entry(book).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateBookQty(int bookId, int Quantity)
        {
            try
            {
                var book = GetById(bookId);
                book.Qty = book.Qty + Quantity;
                context.Entry(book).State = EntityState.Modified;
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
