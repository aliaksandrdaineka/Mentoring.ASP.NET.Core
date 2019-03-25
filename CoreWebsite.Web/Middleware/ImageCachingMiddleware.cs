using CoreWebsite.Web.Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebsite.Web.Middleware
{
    public class ImageCachingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ICacher<byte[]> _cacher;

        public ImageCachingMiddleware(RequestDelegate next, ICacher<byte[]> cacher)
        {
            _next = next;
            _cacher = cacher;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var responseStream = httpContext.Response.Body;

            var key = GetCacheKey(httpContext.Request.Path);
            var cachedResponse = await _cacher.GetAsync(key);

            if (cachedResponse != null)
            {
                await responseStream.WriteAsync(cachedResponse);
                httpContext.Response.Body = responseStream;
                return;
            }

            var responseBytes = await GetResponseBytesAsync(httpContext);
            var responseContentType = httpContext.Response.ContentType;

            var isBmpImageResponse = !string.IsNullOrEmpty(responseContentType)
                && responseContentType.Contains("image/bmp");

            if (isBmpImageResponse)
            {
                await _cacher.AddAsync(key, responseBytes);
            }

            await responseStream.WriteAsync(responseBytes);
            httpContext.Response.Body = responseStream;
        }

        private string GetCacheKey(PathString pathString)
        {
            var fileNameSymbols = pathString.ToString()
                .Where(x => x != '/')
                .Concat(".bmp")
                .ToArray();

            return new string(fileNameSymbols);
        }

        private async Task<byte[]> GetResponseBytesAsync(HttpContext httpContext)
        {
            using (var responseInMemoryStream = new MemoryStream())
            {
                httpContext.Response.Body = responseInMemoryStream;

                await _next(httpContext);

                return responseInMemoryStream.ToArray();
            }
        }
    }
}
