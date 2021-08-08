using System.Collections.Generic;

namespace SmartCharger.Domain.Entities
{
    public class ChargeStation
    {
        public ChargeStation()
        {
            Connectors = new List<Connector>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int GroupId { get; set; }

        public ICollection<Connector> Connectors { get; set; }

    }
}
