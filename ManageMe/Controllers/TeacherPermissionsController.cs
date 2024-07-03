using ManageMe.BusinessLogic;
using ManageMe.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManageMe.Web.Controllers
{
    public class TeacherPermissionsController : Controller
    {
        private readonly TeacherPermissionService _teacherPermissionService;

        public TeacherPermissionsController(TeacherPermissionService teacherPermissionService)
        {
            _teacherPermissionService = teacherPermissionService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Dean")]
        public IActionResult AddTeacherPermission(TeacherPermission teacherPermission)
        {
            try
            {
                _teacherPermissionService.CreateTeacherPermission(teacherPermission);
                return Ok(new
                {
                    success = true
                });
            }
            catch
            {
                return Ok(new
                {
                    success = false
                });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Dean")]
        public IActionResult DeleteTeacherPermission(TeacherPermission teacherPermission)
        {
            try
            {
                _teacherPermissionService.DeleteTeacherPermission(teacherPermission);
                return Ok(new
                {
                    success = true
                });
            }
            catch
            {
                return Ok(new
                {
                    success = false
                });
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Dean")]
        public IActionResult UpdateTeacherPermission(TeacherPermission teacherPermission)
        {
            try
            {
                _teacherPermissionService.UpdateTeacherPermission(teacherPermission);
                return Ok(new
                {
                    success = true
                });
            }
            catch
            {
                return Ok(new
                {
                    success = false
                });
            }
        }

        //public IEnumerable<TeacherPermission> GetTeacherPermissionsByFilterFields(int? subjectId = null, string? teacherId = null, int? activityId = null)
        //{
        //    try
        //    {
        //        var teacherPermissionsByTeacherId = _teacherPermissionService.GetTeacherPermissionsByTeacherId(teacherId);
        //        var teacherPermissionsBySubjectId = _teacherPermissionService.GetTeacherPermissionsBySubjectId(subjectId);
        //        var teacherPermissionsByActivityId = _teacherPermissionService.GetTeacherPermissionsByActivityId(activityId);
        //        var teacherPermissions = teacherPermissionsByTeacherId
        //                                    .Intersect(teacherPermissionsBySubjectId)
        //                                    .Intersect(teacherPermissionsByActivityId);
        //        return teacherPermissions;
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
    }
}
