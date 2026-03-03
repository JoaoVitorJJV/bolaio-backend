using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using UFRA.Bolaio.API.Data;
using static Application.DTOs.BolaoDto;
using static Application.DTOs.PalpitesDto;
using static Application.DTOs.UsuarioDtos;

namespace Infrastructure.Services
{
    public interface IBolaoQueries
    {
        Task<List<ListarBoloesDto>> ListarBoloes();
        Task<List<GetExtratoResponseDto>> GetExtrato(Guid idusuario);
        Task<ListarBoloesDto> GetBolaoByIdNoTrackAsync(string id);
        Task<List<PalpitesAtivosDto>> ListarPalpitesPorUsuarioAsync(Guid guid);
        Task<List<string>> GetLogs();
    }
}
