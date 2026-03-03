using Application.Interfaces;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using static Application.DTOs.UsuarioDtos;

namespace Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuario)
        {
            _usuarioRepository = usuario;
        }
        public async Task AlterarUsuario(AlterarUsuarioDto dto,Guid idUsuario)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(idUsuario);
            if (usuario == null)
            {
                throw new DomainException("Usuário não encontrado");
            }
            var usuarioExistente = await _usuarioRepository.ObterPorEmailAsync(dto.email);

            if (usuarioExistente != null)
            {
                throw new DomainException("Email já cadastrado");
            }

            usuario.AlterarUsuario(dto.nome, dto.email);


            await _usuarioRepository.UpdateAsync(usuario);
        }

        public async Task<UsuarioAdmin> IsAdmin(Guid getId)
        {
            var u = await _usuarioRepository.ObterPorIdAsync(getId);
            if (u == null)
            {
                throw new DomainException("Usuário não encontrado");
            }
            return new UsuarioAdmin(u.Admin );
        }
    }
}
