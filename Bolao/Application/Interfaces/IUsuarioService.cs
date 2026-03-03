using System;
using System.Collections.Generic;
using System.Text;
using static Application.DTOs.UsuarioDtos;

namespace Application.Interfaces
{
    public interface IUsuarioService
    {
        Task AlterarUsuario(AlterarUsuarioDto dto,Guid idUsuario);
        Task<UsuarioAdmin> IsAdmin(Guid getId);
    }
}
