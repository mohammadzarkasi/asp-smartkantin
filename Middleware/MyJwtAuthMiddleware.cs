
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using smartkantin.Models;
using smartkantin.Repository;
using smartkantin.Service;
using smartkantin.Tools;

namespace smartkantin.Middleware
{
    public class MyJwtAuthMiddleware : IMiddleware
    {
        private readonly IMyUserRepository userRepository;
        private readonly ITokenService tokenService;

        public MyJwtAuthMiddleware(IMyUserRepository userRepository, ITokenService tokenService)
        {
            this.userRepository = userRepository;
            this.tokenService = tokenService;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await checkJwtHeader(context);
            await next(context);
        }

        private async Task checkJwtHeader(HttpContext context)
        {
            string headerAuthorizationBearer = context.Request.Headers.Authorization.Where(s => s?.StartsWith("Bearer ") ?? false).FirstOrDefault() ?? "";
            // Console.WriteLine("header auth bearer: " + headerAuthorizationBearer);

            if (headerAuthorizationBearer.IsNullOrEmpty() == true)
            {
                return;
            }
            // Console.WriteLine("header is not null and not empty: " + headerAuthorizationBearer.Length);

            // if (headerAuthorizationBearer.StartsWith("Bearer ") == false)
            // {
            //     return;
            // }
            // Console.WriteLine("header is started with bearer");

            string token = headerAuthorizationBearer[7..];

            var jwtClaims = tokenService.GetClaimsFromToken(token);
            string userId = jwtClaims.Where(c => c.Type == "user_id").FirstOrDefault()?.Value.ToString() ?? "";
            string timestamp = jwtClaims.Where(c => c.Type == "timestamp").FirstOrDefault()?.Value.ToString() ?? "";

            if (timestamp.IsNullOrEmpty() == true || userId.IsNullOrEmpty() == true)
            {
                return;
            }

            var userIdGuid = GuidHelper.ParseGuid(userId);
            Console.WriteLine("parsed guid: " + userIdGuid);
            MyUser? user = await userRepository.GetOneById(userIdGuid);
            if (user == null)
            {
                return;
            }

            var sessionClaims = new List<Claim>(){
                    new(ClaimTypes.Name, user.Username),
                    new(ClaimTypes.Email, user.Email),
                    new("user_id", user.Id.ToString()),
                    // new(ClaimTypes.Authentication, user.Id.ToString()),
                    // new(ClaimTypes.Upn, user.Id.ToString()),
                    
                };

            var roles = await userRepository.GetRolesOfUser(user);
            foreach (var role in roles)
            {
                // var roleName = role.Role.Name;
                // Console.WriteLine("add role: " + roleName);
                sessionClaims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            // var identity = new ClaimsIdentity(claims, "my jwt auth middleware");
            var identity = new ClaimsIdentity(sessionClaims, "custom");

            context.User = new ClaimsPrincipal(identity);
            Console.WriteLine("set claim identity done");


            // Console.WriteLine("list claim:");
            // foreach (var c in claims)
            // {
            //     Console.WriteLine(c.Type + " = " + c.Value);
            // }
            // Console.WriteLine("list claim end.");


            // Console.WriteLine("header authorization: ");
            // foreach (var h in headerAuthorization)
            // {
            //     Console.WriteLine(h);
            // }
            // Console.WriteLine("header end.");
        }
    }
}