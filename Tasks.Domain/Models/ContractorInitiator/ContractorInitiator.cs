using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Domain.Models.ContractorInitiator
{
    internal class ContractorInitiator
    {
        public int Id { get; set; }
        public int ContractorId { get; set; }
        public int InitiatorId { get; set; }
    }
}
