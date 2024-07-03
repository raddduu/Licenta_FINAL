using ManageMe.Entities.Entities;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMe.BusinessLogic
{
    public class DetailService : BaseService
    {
        public DetailService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {

        }

        public List<DetailsDetailVM> GetDetailDescendants(int parentDetailId)
        {
            var details = UnitOfWork.Details.Get().Where(x => x.ParentDetailId == parentDetailId).ToList();

            var detailVMs = new List<DetailsDetailVM>();

            foreach (var detail in details)
            {
                var detailVM = Mapper.Map<DetailsDetailVM>(detail);
                detailVM.ChildrenDetails = GetDetailDescendants(detail.Id);

                detailVMs.Add(detailVM);
            }

            return detailVMs;
        }

        public List<DetailsDetailVM> GetDetailChildren(int parentDetailId)
        {
            var details = UnitOfWork.Details.Get().Where(x => x.ParentDetailId == parentDetailId).ToList();

            var detailVMs = Mapper.Map<List<DetailsDetailVM>>(details);

            return detailVMs;
        }

        public DetailsDetailVM? GetDetailById(int id)
        {
            var detail = UnitOfWork.Details.Get().FirstOrDefault(x => x.Id == id);

            if (detail == null)
            {
                return null;
            }

            var detailVM = Mapper.Map<DetailsDetailVM>(detail);

            detailVM.ChildrenDetails = GetDetailDescendants(detail.Id);

            return detailVM;
        }

        public bool UpdateDetail(EditDetailVM editDetailVM)
        {
            try
            {
                var detail = Mapper.Map<Detail>(editDetailVM);

                UnitOfWork.Details.Update(detail);
                UnitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CreateDetail(CreateDetailVM createDetailVM)
        {
            try
            {
                var detail = Mapper.Map<Detail>(createDetailVM);

                detail.ParagraphId = detail.ParagraphId == -1 ? null : detail.ParagraphId;
                detail.ParentDetailId = detail.ParentDetailId == -1 ? null : detail.ParentDetailId;

                UnitOfWork.Details.Insert(detail);
                UnitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteDetail(int id)
        {
            try
            {
                var detail = UnitOfWork.Details.Get().FirstOrDefault(x => x.Id == id);
                UnitOfWork.Details.Delete(detail);
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
