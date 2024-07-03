using ManageMe.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace ManageMe.Entities.Entities;

public partial class Methodology : IEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();
}
