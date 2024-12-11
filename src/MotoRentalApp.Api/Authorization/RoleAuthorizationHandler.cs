using Microsoft.AspNetCore.Authorization;

namespace MotoRentalApp.Api.Authorization
{
    public class RoleAuthorizationHandler : AuthorizationHandler<RoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "Role" && c.Value == requirement.Role))
            {
                context.Succeed(requirement); 
            }
            return Task.CompletedTask;
        }
    }
}