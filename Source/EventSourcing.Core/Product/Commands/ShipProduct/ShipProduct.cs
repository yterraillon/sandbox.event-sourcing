using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace EventSourcing.Core.Product.Commands.ShipProduct
{
    public static class ShipProduct
    {
        public class Request : IRequest
        {
            public int Id { get; set; }

            public int Quantity { get; set; }
        }

        public class Handler : AsyncRequestHandler<Request>
        {
            private readonly IProductRepository _repository;

            public Handler(IProductRepository repository) => _repository = repository;

            protected override Task Handle(Request request, CancellationToken cancellationToken)
            {
                var product = _repository.Get(request.Id);
                product.ShipProduct(request.Quantity);
                _repository.Save(product);

                return Task.CompletedTask;
            }
        }
    }
}