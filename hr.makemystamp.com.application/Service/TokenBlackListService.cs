using hr.makemystamp.com.application.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hr.makemystamp.com.application.Service
{
    class TokenBlackListService : ITokenBlackListService
    {
        private readonly IMemoryCache _memoryCache;
        public TokenBlackListService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public bool IsTokenRevoked(string token)
        {
            return _memoryCache.TryGetValue(token, out var result);               
        }

        public void RevokeToken(string token, DateTime expiry)
        {
            _memoryCache.Set(token, true, expiry - DateTime.UtcNow);
        }
    }
}
