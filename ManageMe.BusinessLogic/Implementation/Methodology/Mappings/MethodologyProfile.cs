using AutoMapper;
using ManageMe.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMe.BusinessLogic
{
    public class MethodologyProfile : Profile
    {
        public MethodologyProfile()
        {
            CreateMap<Methodology, EditMethodologyVM>();
            CreateMap<Methodology, DetailsMethodologyVM>();

            CreateMap<EditMethodologyVM, Methodology>();
            CreateMap<DetailsMethodologyVM, Methodology>();

            CreateMap<CreateMethodologyVM, Methodology>();

            CreateMap<Methodology, IndexMethodologyVM>();
        }
    }
}
