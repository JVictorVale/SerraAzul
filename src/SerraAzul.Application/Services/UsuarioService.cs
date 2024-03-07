using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SerraAzul.Application.Contracts;
using SerraAzul.Application.DTOs.V1.User;
using SerraAzul.Application.Notifications;
using SerraAzul.Domain.Contracts.Repositories;
using SerraAzul.Domain.Entities;

namespace SerraAzul.Application.Services;

public class UsuarioService : BaseService, IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordHasher<Usuario> _passwordHasher;
    
    public UsuarioService(IMapper mapper, INotificator notificator, IUsuarioRepository usuarioRepository, IPasswordHasher<Usuario> passwordHasher) : base(mapper, notificator)
    {
        _usuarioRepository = usuarioRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<UsuarioDto?> Cadastrar(AdicionarUsuarioDto usuarioDto)
    {
        var usuario = Mapper.Map<Usuario>(usuarioDto);
        if (!await Validar(usuario))
        {
            return null;
        }
        
        usuario.Senha = _passwordHasher.HashPassword(usuario, usuario.Senha);
        _usuarioRepository.Cadastrar(usuario);
        if (await _usuarioRepository.UnitOfWork.Commit())
        {
            return Mapper.Map<UsuarioDto>(usuario);
        }
        
        Notificator.Handle("Não foi possível cadastrar o usuário");
        return null;
    }

    public async Task<UsuarioDto?> Atualizar(int id, AtualizarUsuarioDto usuarioDto)
    {
        if (id != usuarioDto.Id)
        {
            Notificator.Handle("Os ids não conferem!");
            return null;
        }

        var usuario = await _usuarioRepository.ObterPorId(id);
        if (usuario == null)
        {
            Notificator.HandleNotFoundResource();
            return null;
        }

        Mapper.Map(usuarioDto, usuario);
        if (!await Validar(usuario))
        {
            return null;
        }

        usuario.Senha = _passwordHasher.HashPassword(usuario, usuario.Senha);
        _usuarioRepository.Atualizar(usuario);
        if (await _usuarioRepository.UnitOfWork.Commit())
        {
            return Mapper.Map<UsuarioDto>(usuario);
        }
        
        Notificator.Handle("Não foi possível alterar o usuário");
        return null;
    }

    public async Task<UsuarioDto> ObterPorId(int id)
    {
        var usuario = await _usuarioRepository.ObterPorId(id);
        if (usuario != null)
            return Mapper.Map<UsuarioDto>(usuario);

        Notificator.HandleNotFoundResource();
        return null;
    }

    public async Task<UsuarioDto> ObterPorEmail(string email)
    {
        var administrador = await _usuarioRepository.ObterPorEmail(email);
        if (administrador != null)
        {
            return Mapper.Map<UsuarioDto>(administrador);
        }
        
        Notificator.HandleNotFoundResource();
        return null;
    }

    public async Task<List<UsuarioDto>> ObterTodos()
    {
        var administradores = await _usuarioRepository.ObterTodos();
        return Mapper.Map<List<UsuarioDto>>(administradores);
    }

    private async Task<bool> Validar(Usuario usuario)
    {
        if (!usuario.Validar(out var validationResult))
        {
            Notificator.Handle(validationResult.Errors);
        }

        var usuarioExistente = await _usuarioRepository.FirstOrDefault(c =>
            c.Id != usuario.Id && (c.Cpf == usuario.Cpf || c.Email == usuario.Email));
        if (usuarioExistente != null)
        {
            if (usuarioExistente.Email == usuario.Email)
            {
                Notificator.Handle("Já existe um usuário cadastrado com o mesmo endereço de e-mail.");
            }

            if (usuarioExistente.Cpf == usuario.Cpf)
            {
                Notificator.Handle("Já existe um usuário cadastrado com o mesmo CPF.");
            }
        }
        return !Notificator.HasNotification;
    }
}