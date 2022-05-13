using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Core.Common.Utilities
{
    public class MessagesHub : Hub
    {
        [HubName("PushNotify")]
        public class NotificationHub : Hub
        {
            public void Send(object sender, string message)
            {
                // Broadcast the message to all clients except the sender.
                Clients.Others.broadcastMessage(sender, message);
            }
            public void SendTyping(object sender)
            {
                // Broadcast the typing notification to all clients except the sender.
                Clients.Others.typing(sender);
            }
        }
    }
}