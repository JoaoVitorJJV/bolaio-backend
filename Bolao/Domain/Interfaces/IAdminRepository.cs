using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IAdminRepository
    {
        Task AtualizarPartida(Partida partida);
        Task CriarPartida(Partida novaPartida);
        Task<Partida> GetPartidaByIdAsync(Guid idPartida);
        Task<List<Usuario>> ListarUsuarios();
        public Task NovoTime(Times time);
        public List<Times> ObterTimes();
        Task UsuarioAtivo(Guid idUsuario,bool ativo);
    }
}
