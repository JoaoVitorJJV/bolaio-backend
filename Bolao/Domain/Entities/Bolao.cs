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
        public int qtdParticipantes { get; private set; }


        public Bolao()
        {
            
        }

        public Bolao(Usuario organizador, string nome, TipoVisibilidade visibilidade, decimal valor, DateTime dtFechamento, TipoBolao tipoBolao, int qtdParticipantes)
        {
            Id = Guid.NewGuid();
            Organizador = organizador;
            Nome = nome;
            Visibilidade = visibilidade;
            Valor = valor;
            DataFechamento = dtFechamento;
            TipoBolao = tipoBolao;
            this.qtdParticipantes = qtdParticipantes;
        }

        public void AdicionarPalpite(Palpites palpite)
        {
            if (this.Ativo && this.DataFechamento > DateTime.Now)
            {
                Palpites?.Add(palpite);
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
            }
            else
            {
                throw new DomainException("Não é possível remover palpites de um bolão fechado ou inativo.");
            }
        }

        public decimal CalcularPremioTotal()
        {
            return Palpites != null ? Palpites.Count * Valor : 0.00M;
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
