using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using static Application.DTOs.EventosDto;

namespace Application.Interfaces
{
    public interface IAdminService
    {
        Task AtualizaTimes();
        Task CriarPartida(BolaoDto.CriarPartidaDto criarPartidaDto);
    }
}
