using Tasks.Domain.Models.ContractorInitiator;

namespace Tasks.DAL.Repositories.Interface
{
    public interface IContractorInitiatorRepository
    {
        List<ContractorInitiator> GetAll();
    }
}
