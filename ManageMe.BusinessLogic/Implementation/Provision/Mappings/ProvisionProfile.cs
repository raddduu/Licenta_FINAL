using AutoMapper;
using ManageMe.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMe.BusinessLogic
{
    public class ProvisionProfile : Profile
    {
        public ProvisionProfile()
        {
            CreateMap<Provision, EditProvisionVM>();
            CreateMap<Provision, DetailsProvisionVM>();

            CreateMap<EditProvisionVM, Provision>();
            CreateMap<DetailsProvisionVM, Provision>();

            CreateMap<CreateProvisionVM, Provision>();
        }   

    }
}
