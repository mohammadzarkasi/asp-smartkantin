
using smartkantin.Repository;

namespace smartkantin.Middleware
{
    public class MyJwtAuthMiddleware : IMiddleware
    {
        private readonly IMyUserRepository userRepository;

        public MyJwtAuthMiddleware(IMyUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            throw new NotImplementedException();
        }
    }
}