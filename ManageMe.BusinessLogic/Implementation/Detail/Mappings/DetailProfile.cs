using AutoMapper;
using ManageMe.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMe.BusinessLogic
{
    public class DetailProfile : Profile
    {
        public DetailProfile()
        {
            CreateMap<Detail, EditDetailVM>();
            CreateMap<Detail, DetailsDetailVM>();

            CreateMap<EditDetailVM, Detail>();
            CreateMap<DetailsDetailVM, Detail>();

            CreateMap<CreateDetailVM, Detail>();
        }
    }
}
