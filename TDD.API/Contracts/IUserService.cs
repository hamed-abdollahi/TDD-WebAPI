using TDD.API.Entity;

namespace TDD.API.Contracts
{
    public interface IUserService
    {
        User Get(int id);
        IEnumerable<User> GetAll();
        User Add(User user);
        Boolean Delete(int id);

    }
}
