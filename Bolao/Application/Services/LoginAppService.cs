using Application.DTOs;
using Application.Interfaces;
using Domain.Exceptions;
using Domain.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class LoginAppService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAuthService _authService;

        public LoginAppService(IUsuarioRepository usuarioRepository, IAuthService authService)
        {
            _usuarioRepository = usuarioRepository;
            _authService = authService;
        }

        public async Task<LoginResponseDto> RealizarLoginAsync(LoginRequestDto dto)
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
    }
}
