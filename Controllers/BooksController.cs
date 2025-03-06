using BooksApiYayin.Context;
using BooksApiYayin.Models;
using BooksApiYayin.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BooksApiYayin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookDbContext _dbContext;

        public BooksController(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("getAllBoks")]
        public IActionResult GetAllBooks()
        {
            var books = _dbContext.Books.ToList();
            return Ok(books);
        }
        [HttpGet("getByIdBook")]
        public IActionResult GetByIdBook(int id)
        {
            //var book = _dbContext.Books.Where(x => x.Id == id).FirstOrDefault();
            //var book = _dbContext.Books.FirstOrDefault( x => x.Id == id);
            var book = _dbContext.Books.Find(id);
            if (book == null)
            {
                return NotFound("Kitap bulunamadi.");
            }
            return Ok(book);
        }
        [HttpPost("createBook")]
        public IActionResult CreateBook(CreateBookViewModel model)
        {
            if(model.Title== null || model.Title == "" || model.Title.IsNullOrEmpty())
            {
                return BadRequest("Baslik alani bos birakilamaz.");
            }
            if (model.Author == null || model.Author == "" || model.Author.IsNullOrEmpty())
            {
                return BadRequest("Yazar alani bos birakilamaz.");
            }
            var newmodel = new Book
            {
                Author = model.Author,
                Title = model.Title,
                Stock = model.Stock,
            };
            _dbContext.Books.Add(newmodel);
            _dbContext.SaveChanges();
            return Ok("Kitap olusturuldu.");
        }
        [HttpPut("updateBook")]
        public IActionResult UpdateBook(Book model)
        {
            var book = _dbContext.Books.Find(model.Id);
            if(book == null)
            {
                return NotFound("Kitap Bulunamadi.");
            }
            book.Title = model.Title;
            book.Author = model.Author;
            book.Stock = model.Stock;

            _dbContext.Books.Update(book);
            _dbContext.SaveChanges();
            return Ok("Kitap guncellendi.");
        }
        [HttpDelete("deleteBook")]
        public IActionResult DeleteBook(int id)
        {
            var book = _dbContext.Books.Find(id);
            if(book == null)
            {
                return NotFound("Kitap bulunamadi.");
            }
            _dbContext.Books.Remove(book);
            _dbContext.SaveChanges();
            return Ok("Kitap silindi");
        }
        
    }
}
