namespace BookLibrary.Repositories
{
    public interface IUserRepository<T> where T : class
    {
        Task<T?> GetUserById(int id);
        Task<T> GetUserByEmail(string email);
        Task<T> AddUser(T entity);
        Task<string> SignIn(string email, string password);

    }
}
