using Microsoft.EntityFrameworkCore;
using TDD.API.Contracts;
using TDD.API.DAO;
using TDD.API.Entity;

namespace TDD.API.Services
{
    public class UserService : IUserService
    {
        public DbContext _context;
        public UserService(ApplicationContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public User Add(User user)
        {
            _context.Set<User>().Add(user);
            _context.SaveChanges();
            return user;
        }

        public bool Delete(int id)
        {
            var user = _context.Set<User>().Where(x => x.Id == id);
            if (user.Any())
            {
                _context.Set<User>().Remove(user.FirstOrDefault());
                _context.SaveChanges();
                return true;
            }
            else
                return false;


        }

        public User Get(int id)
        {
            return _context.Set<User>().Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Set<User>().ToList();
        }
    }
}
