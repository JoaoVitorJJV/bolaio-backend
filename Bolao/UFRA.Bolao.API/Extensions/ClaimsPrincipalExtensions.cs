using System.Security.Claims;

namespace UFRA.Bolao.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {        
        public static Guid GetId(this ClaimsPrincipal user)
        {
            var idString = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(idString))
                throw new Exception("Token inválido: ID não encontrado nos Claims.");

            if (!Guid.TryParse(idString, out var idGuid))
            {
                throw new Exception("Token inválido: O formato do ID não é um GUID.");
            }

            return idGuid;
        }
    }
}
