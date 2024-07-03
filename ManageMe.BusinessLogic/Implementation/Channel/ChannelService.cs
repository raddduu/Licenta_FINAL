using ManageMe.Common;
using ManageMe.Entities;
using ManageMe.Entities.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ManageMe.BusinessLogic
{
    public class ChannelService : BaseService
    {
        private readonly object _algorithms;

        public ChannelService(ServiceDependencies serviceDependencies, GeneralAlgorithm generalAlgorithm):base(serviceDependencies)
        {
            _algorithms = generalAlgorithm;
        }

        public IEnumerable<ChannelVM>? GetChannels(int pageNumber, int pageSize, string userId, bool isAdmin)
        {
            var allChannelsQuery = UnitOfWork.Channels.Get();

            if (!isAdmin)
            {
                var userRoles = UnitOfWork.ApplicationUsers.Get()
                                .Where(u => u.Id == userId)
                                .Select(u => u.Roles
                                .Select(r => r.Id))
                                .SingleOrDefault();

                allChannelsQuery = allChannelsQuery.Where(c => c.ApplicationRoles.Any(ar => userRoles.Contains(ar.Id)));
            }

            if (allChannelsQuery.Count() == 0)
            {
                return null;
            }

            var model = allChannelsQuery
                        .Include(c => c.Subject)
                        .Include(c => c.ChannelUsers)
                        .Include(c => c.ChannelRequests)
                        .Include(c => c.ApplicationRoles)
                        .Skip((pageNumber - 1) * pageSize).Take(pageSize)
                        .Select(c => Mapper.Map<ChannelVM>(c))
                        .ToList();

            return model;
        }

        public void Create(CreateChannelVM model, string creatorId)
        {
            ExecuteInTransaction(uow =>
            {
                var dbChannel = Mapper.Map<CreateChannelVM, Entities.Channel>(model);
                dbChannel.ApplicationRoles = uow.ApplicationRoles.Get().Where(r => model.Roles.Contains(r.Id)).ToList();
                if (dbChannel.AccessTypeId == 2)
                {
                    dbChannel.JoinCode = GenerateJoinCode(8);
                }
                uow.Channels.Insert(dbChannel);
                uow.SaveChanges();

                uow.ChannelUsers.Insert(new ChannelUser
                {
                    UserId = creatorId,
                    ChannelId = dbChannel.Id,
                    IsModerator = true
                });

                uow.SaveChanges();
            });
        }

        public List<SelectListItem> GetAllAccessTypes()
        {
            var accessTypes = Enum.GetValues(typeof(AccessTypeEnum)).Cast<AccessTypeEnum>();

            var selectListItems = accessTypes.Select(accessType => new SelectListItem
            {
                Value = ((int)accessType).ToString(),
                Text = accessType.ToString()
            }).ToList();

            return selectListItems;
        }

        public bool ChannelExists()
        {
            return UnitOfWork.Channels.Get().Any();
        }

        public Entities.Channel? GetChannelById(int? id)
        {
            return UnitOfWork.Channels?.Get().FirstOrDefault(e => e.Id == id);
        }

        public void AddNewChannel(Entities.Channel channel)
        {
            UnitOfWork.Channels.Insert(channel);
            UnitOfWork.SaveChanges();
        }

        public void UpdateChannel(EditChannelVM editChannel)
        {
            var channel = UnitOfWork.Channels.Get().FirstOrDefault(c => c.Id == editChannel.Id);

            var mappedChannel = Mapper.Map(editChannel, channel);

            UnitOfWork.Channels.Update(channel);
            UnitOfWork.SaveChanges();
        }

        public void DeleteChannel(Entities.Channel channel)
        {
            // before deleting the channel, we need to delete all the channel users and channel requests
            var channelUsers = UnitOfWork.ChannelUsers.Get().Where(cu => cu.ChannelId == channel.Id).ToList();
            for (int i = 0; i < channelUsers.Count; i++)
            {
                UnitOfWork.ChannelUsers.Delete(channelUsers[i]);
            }

            var channelRequests = UnitOfWork.ChannelRequests.Get().Where(cr => cr.ChannelId == channel.Id).ToList();
            for (int i = 0; i < channelRequests.Count; i++)
            {
                UnitOfWork.ChannelRequests.Delete(channelRequests[i]);
            }

            // delete the messages in the channel
            var messages = UnitOfWork.Messages.Get().Where(m => m.ChannelId == channel.Id).ToList();
            for (int i = 0; i < messages.Count; i++)
            {
                UnitOfWork.Messages.Delete(messages[i]);
            }

            // delete the entries in the ChannelRoles table that isn't declared as an entity
            var channelDb = UnitOfWork.Channels.Get().Include(c => c.ApplicationRoles).FirstOrDefault(c => c.Id == channel.Id);

            foreach (var role in channelDb.ApplicationRoles)
            {
                channelDb.ApplicationRoles.Remove(role);
            }

            UnitOfWork.SaveChanges();

            UnitOfWork.Channels.Delete(channel);

            UnitOfWork.SaveChanges();
        }

        public DetailsChannelVM? GetDetailsChannelVM(int? id)
        {
            var channel = UnitOfWork.Channels.Get()
                .Include(c => c.Subject)
                .Include(c => c.ChannelUsers)
                    .ThenInclude(cu => cu.User)
                .Include(c => c.ChannelRequests)
                    .ThenInclude(cr => cr.Requester)
                .Include(c => c.ApplicationRoles)
                .Where(c => c.Id == id)
                .Select(c => Mapper.Map<DetailsChannelVM>(c)).SingleOrDefault();

            return channel;
        }

        public void AddNewMember(int channelId, string userId)
        {
            UnitOfWork.ChannelUsers.Insert(new ChannelUser
            {
                UserId = userId,
                ChannelId = channelId,
                IsModerator = false
            });
            UnitOfWork.SaveChanges();
        }

        public void AcceptRequest(int channelId, string userId)
        {
            UnitOfWork.ChannelUsers.Insert(new ChannelUser
            {
                UserId = userId,
                ChannelId = channelId,
                IsModerator = false
            });
            var request = UnitOfWork.ChannelRequests.Get().SingleOrDefault(cr => cr.ChannelId == channelId && cr.RequesterId == userId);
            UnitOfWork.ChannelRequests.Delete(request);
            UnitOfWork.SaveChanges();
        }

        public void RejectRequest(int channelId, string userId)
        {
            var request = UnitOfWork.ChannelRequests.Get().SingleOrDefault(cr => cr.ChannelId == channelId && cr.RequesterId == userId);
            UnitOfWork.ChannelRequests.Delete(request);
            UnitOfWork.SaveChanges();
        }


        public void RemoveMember(int channelId, string userId)
        {
            var cu = UnitOfWork.ChannelUsers.Get().Where(cu => cu.ChannelId == channelId && cu.UserId == userId).FirstOrDefault();
            UnitOfWork.ChannelUsers.Delete(cu);
            UnitOfWork.SaveChanges();
        }

        public void PromoteMember(int channelId, string userId)
        {
            var cu = UnitOfWork.ChannelUsers.Get().Where(cu => cu.ChannelId == channelId && cu.UserId == userId).FirstOrDefault();
            cu.IsModerator = true;
            UnitOfWork.ChannelUsers.Update(cu);
            UnitOfWork.SaveChanges();
        }


        public List<SelectListItem> GetAllRoles()
        {
            var roles = new List<SelectListItem>();

            foreach (var role in UnitOfWork.ApplicationRoles.Get().OrderBy(s => s.Name))
            {
                roles.Add(new SelectListItem
                {
                    Value = role.Id.ToString(),
                    Text = role.Name.ToString()
                });
            }

            return roles;
        }

        private string GenerateJoinCode(int length)
        {
            string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKL" +
                "MNOPQRSTUVWXYZ1234567890";

            Random random = new Random();
            char[] result = new char[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = characters[random.Next(characters.Length)];
            }

            return new string(result);
        }

        public void AddChannelParticipationRequest(int channelId, string userId)
        {
            UnitOfWork.ChannelRequests.Insert(new ChannelRequest
            {
                RequesterId = userId,
                ChannelId = channelId,
                Date = DateTime.Now
            });
            UnitOfWork.SaveChanges();
        }

        public bool UserIsInChannel(string currentUserId, int channelId)
        {
            return UnitOfWork.ChannelUsers.Get().Any(cu => cu.UserId == currentUserId && cu.ChannelId == channelId);
        }

        public bool UserIsGroupModerator(string? currentUserId, int channelId)
        {
            return UnitOfWork.ChannelUsers.Get().Any(cu => cu.UserId == currentUserId && cu.ChannelId == channelId && cu.IsModerator);
        }

        public bool UserHasRequestedChannelMembership(string userId, int channelId)
        {
            return UnitOfWork.ChannelRequests.Get().Any(cr => cr.RequesterId == userId && cr.ChannelId == channelId);
        }

        public bool UserHasAtLeastOneRoleInCommonWithChannel(IEnumerable<string> currentUserRolesNames, List<string>? channelRolesIds)
        {
            var currentUserRolesIds = UnitOfWork.ApplicationRoles.Get()
                                        .Where(r => currentUserRolesNames.Contains(r.Name))
                                        .Select(r => r.Id).ToList();

            return currentUserRolesIds.Intersect(channelRolesIds).Any();
        }
    }
}
