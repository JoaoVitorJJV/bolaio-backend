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

        public Guid CarteiraId { get; set; }
        public Carteira Carteira { get; set; } = null!;

        public Guid? BolaoId { get; private set; }
        public Bolao? Bolao { get;  private set; }
        public StatusTransacao? Status { get; private set; } = StatusTransacao.Processando;

        public string? ExternalReference { get; set; } // ID do Mercado Pago/Stripe
        public string? QrCode { get; set; }           // Para o usuário pagar       

        public void Finalizar() => Status = StatusTransacao.Concluido;

        public Transacao()
        {
            
        }
        public Transacao(Bolao bolao)
        {
            this.Bolao = bolao;
        }


    }
}
