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
            if (valor <= 0) throw new Exception("Valor deve ser maior que zero");
            SaldoAtual += valor;
            RegistrarTransacao(new Transacao { Valor = valor, Tipo = Enums.TipoTransacao.Deposito });
        }

        public void Sacar(decimal valor)
        {
            if (valor > SaldoAtual) throw new Exception("Saldo insuficiente");
            SaldoAtual -= valor;
            RegistrarTransacao(new Transacao { Valor = -valor, Tipo = Enums.TipoTransacao.Saque });
        }

        private void RegistrarTransacao(Transacao transacao)
        {
            Transacoes.Add(transacao);
        }
    }
}