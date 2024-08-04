namespace PL.Angular.Models
{
    public class OrderRequestModel
    {
        public required string UserId { get; set; }
        public required string ProductId { get; set; }
        public required uint Count { get; set; }
    }
}