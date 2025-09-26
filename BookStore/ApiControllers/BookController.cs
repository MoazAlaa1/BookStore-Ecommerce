using BookStore.BL;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookStore.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        IBook oClsBook;
        ApiResponse response = new ApiResponse();
        public BookController(IBook book)
        {
            oClsBook = book;
        }
        /// <summary>
        /// Get All Books From Database
        /// </summary>
        /// <returns></returns>
        // GET: api/<ValuesController>
        [HttpGet]
        public ApiResponse Get()
        {
            response.Data = oClsBook.GetAll();
            response.Error = null;
            response.StatusCode = "200";
            return response;
        }
        /// <summary>
        /// Get Book By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public ApiResponse Get(int id)
        {
            response.Data = oClsBook.GetById(id);
            response.Error = null;
            response.StatusCode = "200";
            return response;
        }
        /// <summary>
        /// Get books by CategoryId
        /// </summary>
        /// <param name="CategoryId"></param>
        /// <returns></returns>
        [HttpGet("GetByCategoryId/{CategoryId}")]
        public ApiResponse GetByCategoryId(int CategoryId)
        {
            response.Data = oClsBook.GetBooksData(CategoryId);
            response.Error = null;
            response.StatusCode = "200";
            return response;
        }
        [HttpGet("Search/{searchItem}")]
        public ApiResponse Search(string searchItem)
        {
            response.Data = oClsBook.Search(searchItem);
            response.Error = null;
            response.StatusCode = "200";
            return response;
        }
        /// <summary>
        /// Add book in database
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        // POST api/<ValuesController>
        [HttpPost]
        public ApiResponse Post([FromBody] TbBook book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    oClsBook.Save(book,"1");
                    response.Data = "good";
                    response.Error = null;
                    response.StatusCode = "200";
                    return response;
                }
                else
                {
                    response.Data = null;
                    response.Error = "This Model Is Not Valid";
                    response.StatusCode = "502";
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Error = ex.Message;
                response.StatusCode = "502";
                return response;
            }
            
        }
        /// <summary>
        /// Delete book from database
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/<ValuesController>/5
        [HttpPost]
        [Route("Delete")]
        public void Delete([FromBody]int id)
        {
            oClsBook.Delete(id);
        }
    }
}
