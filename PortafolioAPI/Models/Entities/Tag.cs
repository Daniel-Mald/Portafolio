using System;
using System.Collections.Generic;

namespace PortafolioAPI.Models.Entities;

public partial class Tag
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Tagwork> Tagwork { get; set; } = new List<Tagwork>();
}
