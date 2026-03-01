using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Text;
using static Application.DTOs.BolaoDto;
using static Application.DTOs.PalpitesDto;

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



        public async Task<ListarBoloesDto> GetBolaoByIdNoTrackAsync(string id)
        {
            var bolao = await _bolaoRepository.ObterPorIdNoTrackAsync(id);
            if (bolao == null)
            {
                throw new DomainException("Bolão não encontrado");
            }
            ListarBoloesDto dto = new ListarBoloesDto(
                bolao.Id,
                bolao.Nome,
                bolao.Visibilidade,
                bolao.Valor,
                bolao.DataFechamento,
                bolao.TipoBolao,
                bolao.Organizador.Nome,
                bolao.Palpites.Select(p => p.Participante.Id).Distinct().Count().ToString(),
                bolao.Premio,
                bolao.Partida.TimeA.Nome,
                bolao.Partida.TimeB.Nome,
                bolao.Partida.ResultadoTimeA + " - " + bolao.Partida.ResultadoTimeB,
                bolao.MaxParticipantes.ToString(),
                bolao.Partida.Id.ToString()
            );

            return dto;

        }

        public async Task<List<GetTimesDto>> GetTimes()
        {
            var res = await _bolaoRepository.GetTimes();
            return res.Select(x=>new GetTimesDto(x.Id.ToString(),x.Nome,x.BandeiraUrl)).ToList();
        }


        public async Task<List<GetPartidasDto>> ListarPartidas()
        {
            var res = await _bolaoRepository.GetPartidas();            
            return res.Select(r => new GetPartidasDto(r.Id.ToString(),r.DataPartida,r.TimeA.Id.ToString(), r.TimeA.Nome,r.TimeB.Id.ToString(),r.TimeB.Nome,r.TimeA.BandeiraUrl,r.TimeB.BandeiraUrl)).ToList();
        }

        public List<TiposBolao> ObterTiposBolao()
        {
            return Enum.GetValues(typeof(TipoBolao))
                .Cast<TipoBolao>()
                .Select( tb=> new TiposBolao((int)tb, tb.ToString())).ToList();
        }

        public List<VisibilidadeDto> ObterVisibilidadeBolao()
        {
            return Enum.GetValues(typeof(TipoVisibilidade))
                .Cast<TipoVisibilidade>()
                .Select(tb => new VisibilidadeDto((int)tb, tb.ToString())).ToList();
        }

        public Task ProcessarPontuacaoPartidaAsync(Guid partidaId, int golsMandante, int golsVisitante)
        {
            throw new NotImplementedException();
        }

        public async Task RegistrarPalpiteAsync(RegistrarPalpiteDto dto, Guid idUsuario)
        {
            var bolao = await _bolaoRepository.ObterPorIdAsync(dto.BolaoId);
            if (bolao == null)
            {
                throw new DomainException("Bolão não encontrado");
            }

            var usuario = await _usuarioRepository.ObterPorIdAsync(idUsuario);

            var debito = usuario.Carteira.Debitar(bolao.Valor,true);
            Palpites palpite = new Palpites(usuario,bolao,debito,dto.GolsTimeA,dto.GolsTimeB);
            bolao.AdicionarPalpite(palpite);
            await _bolaoRepository.SaveChangesAsync();
        }
    }
}
