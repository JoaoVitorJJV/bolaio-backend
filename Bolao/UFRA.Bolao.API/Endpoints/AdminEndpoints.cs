
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
            
        }

        private static async Task AtualizaTimes([FromServices] IAdminService adminService)
        {
            await adminService.AtualizaTimes();
            

        }
    }
}
