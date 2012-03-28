namespace CliqFlip.Infrastructure.Authentication.Interfaces
{
    public interface IUserAuthentication
    {
        void Login(string username, bool stayLoggedIn, string role);
        void Logout();
    }
}