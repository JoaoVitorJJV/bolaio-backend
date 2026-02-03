using Domain.Exceptions;

namespace Domain.Entities
{
    public class Carteira
    {
        public Guid Id { get; set; }
        public decimal SaldoAtual { get; private set; } = 0;
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public List<Transacao> Transacoes { get; set; } = new();

        public void Depositar(decimal valor)
        {
            if (valor <= 0) throw new DomainException("Valor deve ser maior que zero");
            SaldoAtual += valor;
            RegistrarTransacao(new Transacao { Valor = valor, Tipo = Enums.TipoTransacao.Deposito });
        }

        public void Sacar(decimal valor)
        {
            if (valor > SaldoAtual) throw new DomainException("Saldo insuficiente");
            SaldoAtual -= valor;
            RegistrarTransacao(new Transacao { Valor = -valor, Tipo = Enums.TipoTransacao.Saque });
        }

        public Transacao Debitar(decimal valor)
        {
            if (valor <= 0) throw new DomainException("Valor deve ser maior que zero");
            if (valor > SaldoAtual) throw new DomainException("Saldo insuficiente");
            SaldoAtual -= valor;           
            return RegistrarTransacao(new Transacao { Valor = -valor, Tipo = Enums.TipoTransacao.Debito });
        }

        public Transacao RegistrarTransacao(Transacao transacao)
        {
            Transacoes.Add(transacao);
            return transacao;
        }
    }
}