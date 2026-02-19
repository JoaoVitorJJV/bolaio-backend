using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IBolaoRepository
    {
        public  Task AdicionarAsync(Bolao bolao);
        Task<List<Times>> GetTimes();
        Task<Partida?> ObterPartidaPorIdAsync(Guid partidaId);
        Task<Bolao?> ObterPorIdAsync(Guid bolaoId);
        Task SaveChangesAsync();
    }
}
