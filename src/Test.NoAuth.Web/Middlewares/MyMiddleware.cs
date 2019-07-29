using log4net.Core;
using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.NoAuth.Web.Middlewares
{
    public class MyMiddleware
    {
        private readonly RequestDelegate _next;

        
        public MyMiddleware(RequestDelegate next)
        {
            _next = next;
 
        }
        public async Task InvokeAsync(HttpContext context)
        {
           
            await _next(context);
        }
    }
}
