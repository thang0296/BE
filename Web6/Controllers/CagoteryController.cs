using Humanizer.Localisation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web6.Enti;
using Web6.Models;
using Microsoft.EntityFrameworkCore;

namespace Web6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CagoteryController : ControllerBase
    {
        private readonly BANHMIContext _context;

        public CagoteryController(BANHMIContext ctx)
        {
            _context = ctx;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.UserIn4s.ToList());
        }
        [HttpPost]
        public IActionResult CreateNew(UserIn4 model)
        {
            try
            {
            
                _context.Add(model);
                _context.SaveChanges();
                return Ok(model);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut]
        public IActionResult UpdateUserIn4(UserIn4 model)
        {
   
            var userIn4 = _context.UserIn4s.AsNoTracking().FirstOrDefault(x=>x.Id ==model.Id);
           // _context.Entry(userIn4).CurrentValues.SetValues(model);
            if (userIn4 != null)
            {
                _context.Update(model);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete]
        public IActionResult DeleteUserIn4(string id)
        {
        
            var userIn4 = _context.UserIn4s.FirstOrDefault(i => i.Id == id);// đoạn này đang null này
            if (userIn4 != null)
            {
                _context.Remove(userIn4);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetUserById(string id)
        {
            var userIn4 = _context.UserIn4s.FirstOrDefault(i => i.Id == id);
            if (userIn4 != null)
            {
                return Ok(userIn4);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
