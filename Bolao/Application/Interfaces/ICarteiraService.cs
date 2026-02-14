using System;
using System.Collections.Generic;
using System.Text;
using static Application.DTOs.UsuarioDtos;

namespace Application.Interfaces
{
    public interface ICarteiraService
    {
        Task<decimal> DepositarAsync(Guid usuarioId, decimal valor);
        Task<GetLimitesResponseDto> ObterLimitesAsync(Guid guid);
        Task<decimal> ObterSaldoAsync(Guid usuarioId);
    }
}
