using Application.Interfaces;
using Domain.Exceptions;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UFRA.Bolao.API.Extensions;
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
            group.MapGet("/extrato", GetExtrato);
            group.MapGet("/limites", GetLimites);
        }


        private static async Task<IResult> GetSaldo([FromServices] ICarteiraService service, ClaimsPrincipal user)
        {
            try
            {
                var saldo = await service.ObterSaldoAsync(user.GetId());
                return Results.Ok(new { Saldo = saldo });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { Erro = ex.Message });
            }
        }

        private static async Task<IResult> Depositar([FromBody] DepositoDto dto, [FromServices] ICarteiraService service, ClaimsPrincipal user)
        {
            try
            {
                var usuarioId = user.GetId();
                var novoSaldo = await service.DepositarAsync(usuarioId, dto.Valor);
                return Results.Ok(new { Mensagem = "Depósito realizado!", NovoSaldo = novoSaldo });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { Erro = ex.Message });
            }
        }

        private static async Task<IResult> GetExtrato([FromServices] IBolaoQueries service, ClaimsPrincipal user)
        {
            var extrato = await service.GetExtrato(user.GetId());
            return Results.Ok(extrato);
        }

        private static async Task<IResult> GetLimites([FromServices] ICarteiraService service, ClaimsPrincipal user)
        {
            try
            {
                var limites = await service.ObterLimitesAsync(user.GetId());
                return Results.Ok(limites);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { Erro = ex.Message });
            }

        }
    }
}
