using System;
using System.Collections.Generic;
using EventSourcing.Core.Domain.Exceptions;

namespace EventSourcing.Core.Domain
{
    public class Product
    {
        // TODO : ajouter un productName
        public int Id { get; set; }
        private readonly IList<IEvent> _allEvents = new List<IEvent>();
        private readonly IList<IEvent> _uncommittedEvents = new List<IEvent>();

        // Projection
        private readonly CurrentState _currentState = new();

        public Product(int id)
        {
            Id = id;
        }

        public void ShipProduct(int quantity)
        {
            if (quantity > _currentState.AvailableQuantity)
            {
                throw new InvalidDomainException("Not enough Stock");
            }

            AddEvent(new ProductShipped(Id, quantity, DateTime.Now));
        }

        public void ReceivedProduct(int quantity) => AddEvent(new ProductReceived(Id, quantity, DateTime.Now));

        public void AdjustInventory(int quantity, string reason)
        {
            if (_currentState.AvailableQuantity + quantity < 0)
            {
                throw new InvalidDomainException("Cannot have negative Quantities");
            }

            AddEvent(new InventoryAdjusted(Id, quantity, reason, DateTime.Now));
        }

        public IList<IEvent> GetUncommittedEvents() => new List<IEvent>(_uncommittedEvents);

        public IList<IEvent> GetAllEvents() => new List<IEvent>(_allEvents);

        public void EventsCommitted() => _uncommittedEvents.Clear();

        public int GetAvailableQuantity() => _currentState.AvailableQuantity;

        public void ApplyEvent(IEvent newEvent)
        {
            switch (newEvent)
            {
                case ProductShipped shipProduct:
                    Apply(shipProduct);
                    break;
                case ProductReceived productReceived:
                    Apply(productReceived);
                    break;
                case InventoryAdjusted inventoryAdjusted:
                    Apply(inventoryAdjusted);
                    break;
                default:
                    throw new InvalidOperationException("Unsupported event");
            }

            _allEvents.Add(newEvent);
        }

        private void AddEvent(IEvent newEvent)
        {
            ApplyEvent(newEvent);
            _uncommittedEvents.Add(newEvent);
        }

        private void Apply(ProductShipped productShipped) => _currentState.AvailableQuantity -= productShipped.Quantity;

        private void Apply(ProductReceived productReceived) => _currentState.AvailableQuantity += productReceived.Quantity;

        private void Apply(InventoryAdjusted inventoryAdjusted) => _currentState.AvailableQuantity += inventoryAdjusted.Quantity;
    }

    public class CurrentState
    {
        public int AvailableQuantity { get; set; }
    }
}