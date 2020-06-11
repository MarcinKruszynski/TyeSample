using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using Polly;
using Polly.Retry;
using ProductService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Data
{
    public class ProductContextSeed
    {
        public async Task SeedAsync(ProductContext context, ILogger<ProductContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(ProductContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                if (!context.ProductItems.Any())
                {
                    await context.ProductItems.AddRangeAsync(GetPreconfiguredItems());

                    await context.SaveChangesAsync();                    
                }
            });                                        
        }

        private IEnumerable<ProductItem> GetPreconfiguredItems()
        {
            return new List<ProductItem>()
            {
                new ProductItem { Name = "Zaraza", Price = 25m, StockQuantity = 10000 },
                new ProductItem { Name = "Melassa", Price = 20m, StockQuantity = 3000 }
            };
        }

        private AsyncRetryPolicy CreatePolicy(ILogger<ProductContextSeed> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<NpgsqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }        
    }
}
