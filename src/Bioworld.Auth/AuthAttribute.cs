namespace Bioworld.Auth
{
    using Microsoft.AspNetCore.Authorization;

    public class AuthAttribute : AuthorizeAttribute
    {
        public AuthAttribute(string scheme, string policy = "") :base(policy)
        {
            AuthenticationSchemes = scheme;
        }
    }
}