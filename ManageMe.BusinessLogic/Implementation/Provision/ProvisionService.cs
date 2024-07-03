using ManageMe.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ManageMe.BusinessLogic
{
    public class ProvisionService : BaseService
    {
        public ProvisionService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {

        }

        public List<DetailsProvisionVM> GetProvisionDescendants(int parentProvisionId)
        {
            var provisions = UnitOfWork.Provisions.Get().Where(x => x.ParentProvisionId == parentProvisionId).ToList();

            var provisionVMs = new List<DetailsProvisionVM>();

            foreach (var provision in provisions)
            {
                var provisionVM = Mapper.Map<DetailsProvisionVM>(provision);
                provisionVM.ChildrenProvisions = GetProvisionDescendants(provision.Id);

                provisionVMs.Add(provisionVM);
            }

            return provisionVMs;
        }

        public List<DetailsProvisionVM> GetProvisionChildren(int parentProvisionId)
        {
            var provisions = UnitOfWork.Provisions.Get().Where(x => x.ParentProvisionId == parentProvisionId).ToList();

            var provisionVMs = Mapper.Map<List<DetailsProvisionVM>>(provisions);

            return provisionVMs;
        }

        public DetailsProvisionVM? GetProvisionById(int id)
        {
            var provision = UnitOfWork.Provisions.Get().FirstOrDefault(x => x.Id == id);

            if (provision == null)
            {
                return null;
            }

            var provisionVM = Mapper.Map<DetailsProvisionVM>(provision);

            provisionVM.ChildrenProvisions = GetProvisionDescendants(provision.Id);

            return provisionVM;
        }

        public bool UpdateProvision(EditProvisionVM editProvisionVM)
        {
            try
            {
                var provision = Mapper.Map<Provision>(editProvisionVM);

                UnitOfWork.Provisions.Update(provision);
                UnitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CreateProvision(CreateProvisionVM createProvisionVM)
        {
            try
            {
                var provision = Mapper.Map<Provision>(createProvisionVM);

                provision.ParentProvisionId = provision.ParentProvisionId == -1 ? null : provision.ParentProvisionId;
                provision.ArticleId = provision.ArticleId == -1 ? null : provision.ArticleId;

                UnitOfWork.Provisions.Insert(provision);
                UnitOfWork.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteProvision(int id)
        {
            try
            {
                var provision = UnitOfWork.Provisions.Get().FirstOrDefault(x => x.Id == id);
                UnitOfWork.Provisions.Delete(provision);
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