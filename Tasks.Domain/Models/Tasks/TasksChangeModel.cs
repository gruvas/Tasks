using T = Tasks.Domain.Models.Tasks;
using U = Tasks.Domain.Models.ContractorInitiator;

namespace Tasks.Domain.Models.Tasks
{
    public class TasksChangeModel
    {
        public T.Task Task { get; set; }
        public List<int> UserIds { get; set; }
        public U.ContractorInitiator ContractorInitiator { get; set; }
    }
}
