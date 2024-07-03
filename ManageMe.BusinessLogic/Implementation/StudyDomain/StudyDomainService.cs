using ManageMe.DataAccess;
using ManageMe.Entities;
using ManageMe.BusinessLogic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using ManageMe.Entities.Enums;
using ManageMe.Common.Extensions;
using ManageMe.Entities.Entities;

namespace ManageMe.Services
{
    public class StudyDomainService : BaseService
    {
        public StudyDomainService(ServiceDependencies serviceDependencies) : base(serviceDependencies) {}

        public List<StudyDomain> GetAllStudyDomains()
        {
            return UnitOfWork.StudyDomains.Get().ToList();
        }

        public List<SelectListItem> GetAllStudyDomainsAsSelectList()
        {
            var studyDomains = UnitOfWork.StudyDomains.Get().ToList();

            var selectListItems = studyDomains.Select(studyDomain => new SelectListItem
            {
                Value = studyDomain.Id.ToString(),
                Text = studyDomain.Name
            }).ToList();

            return selectListItems;
        }

        public StudyDomain? GetStudyDomainById(int id)
        {
            return UnitOfWork.StudyDomains.Get().FirstOrDefault(m => m.Id == id);
        }

        public List<SelectListItem> GetAllSubjectTypes()
        {
            var subjectTypes = Enum.GetValues(typeof(SubjectTypeEnum)).Cast<SubjectTypeEnum>();

            var selectListItems = subjectTypes.Select(subjectType => new SelectListItem
            {
                Value = ((int)subjectType).ToString(),
                Text = subjectType.ToString()
            }).ToList();

            return selectListItems;
        }

        public List<SelectListItem> GetAllEvaluationTypes()
        {
            var evaluationFormTypes = Enum.GetValues(typeof(EvaluationTypeEnum)).Cast<EvaluationTypeEnum>();

            var selectListItems = evaluationFormTypes.Select(evaluationFormType => new SelectListItem
            {
                Value = ((int)evaluationFormType).ToString(),
                Text = evaluationFormType.ToString()
            }).ToList();

            return selectListItems;
        }

        public DetailsStudyDomainVM? GetDetailsStudyDomainVM(int id)
        {
            return UnitOfWork.StudyDomains.Get()
                .Where(sd => sd.Id == id)
                .Include(sd => sd.StudyPlans)
                    .ThenInclude(sp => sp.Subject)
                .Select(sd => Mapper.Map<DetailsStudyDomainVM>(sd))
                .FirstOrDefault();
        }

        public void AddStudyDomain(StudyDomainCreateModel studyDomain)
        {
            var studyDomainEntity = Mapper.Map<StudyDomain>(studyDomain);

            UnitOfWork.StudyDomains.Insert(studyDomainEntity);
            UnitOfWork.SaveChanges();
        }

        public void UpdateStudyDomain(StudyDomain studyDomain)
        {
            UnitOfWork.StudyDomains.Update(studyDomain);
            UnitOfWork.SaveChanges();
        }

        public void DeleteStudyDomain(int id)
        {
            var studyDomain =  UnitOfWork.StudyDomains.Get().FirstOrDefault(sd => sd.Id == id);
            if (studyDomain != null)
            {
                UnitOfWork.StudyDomains.Delete(studyDomain);
                UnitOfWork.SaveChanges();
            }
        }

        public bool StudyDomainExists(int id)
        {
            return UnitOfWork.StudyDomains?.Get().Any(e => e.Id == id) ?? false;
        }

        public List<SelectListItem> GetAllFrequencies()
        {
            var frequencies = Enum.GetValues(typeof(SubjectActivityFrequencyEnum)).Cast<SubjectActivityFrequencyEnum>();

            var selectListItems = frequencies.Select(frequency => new SelectListItem
            {
                Value = ((int)frequency).ToString(),
                Text = frequency.GetDisplayName()
            }).ToList();

            return selectListItems;
        }

        public bool SetSemester(int semester)
        {
            try
            {
                var currentSemester = UnitOfWork.Semesters.Get().FirstOrDefault();

                if (currentSemester == null)
                {
                    currentSemester = new Semester
                    {
                        SemesterNumber = semester
                    };

                    UnitOfWork.Semesters.Insert(currentSemester);
                }
                else
                {
                    currentSemester.SemesterNumber = semester;
                    UnitOfWork.Semesters.Update(currentSemester);
                }

                UnitOfWork.SaveChanges();

                // delete all the data in the schedules table
                var schedules = UnitOfWork.Schedules.Get().ToList();

                foreach (var schedule in schedules)
                {
                    UnitOfWork.Schedules.Delete(schedule);
                }

                UnitOfWork.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public int GetCurrentSemester()
        {
            var currentSemester = UnitOfWork.Semesters.Get().FirstOrDefault();

            return currentSemester?.SemesterNumber ?? 1;
        }
    }
}
