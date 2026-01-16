using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Transacao
    {
        public Guid Id { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataHora { get; set; } = DateTime.UtcNow;
        public TipoTransacao Tipo { get; set; }

        public int CarteiraId { get; set; }
        public Carteira Carteira { get; set; } = null!;
    }
}
