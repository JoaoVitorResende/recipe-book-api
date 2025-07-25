﻿using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace WebApi.Test
{
    public class MyRecipeBookClassFixture : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;
        public MyRecipeBookClassFixture(CustomWebApplicationFactory factory) => _httpClient = factory.CreateClient();
        protected async Task<HttpResponseMessage> DoPost(string method, object request, string culture = "en")
        {
            ChangeRequestCulture(culture);
            return await _httpClient.PostAsJsonAsync(method, request);
        }
        protected async Task<HttpResponseMessage> DoPut(string method, object request,string token ,string culture = "en")
        {
            ChangeRequestCulture(culture);
            AuthorizeRequest(token);
            return await _httpClient.PutAsJsonAsync(method, request);
        }
        protected async Task<HttpResponseMessage> DoGet(string method, string token = "", string culture = "en")
        {
            ChangeRequestCulture(culture);
            AuthorizeRequest(token);
            return await _httpClient.GetAsync(method);
        }
        private void ChangeRequestCulture(string culture)
        {
            if (_httpClient.DefaultRequestHeaders.Contains("Accept-Language"))
               _httpClient.DefaultRequestHeaders.Remove("Accept-Language");
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", culture);
        }
        private void AuthorizeRequest(string token)
        {
            if (string.IsNullOrEmpty(token))
                return;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
        }
    }
}
