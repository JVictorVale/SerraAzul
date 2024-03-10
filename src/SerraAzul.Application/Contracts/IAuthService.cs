using SerraAzul.Application.DTOs.V1.Auth;

namespace SerraAzul.Application.Contracts;

public interface IAuthService
{
    Task<TokenDto?> Login(LoginDto login);
}