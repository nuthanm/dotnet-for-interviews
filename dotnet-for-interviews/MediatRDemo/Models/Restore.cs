namespace MediatRDemo.Models
{
    public class Restore
    {
        public List<RestorePayLoad> Payloads { get; set; }
    }

    public class RestorePayLoad
    {
        public int RequestId { get; set; }

        public bool IsArchive { get; set; }
    }
}
