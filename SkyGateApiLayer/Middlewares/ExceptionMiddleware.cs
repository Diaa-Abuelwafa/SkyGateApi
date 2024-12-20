using SkyGateDomainLayer.Errors;
using System.Net;
using System.Text.Json;

namespace SkyGateApiLayer.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate Next;
        private readonly IHostEnvironment Env;

        public ExceptionMiddleware(RequestDelegate Next, IHostEnvironment Env)
        {
            this.Next = Next;
            this.Env = Env;
        }
        public async Task InvokeAsync(HttpContext Context)
        {
            try
            {
                await Next.Invoke(Context);
            }
            catch(Exception Ex)
            {
                Context.Response.ContentType = "application/json";
                Context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var Response = new ApiServerErrorResponse((int)HttpStatusCode.InternalServerError, Ex.StackTrace);

                var ResponseJson = JsonSerializer.Serialize(Response);

                await Context.Response.WriteAsync(ResponseJson);
            }
        }
    }
}
