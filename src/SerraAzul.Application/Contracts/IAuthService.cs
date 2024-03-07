using SerraAzul.Application.DTOs.V1.Auth;
using SerraAzul.Application.DTOs.V1.User;

namespace SerraAzul.Application.Contracts;

public interface IAuthService
{
    Task<TokenDto?> Login(LoginDto login);
}