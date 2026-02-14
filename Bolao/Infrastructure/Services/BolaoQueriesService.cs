using Application.DTOs;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using UFRA.Bolaio.API.Data;
using static Application.DTOs.BolaoDto;
using static Application.DTOs.UsuarioDtos;

namespace Infrastructure.Services
{
    public class BolaoQueriesService : IBolaoQueries
    {
        private readonly AppDbContext _appDbContext;
        public BolaoQueriesService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<GetExtratoResponseDto>> GetExtrato(Guid id)
        {            
            var transacoes = await _appDbContext.Transacoes
                .Where(w=>w.Carteira.UsuarioId == id)
                .Select(
                        s => new GetExtratoResponseDto(
                            s.DataHora.ToString(),
                            s.Bolao != null? $"({s.Tipo.ToString()}) "+s.Bolao.Partida.TimeA.Sigla + " x " + s.Bolao.Partida.TimeB.Sigla: $"{s.Tipo.ToString()}",
                            s.Status.ToString(),
                            s.Valor)
                     ).AsNoTracking().ToListAsync();
            
            return transacoes;
        }

        public async Task<List<ListarBoloesDto>> ListarBoloes()
        {
            return await _appDbContext.Boloes
            .Where(x => x.Visibilidade == TipoVisibilidade.Publico)
            .Select(b => new ListarBoloesDto(
                b.Id,
                b.Nome,
                b.Visibilidade,
                b.Valor,
                b.DataFechamento,
                b.TipoBolao,
                b.Organizador.Nome,
                b.Palpites.Select(p => p.Participante.Id).Distinct().Count().ToString(),
                b.Premio,
                b.Partida.TimeA.Nome,
                b.Partida.TimeB.Nome,
                b.Partida.ResultadoTimeA + " - " + b.Partida.ResultadoTimeB
                ))
            .AsNoTracking()
            .ToListAsync();
        }
    }
}
