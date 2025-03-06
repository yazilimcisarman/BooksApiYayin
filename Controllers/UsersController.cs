using BooksApiYayin.Context;
using BooksApiYayin.Models;
using BooksApiYayin.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksApiYayin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly BookDbContext _dbContext;

        public UsersController(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("getAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = _dbContext.Users.ToList();
            return Ok(users);
        }
        [HttpGet("getByIdUser")]
        public IActionResult GetByIdUser(int id)
        {
            if(id == 0)
            {
                return BadRequest("Id zorunlu alandir.");
            }
            var user = _dbContext.Users.Where(x => x.Id == id).FirstOrDefault();
            if(user == null)
            {
                return NotFound("Kullanici bulunamadi.");
            }
            return Ok(user);
        }
        [HttpPost("createUser")]
        public IActionResult CreateUser(CreateUserViewModel model)
        {
            var newmodel = new User
            {
                Name = model.Name,
               Surname= model.Surname,
               Email = model.Email,
               Phone = model.Phone
            };
            _dbContext.Users.Add(newmodel);
            _dbContext.SaveChanges();
            return Ok("Kullanici olusturuldu.");
        }
        [HttpPut("updateUser")]
        public IActionResult UpdateUser(User model)
        {
            var user = _dbContext.Users.Where(x => x.Id == model.Id).FirstOrDefault();
            if(user == null)
            {
                return NotFound("Kullanici bulunamadi.");
            }
            else
            {
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.Email = model.Email;
                user.Phone = model.Phone;
                _dbContext.Users.Update(user);
                _dbContext.SaveChanges();

                return Ok("Kullanici Guncellendi.");
            }
        }
        [HttpDelete("deleteUser")]
        public IActionResult DeleteUser(int id)
        {
            var user = _dbContext.Users.Where(x => x.Id == id).FirstOrDefault();
            if (user == null)
            {
                return NotFound("Kullanici bulunamadi.");
            }
            else
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
                return Ok("Kullanici silindi.");
            }
        }
    }
}
