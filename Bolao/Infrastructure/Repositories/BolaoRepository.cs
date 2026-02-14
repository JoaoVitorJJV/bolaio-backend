using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UFRA.Bolaio.API.Data;
using static Application.DTOs.BolaoDto;

namespace Infrastructure.Repositories
{
    public class BolaoRepository : IBolaoRepository
    {
        private readonly AppDbContext _appDbContext;
        public BolaoRepository(AppDbContext context)
        {
            _appDbContext = context;
        }
        public async Task AdicionarAsync(Bolao bolao)
        {
            await _appDbContext.Boloes.AddAsync(bolao);
        }

        //public async Task<List<ListarBoloesDto>> ListarBoloes()
        //{
        //    var resultado = await _appDbContext.Boloes
        //                                        .Where(x => x.Visibilidade == Domain.Enums.TipoVisibilidade.Publico)
        //                                        .Select(b => new ListarBoloesDto(
        //                                            b.Id,
        //                                            b.Nome,
        //                                            b.Visibilidade,
        //                                            b.Valor,
        //                                            b.DataFechamento,
        //                                            b.TipoBolao,
        //                                            b.Organizador.Nome,
        //                                            b.Palpites
        //                                                .Select(p => p.Participante.Id) // Ou p.UsuarioId
        //                                                .Distinct()
        //                                                .Count().ToString(),
        //                                            b.Premio,
        //                                            b.Partida.TimeA.Nome,
        //        b.Partida.TimeB.Nome
        //                                                )
        //                                        )
        //                                        .AsNoTracking()
        //                                        .ToListAsync();
        //    return resultado;
        //    //return await _appDbContext.Boloes
        //    //    .Include(x=>x.Organizador).Include(p=>p.Palpites.Select(u=>u.Participante.Id).Distinct().Count())
        //    //    .Where(x=>x.Visibilidade == Domain.Enums.TipoVisibilidade.Publico)
        //    //    .AsNoTracking()
        //    //    .ToListAsync();
        //}

        public async Task<Partida?> ObterPartidaPorIdAsync(Guid partidaId)
        {
            return await _appDbContext.Partidas.FirstOrDefaultAsync(x => x.Id == partidaId);
        }

        public async Task<Bolao?> ObterPorIdAsync(Guid bolaoId)
        {
            return await _appDbContext.Boloes.Include(b=>b.Palpites).ThenInclude(p=>p.Participante).FirstOrDefaultAsync(x => x.Id == bolaoId);
        }

        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
