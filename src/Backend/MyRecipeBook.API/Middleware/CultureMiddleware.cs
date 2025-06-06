﻿﻿using System.Globalization;
using MyRecipeBook.Domain.Extensions;

namespace MyRecipeBook.API.Middleware
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;
        public async Task Invoke(HttpContext context)
        {
            var supportedLanguages = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();
            var requestedCulture = context.Request.Headers["Accept-Language"].FirstOrDefault();
            var cultureCode = requestedCulture!.Split(',')[0].Split(';')[0].Trim();
            var cultureInfo = new CultureInfo("pt-BR");
            if (cultureCode.NotEmpty() && supportedLanguages.Exists(culture => culture.Name.Equals(cultureCode)))
                cultureInfo = new CultureInfo(cultureCode);
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;
            await _next(context);
        }
        public CultureMiddleware(RequestDelegate next) => _next = next;
    }
}