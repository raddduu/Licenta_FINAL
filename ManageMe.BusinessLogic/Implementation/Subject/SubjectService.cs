using AutoMapper;
using ManageMe.DataAccess;
using ManageMe.Entities;
using ManageMe.Entities.Entities;
using ManageMe.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManageMe.BusinessLogic.Implementation.Subject
{
    public class SubjectService : BaseService
    {
        public SubjectService(ServiceDependencies serviceDependencies) : base(serviceDependencies) { }

        public bool SubjectExists()
        {
            return UnitOfWork.Subjects.Get().Any();
        }

        public Entities.Subject? GetSubjectById(int? id)
        {
            return UnitOfWork.Subjects?.Get().FirstOrDefault(e => e.Id == id);
        }

        public DetailsSubjectVM? GetCompleteSubject (int? id)
        {
            var subject = UnitOfWork.Subjects.Get()
                            .Include(s => s.TeacherPermissions)
                                .ThenInclude(tp => tp.Teacher)
                            .FirstOrDefault(e => e.Id == id);

            if (subject == null)
            {
                return null;
            }

            return Mapper.Map<Entities.Subject, DetailsSubjectVM>(subject);
        }

        public void AddNewSubject (SubjectCreateModel subject)
        {
            var subjectToAdd = Mapper.Map<SubjectCreateModel, Entities.Subject>(subject);

            UnitOfWork.Subjects.Insert(subjectToAdd);
            UnitOfWork.SaveChanges();
        }

        public void UpdateSubject(UpdateSubjectVM updateSubject)
        {
            var subject = Mapper.Map<UpdateSubjectVM, Entities.Subject>(updateSubject);
            //subject.Description = GetTextFromHtml(updateSubject.Description);
            UnitOfWork.Subjects.Update(subject);
            UnitOfWork.SaveChanges();
        }

        private string? GetTextFromHtml(string? html)
        {
            var text = html?.Replace("<p>", "").Replace("</p>", "").Replace("<div>", "").Replace("</div>", "");
            text = text?.Replace("<br>", "\n").Replace("<br />", "\n").Replace("<br/>", "\n");

            return text;
        }

        public bool DeleteSubject(Entities.Subject subject)
        {
            try
            {
                UnitOfWork.Subjects.Delete(subject);
                UnitOfWork.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Entities.Subject> GetSubjects(string search)
        {
            var entities = UnitOfWork.Subjects.Get();

            if (search != "")
            {
                return entities.Where(s => s.Name.Contains(search));
            }

            else
            {
                return entities.OrderBy(s => s.Name);
            }
        }

        public void Create(CreateChannelVM model)
        {
            ExecuteInTransaction(uow =>
            {
                //CityCreateModelValidator.Validate(model).ThenThrow();

                var entityToAdd = Mapper.Map<CreateChannelVM, Channel>(model);

                uow.Channels.Insert(entityToAdd);
                uow.SaveChanges();
            });
        }

        public List<SelectListItem> GetAllSubjects(int? groupId)
        {
            var subjects = new List<SelectListItem>();

            var studyDomainId = UnitOfWork.Groups.Get()
                .Where(g => g.Id == groupId)
                .Include(g => g.Batch)
                .Select(g => g.Batch.StudyDomainId)
                .SingleOrDefault();
                

            if (studyDomainId == 0 && groupId != null)
            {
                return subjects;
            }

            var availableSubjects = new List<Entities.Subject>();

            if (groupId != null)
            {
                availableSubjects = UnitOfWork.Subjects.Get().Where( s => UnitOfWork.StudyPlans.Get()
                                                                    .Where(sp => sp.StudyDomainId == studyDomainId)
                                                                    .Select(sp => sp.SubjectId)
                                                                    .ToList()
                                                                    .Contains(s.Id))
                                                     .OrderBy(s => s.Name)
                                                     .ToList();
            }
            else
            {
                availableSubjects = UnitOfWork.Subjects.Get().OrderBy(s => s.Name).ToList();
            }

            foreach (var subject in availableSubjects)
            {
                subjects.Add(new SelectListItem
                {
                    Value = subject.Id.ToString(),
                    Text = subject.Name.ToString()
                });
            }

            return subjects;
        }

        public IEnumerable<Entities.Subject> GetTeacherPermissions(string userId)
        {
            var teacherPermissions = UnitOfWork.TeacherPermissions.Get()
                                        .Include(tp => tp.Subject)
                                        .Where(tp => tp.TeacherId == userId);

            return teacherPermissions.Select(tp => tp.Subject);
            
        }

        //public void AddShortNames()
        //{
        //    var subjects = UnitOfWork.Subjects.Get();

        //    foreach (var subject in subjects)
        //    {
        //        subject.ShortName = String.Join("", subject.Name.Split(' ').Where(w => w.Length >= 3 && w != "pentru").Select(w => Char.ToUpper(w[0])).ToList());
        //    }

        //    UnitOfWork.SaveChanges();
        //}
    }
}
