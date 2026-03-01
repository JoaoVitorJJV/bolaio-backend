using System;
using System.Collections.Generic;
using System.Text;
using static Application.DTOs.CarteiraDto;
using static Application.DTOs.UsuarioDtos;

namespace Application.Interfaces
{
    public interface ICarteiraService
    {
        Task<decimal> DepositarAsync(Guid usuarioId, decimal valor);
        Task<decimal> GetInvestidoEmJogos(Guid guid);
        Task<string>GetLucroPotencial(Guid guid);
        Task<string> GetTaxaAcerto(Guid guid);
        Task<GetLimitesResponseDto> ObterLimitesAsync(Guid guid);
        Task<decimal> ObterSaldoAsync(Guid usuarioId);
        Task<decimal> Sacar(SacarDto v, Guid guid);
    }
}
