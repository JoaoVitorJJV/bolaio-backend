using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using static Application.DTOs.CarteiraDto;
using static Application.DTOs.UsuarioDtos;

namespace Application.Services
{
    public class CarteiraAppService : ICarteiraService
    {
        private readonly ICarteiraRepository _carteiraRepository;
        private readonly IBolaoRepository _bolaoRepository;
        public CarteiraAppService(ICarteiraRepository carteira, IBolaoRepository bolaoRepository)
        {
            _carteiraRepository = carteira;
            _bolaoRepository = bolaoRepository;
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

        public async Task<decimal> GetInvestidoEmJogos(Guid guid)
        {
            var palpites = await _bolaoRepository.GetPalpitesAtivosByUsuarioIdAsync(guid);
            decimal totalInvestido = 0;
            foreach (var p in palpites)
            {
                totalInvestido += p.Bolao.Valor * p.QtdCotas;
            }
            return totalInvestido;

        }

        public async Task<string> GetLucroPotencial(Guid guid)
        {
            //var palpites = await _bolaoRepository.GetPalpitesAtivosByUsuarioIdAsync(guid);
            throw new NotImplementedException();
        }

        public async Task<string> GetTaxaAcerto(Guid guid)
        {
            var palpites = await _bolaoRepository.GetPalpitesConcluidosByUsuarioIdAsync(guid);
            float taxaAcerto = 0;
            if (palpites.Count > 0)
            {
                int acertos = 0;
                foreach (var p in palpites)
                {
                    if (p.StatusPalpite == StatusPalpite.Vencedor)
                        acertos++;
                }
                taxaAcerto = ((float)acertos / palpites.Count) * 100;
            }

            return taxaAcerto.ToString();
        }

        public Task<GetLimitesResponseDto> ObterLimitesAsync(Guid guid)
        {
            GetLimitesResponseDto limites = new GetLimitesResponseDto(50, 200);
            return Task.FromResult(limites);
        }

        public async Task<decimal> ObterSaldoAsync(Guid usuarioId)
        {
            var saldo = await _carteiraRepository.GetCarteiraByUsuarioIdAsync(usuarioId);
            return saldo.SaldoAtual;
        }

        public async Task<decimal> Sacar(SacarDto valor, Guid usuarioId)
        {
            var carteira = await _carteiraRepository.GetCarteiraByUsuarioIdAsync(usuarioId);
            if (carteira is null)
                throw new DomainException("Carteira não encontrada.");
            carteira.Sacar(valor.valor);

            await _carteiraRepository.UpdateAsync(carteira);

            return carteira.SaldoAtual;
        }
    }
}
