using Dapper;
using Npgsql;
using System.Data;
using Tasks.DAL.Repositories.Interface;
using Tasks.Domain.Models.ContractorInitiator;


public class ContractorInitiatorRepository : IContractorInitiatorRepository
{
    string connectionString = "";

    public ContractorInitiatorRepository(string connection)
    {
        connectionString = connection;
    }

    public List<ContractorInitiator> GetAll()
    {
        using (IDbConnection db = new NpgsqlConnection(connectionString))
        {
            return db.Query<ContractorInitiator>("SELECT * FROM main.contractor_initiator").ToList();
        }
    }

    public ContractorInitiator GetById(int id)
    {
        using (IDbConnection db = new NpgsqlConnection(connectionString))
        {
            return db.QueryFirstOrDefault<ContractorInitiator>("SELECT * FROM main.contractor_initiator WHERE \"Id\" = @Id", new { Id = id });
        }
    }
}
