using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PasswordManagerTask.Data;
using Domain.Model;
using System.Net;

namespace PasswordManagerTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PasswordControllers : ControllerBase
    {
        private readonly PasswordContext _context;

        public PasswordControllers(PasswordContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PasswordEntity>>> GetPasswordEntity()
        {
            return await _context.Password.OrderByDescending(x => x.DataCreated).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<PasswordEntity>> Post(PasswordEntity entity)
        {
            if (entity.Type == "email" && !IsValidEmail(entity.Name))
            {
                return BadRequest("Неверный email адрес");
            }

            if (_context.Password.Any(x => x.Name == entity.Name))
            {
                return Conflict("Запись с таким именем уже существует");
            }

            entity.DataCreated = DateTime.Now;

            try
            {
                _context.Password.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения записи: {ex.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, "Ошибка сохранения записи");
            }

            return CreatedAtAction("GetPasswordEntity", new { id = entity.Id }, entity);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}