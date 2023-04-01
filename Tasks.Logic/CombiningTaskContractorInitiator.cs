using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Models.ContractorInitiator;
using Tasks.Domain.Models.Tasks;
using Task = Tasks.Domain.Models.Tasks.Task;

namespace Tasks.Logic
{
    public class Combining
    {
        public static List<TasksChangeModel> TaskContractorInitiator(List<Task> tasks, 
            List<ContractorInitiator> contractorInitiator)
        {
            var taskContractorInitiator = new List<TasksChangeModel>();

            for (int i = 0; i < tasks.Count; i++)
            {
                for (int ii = 0; ii < contractorInitiator.Count; ii++)
                {
                    if (tasks[i].ContractorInitiatorId == contractorInitiator[ii].Id)
                    {
                        taskContractorInitiator.Add(new TasksChangeModel
                        {
                            Task = tasks[i],
                            ContractorInitiator = contractorInitiator[ii]
                        });
                    }
                }
            }

            return taskContractorInitiator;
        }
    }
}
