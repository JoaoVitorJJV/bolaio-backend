using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IAdminRepository
    {
        Task CriarPartida(Partida novaPartida);
        public Task NovoTime(Times time);
        public List<Times> ObterTimes();

    }
}
