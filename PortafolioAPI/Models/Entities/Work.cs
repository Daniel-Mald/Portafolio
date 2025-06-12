using System;
using System.Collections.Generic;

namespace PortafolioAPI.Models.Entities;

public partial class Work
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int IdUser { get; set; }

    public virtual Users IdUserNavigation { get; set; } = null!;

    public virtual ICollection<Tagwork> Tagwork { get; set; } = new List<Tagwork>();
}
