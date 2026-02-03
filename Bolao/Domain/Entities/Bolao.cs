using Domain.Enums;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Bolao
    {
        public Guid Id { get; private set; }        
        public Usuario Organizador { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataCriacao { get; private set; } = DateTime.Now;
        public DateTime DataFechamento { get; private set; }
        public TipoVisibilidade Visibilidade { get; private set; }
        public List<Palpites>? Palpites { get; set; } = new List<Palpites>();
        public TipoEsporte TipoEsporte { get; set; } = TipoEsporte.Futebol;
        public bool Ativo { get; private set; } = true;
        public decimal Valor { get; private set; } = 0.00M;// tem que definir 
        public TipoBolao TipoBolao { get; private set; }
        public int qtdParticipantes { get; private set; } = 0;
        public string Premio { get; private set; } = "0";
        public decimal TaxaAdministrativa { get; private set; } = 0.05M; // 5% de taxa
        public Partida Partida { get; private set; }
        public int MaxParticipantes { get; private set; } = 1;

        public Bolao()
        {
            
        }

        public Bolao(Usuario organizador, string nome, TipoVisibilidade visibilidade, decimal valor, DateTime dtFechamento, TipoBolao tipoBolao, int maxParticipantes, Partida partida)
        {
            Id = Guid.NewGuid();
            Organizador = organizador;
            Nome = nome;
            Visibilidade = visibilidade;
            Valor = valor;
            DataFechamento = dtFechamento;
            TipoBolao = tipoBolao;
            Partida = partida;
        }

        public void AdicionarPalpite(Palpites palpite)
        {
            if (this.Ativo && this.DataFechamento > DateTime.Now)
            {
                if (this.Palpites.Any(p=>p.Participante.Id == palpite.Participante.Id))
                {
                    throw new DomainException("Você já está participando deste bolão!.");
                }
                Palpites?.Add(palpite);
                this.qtdParticipantes++;
                CalcularPremioTotal();
            }
            else
            {
                throw new DomainException("Não é possível adicionar palpites a um bolão fechado ou inativo.");
            }
        }

        public void RemoverPalpite(Palpites palpite)
        {
            if (this.Ativo && DateTime.Now < this.DataFechamento)
            {
                Palpites?.Remove(palpite);
                this.qtdParticipantes--;
                CalcularPremioTotal();
            }
            else
            {
                throw new DomainException("Não é possível sair de um bolão fechado ou inativo.");
            }
        }

        public decimal CalcularPremioTotal()
        {
            Premio = Palpites != null ? (Palpites.Count * Valor).ToString("C2") : 0.00M.ToString("C2");
            return Premio != null ? decimal.Parse(Premio, System.Globalization.NumberStyles.Currency) : 0.00M;
        }

        public void FecharBolao()
        {
            Ativo = false;
        }

        public int ObterNumeroDeParticipantes()
        {
            return Palpites != null ? Palpites.Count : 0;
        }

        public void NotificarParticipantes(string mensagem)
        {
           throw new DomainException("Falta implementar");
        }
    }
}
