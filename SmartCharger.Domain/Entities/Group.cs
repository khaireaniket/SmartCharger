using System.Collections.Generic;

namespace SmartCharger.Domain.Entities
{
    public class Group
    {
        public Group()
        {
            ChargeStations = new List<ChargeStation>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public float CapacityInAmps { get; set; }

        public ICollection<ChargeStation> ChargeStations { get; set; }
    }
}