using Blog_Pessoal.Model;

namespace Blog_Pessoal.Security
{
    public interface IAuthService
    {
        Task<UserLogin?> Autenticar(UserLogin userLogin);
    }
}
