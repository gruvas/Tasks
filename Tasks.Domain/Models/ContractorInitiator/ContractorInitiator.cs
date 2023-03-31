
using System.ComponentModel.DataAnnotations.Schema;

namespace Tasks.Domain.Models.ContractorInitiator
{
    [Table("contractor_initiator", Schema = "main")]
    public class ContractorInitiator
    {
        public int Id { get; set; }
        public int ContractorId { get; set; }
        public int InitiatorId { get; set; }
    }
}
