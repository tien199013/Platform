using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Platform
{
    public class QueryStringMiddleWare
    {
        private readonly RequestDelegate _nextDelegate;

        public QueryStringMiddleWare(RequestDelegate nextDelegate)
        {
            _nextDelegate = nextDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
            {
                await context.Response.WriteAsync("Class-based Middleware \n");
            }

            await _nextDelegate(context);
        }
    }

    public class LocationMiddleware
    {
        private readonly RequestDelegate _nextDelegate;
        private readonly MessageOptions _options;

        public LocationMiddleware(RequestDelegate nextDelegate, IOptions<MessageOptions> options)
        {
            _nextDelegate = nextDelegate;
            _options = options.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/location")
            {
                await context.Response.WriteAsync($"{_options.CityName}, {_options.CountryName}");
            }
            else
            {
                await _nextDelegate(context);
            }
        }
    }
}
