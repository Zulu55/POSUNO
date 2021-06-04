using Microsoft.EntityFrameworkCore;
using POSUNO.Api.Data.Entities;
using POSUNO.Api.Enums;
using POSUNO.Api.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace POSUNO.Api.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            await CheckUserAsync("Juan", "Zuluaga", "juan@yopmail.com", "322 311 4620");
            await CheckUserAsync("Ledys", "Bedoya", "ledys@yopmail.com", "322 311 4620");
            await CheckCustomersAsync();
            await CheckProductsAsync();
        }

        private async Task<User> CheckUserAsync(string firstName, string lastName, string email, string phone)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, UserType.Admin.ToString());
                await _userHelper.AddUserToRoleAsync(user, UserType.User.ToString());
            }

            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }


        private async Task CheckCustomersAsync()
        {
            if (!_context.Customers.Any())
            {
                User user = await _context.Users.FirstOrDefaultAsync();
                for (int i = 1; i <= 50; i++)
                {
                    _context.Customers.Add(new Customer { FirstName = $"Cliente {i}", LastName = $"Apellido {i}", Phonenumber = "322 311 4620", Address = "Calle Luna Calle Sol", Email = $"cliente{i}@yopmail.com", IsActive = true, User = user });
                }

                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckProductsAsync()
        {
            if (!_context.Products.Any())
            {
                Random random = new Random();
                User user = await _context.Users.FirstOrDefaultAsync();
                for (int i = 1; i <= 200; i++)
                {
                    _context.Products.Add(new Product { Name = $"Producto {i}", Description = $"Producto {i}", Price = random.Next(5, 1000), Stock = random.Next(0, 500), IsActive = true, User = user });
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
