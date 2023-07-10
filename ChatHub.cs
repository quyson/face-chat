using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace facechat
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            Console.WriteLine(message);
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public override async Task OnConnectedAsync()
        {
            string connectionId = Context.ConnectionId;
            string username = Context.User.Identity.Name;

            await Clients.Others.SendAsync("JoinUser", username);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string connectionId = Context.ConnectionId;
            string username = Context.User.Identity.Name;

            await Clients.Others.SendAsync("LeaveUser", username);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
