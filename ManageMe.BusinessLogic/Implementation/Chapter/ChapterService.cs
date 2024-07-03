using ManageMe.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMe.BusinessLogic
{
    public class ChapterService : BaseService
    {
        private readonly SectionService _sectionService;
        private readonly ArticleService _articleService;

        public ChapterService(ServiceDependencies serviceDependencies, SectionService sectionService, ArticleService articleService) : base(serviceDependencies)
        {
            _sectionService = sectionService;
            _articleService = articleService;
        }

        public List<int> GetChapterSectionIds(int chapterId)
        {
            var chapter = UnitOfWork.Chapters.Get().Include(x => x.Sections).FirstOrDefault(x => x.Id == chapterId);

            if (chapter == null)
            {
                return new List<int>();
            }

            var sectionIds = chapter.Sections.Select(x => x.Id).ToList();

            return sectionIds;
        }

        public List<int> GetChapterArticleIds(int chapterId)
        {
            var chapter = UnitOfWork.Chapters.Get().Include(x => x.Articles).FirstOrDefault(x => x.Id == chapterId);

            if (chapter == null)
            {
                return new List<int>();
            }

            var articleIds = chapter.Articles.Select(x => x.Id).ToList();

            return articleIds;
        }

        public DetailsChapterVM? GetChapterById(int id)
        {
            var chapter = UnitOfWork.Chapters.Get()
                .FirstOrDefault(x => x.Id == id);

            if (chapter == null)
            {
                return null;
            }

            var chapterVM = Mapper.Map<DetailsChapterVM>(chapter);

            var sectionIds = GetChapterSectionIds(chapter.Id);

            foreach (var sectionId in sectionIds)
            {
                var sectionVM = _sectionService.GetSectionById(sectionId);

                if (sectionVM != null)
                {
                    chapterVM.Sections.Add(sectionVM);
                }
            }

            var articleIds = GetChapterArticleIds(chapter.Id);

            foreach (var articleId in articleIds)
            {
                var articleVM = _articleService.GetArticleById(articleId);

                if (articleVM != null)
                {
                    chapterVM.Articles.Add(articleVM);
                }
            }

            return chapterVM;
        }

        public bool UpdateChapter(EditChapterVM editChapterVM)
        {
            try
            {
                var chapter = Mapper.Map<Chapter>(editChapterVM);

                UnitOfWork.Chapters.Update(chapter);
                UnitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CreateChapter(CreateChapterVM createChapterVM)
        {
            try
            {
                var chapter = Mapper.Map<Chapter>(createChapterVM);

                UnitOfWork.Chapters.Insert(chapter);
                UnitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteChapter(int id)
        {
            try
            {
                var chapter = UnitOfWork.Chapters.Get().FirstOrDefault(x => x.Id == id);
                UnitOfWork.Chapters.Delete(chapter);
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
