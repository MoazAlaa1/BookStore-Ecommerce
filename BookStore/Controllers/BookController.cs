using BookStore.BL;
using BookStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace Context_form_database.Controllers
{
    public class BookController : Controller
    {
        IBook oClsBook;
        public BookController(IBook book)
        {
            oClsBook = book;    
        }
        public IActionResult BookDetails(int id)
        {
            VmBookDetails Vm = new VmBookDetails();
            Vm.Book = oClsBook.GetBookById(id);
            Vm.lstRelatedBooks = oClsBook.GetRelatedBooks(id).Take(12).ToList();
            Vm.PriceDiscounted = oClsBook.GetPriceAfterDiscount(id);
            return View(Vm);
        }
        public IActionResult AuthorBooks(int id,string name)
        {
            ViewBag.Name = name;
            return View(oClsBook.GetByAutherId(id));
        }
        public IActionResult BookList()
        {
            return View();
        }
    }
}
