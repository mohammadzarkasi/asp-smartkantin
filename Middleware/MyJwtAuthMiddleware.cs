
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
            Tuple<string,string>? userIdAndTimestamp = checkJwtHeader(context);
            if(userIdAndTimestamp != null)
            {
                var userAndRoles = await GetUserAndRoles(userIdAndTimestamp.Item1);
                if(userAndRoles != null)    {
                    setUserClaimsIdentity(context, userAndRoles.Item1, userAndRoles.Item2);
                }
            }
            await next(context);
        }

        private Tuple<string, string>? checkJwtHeader(HttpContext context)
        {
            string headerAuthorizationBearer = context.Request.Headers.Authorization.Where(s => s?.StartsWith("Bearer ") ?? false).FirstOrDefault() ?? "";

            if (headerAuthorizationBearer.IsNullOrEmpty() == true)
            {
                return null;
            }

            string token = headerAuthorizationBearer[7..];

            var jwtClaims = tokenService.GetClaimsFromToken(token);
            string userId = jwtClaims.Where(c => c.Type == "user_id").FirstOrDefault()?.Value.ToString() ?? "";
            string timestamp = jwtClaims.Where(c => c.Type == "timestamp").FirstOrDefault()?.Value.ToString() ?? "";

            if (timestamp.IsNullOrEmpty() == true || userId.IsNullOrEmpty() == true)
            {
                return null;
            }

            return new Tuple<string, string>(userId, timestamp);

        }

        private async Task<Tuple<MyUser, IEnumerable<MyRole>>?> GetUserAndRoles(string userId)
        {
            var userIdGuid = GuidHelper.ParseGuid(userId);
            
            Console.WriteLine("parsed guid: " + userIdGuid);
            
            var user = await userRepository.GetOneById(userIdGuid);
            if (user == null)
            {
                return null;
            }

            var roles = await userRepository.GetRolesOfUser(user);

            return new Tuple<MyUser, IEnumerable<MyRole>>(user,roles);
            
        }

        private static void setUserClaimsIdentity(HttpContext context, MyUser user, IEnumerable<MyRole> roles)
        {
            var sessionClaims = new List<Claim>(){
                    new(ClaimTypes.Name, user.Username),
                    new(ClaimTypes.Email, user.Email),
                    new("user_id", user.Id.ToString()),
                };

            foreach (var role in roles)
            {
                sessionClaims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            // var identity = new ClaimsIdentity(claims, "my jwt auth middleware");
            var identity = new ClaimsIdentity(sessionClaims, "custom");

            context.User = new ClaimsPrincipal(identity);
            Console.WriteLine("set claim identity done");
        }
    }
}