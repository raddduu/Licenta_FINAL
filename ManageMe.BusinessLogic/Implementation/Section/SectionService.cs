using ManageMe.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMe.BusinessLogic
{
    public class SectionService : BaseService
    {
        private readonly ParagraphService _paragraphService;

        public SectionService(ServiceDependencies serviceDependencies, ParagraphService paragraphService) : base(serviceDependencies)
        {
            _paragraphService = paragraphService;
        }

        public List<DetailsSectionVM> GetSectionDescendants(int parentSectionId)
        {
            var sections = UnitOfWork.Sections.Get().Where(x => x.ParentSectionId == parentSectionId).ToList();

            var sectionVMs = new List<DetailsSectionVM>();

            foreach (var section in sections)
            {
                var sectionVM = GetSectionById(section.Id);

                if (sectionVM != null)
                {
                    sectionVMs.Add(sectionVM);
                }
            }

            return sectionVMs;
        }

        public List<DetailsSectionVM> GetSectionChildren(int parentSectionId)
        {
            var sections = UnitOfWork.Sections.Get().Where(x => x.ParentSectionId == parentSectionId).ToList();

            var sectionVMs = Mapper.Map<List<DetailsSectionVM>>(sections);

            return sectionVMs;
        }

        public List<int> GetSectionParagraphIds(int sectionId)
        {
            var section = UnitOfWork.Sections.Get().Include(x => x.Paragraphs).FirstOrDefault(x => x.Id == sectionId);

            if (section == null)
            {
                return new List<int>();
            }

            var paragraphIds = section.Paragraphs.Select(x => x.Id).ToList();

            return paragraphIds;
        }

        public DetailsSectionVM? GetSectionById(int id)
        {
            var section = UnitOfWork.Sections.Get().FirstOrDefault(x => x.Id == id);

            if (section == null)
            {
                return null;
            }

            var sectionVM = Mapper.Map<DetailsSectionVM>(section);

            sectionVM.ChildrenSections = GetSectionDescendants(section.Id);

            var paragraphIds = GetSectionParagraphIds(section.Id);

            foreach (var paragraphId in paragraphIds)
            {
                var paragraphVM = _paragraphService.GetParagraphById(paragraphId);

                if (paragraphVM != null)
                {
                    sectionVM.Paragraphs.Add(paragraphVM);
                }
            }

            return sectionVM;
        }

        public bool UpdateSection(EditSectionVM editSectionVM)
        {
            try
            {
                var section = Mapper.Map<Section>(editSectionVM);

                UnitOfWork.Sections.Update(section);
                UnitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CreateSection(CreateSectionVM createSectionVM)
        {
            try
            {
                var section = Mapper.Map<Section>(createSectionVM);

                section.ChapterId = section.ChapterId == -1 ? null : section.ChapterId;
                section.ParentSectionId = section.ParentSectionId == -1 ? null : section.ParentSectionId;

                UnitOfWork.Sections.Insert(section);
                UnitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteSection(int id)
        {
            try
            {
                var section = UnitOfWork.Sections.Get().FirstOrDefault(x => x.Id == id);
                UnitOfWork.Sections.Delete(section);
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
