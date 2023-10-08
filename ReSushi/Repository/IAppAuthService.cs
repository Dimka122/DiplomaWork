using ReSushi.Models;

namespace ReSushi.Repository
{
    public interface IAppAuthService
    {
        Task<Token> Authenticate(User user);
    }
}
