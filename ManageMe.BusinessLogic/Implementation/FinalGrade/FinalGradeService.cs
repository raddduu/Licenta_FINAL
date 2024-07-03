using ManageMe.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManageMe.BusinessLogic
{
    public class FinalGradeService : BaseService
    {
        public FinalGradeService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
        }

        public List<FinalGradeVM> GetFinalGradesForStudent(string studentId)
        {
            var finalGrades = UnitOfWork.FinalGrades.Get().Where(x => x.StudentId == studentId)
                .Include(x => x.Subject)
                .Include(x => x.Student);

            var finalGradesVM = Mapper.Map<List<FinalGradeVM>>(finalGrades);

            return finalGradesVM;
        }

        public List<FinalGradeVM> GetFinalGradesForSubjectForGroup (int subjectId, int groupId)
        {
            var finalGrades = UnitOfWork.FinalGrades.Get()
                .Include(x => x.Subject)
                .Include(x => x.Student)
                .Where(x => x.SubjectId == subjectId && x.Student.GroupId == groupId);

            var finalGradesVM = Mapper.Map<List<FinalGradeVM>>(finalGrades);

            return finalGradesVM;
        }

        public List<FinalGradeVM> GetFinalGradesForSubject(int subjectId)
        {
            var finalGrades = UnitOfWork.FinalGrades.Get().Where(x => x.SubjectId == subjectId)
                .Include(x => x.Subject)
                .Include(x => x.Student);

            var finalGradesVM = Mapper.Map<List<FinalGradeVM>>(finalGrades);

            return finalGradesVM;
        }

        public List<FinalGradeCreateModel> GetFinalGradesCreateModelsFromClassbokStudentVM(List<ClassbookStudentVM> classbookStudentVMs, int subjectId)
        {
            var finalGradesCreateModels = new List<FinalGradeCreateModel>();

            foreach (var classbookStudentVM in classbookStudentVMs)
            {
                var finalGradeCreateModel = new FinalGradeCreateModel
                {
                    StudentId = classbookStudentVM.Id,
                    Grade = classbookStudentVM.RoundedGrade,
                    SubjectId = subjectId
                };

                finalGradesCreateModels.Add(finalGradeCreateModel);
            }

            return finalGradesCreateModels;
        }

        public bool UpdateFinalGrades(List<FinalGradeCreateModel> finalGradeCreateModels, int subjectId)
        {
            try
            {
                var finalGrades = UnitOfWork.FinalGrades.Get().Where(g => g.SubjectId == subjectId
                               && finalGradeCreateModels.Select(gcm => gcm.StudentId).Contains(g.StudentId)).ToList();

                foreach (var finalGradeCreateModel in finalGradeCreateModels)
                {
                    var finalGrade = finalGrades.SingleOrDefault(g => g.StudentId == finalGradeCreateModel.StudentId);

                    if (finalGrade == null)
                    {
                        finalGrade = new FinalGrade
                        {
                            StudentId = finalGradeCreateModel.StudentId,
                            SubjectId = subjectId,
                            Grade = finalGradeCreateModel.Grade
                        };

                        UnitOfWork.FinalGrades.Insert(finalGrade);
                    }
                    else
                    {
                        finalGrade.Grade = finalGradeCreateModel.Grade;
                        UnitOfWork.FinalGrades.Update(finalGrade);
                    }
                }

                UnitOfWork.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateFinalGrade(FinalGradeCreateModel finalGradeCreateModel)
        {
            try
            {
                var finalGrade = UnitOfWork.FinalGrades.Get().SingleOrDefault(g => g.StudentId == finalGradeCreateModel.StudentId);

                if (finalGrade == null)
                {
                    finalGrade = new FinalGrade
                    {
                        StudentId = finalGradeCreateModel.StudentId,
                        SubjectId = finalGradeCreateModel.SubjectId,
                        Grade = finalGradeCreateModel.Grade
                    };

                    UnitOfWork.FinalGrades.Insert(finalGrade);
                }
                else
                {
                    finalGrade.Grade = finalGradeCreateModel.Grade;
                    UnitOfWork.FinalGrades.Update(finalGrade);
                }

                UnitOfWork.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteFinalGrade(string studentId, int subjectId)
        {
            try
            {
                var finalGrade = UnitOfWork.FinalGrades.Get().SingleOrDefault(g => g.StudentId == studentId && g.SubjectId == subjectId);

                if (finalGrade == null)
                {
                    return false;
                }

                UnitOfWork.FinalGrades.Delete(finalGrade);
                UnitOfWork.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteFinalGrades(List<FinalGradeCreateModel> finalGradeCreateModels, int subjectId)
        {
            try
            {
                var finalGrades = UnitOfWork.FinalGrades.Get().Where(g => g.SubjectId == subjectId
                                              && finalGradeCreateModels.Select(gcm => gcm.StudentId).Contains(g.StudentId)).ToList();

                foreach (var finalGrade in finalGrades)
                {
                    UnitOfWork.FinalGrades.Delete(finalGrade);
                }

                UnitOfWork.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteFinalGradesForStudent(string studentId)
        {
            try
            {
                var finalGrades = UnitOfWork.FinalGrades.Get().Where(g => g.StudentId == studentId).ToList();

                foreach (var finalGrade in finalGrades)
                {
                    UnitOfWork.FinalGrades.Delete(finalGrade);
                }

                UnitOfWork.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteFinalGradesForSubject(int subjectId)
        {
            try
            {
                var finalGrades = UnitOfWork.FinalGrades.Get().Where(g => g.SubjectId == subjectId).ToList();

                foreach (var finalGrade in finalGrades)
                {
                    UnitOfWork.FinalGrades.Delete(finalGrade);
                }

                UnitOfWork.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddFinalGradesFromClassbook(List<ClassbookStudentVM> classbookStudentVMs)
        {
            try
            {
                var finalGradesCreateModel = Mapper.Map<List<FinalGradeCreateModel>>(classbookStudentVMs);

                var finalGrades = Mapper.Map<List<FinalGrade>>(finalGradesCreateModel);

                foreach (var finalGrade in finalGrades)
                {
                    try
                    {
                        UnitOfWork.FinalGrades.Insert(finalGrade);
                        UnitOfWork.SaveChanges();
                    }
                    catch 
                    {
                        UnitOfWork.FinalGrades.Update(finalGrade);
                        UnitOfWork.SaveChanges();
                    }
                }

                UnitOfWork.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
