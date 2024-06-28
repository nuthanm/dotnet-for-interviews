namespace MediatRDemo.Models
{
    public class LegalHoldPayLoad
    {
        public int RequestId { get; set; }

        public bool IsLegalHold { get; set; }

        public bool IsLifted { get; set; }
    }
}
