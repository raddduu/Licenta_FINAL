using ManageMe.Entities;
using ManageMe.Entities.Entities;

namespace ManageMe.BusinessLogic
{
    public class TeacherPermissionService : BaseService
    {
        public TeacherPermissionService (ServiceDependencies dependencies):base(dependencies) {}

        public void CreateTeacherPermission(TeacherPermission teacherPermission)
        {
            UnitOfWork.TeacherPermissions.Insert(teacherPermission);
            UnitOfWork.SaveChanges();
        }

        public void DeleteTeacherPermission(TeacherPermission teacherPermission)
        {
            UnitOfWork.TeacherPermissions.Delete(teacherPermission);
            UnitOfWork.SaveChanges();
        }

        public void UpdateTeacherPermission(TeacherPermission teacherPermission)
        {
            UnitOfWork.TeacherPermissions.Update(teacherPermission);
            UnitOfWork.SaveChanges();
        }

        public IEnumerable<TeacherPermission>? GetTeacherPermissionsBySubjectId(int? subjectId)
        {
            return subjectId != null 
                ? UnitOfWork.TeacherPermissions.Get().Where(x => x.SubjectId == subjectId)
                : UnitOfWork.TeacherPermissions.Get();
        }

        public IEnumerable<TeacherPermission>? GetTeacherPermissionsByTeacherId(string? teacherId)
        {
            return teacherId != null
                ? UnitOfWork.TeacherPermissions.Get().Where(x => x.TeacherId == teacherId)
                : UnitOfWork.TeacherPermissions.Get();
        }

        public IEnumerable<TeacherPermission>? GetTeacherPermissionsByActivityId(int? activityId)
        {
            return activityId != null
                ? UnitOfWork.TeacherPermissions.Get().Where(x => x.ActivityId == activityId)
                : UnitOfWork.TeacherPermissions.Get();
        }

        public IEnumerable<UserMinimalInfo>? GetTeachersFromTeacherPermissions(IEnumerable<TeacherPermission> teacherPermissions)
        {
            return Mapper.Map<IEnumerable<UserMinimalInfo>>(teacherPermissions.Select(x => x.Teacher));
        }
    }
}
