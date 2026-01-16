using Application.Interfaces;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Application.DTOs.UsuarioDtos;

namespace UFRA.Bolao.API.Endpoints
{
    public static class CarteiraEndpoints
    {
        public static void MapCarteiraEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/carteira")
             .RequireAuthorization() 
             .WithTags("Carteira")
             .WithSummary("Gerencia saldo e saque da carteira do usuário").WithDescription("Endpoints para gerenciamento da carteira do usuário.");

            group.MapGet("/saldo", GetSaldo);
            group.MapPost("/depositar", Depositar);
        }

       
        private static async Task<IResult> GetSaldo([FromServices] ICarteiraService service,ClaimsPrincipal user)
        {
            try
            {
                var saldo = await service.ObterSaldoAsync(GetUserId(user));
                return Results.Ok(new { Saldo = saldo });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { Erro = ex.Message });
            }
        }

        private static async Task<IResult> Depositar([FromBody] DepositoDto dto,[FromServices] ICarteiraService service,ClaimsPrincipal user)
        {
            try
            {
                var usuarioId = GetUserId(user);
                var novoSaldo = await service.DepositarAsync(usuarioId, dto.Valor);
                return Results.Ok(new { Mensagem = "Depósito realizado!", NovoSaldo = novoSaldo });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { Erro = ex.Message });
            }
        }
        private static Guid GetUserId(ClaimsPrincipal user)
        {            
            var idString = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;            
            if (string.IsNullOrEmpty(idString))
                throw new Exception("Token inválido: ID não encontrado nos Claims.");
            
            if (!Guid.TryParse(idString, out var idGuid))
            {
                throw new Exception($"Token inválido");
            }

            return idGuid;
        }
    }
}
