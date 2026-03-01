using Application.Interfaces;
using Domain.Exceptions;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UFRA.Bolao.API.Extensions;
using static Application.DTOs.CarteiraDto;
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
             .WithDescription("Endpoints para gerenciamento da carteira do usuário.");

            group.MapGet("/saldo", GetSaldo);
            group.MapPost("/depositar", Depositar);
            group.MapGet("/extrato", GetExtrato);
            group.MapGet("/limites", GetLimites);
            group.MapPost("/sacar", Sacar);
            group.MapGet("/investido-em-jogos", GetInvestidoEmJogos);
            group.MapGet("/taxa-acerto", GetTaxaAcerto);
            group.MapGet("/lucro-potencial", GetLucroPotencial);
        }

        private static async Task<IResult> Sacar([FromServices] ICarteiraService carteiraService,ClaimsPrincipal user, SacarDto valor)
        {
            var saldo = await carteiraService.Sacar(valor, user.GetId());
            return Results.Ok(new { Mensagem = "Saque realizado!", SaldoAtualizado = saldo });
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

        private static async Task<IResult> GetInvestidoEmJogos([FromServices] ICarteiraService carteiraService, ClaimsPrincipal user)
        {
            var result = await carteiraService.GetInvestidoEmJogos(user.GetId());
            return Results.Ok(result);
        }

        private static async Task<IResult> GetTaxaAcerto([FromServices] ICarteiraService carteiraService, ClaimsPrincipal user)
        {
            var result = await carteiraService.GetTaxaAcerto(user.GetId());
            return Results.Ok(result);
        }

        private static async Task<IResult> GetLucroPotencial([FromServices] ICarteiraService carteiraService, ClaimsPrincipal user)
        {
            //var result = await carteiraService.GetLucroPotencial(user.GetId());
            return Results.Ok("10");
        }
    }
}
