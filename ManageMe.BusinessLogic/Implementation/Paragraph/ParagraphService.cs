using ManageMe.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMe.BusinessLogic
{
    public class ParagraphService : BaseService
    {
        private readonly DetailService _detailService;

        public ParagraphService(ServiceDependencies serviceDependencies, DetailService detailService) : base(serviceDependencies)
        {
            _detailService = detailService;
        }

        public List<int> GetParagraphDetailIds(int paragraphId)
        {
            var paragraph = UnitOfWork.Paragraphs.Get().Include(x => x.Details).FirstOrDefault(x => x.Id == paragraphId);

            if (paragraph == null)
            {
                return new List<int>();
            }

            var detailIds = paragraph.Details.Select(x => x.Id).ToList();

            return detailIds;
        }

        public DetailsParagraphVM? GetParagraphById(int id)
        {
            var paragraph = UnitOfWork.Paragraphs.Get()
                .FirstOrDefault(x => x.Id == id);

            if (paragraph == null)
            {
                return null;
            }

            var paragraphVM = Mapper.Map<DetailsParagraphVM>(paragraph);

            var detailIds = GetParagraphDetailIds(paragraph.Id);

            foreach (var detailId in detailIds)
            {
                var detailVM = _detailService.GetDetailById(detailId);

                if (detailVM != null)
                {
                    paragraphVM.Details.Add(detailVM);
                }
            }

            return paragraphVM;
        }

        public bool UpdateParagraph(EditParagraphVM editParagraphVM)
        {
            try
            {
                var paragraph = Mapper.Map<Paragraph>(editParagraphVM);

                UnitOfWork.Paragraphs.Update(paragraph);
                UnitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CreateParagraph(CreateParagraphVM createParagraphVM)
        {
            try
            {
                var paragraph = Mapper.Map<Paragraph>(createParagraphVM);

                UnitOfWork.Paragraphs.Insert(paragraph);
                UnitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteParagraph(int id)
        {
            try
            {
                var paragraph = UnitOfWork.Paragraphs.Get().FirstOrDefault(x => x.Id == id);
                UnitOfWork.Paragraphs.Delete(paragraph);
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
