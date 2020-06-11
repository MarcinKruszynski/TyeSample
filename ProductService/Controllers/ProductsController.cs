using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductService.Model;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<ProductItem> Get()
        {
            return new ProductItem[]
            {
                new ProductItem { Id = 1, Name = "Zaraza", Price = 35m, StockQuantity = 5000 },
                new ProductItem { Id = 2, Name = "Melassa", Price = 30m, StockQuantity = 3000 }
            };
        }
    }
}
