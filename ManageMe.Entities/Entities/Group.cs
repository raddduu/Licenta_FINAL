using ManageMe.Common.Interfaces;
using ManageMe.Entities.Entities;
using System;
using System.Collections.Generic;

namespace ManageMe.Entities;

public partial class Group : IEntity
{
    public int Id { get; set; }

    public string Number { get; set; } = null!;

    //public int StudyYear { get; set; }

    public int BatchId { get; set; }

    public virtual Batch Batch { get; set; } = null!;

    public virtual ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

    public virtual ICollection<TeacherPermission> TeacherPermissions { get; set; } = new List<TeacherPermission>();

    public virtual ICollection<GradingCriterion> GradingCriteria { get; set; } = new List<GradingCriterion>();

    public virtual ICollection<GradingCriterionSubject> GradingCriterionSubjects { get; set; } = new List<GradingCriterionSubject>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
