using System;
using System.Collections.Generic;

namespace Persistance.Entities;

public partial class Hotel
{
    public long HotelId { get; set; }

    public string HotelName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int? StarRating { get; set; }

    public long? JourneyId { get; set; }

    public virtual Journey? Journey { get; set; }

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
}
