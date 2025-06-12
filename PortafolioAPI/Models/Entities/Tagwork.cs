using System;
using System.Collections.Generic;

namespace PortafolioAPI.Models.Entities;

public partial class Tagwork
{
    public int Id { get; set; }

    public int IdWork { get; set; }

    public int IdTag { get; set; }

    public virtual Tag IdTagNavigation { get; set; } = null!;

    public virtual Work IdWorkNavigation { get; set; } = null!;
}
