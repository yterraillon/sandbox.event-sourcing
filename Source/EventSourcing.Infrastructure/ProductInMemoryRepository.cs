using System.Collections.Generic;
using EventSourcing.Core;
using EventSourcing.Core.Domain;

namespace EventSourcing.Infrastructure
{
    public class ProductInMemoryRepository : IProductRepository
    {
        private readonly Dictionary<int, List<IEvent>> _inMemoryStream = new();

        public Product Get(int id)
        {
            var product = new Product(id);

            if (!_inMemoryStream.ContainsKey(id)) return product;

            foreach (var evnt in _inMemoryStream[id])
            {
                product.ApplyEvent(evnt);
            }

            return product;
        }

        public void Save(Product product)
        {
            if (!_inMemoryStream.ContainsKey(product.Id))
            {
                _inMemoryStream.Add(product.Id, new List<IEvent>());
            }

            var newEvents = product.GetUncommittedEvents();
            _inMemoryStream[product.Id].AddRange(newEvents);
            product.EventsCommitted();
        }

        // TODO : faire une table de projections à la place
        //public IEnumerable<Product> GetAll()
        //{
        //}
    }
}