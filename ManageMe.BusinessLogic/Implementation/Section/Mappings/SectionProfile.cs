using AutoMapper;
using ManageMe.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMe.BusinessLogic
{
    public class SectionProfile : Profile
    {
        public SectionProfile()
        {
            CreateMap<Section, EditSectionVM>();
            CreateMap<Section, DetailsSectionVM>();

            CreateMap<EditSectionVM, Section>();
            CreateMap<DetailsSectionVM, Section>();

            CreateMap<CreateSectionVM, Section>();
        }
    }
}
