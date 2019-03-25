using System.Threading.Tasks;

namespace CoreWebsite.Web.Infrastructure
{
    public interface ICacher<T>
    {
        Task AddAsync(string key, T value);
        Task<T> GetAsync(string key);
        Task ClearAsync();
    }
}