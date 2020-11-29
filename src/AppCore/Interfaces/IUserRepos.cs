using AppCore.Models;

namespace AppCore.Interfaces
{
    public interface IUserRepos : IRepository<User>
    {
        new User Add(User source, User user);
        new void Update(User source, User user);
        new void Delete(User source, User user);
        void Activate(User source, User user);
        void Disable(User source, User user);
    }
}