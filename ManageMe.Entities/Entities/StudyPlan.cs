using ManageMe.Common.Interfaces;
using ManageMe.Entities.Entities;
using System;
using System.Collections.Generic;

namespace ManageMe.Entities;

public partial class StudyPlan : IEntity
{
    public int StudyDomainId { get; set; }

    public int SubjectId { get; set; }

    public int SubjectType { get; set; }

    public int Semester { get; set; }

    public int CourseCredits { get; set; }

    public int SeminaryCredits { get; set; }

    public int LaboratoryCredits { get; set; }

    public int ProjectCredits { get; set; }

    public int EvaluationForm { get; set; }

    public int SubjectOptionality { get; set; }

    public int TotalCredits { get; set; }

    public virtual StudyDomain StudyDomain { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;

    //public virtual ICollection<SubjectActivityFrequency> SubjectActivityFrequencies { get; set; } = new List<SubjectActivityFrequency>();
}
