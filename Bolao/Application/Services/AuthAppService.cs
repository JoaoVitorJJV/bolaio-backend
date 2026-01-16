using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthAppService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuthService _authService;

        public AuthAppService(IUsuarioRepository usuarioRepository, IAuthService authService)
        {
            _usuarioRepository = usuarioRepository;
            _authService = authService;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
        {

            var usuario = await _usuarioRepository.ObterPorEmailAsync(dto.Email);

            if (usuario == null)
            {
                 
                throw new DomainException("Usuário ou senha inválidos.");
            }

             
            if (!_authService.VerificaLogin(dto.Senha, usuario.PasswordHash, usuario.PasswordSalt))
            {
                throw new DomainException("Usuário ou senha inválidos.");
            }

             
            var token = _authService.CreateToken(usuario);

             
            return new LoginResponseDto(token, usuario.Nome, usuario.Email);
        }
        public async Task<RegisterResponseDto> RegistrarAsync(RegisterRequestDto dto)
        {
            var usuarioExistente = await _usuarioRepository.ObterPorEmailAsync(dto.Email);
            if (usuarioExistente != null)
            {
                throw new DomainException("Este e-mail já está em uso.");
            }

            _authService.CriarPasswordHash(dto.Senha, out byte[] passwordHash, out byte[] passwordSalt);

            var novoUsuario = new Usuario(dto.Nome, dto.Email);
            novoUsuario.DefinirSenha(passwordHash, passwordSalt);
            novoUsuario.Carteira = new Carteira();

            await _usuarioRepository.AdicionarAsync(novoUsuario);
            await _usuarioRepository.SaveChangesAsync();
            return new RegisterResponseDto(novoUsuario.Id, novoUsuario.Nome, novoUsuario.Email);
        }
    }
}
