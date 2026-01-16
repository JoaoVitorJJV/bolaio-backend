using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface ICarteiraRepository
    {
        Task<Carteira?> GetCarteiraByUsuarioIdAsync(Guid usuarioId);
        Task UpdateAsync(Carteira carteira);
    }
}
