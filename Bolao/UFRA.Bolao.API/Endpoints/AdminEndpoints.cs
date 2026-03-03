
using Application.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using static Application.DTOs.BolaoDto;
using static Application.DTOs.UsuarioDtos;

namespace UFRA.Bolao.API.Endpoints
{
    public static class AdminEndpoints
    {        
        public static void MapAdminEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/admin")
              .RequireAuthorization( policy => policy.RequireRole("admin"))
              .WithTags("Admin")
              .WithSummary("")
              .WithDescription("");

            group.MapPost("/atualizarTimes", AtualizaTimes);
            group.MapPost("/criarPartida", CriarPartida);
            group.MapPost("/status-usuario",StatusUsuario).WithDescription("Ativa ou inativa usuário por id"); // ok
            group.MapPost("/status-partida", StatusDaPartida).WithDescription("Altera o status da partida"); //ok
            group.MapPost("/listar-usuarios", ListarUsuarios).WithDescription("Altera o status da partida"); //ok
            group.MapPost("/estornar-palpites", EstornarPalpites).WithDescription("Cancela bolao e estorna os palpites para as carteiras");
            group.MapGet("/log",GetLogs);//ok
            group.MapGet("/listar-usuarios",ListarUsuarios); //ok
        }

        private static async Task<IResult> ListarUsuarios(IAdminService adminService)
        {
            return Results.Ok(await adminService.ListarUsuarios());
        }


        private static async Task<IResult> EstornarPalpites(IAdminService adminService)
        {
            await adminService.EstornarPalpites();

            return Results.Ok(new { message = "Palpites estornados com sucesso." });
        }

        private static async Task<IResult> StatusDaPartida(IAdminService adminService, StatusDaPartidaDto status)
        {
            await adminService.StatusDaPartida(status);
            return Results.Ok(new { message = "status alterado com sucesso." });
        }

        private static async Task<IResult> StatusUsuario(IAdminService adminService, UsuarioAtivoDto dto)
        {
            await adminService.UsuarioAtivo(dto);
            return Results.Ok(new { message = "usuario alterado sucesso." });
        }

        private static async Task<IResult> GetLogs(IBolaoQueries bolaoQueries)
        {
            var res = await bolaoQueries.GetLogs();
            return Results.Ok(res);
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
