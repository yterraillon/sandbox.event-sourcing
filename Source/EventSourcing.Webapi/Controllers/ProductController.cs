using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventSourcing.Core.Product.Commands.AdjustInventory;
using EventSourcing.Core.Product.Commands.CreateProduct;
using EventSourcing.Core.Product.Commands.ReceiveProduct;
using EventSourcing.Core.Product.Commands.ShipProduct;
using EventSourcing.Core.Product.Queries.GetProduct;
using EventSourcing.Webapi.Controllers.Models;
using MediatR;

namespace EventSourcing.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator) => _mediator = mediator;

        // GET: api/<StockController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _mediator.Send(new GetProduct.Request
            {
                Id = id
            });

            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post(CreateProductRequest request)
        {
            _mediator.Send(new CreateProduct.Request
            {
                Id = request.Id,
                Quantity = request.Quantity
            });

            return Ok($"Product Created - {request.Id}");
        }

        [HttpPost]
        [Route("ship")]
        public IActionResult ShipProduct(ShipProductRequest request)
        {
            _mediator.Send(new ShipProduct.Request
            {
                Id = request.Id,
                Quantity = request.Quantity
            });

            return Ok($"Product Shipped - {request.Id}");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] int quantity)
        {
            _mediator.Send(new ReceiveProduct.Request
            {
                Id = id,
                Quantity = quantity
            });
 
            return Ok($"Product Received - {id}");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] AdjustInventoryRequest request)
        {
            _mediator.Send(new AdjustInventory.Request
            {
                Id = id,
                Quantity = request.Quantity,
                Reason = request.Reason
            });

            return Ok($"Inventory Adjusted - {id}");
        }
    }
}
