using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Framework.Infrastructure.Services;
using Microsoft.Extensions.Caching.Memory;
using Xunit;

namespace Framework.Infrastructure.Tests.Services
{
    public class CacheServiceTests
    {
        private readonly CacheService _target;

        public CacheServiceTests()
        {
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            _target = new CacheService(memoryCache, TimeSpan.FromSeconds(3));
        }

        [Fact]
        public void SetAsync_IfKeyIsNull_ThrowArgumentNullException()
        {
            // Arrange
            // Act
            Func<Task> result = async () => await _target.SetAsync(null, _ => Task.FromResult("test"));

            // Assert
            result.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SetAsync_IfFactoryIsNull_ThrowArgumentNullException()
        {
            // Arrange
            // Act
            Func<Task> result = async () => await _target.SetAsync<int>("testKey", null);

            // Assert
            result.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public async void SetAsync_IfCacheIsEmpty_ReturnList()
        {
            // Arrange
            var list = new List<int> {12, 32};
            
            // Act
            var result = await _target.SetAsync("testKey", _ => Task.FromResult(list));

            // Assert
            result.Should().BeEquivalentTo(list);
        }
    }
}
