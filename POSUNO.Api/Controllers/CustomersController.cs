using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSUNO.Api.Data;
using POSUNO.Api.Data.Entities;
using POSUNO.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace POSUNO.Api.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly DataContext _context;

        public CustomersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            Customer customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, CustomerRequest request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            string email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return BadRequest("Usuario no existe.");
            }

            Customer customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return BadRequest("Cliente no existe.");
            }

            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.Phonenumber = request.Phonenumber;
            customer.Address = request.Address;
            customer.Email = request.Email;
            customer.IsActive = request.IsActive;
            customer.User = user;

            Customer oldCustomer = await _context.Customers.FindAsync(id);
            if (oldCustomer == null)
            {
                return BadRequest("Cliente no existe.");
            }

            oldCustomer.FirstName = customer.FirstName;
            oldCustomer.LastName = customer.LastName;
            oldCustomer.Phonenumber = customer.Phonenumber;
            oldCustomer.Address = customer.Address;
            oldCustomer.Email = customer.Email;
            oldCustomer.IsActive = customer.IsActive;
            _context.Entry(oldCustomer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(CustomerRequest request)
        {
            string email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return BadRequest("Usuario no existe.");
            }

            Customer oldCustomer = await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
            if (oldCustomer != null)
            {
                return BadRequest("Ya hay un cliente registrado con ese email.");
            }

            Customer customer = new Customer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phonenumber = request.Phonenumber,
                Address = request.Address,
                Email = request.Email,
                IsActive = request.IsActive,
                User = user
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            Customer customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
