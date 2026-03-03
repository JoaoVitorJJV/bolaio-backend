using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class UsuarioDtos
    {
        public record DepositoDto(decimal Valor);
        public record GetExtratoResponseDto(string data,string descricao,string status,decimal valor);
        public record GetLimitesResponseDto(decimal usado, decimal limite);
        public record DepositoRealizado(Guid TransacaoId, decimal Valor, string UsuarioId);
        public record AlterarUsuarioDto(string nome,string email);
        public record UsuarioAtivoDto(Guid idUsuario, bool ativo);
        public record ListarUsuariosDto(Guid Id, string Nome, string Email, bool Ativo,string idCarteira);
        public record UsuarioAdmin(bool admin);
    }
}
