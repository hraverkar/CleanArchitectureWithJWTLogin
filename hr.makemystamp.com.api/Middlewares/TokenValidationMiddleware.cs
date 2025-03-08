using hr.makemystamp.com.api.Attributes;
using hr.makemystamp.com.application.Interfaces;
using System.Threading.Tasks;

namespace hr.makemystamp.com.api.Middlewares
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ITokenBlackListService _tokenBlackListService;
        public TokenValidationMiddleware(RequestDelegate requestDelegate, ITokenBlackListService tokenBlackListService)
        {
            _requestDelegate = requestDelegate;
        }
        public async Task Invoke(HttpContext context)
        {
            var endPoint = context.GetEndpoint();
            if (endPoint != null)
            {

                var hasAllowAnonymous = endPoint.Metadata.GetMetadata<AllowAnonymousMiddlewareAttribute>() != null;
                if (hasAllowAnonymous)
                {
                    await _requestDelegate(context);
                    return;
                }
            }

            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                var token = context.Request.Headers["Authorization"].ToString().Split(" ").Last();
                if (token.Length > 0)
                {
                    if (_tokenBlackListService.IsTokenRevoked(token))
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("Unauthorizes: Token has been revoked");
                        return;
                    }
                    await _requestDelegate(context);
                    return;
                }
            }
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorizes: Token has been revoked");
        }
    }
}
