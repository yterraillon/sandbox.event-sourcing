using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventSourcing.Core.Domain;
using MediatR;

namespace EventSourcing.Core.Product.Queries.GetProduct
{
    public static class GetProduct
    {
        public class Request : IRequest<ProductViewModel>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Request, ProductViewModel>
        {
            private readonly IProductRepository _repository;

            public Handler(IProductRepository repository) => _repository = repository;

            public Task<ProductViewModel> Handle(Request request, CancellationToken cancellationToken)
            {
                var product = _repository.Get(request.Id);
                return Task.FromResult(MapToProductViewModel(product));
            }

            private static ProductViewModel MapToProductViewModel(Domain.Product product) =>
                new()
                {
                    Id = product.Id,
                    CurrentQuantity = product.GetAvailableQuantity(),
                    History = BuildHistory(product.GetAllEvents())
                };

            private static IEnumerable<string> BuildHistory(IEnumerable<IEvent> events) => events.Select(evnt => nameof(evnt));
        }
    }
}