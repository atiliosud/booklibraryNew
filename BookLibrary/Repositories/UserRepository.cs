using BookLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using BookLibrary.Services;

namespace BookLibrary.Repositories
{
    public class UserRepository<T> : IUserRepository<T> where T : User
    {
        private readonly BookLibraryContext _context;
        private readonly JwtService _jwtService;

        public UserRepository(BookLibraryContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<T?> GetUserById(int id)
        {
            try
            {
                return await _context.Set<T>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while fetching user by ID.", ex);
            }
        }

        public async Task<T?> GetUserByEmail(string email)
        {
            try
            {
                return await _context.Set<T>().FirstOrDefaultAsync(u => u.Email == email);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while fetching user by email.", ex);
            }
        }

        public async Task<T> AddUser(T user)
        {
            try
            {
                await _context.Set<T>().AddAsync(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while adding user.", ex);
            }
        }

        public async Task<string> SignIn(string email, string password)
        {
            try
            {
                var user = await _context.Set<T>().FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
                if (user != null)
                {
                    return _jwtService.Generate(user);
                }
                else
                {
                    throw new Exception("Invalid email or password.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred during sign-in.", ex);
            }
        }
    }
}
