namespace MediatRDemo.Models
{
    public class Request<T>
    {
        public List<T> Payloads { get; set; }
    }
}
