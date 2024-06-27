namespace MediatRDemo.Models
{
    public class LegalHold
    {
        public List<LegalHoldPayLoad> Payloads { get; set; }
    }

    public class LegalHoldPayLoad
    {
        public int RequestId { get; set; }

        public bool IsLegalHold { get; set; }

        public bool IsLifted { get; set; }
    }
}
