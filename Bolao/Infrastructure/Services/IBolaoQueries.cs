using System;
using System.Collections.Generic;
using System.Text;
using static Application.DTOs.BolaoDto;
using static Application.DTOs.UsuarioDtos;

namespace Infrastructure.Services
{
    public interface IBolaoQueries
    {
        Task<List<ListarBoloesDto>> ListarBoloes();
        Task<List<GetExtratoResponseDto>> GetExtrato(Guid idusuario);

    }
}
