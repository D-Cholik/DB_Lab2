using System;
using System.Collections.Generic;

namespace Persistance.Entities;

public partial class Journey
{
    public long JourneyId { get; set; }

    public string JourneyName { get; set; } = null!;

    public DateTime DepartureDate { get; set; }

    public decimal Cost { get; set; }

    public long ClientId { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
}
