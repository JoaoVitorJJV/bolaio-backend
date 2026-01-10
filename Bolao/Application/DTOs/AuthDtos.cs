using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record LoginRequestDto(string Email, string Senha);

    public record LoginResponseDto(string Token, string Nome, string Email);
    public record RegisterRequestDto(string Nome, string Email, string Senha);
    public record RegisterResponseDto(Guid Id, string Nome, string Email);
}
