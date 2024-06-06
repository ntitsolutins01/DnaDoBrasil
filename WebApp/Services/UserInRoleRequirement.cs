using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Services
{
    public class UserInRoleRequirement : IAuthorizationRequirement
    {
        public UserInRoleRequirement(string role)
        {
            Role = role;
        }

        public string Role { get; set; }
    }


    public class UserInRoleHandler : AuthorizationHandler<UserInRoleRequirement, string[]>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            UserInRoleRequirement requirement, string[] resource)
        {
            if (resource.Contains(context.User.FindFirst(ClaimTypes.Role).Value))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
