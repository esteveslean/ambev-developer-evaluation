namespace Ambev.DeveloperEvaluation.MessageBroker;

public interface IBaseMessage
{
    public string Msg { get; set; }
    public bool Success { get; set; }
    public DateTime CreatedAt { get; set; }
}