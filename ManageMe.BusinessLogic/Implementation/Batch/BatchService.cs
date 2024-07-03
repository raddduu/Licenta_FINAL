using ManageMe.Entities.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ManageMe.BusinessLogic
{
    public class BatchService : BaseService
    {
        public BatchService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
        }

        public bool EditBatch(BatchCreateModel newBatch)
        {
            var batch = UnitOfWork.Batches.Get()
                                            .Where(b => b.Id == newBatch.Id)
                                            .SingleOrDefault();

            if (batch == null)
            {
                return false;
            }

            try
            {
                UnitOfWork.Batches.Update(batch);
                UnitOfWork.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddBatch(BatchCreateModel batch)
        {
            var batchExists = UnitOfWork.Batches.Get()
                                                    .Where(b => b.Id == batch.Id)
                                                    .SingleOrDefault();

            if (batchExists != null)
            {
                return false;
            }

            try
            {
                var dbBatch = Mapper.Map<Batch>(batch);
                UnitOfWork.Batches.Insert(dbBatch);
                UnitOfWork.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteBatch(int id)
        {
            try
            {
                var batch = UnitOfWork.Batches.Get().SingleOrDefault(b => b.Id == id);

                if (batch == null)
                {
                    return false;
                }

                UnitOfWork.Batches.Delete(batch);
                UnitOfWork.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<SelectListItem> GetAllBatches()
        {
            var batches = UnitOfWork.Batches.Get();

            var selectListItems = batches.Select(batch => new SelectListItem
            {
                Value = batch.Id.ToString(),
                Text = batch.Number
            }).ToList();

            return selectListItems;
        }

        public BatchVM GetBatchVM(int id)
        {
            var batch = UnitOfWork.Batches.Get()
                .Where(b => b.Id == id)
                .Include(b => b.StudyDomain)
                .SingleOrDefault();

            var batchVM = Mapper.Map<BatchVM>(batch);

            return batchVM;
        }

        public List<BatchVM> GetBatchVMs()
        {
            var batches = UnitOfWork.Batches.Get()
                .Include(b => b.StudyDomain)
                .ToList();

            var batchVMs = Mapper.Map<List<BatchVM>>(batches);

            return batchVMs;
        }
    }
}