namespace SmartCharger.Domain.Entities
{
    public class Connector
    {
        public int Id { get; set; }

        public float MaxCurrentInAmps { get; set; }

        public int ChargeStationId { get; set; }
    }
}
