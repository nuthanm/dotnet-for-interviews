namespace MediatRDemo.Models
{
    public class Delete
    {
        public List<DeletePayLoad> Payloads { get; set; }
    }

    public class DeletePayLoad
    {
        public int RequestId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
