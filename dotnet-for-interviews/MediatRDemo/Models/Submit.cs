namespace MediatRDemo.Models
{
    public class SubmitPayLoad
    {
        public int RequestId { get; set; }

        public bool IsReinitate { get; set; }

        public bool IsRequestProcess { get; set; }

        public string BlobUrl { get; set; }
    }
}
