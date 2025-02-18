﻿using System.Globalization;

namespace MyRecipeBook.API.Middleware
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;
        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var requestedCulture = context.Request.Headers["Accept-Language"].FirstOrDefault();
            var cultureCode = requestedCulture.Split(',')[0].Split(';')[0].Trim();
            var cultureInfo = new CultureInfo(cultureCode);
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;
            await _next(context);
        }
    }
}
