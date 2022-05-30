namespace Entities.Transports
{
    public class TransportPlace
    {
        public int Id { get; set; }
        public virtual Transport Transport { get; set; }
        public int TransportId { get; set; }
        public int Number { get; set; }
        public decimal Price { get; set; }
        public bool IsBooked { get; set; }
    }
}
