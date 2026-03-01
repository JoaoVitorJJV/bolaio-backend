using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Infrastructure.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using UFRA.Bolaio.API.Data;
using static Application.DTOs.BolaoDto;
using static Application.DTOs.PalpitesDto;

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

        public async Task<List<Partida>> GetPartidas()
        {
            return await _appDbContext.Partidas.Include(x=> x.TimeA).Include(x=>x.TimeB).AsNoTracking().Where(p=>p.DataPartida >= DateTime.UtcNow).ToListAsync();
        }

        public async Task<Times> GetTimesByIdAsync(string id)
        {
            return await _appDbContext.Times.FirstOrDefaultAsync(x => x.Id.ToString() == id);
        }

        public async Task<List<Times>> GetTimes()
        {
            return await _appDbContext.Times.AsNoTracking().ToListAsync();
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
            return await _appDbContext.Boloes.Include(b=>b.Partida).Include(b=>b.Palpites).ThenInclude(p=>p.Participante).FirstOrDefaultAsync(x => x.Id == bolaoId);
        }

        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<Palpites>> GetPalpitesAtivosByUsuarioIdAsync(Guid guid)
        {
            return await _appDbContext.Palpites
                .Include(p => p.Bolao)
                .Where(p => p.Participante.Id == guid && p.Bolao.Status == StatusBolao.Aberto).AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Palpites>> GetPalpitesConcluidosByUsuarioIdAsync(Guid guid)
        {
            return await _appDbContext.Palpites
                .Include(p => p.Bolao)
                .Where(p => p.Participante.Id == guid && p.Bolao.Status == StatusBolao.Concluido).AsNoTracking()
                .ToListAsync();
        }

        

        public async Task<Bolao> ObterPorIdNoTrackAsync(string bolaoId)
        {
            //if (!Guid.TryParse(bolaoId, out var idGuid))
            //{
            //    return null; // Ou lance uma exceção de ID inválido
            //}

            //// 2. Use a variável Guid na consulta
            //return await _appDbContext.Boloes
            //    .Include(b => b.Partida)
            //    .ThenInclude(i=> i.TimeA)
            //    .Include(b => b.Partida)
            //    .Include(b => b.Palpites)
            //    .ThenInclude(p => p.Participante)
            //    .Include(x => x.Organizador)                
            //    .AsNoTracking()
            //    .FirstOrDefaultAsync(x => x.Id == idGuid);
            throw new NotImplementedException();
        }
    }
}
