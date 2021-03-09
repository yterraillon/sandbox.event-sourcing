using System.Collections.Generic;

namespace EventSourcing.Core.Product.Queries.GetProduct
{
    public class ProductViewModel
    {
        public int Id { get; init; }

        public int CurrentQuantity { get; init; }

        public IEnumerable<string> History { get; init; }
    }
}