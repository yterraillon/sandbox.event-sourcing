using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace EventSourcing.Core.Product.Queries.GetAllProducts
{
    public static class GetAllProducts
    {
        public class Request : IRequest<IEnumerable<ProductViewModel>>
        {
        }

        public class Handler : IRequestHandler<Request, IEnumerable<ProductViewModel>>
        {
            public Task<IEnumerable<ProductViewModel>> Handle(Request request, CancellationToken cancellationToken)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}