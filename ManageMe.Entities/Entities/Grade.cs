using ManageMe.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace ManageMe.Entities;

public partial class Grade : IEntity
{
    public string StudentId { get; set; }

    public int SubjectId { get; set; }

    public int GradingActivityId { get; set; }

    public int WeekNumber { get; set; }

    public decimal Value { get; set; }

    public virtual GradingActivity GradingActivity { get; set; } = null!;

    public virtual ApplicationUser Student { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;


}
