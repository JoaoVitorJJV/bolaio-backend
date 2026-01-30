
using Application.Interfaces;
using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Application.DTOs.BolaoDto;

namespace UFRA.Bolao.API.Endpoints
{
    public static class BolaoEndpoints
    {
        public static void MapBolaoEndpoints (this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/bolao")
              .RequireAuthorization()
              .WithTags("Bolão")
              .WithSummary("")
              .WithDescription("Endpoints do bolão");

            group.MapGet("/listar", ListarBoloes);
            group.MapPost("/registrar_bolao", NovoBolao);
            group.MapPost("/registrar_palpite",RegistrarPalpite);
        }

        private static async Task<IResult> ListarBoloes([FromServices] IBolaoService service)
        {
            var resultado = await service.ListarBoloes();
            return Results.Ok(resultado);
        }

        private static async Task<IResult> NovoBolao([FromBody] CriarBolaoDto CriarBolaoDto, [FromServices] IBolaoService service,IUsuarioRepository usuarioRepository, ClaimsPrincipal user)
        {            
            var usuario = await usuarioRepository.ObterPorIdAsync(Guid.Parse(user.FindFirst(ClaimTypes.NameIdentifier).Value));
            var dtoComOrganizador = CriarBolaoDto with { Organizador = usuario! };            
            var resultado = await service.CriarBolaoAsync(dtoComOrganizador);
            return Results.Created($"/bolao/{resultado.Nome}", resultado);
        }

        private static async Task<IResult> RegistrarPalpite([FromBody] RegistrarPalpiteDto dto,[FromServices] IBolaoService bolaoService,ClaimsPrincipal user)
        {
            var userId = Guid.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? throw new UnauthorizedAccessException());
            await bolaoService.RegistrarPalpiteAsync(dto, userId);
            return Results.Ok(new { message = "Palpite registrado com sucesso!" });
        }
    }
}
