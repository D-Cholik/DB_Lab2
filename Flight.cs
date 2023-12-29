using System;
using System.Collections.Generic;

namespace Persistance.Entities;

public partial class Flight
{
    public long FlightId { get; set; }

    public int FlightNumber { get; set; }

    public DateTime Departure { get; set; }

    public int Destination { get; set; }

    public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
}
