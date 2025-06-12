using System.Security.Claims;
using PortafolioAPI.Models.Entities;

namespace PortafolioAPI.Helpers
{
    public static class FromToken
    {
        public static int GetId(ClaimsPrincipal claims)
        {
            var claim = claims.FindFirst("Id");
            if (claim == null) return 0 ;
            return int.Parse(claim.Value);
        }
    }
}
