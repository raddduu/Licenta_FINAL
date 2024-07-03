using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManageMe.Entities.Entities;

namespace ManageMe.BusinessLogic
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<Article, EditArticleVM>();
            CreateMap<Article, DetailsArticleVM>();

            CreateMap<EditArticleVM, Article>();
            CreateMap<DetailsArticleVM, Article>();

            CreateMap<CreateArticleVM, Article>();
        }
    }
}
