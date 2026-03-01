
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static Application.DTOs.BolaoDto;

namespace UFRA.Bolao.API.Endpoints
{
    public static class AdminEndpoints
    {
        public static void MapAdminEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/admin")
              .RequireAuthorization()
              .WithTags("Admin")
              .WithSummary("")
              .WithDescription("");

            group.MapPost("/atualizarTimes", AtualizaTimes);
            group.MapPost("/criarPartida", CriarPartida);

        }

        private static async Task AtualizaTimes([FromServices] IAdminService adminService)
        {
            await adminService.AtualizaTimes();
         
        }

        public static async Task<IResult> CriarPartida([FromServices] IAdminService adminService, [FromBody] CriarPartidaDto criarPartidaDto)
        {
            await adminService.CriarPartida(criarPartidaDto);
            return Results.Ok(new { message = "Partida criada com sucesso." });
        }
    }
}
