using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SkyGateDomainLayer.Interfaces.Caching;
using System.Text;

namespace SkyGateApiLayer.Filters
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int NumbersOfHours;

        public CachedAttribute(int NumbersOfHours)
        {
            this.NumbersOfHours = NumbersOfHours;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var Key = GenerateKey(context.HttpContext.Request);

            var CacheService = context.HttpContext.RequestServices.GetRequiredService<ICachedService>();

            var Data = await CacheService.GetDataAsync(Key);

            if(Data is not null)
            {
                var Response = new ContentResult();

                Response.Content = Data;
                Response.ContentType = "application/json";
                Response.StatusCode = 200;

                context.Result = Response;

                return;
            }

            var Result = await next.Invoke();

            if(Result.Result is OkObjectResult ResponseData)
            {
                await CacheService.SetDataAsync(Key, ResponseData.Value, TimeSpan.FromHours(NumbersOfHours));
            }

            return;
        }

        private string GenerateKey(HttpRequest Request)
        {
            StringBuilder Key = new StringBuilder();

            Key.Append(Request.Path);

            foreach(var (key, value) in Request.Query.OrderBy(x => x.Key))
            {
                Key.Append($"{key}|{value}");
            }

            return Key.ToString();
        }
    }
}
