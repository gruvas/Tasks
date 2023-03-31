using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.DAL.Repositories.Interface;
using Tasks.Domain.Models.ContractorInitiator;
using Tasks.Domain.Models.Tasks;



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
}
