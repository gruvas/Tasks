using T = Tasks.Domain.Models.ContractorInitiator;

namespace Tasks.Domain.Models.Tasks
{
    public class TaskContractorInitiator
    {
        public Task Task { get; set; }
        public T.ContractorInitiator СontractorInitiator { get; set; }
    }
}
