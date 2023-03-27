namespace Tasks.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public int ContractorId { get; set; }
        public int InitiatoridId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
