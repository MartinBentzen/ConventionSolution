using Microsoft.AspNetCore.Authorization;

namespace WebApi.MarvelConvention.Dto
{
    public static class Policies    
    {    
        public const string Admin = "admin";    
        public const string Participant = "participant";    
    
        public static AuthorizationPolicy AdminPolicy()    
        {    
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Admin).Build();    
        }    
    
        public static AuthorizationPolicy ParticipantPolicy()    
        {    
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Participant).Build();    
        }    
    }    
}
