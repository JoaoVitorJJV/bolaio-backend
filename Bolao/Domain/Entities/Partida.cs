using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Partida
    {
        public Guid Id { get; private set; }
        public Times TimeA { get; private set; }
        public Times TimeB { get; private set; }
        public DateTimeOffset DataPartida { get; private set; }
        public string ResultadoTimeA { get; private set; } = "0";
        public string ResultadoTimeB { get; private set; } = "0";
        public StatusPartida StatusPartida { get; private set; } = StatusPartida.Agendada;

        public Partida()
        {
        }
        public Partida(Times timeA, Times timeB,DateTimeOffset dataPartida)
        {
            TimeA = timeA;
            TimeB = timeB;
            DataPartida = dataPartida;
        }

        public void RegistrarResultado(string resultadoA, string resultadoB)
        {
            ResultadoTimeA = resultadoA;
            ResultadoTimeB = resultadoB;
        }

        public void AtualizarStatus(StatusPartida statusPartida)
        {
            this.StatusPartida = statusPartida;
        }
    }
}
