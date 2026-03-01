using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IBolaoRepository
    {
        public  Task AdicionarAsync(Bolao bolao);
        Task<List<Partida>> GetPartidas();
        Task<List<Times>> GetTimes();
        Task<Partida?> ObterPartidaPorIdAsync(Guid partidaId);
        Task<Bolao?> ObterPorIdAsync(Guid bolaoId);
        Task SaveChangesAsync();
        Task<Times> GetTimesByIdAsync(string id);
        Task<List<Palpites>> GetPalpitesAtivosByUsuarioIdAsync(Guid guid);
        Task<List<Palpites>> GetPalpitesConcluidosByUsuarioIdAsync(Guid guid);        
        Task<Bolao> ObterPorIdNoTrackAsync(string bolaoId);
    }
}
