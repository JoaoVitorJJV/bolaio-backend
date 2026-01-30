using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UFRA.Bolaio.API.Data;

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

        public async Task<List<Bolao>> ListarBoloes()
        {
            return await _appDbContext.Boloes
                .Include(x=>x.Organizador)
                .Where(x=>x.Visibilidade == Domain.Enums.TipoVisibilidade.Publico)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Bolao?> ObterPorIdAsync(Guid bolaoId)
        {
            return await _appDbContext.Boloes.FirstOrDefaultAsync(x => x.Id == bolaoId);
        }

        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
