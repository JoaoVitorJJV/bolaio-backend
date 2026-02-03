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
        public DateTime DataPartida { get; private set; }
        public string ResultadoTimeA { get; private set; }
        public string ResultadoTimeB { get; private set; }

        public Partida()
        {
        }
        public Partida(Times timeA, Times timeB,DateTime dataPartida)
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
    }
}
