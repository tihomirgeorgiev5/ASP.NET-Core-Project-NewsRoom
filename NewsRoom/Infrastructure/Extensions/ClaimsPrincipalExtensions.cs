﻿using System.Security.Claims;
using static NewsRoom.Areas.Admin.AdminConstants;



namespace NewsRoom.Infrastructure.Extensions

    
{
    public static class ClaimsPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal user)
            => user .FindFirst(ClaimTypes.NameIdentifier).Value;

        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole(AdministratorRoleName);
    }
}
