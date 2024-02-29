namespace Domain.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate{ get; set; }
        public string OrderNumber { get; set; } = "";
        public string Name { get; set; } = "";
        public string Phone { get; set; } = "";
        public bool Shipped { get; set; }
        public decimal Total { get; set; }
        public int Priority { get; set; }

        public OrderDto() { }
    }
}