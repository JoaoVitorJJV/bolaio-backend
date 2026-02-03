using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using static Application.DTOs.BolaoDto;

namespace Application.Services
{
    public class BolaoService : IBolaoService
    {
        private readonly IBolaoRepository _bolaoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        public BolaoService(IBolaoRepository bolaoRepository, IUsuarioRepository usuarioRepository)
        {
            _bolaoRepository = bolaoRepository;
            _usuarioRepository = usuarioRepository;
        }
        public async Task<CriarBolaoResponseDto> CriarBolaoAsync(CriarBolaoDto Dto)
        {
            if (Dto.Organizador == null)
            {
                throw new DomainException("Usuário inválido");
            }
            var partida = await _bolaoRepository.ObterPartidaPorIdAsync(Dto.partidaId);
            if (partida == null)
            {
                throw new DomainException("Partida não encontrada");
            }

            if (partida.DataPartida <= DateTime.Now)
            {
                throw new DomainException("Não é possível criar um bolão para uma partida já iniciada ou encerrada");
            }


            Bolao bolao = new Bolao(Dto.Organizador, Dto.Nome, Dto.Visibilidade, Dto.Valor, Dto.DtFechamento, Dto.TipoBolao,Dto.maxParticipantes, partida);
            await _bolaoRepository.AdicionarAsync(bolao);
            await _bolaoRepository.SaveChangesAsync();
            return new CriarBolaoResponseDto(bolao.Nome, bolao.DataFechamento, Dto.Organizador.Nome);
        }

        public async Task RegistrarPalpiteAsync(RegistrarPalpiteDto dto, Guid idUsuario)
        {
            var bolao = await _bolaoRepository.ObterPorIdAsync(dto.BolaoId);
            if (bolao == null)
            {
                throw new DomainException("Bolão não encontrado");
            }

            var usuario = await _usuarioRepository.ObterPorIdAsync(idUsuario);

            var debito = usuario.Carteira.Debitar(bolao.Valor);
            Palpites palpite = new Palpites(usuario,bolao,debito);
            bolao.AdicionarPalpite(palpite);
            await _bolaoRepository.SaveChangesAsync();
        }
    }
}
