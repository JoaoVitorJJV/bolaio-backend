using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UFRA.Bolaio.API.Data;

namespace Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        public readonly AppDbContext _context;
        public AdminRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AtualizarPartida(Partida partida)
        {
            await _context.Partidas.ExecuteUpdateAsync(p => p.SetProperty(p => p.StatusPartida, partida.StatusPartida));
        }

        public async Task CriarPartida(Partida novaPartida)
        {
            await _context.Partidas.AddAsync(novaPartida);
            await _context.SaveChangesAsync();
        }

        public async Task<Partida> GetPartidaByIdAsync(Guid idPartida)
        {
            return await _context.Partidas.FindAsync(idPartida);
        }

        public async Task<List<Usuario>> ListarUsuarios()
        {
            return await _context.usuarios
                .Include(u => u.Carteira).AsNoTracking()
                .ToListAsync();
        }

        public async Task NovoTime(Times time)
        {
            if (!_context.Times.Any(x => x.CodigoExterno == time.CodigoExterno))
            {
                _context.Times.Add(time);
                await _context.SaveChangesAsync();
            }
            
            
        }

        public List<Times> ObterTimes()
        {
            throw new NotImplementedException();    
        }

        public async Task UsuarioAtivo(Guid idUsuario,bool ativo)
        {
            var user = await _context.usuarios.FirstOrDefaultAsync(u=>u.Id == idUsuario);
            user.Ativar(ativo);
            await _context.SaveChangesAsync();
        }
    }
}
