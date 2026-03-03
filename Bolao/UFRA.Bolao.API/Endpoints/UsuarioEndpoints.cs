
using Application.Interfaces;
using System.Security.Claims;
using UFRA.Bolao.API.Extensions;
using static Application.DTOs.UsuarioDtos;

namespace UFRA.Bolao.API.Endpoints
{
    public static class UsuarioEndpoints
    {
        public static void MapUsuarioEndpoins(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/usuario")
                .RequireAuthorization();

            group.MapPost("/editar-usuario",AlterarUsuario);
            group.MapGet("/admin",IsAdmin);
        }

        private static async Task<IResult> IsAdmin(IUsuarioService usuarioService,ClaimsPrincipal user)
        {
            return Results.Ok(await usuarioService.IsAdmin(user.GetId()));
        }

        private static async Task<IResult> AlterarUsuario(IUsuarioService usuarioService, AlterarUsuarioDto dto,ClaimsPrincipal user)
        {
            await usuarioService.AlterarUsuario(dto,user.GetId());
            return Results.Ok("Usuário atualizado com sucesso!");
        }
    }
}
