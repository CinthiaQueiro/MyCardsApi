namespace Domain.Entities
{
    public class Message<T>
    {
        public bool IsSuccess { get; set; }

        public string ReturnMessage { get; set; }

        public T Data { get; set; }

    }
}