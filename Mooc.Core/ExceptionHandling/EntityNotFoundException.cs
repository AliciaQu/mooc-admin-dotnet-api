namespace Mooc.Core.ExceptionHandling;

public class EntityNotFoundException : MoocException
{
    public override string Code { get; set; } = "100001";
    public EntityNotFoundException(string message) : base(message)
    {
        this.ResponseMessage = message;
    }

    public EntityNotFoundException(string message, string responseMessage) : base(message)
    {
        this.ResponseMessage = responseMessage;
    }
}
