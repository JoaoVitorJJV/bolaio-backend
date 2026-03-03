using Application.DTOs;
using Application.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

            group.MapPost("/register", RegistrarUsuario)
             .AllowAnonymous()
             .WithName("RegistrarUsuario")
             .WithSummary("Cria uma nova conta de usuário")
             .WithDescription("Recebe nome, email e senha. A senha deve ter letras maiúsculas, minúsculas e caracteres especiais.")
             .Produces<RegisterResponseDto>(StatusCodes.Status201Created)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .ProducesProblem(StatusCodes.Status409Conflict);

            group.MapPost("/reset-password", RedefinirSenha)
             .AllowAnonymous()
             .WithName("RedefinirSenha")
             .WithSummary("Redefine a senha de um usuário")
             .WithDescription("Recebe o email do usuário e a nova senha. A senha deve ter letras maiúsculas, minúsculas e caracteres especiais.")
             .Produces<string>(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .ProducesProblem(StatusCodes.Status404NotFound);
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

        private static async Task<IResult> RedefinirSenha([FromBody] ResetPasswordRequestDto request, [FromServices] AuthAppService appService)
        {
            var resultado = await appService.ResetPasswordAsync(request);
            return Results.Ok(resultado);
        }

        
    }
}
