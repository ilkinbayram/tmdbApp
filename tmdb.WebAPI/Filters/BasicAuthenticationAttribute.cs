using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace tmdb.WebAPI.Filters
{
    public class BasicAuthenticationAttribute : Attribute, IAuthorizationFilter
    {
        private const string _username = "bsc.adm-nbd0ty87ryhejbhg";
        private const string _password = "ngh9hty9h74nytlhg0.e8756ng8dnfg5bw9y7fh.b48rfte";


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string authHeader = context.HttpContext.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Basic "))
            {
                var encodedCredentials = authHeader.Substring("Basic ".Length).Trim();
                var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
                var credentialsArray = decodedCredentials.Split(':');

                var username = credentialsArray[0];
                var password = credentialsArray[1];
                if (username == _username && password == _password)
                {
                    return;
                }
            }

            context.Result = new UnauthorizedResult();
        }
    }
}
