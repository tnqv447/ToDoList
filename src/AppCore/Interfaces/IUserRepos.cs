using AppCore.Models;

namespace AppCore.Interfaces {
    public interface IUserRepos : IRepository<User> {
        // User Add (User source, User user);
        // void Update (User source, User user);
        // void Delete (User source, User user);
        void Activate (User source, User user);
        void Disable (User source, User user);

        User GetUserByAccount (string username, string password);
        bool isUserNameExists (string username);
    }
}