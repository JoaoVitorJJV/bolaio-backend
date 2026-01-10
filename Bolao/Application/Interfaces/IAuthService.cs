using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        void CriarPasswordHash(string senha, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerificaLogin(string senha, byte[] passwordHash, byte[] passwordSalt);
        string CreateToken(Usuario usuario);
    }
}
