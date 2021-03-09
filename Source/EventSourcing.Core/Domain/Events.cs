using System;

namespace EventSourcing.Core.Domain
{
    public interface IEvent {}

    public record ProductShipped(int Id, int Quantity, DateTime TimeShipped) : IEvent;

    public record ProductReceived(int Id, int Quantity, DateTime TimeReceived) : IEvent;

    public record InventoryAdjusted(int Id, int Quantity, string Reason, DateTime TimeAdjusted) : IEvent;
}