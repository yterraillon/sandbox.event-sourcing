namespace EventSourcing.Webapi.Controllers.Models
{
    public class AdjustInventoryRequest
    {
        public int Quantity { get; set; }
        public string Reason { get; set; }
    }
}