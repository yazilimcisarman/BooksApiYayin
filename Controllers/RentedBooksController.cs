using BooksApiYayin.Context;
using BooksApiYayin.Models;
using BooksApiYayin.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksApiYayin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentedBooksController : ControllerBase
    {
        private readonly BookDbContext _dbContext;

        public RentedBooksController(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("getAllRentedBooks")]
        public IActionResult GetAllRentedBooks()
        {
            var rentedBooks = _dbContext.RentedBooks.ToList();
            foreach (var book in rentedBooks)
            {
                book.User = _dbContext.Users.Find(book.UserId);
                book.Book = _dbContext.Books.Find(book.BookId);
            }
            return Ok(rentedBooks);
        }
        [HttpGet("getByIdRentedBook")]
        public IActionResult GetByIdRentedBook(int id)
        {
            var rentedBook = _dbContext.RentedBooks.Find(id);
            if(rentedBook != null)
            {
                //rentedBook.User = _dbContext.Users.Find(rentedBook.UserId);
                rentedBook.User = _dbContext.Users.FirstOrDefault(x => x.Id == rentedBook.UserId);
                rentedBook.Book = _dbContext.Books.Find(rentedBook.BookId);
            }
            return Ok(rentedBook);
        }
        //kiralama islemi olsutugunda bizim kitap stogumuzdan bir dusmesini istiyoruz
        [HttpPost("createRentedBook")]
        public IActionResult CreateRentedBook(CreateRentedBookViewModel model)
        {
            var book = _dbContext.Books.Find(model.BookId);
            if(book.Stock > 0)
            {
                //eklem islemi
                var newmodel = new RentedBook
                {
                    UserId = model.UserId,
                    User = null,
                    BookId = model.BookId,
                    Book=null,
                    StartDate = DateTime.Now,
                    EndDate = null,
                };
                //model.StartDate = DateTime.Now;
                _dbContext.RentedBooks.Add(newmodel);
                book.Stock--;
                _dbContext.SaveChanges();
            }
            else
            {
                return BadRequest("Bu kitap elimizde mevcut degil.");
            }
           

            return Ok("Kitap kiralandi.");
        }
        //burada efcore kullanarak foreach kullanmadan direkt olarak user ve boo knesneleri aktarma yaptik
        [HttpGet("getAllRentedBooksDeneme")]
        public IActionResult GetAllRentedBooksDeneme()
        {
            var rentedBooks = _dbContext.RentedBooks.ToList();
            var users = _dbContext.Users.ToList();
            var books = _dbContext.Books.ToList();
            
            return Ok(rentedBooks);
        }
        [HttpPut("updateRentedBook")]
        public IActionResult UpdateRentedBook(UpdateRentedBookViewModel model)
        {
            var rendtedBook = _dbContext.RentedBooks.Find(model.Id);
            rendtedBook.UserId = model.UserId;
            if(rendtedBook.BookId != model.BookId)
            {
                var bookdata = _dbContext.Books.Find(rendtedBook.BookId);
                bookdata.Stock++;
                var bookmodel = _dbContext.Books.Find(model.BookId);
                bookmodel.Stock--;
            }
            rendtedBook.BookId = model.BookId;
            rendtedBook.StartDate = DateTime.Now;
            _dbContext.RentedBooks.Update(rendtedBook);
            _dbContext.SaveChanges();
            return Ok("Kiralanmis kitap guncellendi.");
        }
        [HttpDelete("deleteRentedBook")]
        public IActionResult DeleteRentedBook(int id)
        {
            var rentedBook = _dbContext.RentedBooks.Find(id);
            _dbContext.RentedBooks.Remove(rentedBook);
            _dbContext.SaveChanges();
            return Ok("Kiralama islemi silindi.");
        }

    }
}
