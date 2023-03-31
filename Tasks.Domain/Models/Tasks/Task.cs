using System.ComponentModel.DataAnnotations.Schema;

namespace Tasks.Domain.Models.Tasks
{
    [Table("tasks", Schema = "main")]
    public class Task
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
        public int ContractorInitiatorId { get; set; }

        public Task()
        {
            CreatedDate = DateTimeOffset.UtcNow;
        }
    }
}
