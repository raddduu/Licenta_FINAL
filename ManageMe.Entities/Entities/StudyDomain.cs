using ManageMe.Common.Interfaces;
using ManageMe.Entities.Entities;
using System;
using System.Collections.Generic;

namespace ManageMe.Entities;

public partial class StudyDomain : IEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int StudyYears { get; set; }

    public virtual ICollection<Batch> Batches { get; set; } = new List<Batch>();

    public virtual ICollection<StudyPlan> StudyPlans { get; set; } = new List<StudyPlan>();
}
