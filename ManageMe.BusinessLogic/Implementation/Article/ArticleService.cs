using ManageMe.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMe.BusinessLogic
{
    public class ArticleService : BaseService
    {
        private readonly ProvisionService _provisionService;

        public ArticleService(ServiceDependencies serviceDependencies, ProvisionService provisionService) : base(serviceDependencies)
        {
            _provisionService = provisionService;
        }

        public List<int> GetArticleProvisionIds(int articleId)
        {
            var article = UnitOfWork.Articles.Get().Include(x => x.Provisions).FirstOrDefault(x => x.Id == articleId);

            if (article == null)
            {
                return new List<int>();
            }

            var provisionIds = article.Provisions.Select(x => x.Id).ToList();

            return provisionIds;
        }

        public DetailsArticleVM? GetArticleById(int id)
        {
            var article = UnitOfWork.Articles.Get()
                .FirstOrDefault(x => x.Id == id);

            if (article == null)
            {
                return null;
            }

            var articleVM = Mapper.Map<DetailsArticleVM>(article);

            var provisionIds = GetArticleProvisionIds(article.Id);

            foreach (var provisionId in provisionIds)
            {
                var provisionVM = _provisionService.GetProvisionById(provisionId);

                if (provisionVM != null)
                {
                    articleVM.Provisions.Add(provisionVM);
                }
            }

            return articleVM;
        }

        public bool UpdateArticle(EditArticleVM editArticleVM)
        {
            try
            {
                var Article = Mapper.Map<Article>(editArticleVM);

                UnitOfWork.Articles.Update(Article);
                UnitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CreateArticle(CreateArticleVM createArticleVM)
        {
            try
            {
                var Article = Mapper.Map<Article>(createArticleVM);

                UnitOfWork.Articles.Insert(Article);
                UnitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteArticle(int id)
        {
            try
            {
                var Article = UnitOfWork.Articles.Get().FirstOrDefault(x => x.Id == id);
                UnitOfWork.Articles.Delete(Article);
                UnitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
