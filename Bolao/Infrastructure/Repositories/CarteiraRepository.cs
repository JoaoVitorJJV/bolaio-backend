using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UFRA.Bolaio.API.Data;

namespace Infrastructure.Repositories
{
    public class CarteiraRepository : ICarteiraRepository
    {
        private readonly AppDbContext _context;
        public CarteiraRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Carteira?> GetCarteiraByUsuarioIdAsync(Guid usuarioId)
        {
            Carteira? carteira = await _context.Carteiras.FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);
            
            return carteira;
        }

        public async Task UpdateAsync(Carteira carteira)
        {
            _context.Carteiras.Update(carteira);            
            await _context.SaveChangesAsync();
        }
    }
}
