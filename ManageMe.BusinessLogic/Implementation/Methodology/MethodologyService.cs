using ManageMe.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMe.BusinessLogic
{
    public class MethodologyService : BaseService
    {
        private readonly ChapterService _chapterService;

        public MethodologyService(ServiceDependencies serviceDependencies, ChapterService chapterService) : base(serviceDependencies)
        {
            _chapterService = chapterService;
        }

        public List<int> GetMethodologyChapterIds(int methodologyId)
        {
            var methodology = UnitOfWork.Methodologies.Get().Include(x => x.Chapters).FirstOrDefault(x => x.Id == methodologyId);

            if (methodology == null)
            {
                return new List<int>();
            }

            var chapterIds = methodology.Chapters.Select(x => x.Id).ToList();

            return chapterIds;
        }

        public DetailsMethodologyVM? GetMethodologyById(int id)
        {
            var methodology = UnitOfWork.Methodologies.Get()
                .FirstOrDefault(x => x.Id == id);

            if (methodology == null)
            {
                return null;
            }

            var methodologyVM = Mapper.Map<DetailsMethodologyVM>(methodology);

            var chapterIds = GetMethodologyChapterIds(methodology.Id);

            foreach (var chapterId in chapterIds)
            {
                var chapterVM = _chapterService.GetChapterById(chapterId);

                if (chapterVM != null)
                {
                    methodologyVM.Chapters.Add(chapterVM);
                }
            }

            return methodologyVM;
        }

        public List<IndexMethodologyVM> GetIndexMethodologies()
        {
            var methodologies = UnitOfWork.Methodologies.Get().ToList();

            var methodologyVMs = Mapper.Map<List<IndexMethodologyVM>>(methodologies);

            return methodologyVMs;
        }

        public bool UpdateMethodology(EditMethodologyVM editMethodologyVM)
        {
            try
            {
                var methodology = Mapper.Map<Methodology>(editMethodologyVM);

                UnitOfWork.Methodologies.Update(methodology);
                UnitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CreateMethodology(CreateMethodologyVM createMethodologyVM)
        {
            try
            {
                var methodology = Mapper.Map<Methodology>(createMethodologyVM);

                UnitOfWork.Methodologies.Insert(methodology);
                UnitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteMethodology(int id)
        {
            try
            {
                var methodology = UnitOfWork.Methodologies.Get().FirstOrDefault(x => x.Id == id);
                UnitOfWork.Methodologies.Delete(methodology);
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
