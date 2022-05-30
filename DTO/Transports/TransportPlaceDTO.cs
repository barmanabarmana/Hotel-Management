namespace DTO.Transports
{
    public class TransportPlaceDTO
    {
        public TransportPlaceDTO() { }
        public TransportPlaceDTO(int TransportId, int Number, decimal Price)
        {
            this.Number = Number;
            this.Price = Price;
            this.TransportId = TransportId;
            IsBooked = false;
        }

        public int Id { get; set; }
        public virtual TransportDTO Transport { get; set; }
        public int TransportId { get; set; }
        public int Number { get; set; }
        public decimal Price { get; set; }
        public bool IsBooked { get; set; }
    }
}
