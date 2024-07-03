using AutoMapper;
using ManageMe.DataAccess;

namespace ManageMe.BusinessLogic
{
    public class ServiceDependencies
    {
        public IMapper Mapper { get; set; }
        public UnitOfWork UnitOfWork { get; set; }
        public ServiceDependencies(IMapper mapper, UnitOfWork unitOfWork)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }
    }
}
