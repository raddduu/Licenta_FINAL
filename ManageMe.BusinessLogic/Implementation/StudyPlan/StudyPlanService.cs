using ManageMe.BusinessLogic;
using ManageMe.Entities;
using ManageMe.Entities.Entities;
using ManageMe.Entities.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ManageMe.BusinessLogic
{
    public class StudyPlanService : BaseService
    {
        //private readonly SubjectActivityFrequencyService _subjectActivityFrequencyService;

        public StudyPlanService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
        }

        public IEnumerable<Entities.Subject> GetAllSubjects()
        {
            return UnitOfWork.Subjects.Get();
        }

        public List<DetailsStudyPlanVM> GetAllStudyPlans(int studyDomainId)
        {
            return UnitOfWork.StudyPlans.Get()
                .Where(sp => sp.StudyDomainId == studyDomainId)
                .Include(sp => sp.Subject)
                .Select(sp => Mapper.Map<DetailsStudyPlanVM>(sp))
                .ToList();
        }

        public void AddNewStudyPlan(CreateStudyPlanVM studyPlan)
        {
            var dbStudyPlan = Mapper.Map<CreateStudyPlanVM, StudyPlan>(studyPlan);
            UnitOfWork.StudyPlans.Insert(dbStudyPlan);
            UnitOfWork.SaveChanges();
        }

        public void UpdateStudyPlan(UpdateStudyPlanVM studyPlan)
        {
            var dbStudyPlan = Mapper.Map<UpdateStudyPlanVM, StudyPlan>(studyPlan);
            //dbStudyPlan.Subject = UnitOfWork.Subjects.Get().Where(s => s.Name == studyPlan.SubjectName).FirstOrDefault();
            UnitOfWork.StudyPlans.Update(dbStudyPlan);
            UnitOfWork.SaveChanges();
        }

        public void DeleteStudyPlan(int studyDomainId, int subjectId)
        {
            var studyPlan = UnitOfWork.StudyPlans.Get()
                            .FirstOrDefault(sp => sp.StudyDomainId == studyDomainId && sp.SubjectId == subjectId);
            if (studyPlan != null)
            {
                UnitOfWork.StudyPlans.Delete(studyPlan);
                UnitOfWork.SaveChanges();
            }
        }

        public UpdateStudyPlanVM? GetStudyPlan(int studyDomainId, int subjectId)
        {
            var studyPlanDb = UnitOfWork.StudyPlans.Get()
                .Where(sp => sp.StudyDomainId == studyDomainId && sp.SubjectId == subjectId)
                .Include(sp => sp.Subject)
                .FirstOrDefault();

            if (studyPlanDb == null)
            {
                return null;
            }

            var studyPlan = Mapper.Map<StudyPlan, UpdateStudyPlanVM>(studyPlanDb);
            
            return studyPlan;
        }

        public List<SelectListItem> GetSubjectTypeNames()
        {
            return Enum.GetValues(typeof(SubjectTypeEnum))
                .Cast<SubjectTypeEnum>()
                .Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = ((int)v).ToString()
                })
                .ToList();
            
        }
        public List<SelectListItem> GetEvaluationFormNames()
        {
            return Enum.GetValues(typeof(EvaluationTypeEnum))
                .Cast<EvaluationTypeEnum>()
                .Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = ((int)v).ToString()
                })
                .ToList();
        }

        private bool SubjectHasLaboratoryAtStudyDomain(int subjectId, int studyDomainId)
        {
            return UnitOfWork.StudyPlans.Get()
                .Where(sp => sp.SubjectId == subjectId && sp.StudyDomainId == studyDomainId)
                .Select(sp => sp.LaboratoryCredits)
                .FirstOrDefault() != 0;
        }

        private bool SubjectHasSeminaryAtStudyDomain(int subjectId, int studyDomainId)
        {
            return UnitOfWork.StudyPlans.Get()
                .Where(sp => sp.SubjectId == subjectId && sp.StudyDomainId == studyDomainId)
                .Select(sp => sp.SeminaryCredits)
                .FirstOrDefault() != 0;
        }

        private bool SubjectHasLectureAtStudyDomain(int subjectId, int studyDomainId)
        {
            return UnitOfWork.StudyPlans.Get()
                .Where(sp => sp.SubjectId == subjectId && sp.StudyDomainId == studyDomainId)
                .Select(sp => sp.CourseCredits)
                .FirstOrDefault() != 0;
        }

        public DetailsSubjectVM? CheckSubjectCourseLaboratorySeminaryExistsAtAnyStudyDomain(DetailsSubjectVM subject)
        {
            var studyDomains = UnitOfWork.StudyDomains.Get().ToList();
            foreach (var studyDomain in studyDomains)
            {
                if (SubjectHasLectureAtStudyDomain(subject.Id, studyDomain.Id))
                {
                    subject.HasLecture = true;
                }

                if (SubjectHasLaboratoryAtStudyDomain(subject.Id, studyDomain.Id))
                {
                    subject.HasLaboratory = true;
                }

                if (SubjectHasSeminaryAtStudyDomain(subject.Id, studyDomain.Id))
                {
                    subject.HasSeminary = true;
                }
            }

            return subject;
        }
    }
}
