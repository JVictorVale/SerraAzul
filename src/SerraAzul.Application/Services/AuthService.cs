using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Security.Jwt.Core.Interfaces;
using SerraAzul.Application.Contracts;
using SerraAzul.Application.DTOs.V1.Auth;
using SerraAzul.Application.Notifications;
using SerraAzul.Core.Settings;
using SerraAzul.Domain.Contracts.Repositories;
using SerraAzul.Domain.Entities;

namespace SerraAzul.Application.Services;

public class AuthService : BaseService, IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordHasher<Usuario> _passwordHasher;
    private readonly IJwtService _jwtService;
    private readonly JwtSettings _jwtSettings;
    
    public AuthService(IMapper mapper, INotificator notificator, IUsuarioRepository usuarioRepository, IPasswordHasher<Usuario> passwordHasher, IOptions<JwtSettings> jwtSettings, IJwtService jwtService) : base(mapper, notificator)
    {
        _usuarioRepository = usuarioRepository;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<TokenDto?> Login(LoginDto login)
    {
        if (string.IsNullOrEmpty(login.Senha))
        {
            Notificator.HandleNotFoundResource();
            return null;
        }

        var usuario = await _usuarioRepository.ObterPorEmail(login.Email);
        if (usuario == null)
        {
            Notificator.HandleNotFoundResource();
            return null;
        }

        if (_passwordHasher.VerifyHashedPassword(usuario, usuario.Senha, login.Senha) !=
            PasswordVerificationResult.Failed)
            return new TokenDto
            {
                Token = await GerarToken(usuario)
            };
        
        Notificator.Handle("Não foi possível fazer o login");
        return null;
    }

    
    
    private async Task<string> GerarToken(Usuario usuario)
    {
        var tokenHandle = new JwtSecurityTokenHandler();

        var claimsIdentity = new ClaimsIdentity();
        claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, usuario.Email));

        var key = await _jwtService.GetCurrentSigningCredentials();
        var token = tokenHandle.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _jwtSettings.Emissor,
            Audience = _jwtSettings.ComumValidoEm,
            Subject = claimsIdentity,
            Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpiracaoHoras),
            SigningCredentials = key
        });

        return tokenHandle.WriteToken(token);
    }
}