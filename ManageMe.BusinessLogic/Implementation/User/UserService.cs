using ManageMe.Entities;
using ManageMe.Entities.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ManageMe.BusinessLogic
{
    public class UserService : BaseService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(ServiceDependencies serviceDependencies, UserManager<ApplicationUser> userManager) : base(serviceDependencies)
        {
            _userManager = userManager;
        }

        public bool UserExists()
        {
            return UnitOfWork.ApplicationUsers.Get().Any();
        }

        public List<UserMinimalInfo>? GetTeachersForSubjectActivity(int subjectId, int activityId, int groupId)
        {
            var group = UnitOfWork.Groups.Get().FirstOrDefault(g => g.Id == groupId);

            if (group == null)
            {
                return null;
            }

            var teacherPermissionsBySubjectIdAndActivityId = UnitOfWork.TeacherPermissions.Get()
                                                             .Where(tp => tp.SubjectId == subjectId 
                                                                          && tp.ActivityId == activityId
                                                                          && !tp.Groups.Contains(group))
                                                             .Include(tp => tp.Teacher)
                                                             .Select(tp => Mapper.Map<UserMinimalInfo>(tp.Teacher));

            return teacherPermissionsBySubjectIdAndActivityId.ToList();
        }

        public List<UserMinimalInfo> GetAllUsersInRole(string roleName)
        {
            var users = UnitOfWork.ApplicationUsers.Get().Include(u => u.Roles).Where(u => u.Roles.Any(r => r.Name == roleName));
            var mappedUsers = Mapper.Map<IEnumerable<UserMinimalInfo>>(users);
            return mappedUsers.ToList();
        }

        public List<UserMinimalInfo> GetAllStudentsNotInGroups()
        {
            var users = UnitOfWork.ApplicationUsers.Get()
                .Include(u => u.Roles)
                .Where(u => u.Roles.Any(r => r.Name == "Student") 
                            && u.GroupId == null)
                .OrderBy(e => e.LastName)
                .ThenBy(e => e.FirstName);


            var mappedUsers = Mapper.Map<IEnumerable<UserMinimalInfo>>(users);
            return mappedUsers.ToList();
        }

        public List<UserMinimalInfo> GetAllUsersInRole(string roleName, int subjectId, int activityId)
        {
            var users = UnitOfWork.ApplicationUsers.Get()
                            .Include(u => u.Roles)
                            .Where(u => u.Roles.Any(r => r.Name == roleName)
                                && !u.TeacherPermissions.Any(tp => tp.SubjectId == subjectId
                                                             && tp.ActivityId == activityId))
                            .OrderBy(e => e.LastName)
                            .ThenBy(e => e.FirstName);

            var mappedUsers = Mapper.Map<IEnumerable<UserMinimalInfo>>(users);
            return mappedUsers.ToList();
        }

        public Entities.ApplicationUser? GetUserById(string? id)
        {
            return UnitOfWork.ApplicationUsers?.Get().Include(u => u.Roles).FirstOrDefault(e => e.Id == id);
        }

        public async Task<bool> RemoveFromRole (string userId, string roleName)
        {
            var user = UnitOfWork.ApplicationUsers.Get().Include(u => u.Roles).FirstOrDefault(e => e.Id == userId);

            try
            {
                user.Roles.Remove(user.Roles.Where(r => r.Name == roleName).FirstOrDefault());
                
                UnitOfWork.SaveChanges();
                await _userManager.RemoveFromRoleAsync(user, roleName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddToRoleAsync(string userId, string roleName)
        {
            var user = UnitOfWork.ApplicationUsers.Get().Include(u => u.Roles).FirstOrDefault(e => e.Id == userId);

            try
            {
                var role = UnitOfWork.ApplicationRoles.Get().Where(r => r.Name == roleName).FirstOrDefault();
                user.Roles.Add(role);
                UnitOfWork.SaveChanges();
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public UserMinimalInfo GetCompleteUser (string? id)
        {
            var user = UnitOfWork.ApplicationUsers.Get().FirstOrDefault(e => e.Id == id);
            var mappedUser = Mapper.Map<Entities.ApplicationUser, UserMinimalInfo>(user);
            return mappedUser;
        }

        public void AddNewUser (Entities.ApplicationUser user)
        {
            UnitOfWork.ApplicationUsers.Insert(user);
            UnitOfWork.SaveChanges();
        }

        public void UpdateUser(Entities.ApplicationUser user)
        {
            UnitOfWork.ApplicationUsers.Update(user);
            UnitOfWork.SaveChanges();
        }

        public bool DeleteUser(string userId)
        {
            var user = UnitOfWork.ApplicationUsers.Get().FirstOrDefault(e => e.Id == userId);

            if (user != null)
            {
                UnitOfWork.ApplicationUsers.Delete(user);
                UnitOfWork.SaveChanges();
                return true;
            }

            return false;
        }

        public IEnumerable<Entities.ApplicationUser> GetUsers(int pageNumber, int pageSize)
        {
            var model = UnitOfWork.ApplicationUsers.Get();

            return model.ToList();
        }

        public void Update(UpdateUserVM model)
        {
            ExecuteInTransaction(uow =>
            {
                //CityUpdateModelValidator.Validate(model).ThenThrow();

                var entityToUpdate = uow.ApplicationUsers.Get().FirstOrDefault(e => e.Id == model.Id);

                Mapper.Map(model, entityToUpdate);

                uow.ApplicationUsers.Update(entityToUpdate);
                uow.SaveChanges();
            });
        }

        public void Delete(string id)
        {
            ExecuteInTransaction(uow =>
            {
                var entityToDelete = uow.ApplicationUsers.Get().FirstOrDefault(e => e.Id == id);

                uow.ApplicationUsers.Delete(entityToDelete);
                uow.SaveChanges();
            });
        }

        public IEnumerable<UserMinimalInfo> GetAll(string search)
        {
            var entities = UnitOfWork.ApplicationUsers.Get().Include(u => u.Roles);

            if (search == "")
            {
                var models = Mapper.Map<IEnumerable<Entities.ApplicationUser>, IEnumerable<UserMinimalInfo>>(entities.OrderBy(e => e.LastName));

                return models;
            }

            else
            {
                var models = Mapper.Map<IEnumerable<Entities.ApplicationUser>, IEnumerable<UserMinimalInfo>>
                    (entities
                    .Where(e => e.FirstName.Contains(search)
                                    || e.LastName.Contains(search))
                    .OrderBy(e => e.LastName));

                return models;
            }
        }

        public IEnumerable<UserMinimalInfo> GetByPage(int pageNumber, int pageSize)
        {
            var entities = UnitOfWork.ApplicationUsers.Get()
                            .Skip((pageNumber - 1) * pageSize).Take(pageSize)
                            .Select(u => Mapper.Map<UserMinimalInfo>(u)); ;

            return entities.ToList();
        }

        public byte[]? GetProfilePicture(string id)
        {
            var user = UnitOfWork.ApplicationUsers.Get().FirstOrDefault(u => u.Id == id);
            return user != null ? user.ProfilePicture : null;
        }

        public void UpdatePersonalData(PersonalDataUser user)
        {
            var entityToUpdate = UnitOfWork.ApplicationUsers.Get().FirstOrDefault(e => e.Id == user.Id);
            if (entityToUpdate != null)
            {
                entityToUpdate.BirthDate = user.BirthDate;
                entityToUpdate.FirstName = user.FirstName;
                entityToUpdate.LastName = user.LastName;
                entityToUpdate.UserName = user.UserName;
                entityToUpdate.Email = user.Email;
                entityToUpdate.ProfilePicture = user.ProfilePictureByteArray;
                UnitOfWork.ApplicationUsers.Update(entityToUpdate);
            }
        }

        public PersonalDataUser? GetUserPersonalData(string? id)
        {
            var user = UnitOfWork.ApplicationUsers.Get().FirstOrDefault(e => e.Id == id);
            if (user != null)
            {
                var mappedUser = Mapper.Map<PersonalDataUser>(user);
                return mappedUser;
            }
            else
            {
                return null;
            }
        }

        public bool UploadProfilePicture(string? userId, byte[] fileBytes)
        {
            try
            {
                var user = UnitOfWork.ApplicationUsers.Get().FirstOrDefault(e => e.Id == userId);

                if (user == null)
                {
                    return false;
                }

                user.ProfilePicture = fileBytes;
                UnitOfWork.ApplicationUsers.Update(user);
                UnitOfWork.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public bool RemoveProfilePicture(string? userId)
        {
            try
            {
                var user = UnitOfWork.ApplicationUsers.Get().FirstOrDefault(e => e.Id == userId);

                if (user == null)
                {
                    return false;
                }

                user.ProfilePicture = null;
                UnitOfWork.ApplicationUsers.Update(user);
                UnitOfWork.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<SelectListItem> GetAllTeachersThatTeachThisActivityAtThisGroup(int subjectId, int activityId, int groupId)
        {
            var teachersThatTeachThisActivityAtThisGroup = UnitOfWork.Groups.Get()
                .Where(g => g.Id == groupId)
                .Include(g => g.TeacherPermissions)
                .ThenInclude(tp => tp.Teacher)
                .Select(g => g.TeacherPermissions.Where(tp => tp.ActivityId == activityId && tp.SubjectId == subjectId))
                .Select(tp => tp.Select(t => t.Teacher))
                .FirstOrDefault();
          
            var teachersThatTeachThisActivityAtThisGroupList = teachersThatTeachThisActivityAtThisGroup?.ToList();

            var teachersThatTeachThisActivityAtThisGroupSelectList = new List<SelectListItem>();

            if (teachersThatTeachThisActivityAtThisGroupList != null)
            {
                foreach (var teacher in teachersThatTeachThisActivityAtThisGroupList)
                {
                    teachersThatTeachThisActivityAtThisGroupSelectList.Add(new SelectListItem
                    {
                        Text = teacher.FirstName + " " + teacher.LastName,
                        Value = teacher.Id
                    });
                }
            }

            return teachersThatTeachThisActivityAtThisGroupSelectList;
        }

        public bool UserHasRole(string userId, string roleName)
        {
            var user = UnitOfWork.ApplicationUsers.Get().Include(u => u.Roles).FirstOrDefault(e => e.Id == userId);

            return user != null ? user.Roles.Any(r => r.Name == roleName) : false;
        }
    }
}
