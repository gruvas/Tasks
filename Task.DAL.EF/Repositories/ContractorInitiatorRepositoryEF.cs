using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.DAL.EF;
using Tasks.DAL.Repositories.Interface;
using Tasks.Domain.Models.ContractorInitiator;

public class ContractorInitiatorRepositoryEF : IContractorInitiatorRepository
{
    private readonly PostgreeContext _dbContext;

    public ContractorInitiatorRepositoryEF(PostgreeContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<ContractorInitiator> GetAll()
    {
        return _dbContext.ContractorInitiator.ToList();
    }

    public ContractorInitiator GetById(int id)
    {
        return _dbContext.ContractorInitiator.FirstOrDefault(ci => ci.Id == id);
    }
}