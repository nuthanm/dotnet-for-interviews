namespace MediatRDemo.Models
{
    public class InActive
    {
        public List<InActivePayLoad> Payloads { get; set; }
    }

    public class InActivePayLoad
    {
        public int RequestId { get; set; }

        public bool IsInactive { get; set; }
    }
}
