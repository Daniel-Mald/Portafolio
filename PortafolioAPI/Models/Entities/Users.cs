using System;
using System.Collections.Generic;

namespace PortafolioAPI.Models.Entities;

public partial class Users
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string User { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Work> Work { get; set; } = new List<Work>();
}
