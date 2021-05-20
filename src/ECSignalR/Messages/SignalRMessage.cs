using Microsoft.AspNetCore.SignalR;

namespace ECSignalR.Messages
{
    public record SignalRMessage
    {
        public object Message { get; init; }

        public IClientProxy Caller { get; init; }
    }
}