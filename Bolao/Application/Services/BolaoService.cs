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

            Bolao bolao = new Bolao(Dto.Organizador, Dto.Nome, Dto.Visibilidade, Dto.Valor, Dto.DtFechamento, Dto.TipoBolao,Dto.qtdParticipantes);
            await _bolaoRepository.AdicionarAsync(bolao);
            await _bolaoRepository.SaveChangesAsync();
            return new CriarBolaoResponseDto(bolao.Nome, bolao.DataFechamento, Dto.Organizador.Nome);
        }

        public async Task<List<ListarBoloesDto>> ListarBoloes()
        {
            List<Bolao> boloes = await _bolaoRepository.ListarBoloes();
            return boloes.Select(b => new ListarBoloesDto(b.Id,b.Nome,b.Visibilidade,b.Valor,b.DataFechamento,b.TipoBolao,b.Organizador.Id.ToString())).ToList();
        }

        public async Task RegistrarPalpiteAsync(RegistrarPalpiteDto dto, Guid idUsuario)
        {
            var bolao = await _bolaoRepository.ObterPorIdAsync(dto.BolaoId);
            if (bolao == null)
            {
                throw new DomainException("Bolão não encontrado");
            }

            var usuario = await _usuarioRepository.ObterPorIdAsync(idUsuario);

            Palpites palpite = new Palpites(usuario,bolao);


            bolao.AdicionarPalpite(palpite);
            await _bolaoRepository.SaveChangesAsync();
        }
    }
}
