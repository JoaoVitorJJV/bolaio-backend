using Application.DTOs;
using Application.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace UFRA.Bolaio.API.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/auth").WithTags("Autenticação");

            group.MapPost("/login", RealizarLogin)
                 .AllowAnonymous()
                 .WithName("LoginUsuario")
                 .Produces<LoginResponseDto>(StatusCodes.Status200OK)
                 .ProducesProblem(StatusCodes.Status400BadRequest);
        }


        private static async Task<IResult> RealizarLogin([FromBody] LoginRequestDto request, [FromServices] AuthAppService appService)
        {           
            var resultado = await appService.LoginAsync(request);
            return Results.Ok(resultado);
        }

        private static async Task<IResult> RegistrarUsuario([FromBody] RegisterRequestDto dto, [FromServices] AuthAppService service)
        {
            var resultado = await service.RegistrarAsync(dto);

            return Results.Created($"/usuarios/{resultado.Id}", resultado);
        }
    }
}
