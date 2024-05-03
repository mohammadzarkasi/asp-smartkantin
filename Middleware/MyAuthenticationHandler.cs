using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace smartkantin.Middleware
{

    public class MyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public MyAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            // ISystemClock clock
            TimeProvider clock
            ) 
            : base(
                options, 
                logger, 
                encoder 
                // clock
                )
        {
            Console.WriteLine("ctor my auth handler");
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            Console.WriteLine("handle auth async");
            return AuthenticateResult.NoResult();
        }
    }
}