using AutoMapper;
using ManageMe.BusinessLogic;
using ManageMe.Common;
using ManageMe.DataAccess;
using ManageMe.Entities;
using ManageMe.Entities.Enums;
using Microsoft.AspNetCore.Http;

namespace ManageMe.BusinessLogic
{
    public class ChannelProfile : Profile
    {
        public ChannelProfile()
        {
            CreateMap<ChannelUser, DetailsChannelUserVM>()
                .ForMember(dcuvm => dcuvm.Id, dcuvm => dcuvm.MapFrom(cu => cu.UserId))
                .ForMember(dcuvm => dcuvm.Name, dcuvm => dcuvm.MapFrom(cu => $"{cu.User.FirstName} {cu.User.LastName}"))
                .ForMember(dcuvm => dcuvm.Email, dcuvm => dcuvm.MapFrom(cu => cu.User.Email))
                .ForMember(dcuvm => dcuvm.ProfilePicture, dcuvm => dcuvm.Ignore())
                .ForMember(dcuvm => dcuvm.ProfilePictureByteArray, dcuvm => dcuvm.MapFrom(cu => cu.User.ProfilePicture));

            CreateMap<ChannelRequest, ChannelRequesterUserVM>()
                .ForMember(dcuvm => dcuvm.Id, dcuvm => dcuvm.MapFrom(cu => cu.RequesterId))
                .ForMember(dcuvm => dcuvm.Name, dcuvm => dcuvm.MapFrom(cu => $"{cu.Requester.FirstName} {cu.Requester.LastName}"))
                .ForMember(dcuvm => dcuvm.Email, dcuvm => dcuvm.MapFrom(cu => cu.Requester.Email))
                .ForMember(dcuvm => dcuvm.ProfilePicture, dcuvm => dcuvm.Ignore())
                .ForMember(dcuvm => dcuvm.ProfilePictureByteArray, dcuvm => dcuvm.MapFrom(cu => cu.Requester.ProfilePicture));

            CreateMap<Channel, ChannelVM>()
                .ForMember(cvm => cvm.Subject, cvm => cvm.MapFrom(c => c.Subject.Name))
                .ForMember(cvm => cvm.ChannelMembers
                            , cvm => cvm.MapFrom(c => c.ChannelUsers))
                .ForMember(cvm => cvm.ChannelRequesters
                            , cvm => cvm.MapFrom(c => c.ChannelRequests))
                .ForMember(cvm => cvm.AccessType, cvm => cvm.MapFrom(c => Enum.GetName(typeof(AccessTypeEnum), c.AccessTypeId)))
                .ForMember(cvm => cvm.ChannelPic, cvm => cvm.Ignore())
                .ForMember(cvm => cvm.ChannelPicByteArray, cvm => cvm.MapFrom(c => c.ChannelPic))
                .ForMember(cvm => cvm.Roles, cvm => cvm.MapFrom(c => c.ApplicationRoles.Select(r => r.Name).ToList()));

            CreateMap<Channel, DetailsChannelVM>()
                .ForMember(cvm => cvm.Subject, cvm => cvm.MapFrom(c => c.Subject.Name))
                .ForMember(cvm => cvm.ChannelMembers
                           , cvm => cvm.MapFrom(c => c.ChannelUsers))
                .ForMember(cvm => cvm.ChannelRequesters
                           , cvm => cvm.MapFrom(c => c.ChannelRequests))
                .ForMember(cvm => cvm.AccessType, cvm => cvm.MapFrom(c => Enum.GetName(typeof(AccessTypeEnum), c.AccessTypeId)))
                .ForMember(cvm => cvm.ChannelPic, cvm => cvm.Ignore())
                .ForMember(cvm => cvm.ChannelPicByteArray, cvm => cvm.MapFrom(c => c.ChannelPic));

            CreateMap<CreateChannelVM, Channel>()
                .ForMember(c => c.ChannelPic, c => c.MapFrom(cvm => cvm.ChannelPicByteArray));

            CreateMap<EditChannelVM, Channel>()
                .ForMember(c => c.ChannelPic, c => c.MapFrom(cvm => cvm.ChannelPicByteArray));
        }
    }
}
