using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class CarteiraAppService : ICarteiraService
    {
        private readonly ICarteiraRepository _carteiraRepository;
        public CarteiraAppService(ICarteiraRepository carteira)
        {
            _carteiraRepository = carteira;
        }
        public async Task<decimal> DepositarAsync(Guid usuarioId, decimal valor)
        {
            var carteira = await _carteiraRepository.GetCarteiraByUsuarioIdAsync(usuarioId);
            
            if (carteira is null)
                throw new DomainException("Carteira não encontrada.");
            
            carteira.Depositar(valor);

            await _carteiraRepository.UpdateAsync(carteira);

            return carteira.SaldoAtual;
        }

        public async Task<decimal> ObterSaldoAsync(Guid usuarioId)
        {
            var saldo = await _carteiraRepository.GetCarteiraByUsuarioIdAsync(usuarioId);
            return saldo.SaldoAtual;
        }
    }
}
