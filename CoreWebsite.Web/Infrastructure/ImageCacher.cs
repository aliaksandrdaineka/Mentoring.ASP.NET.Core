using CoreWebsite.BLL.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace CoreWebsite.Web.Infrastructure
{
    public class ImageCacher : ICacher<byte[]>
    {
        private readonly Timer _timer;

        private string CacheDirectory { get; }
        private int MaxCachedImagesCount { get; }
        private int CacheExpirationMilliseconds { get; }

        public ImageCacher(IApplicationLifetime applicationLifetime, IHostingEnvironment hostingEnvironment, ISettingsProvider settings)
        {
            applicationLifetime.ApplicationStopping.Register(async () => await ClearAsync());
            CacheDirectory = Path.Combine(hostingEnvironment.WebRootPath, settings.CacheDirectoryName);
            MaxCachedImagesCount = settings.MaxCachedImagesCount;

            if (!Directory.Exists(CacheDirectory))
            {
                Directory.CreateDirectory(CacheDirectory);
            }

            _timer = new Timer
            {
                Interval = settings.CacheExpirationTime
            };

            _timer.Elapsed += async (o, s) => await ClearAsync();
        }

        public async Task AddAsync(string key, byte[] value)
        {
            var cachedFilesCount = Directory.EnumerateFiles(CacheDirectory).Count();

            if (cachedFilesCount > MaxCachedImagesCount)
            {
                return;
            }
            var filePath = Path.Combine(CacheDirectory, key);
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await fileStream.WriteAsync(value);
            }
        }

        public async Task<byte[]> GetAsync(string key)
        {
            _timer.Stop();
            _timer.Start();

            var filePath = Path.Combine(CacheDirectory, key);
            if (File.Exists(filePath))
            {
                return await File.ReadAllBytesAsync(filePath);
            }

            return null;
        }

        public async Task ClearAsync()
        {
            _timer.Stop();
            var files = Directory.EnumerateFiles(CacheDirectory);

            foreach(var filePath in files)
            {
                File.Delete(filePath);
            }

            await Task.CompletedTask;
        }
    }
}
