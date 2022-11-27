using System.Globalization;
using Classroom.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Classroom.Services;

public class LocalizerService
{
    private readonly AppDbContext _context;
    private readonly IMemoryCache _memoryCache;

    public LocalizerService(AppDbContext context, IMemoryCache memoryCache)
    {
        _context = context ;
        _memoryCache = memoryCache ;
    }

    public string this[string key] => GetLocalizedString(key);

    public string GetLocalizedString(string key)
    {
        var cacheKey = $"localized_{CultureInfo.CurrentCulture.Name}_{key}";

        return _memoryCache.GetOrCreate(cacheKey, entry =>
        {
            var localizedString = _context.LocalizedStrings.FirstOrDefault(l => l.Key == key);
            if (localizedString is null)
                return key;

            var currentCulture = CultureInfo.CurrentCulture.Name;
            var localized = currentCulture.ToLower() switch
            {
                "uz" => localizedString.Uz,
                "ru" => localizedString.Ru,
                "en" => localizedString.En,
                _ => key
            };

            entry.SlidingExpiration = TimeSpan.FromHours(1);

            return localized ?? key;
        });
    }
}