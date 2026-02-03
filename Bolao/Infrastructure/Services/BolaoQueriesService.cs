using Application.DTOs;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UFRA.Bolaio.API.Data;
using static Application.DTOs.BolaoDto;

namespace Infrastructure.Services
{
    public class BolaoQueriesService : IBolaoQueries
    {
        private readonly AppDbContext _appDbContext;
        public BolaoQueriesService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
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
                b.Premio
                ))
            .AsNoTracking()
            .ToListAsync();
        }
    }
}
