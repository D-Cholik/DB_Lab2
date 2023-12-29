using System;
using System.Collections.Generic;

namespace Persistance.Entities;

public partial class Client
{
    public long ClientId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public virtual ICollection<Journey> Journeys { get; set; } = new List<Journey>();
}
