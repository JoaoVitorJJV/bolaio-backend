using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ICarteiraService
    {
        Task<decimal> DepositarAsync(Guid usuarioId, decimal valor);
        Task<decimal> ObterSaldoAsync(Guid usuarioId);
    }
}
