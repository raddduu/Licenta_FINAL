using ManageMe.Common.Interfaces;
using ManageMe.Entities.Entities;
using System;
using System.Collections.Generic;

namespace ManageMe.Entities;

public class Subject : IIdentifiable, IEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string ShortName { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Channel> Channels { get; set; } = new List<Channel>();

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual ICollection<StudyPlan> StudyPlans { get; set; } = new List<StudyPlan>();

    public virtual ICollection<TeacherPermission> TeacherPermissions { get; set; } = new List<TeacherPermission>();

    public virtual ICollection<GradingCriterion> GradingCriteria { get; set; } = new List<GradingCriterion>();

    public virtual ICollection<GradingCriterionSubject> GradingCriterionSubjects { get; set; } = new List<GradingCriterionSubject>();

    public virtual ICollection<FinalGrade> FinalGrades { get; set; } = new List<FinalGrade>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
