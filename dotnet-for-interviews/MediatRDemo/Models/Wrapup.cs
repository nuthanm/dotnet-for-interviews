namespace MediatRDemo.Models
{
    public class Wrapup
    {
        public List<WrapupPayLoad> Payloads { get; set; }
    }

    public class WrapupPayLoad
    {
        public int RequestId { get; set; }

        public bool IsWrapup { get; set; }
    }
}
