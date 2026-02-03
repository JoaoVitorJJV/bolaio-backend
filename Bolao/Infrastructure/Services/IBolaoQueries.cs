using System;
using System.Collections.Generic;
using System.Text;
using static Application.DTOs.BolaoDto;

namespace Infrastructure.Services
{
    public interface IBolaoQueries
    {
        Task<List<ListarBoloesDto>> ListarBoloes();

    }
}
