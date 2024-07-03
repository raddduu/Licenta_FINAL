using AutoMapper;
using ManageMe.BusinessLogic;
using ManageMe.Common;
using ManageMe.DataAccess;
using ManageMe.Entities;
using ManageMe.Entities.Enums;
using Microsoft.AspNetCore.Http;

namespace ManageMe.BusinessLogic
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, IndexMessageVM>();

            CreateMap<CreateMessageVM, Message>();
        }
    }
}
