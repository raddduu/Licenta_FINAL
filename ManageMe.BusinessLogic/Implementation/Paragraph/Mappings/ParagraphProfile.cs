using AutoMapper;
using ManageMe.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMe.BusinessLogic
{
    public class ParagraphProfile : Profile
    {
        public ParagraphProfile()
        {
            CreateMap<Paragraph, EditParagraphVM>();
            CreateMap<Paragraph, DetailsParagraphVM>();

            CreateMap<EditParagraphVM, Paragraph>();
            CreateMap<DetailsParagraphVM, Paragraph>();

            CreateMap<CreateParagraphVM, Paragraph>();
        }
    }
}
