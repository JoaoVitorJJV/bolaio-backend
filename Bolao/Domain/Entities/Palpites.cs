using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Palpites
    {
        public Guid Id { get; private set; }
        public Usuario Participante { get; private set; }
        public Bolao Bolao { get; private set; }       
        public int QtdCotas { get; private set; } = 1;
        public DateTime DataCriacao { get; private set; } = DateTime.Now;
        public Palpites()
        {
            
        }
        public Palpites(Usuario usuario,Bolao bolao)
        {
            Participante = usuario;
            Bolao = bolao;
        }
        public Palpites(Usuario usuario, Bolao bolao, int qtdCotas)
        {
            Participante = usuario;
            Bolao = bolao;
        }

        public void AdicionarCotas(int quantidade)
        {
            if (quantidade <= 0)
            {
                throw new DomainException("A quantidade de cotas deve ser maior que zero.");
            }
            QtdCotas += quantidade;
        }

        public void RemoverCotas(int quantidade)
        {
            if (quantidade <= 0)
            {
                throw new DomainException("A quantidade de cotas deve ser maior que zero.");
            }
            if (QtdCotas - quantidade < 1)
            {
                throw new DomainException("O participante deve ter pelo menos uma cota.");
            }
            QtdCotas -= quantidade;
        }

    }
}