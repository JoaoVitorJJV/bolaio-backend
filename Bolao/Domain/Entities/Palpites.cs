using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Palpites
    {
        public Guid Id { get; private set; }
        public Usuario Participante { get; private set; }
        public Guid TransacaoId { get; private set; }
        public Transacao? Transacao { get; private set; }
        public Bolao Bolao { get; private set; }       
        public int QtdCotas { get; private set; } = 1;
        public DateTime DataCriacao { get; private set; } = DateTime.Now;
        public StatusPalpite StatusPalpite { get; private set; } = StatusPalpite.Pendente;
        public int PalpiteGolsA { get; private set; }
        public int PalpiteGolsB { get; set; }
        public Palpites()
        {
            
        }
        public Palpites(Usuario usuario,Bolao bolao, Transacao transacao,int golsA, int golsB)
        {
            Participante = usuario;
            Bolao = bolao;
            Transacao = transacao;
            PalpiteGolsA = golsA;
            PalpiteGolsB = golsB;

        }
        public Palpites(Usuario usuario, Bolao bolao, int qtdCotas,Transacao transacao, int golsA, int golsB)
        {
            Participante = usuario;
            Bolao = bolao;
            Transacao = transacao;
            QtdCotas = qtdCotas;
            PalpiteGolsA = golsA;
            PalpiteGolsB = golsB;
        }

        public void AtualizarStatus(StatusPalpite status)
        {
            if (this.Bolao.Status != StatusBolao.Concluido)
            {
                throw new DomainException("Não é possível atualizar o status do palpite, pois o bolão ainda não foi concluído.");
            }
            StatusPalpite = status;
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