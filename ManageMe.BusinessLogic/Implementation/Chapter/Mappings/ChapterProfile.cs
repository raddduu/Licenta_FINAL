using AutoMapper;
using ManageMe.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMe.BusinessLogic
{
    public class ChapterProfile : Profile
    {
        public ChapterProfile()
        {
            CreateMap<Chapter, EditChapterVM>();
            CreateMap<Chapter, DetailsChapterVM>();

            CreateMap<EditChapterVM, Chapter>();
            CreateMap<DetailsChapterVM, Chapter>();

            CreateMap<CreateChapterVM, Chapter>();
        }
    }
}
