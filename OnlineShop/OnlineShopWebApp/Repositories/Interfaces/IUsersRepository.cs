namespace OnlineShopWebApp.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        IEnumerable<User> GetAll();
        User TryGetById(Guid id);
        User TryGetByEmail(string email);
        void Add(User user);
        void Edit(User user);
        void Remove(Guid userId);
    }
}
