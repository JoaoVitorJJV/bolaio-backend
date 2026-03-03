using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using static Application.DTOs.BolaoDto;
using static Application.DTOs.EventosDto;
using static Application.DTOs.UsuarioDtos;

namespace Application.Interfaces
{
    public interface IAdminService
    {
        Task StatusDaPartida(StatusDaPartidaDto status);
        Task AtualizaTimes();
        Task CriarPartida(BolaoDto.CriarPartidaDto criarPartidaDto);
        Task EstornarPalpites();
        Task UsuarioAtivo(UsuarioAtivoDto dto);
        Task<List<ListarUsuariosDto>> ListarUsuarios();
    }
}
