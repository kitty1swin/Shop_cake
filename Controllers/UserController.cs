using Microsoft.AspNetCore.Mvc;
using Shop_cake.Data;
using Shop_cake.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop_cake.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly List<User> users = new List<User>();
        private readonly AppDBContext _db;

        public UserController(AppDBContext db)
        {
            _db = db;
        }

        // GET 
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return _db.Users.ToArray();
        }

        // GET
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        // POST 
        [HttpPost("register")]
        public ActionResult<User> Register([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User data is missing");
            }

            // Проверка наличия обязательных полей
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("Email and password are required fields");
            }

            // Проверка уникальности email
            if (_db.Users.Any(u => u.Email == user.Email))
            {
                return BadRequest("User with this email already exists");
            }

            // Хеширование пароля
            string hashedPassword = HashPassword(user.Password);
            user.Password = hashedPassword;

            // Добавление пользователя в базу данных
            _db.Users.Add(user);
            _db.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        // PUT 
        [HttpPut("{id}")]
        public IActionResult Put(int id, User updatedUser)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            user.Name = updatedUser.Name;
            user.Surname = updatedUser.Surname;
            user.Patronymic = updatedUser.Patronymic;
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;
            user.PhoneNumber = updatedUser.PhoneNumber;
            _db.SaveChanges();

            return NoContent();
        }

        private string HashPassword(string password)
        {
            // реализация хеширования пароля
            return password;
        }
    }
}