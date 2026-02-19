using Domain.Entities;
using Domain.Interfaces;
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
    }
}
