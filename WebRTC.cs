using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Threading.Tasks;

namespace facechat
{
    public class WebRTC : Hub
    {
        private static readonly ConcurrentDictionary<string, string> ActiveConnections = new ConcurrentDictionary<string, string>();

        public override Task OnConnectedAsync()
        {
            ActiveConnections.TryAdd(Context.ConnectionId, "lol");
            Console.WriteLine($"New Connection {Context.ConnectionId}");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            ActiveConnections.TryRemove(Context.ConnectionId, out _);
            return base.OnDisconnectedAsync(exception);
        }

        private bool IsValidConnection(string connectionId)
        {
            return ActiveConnections.ContainsKey(connectionId);
        }

        public async Task Offer(string connectionId, string sdpOffer)
        {
            Console.WriteLine(sdpOffer);
            Console.WriteLine(connectionId);

            if (IsValidConnection(connectionId))
            {
                await Clients.Client(connectionId).SendAsync("ReceiveOffer", Context.ConnectionId, sdpOffer);
                Console.WriteLine("sent");
            }
            else
            {
                Console.WriteLine("Error!");
            }

                
            
        }

        public async Task Answer(string connectionId, string sdpAnswer)
        {
            await Clients.Client(connectionId).SendAsync("ReceiveAnswer", Context.ConnectionId, sdpAnswer);
        }

        public async Task IceCandidate(string connectionId, string candidate)
        {
            await Clients.Client(connectionId).SendAsync("ReceiveIceCandidate", Context.ConnectionId, candidate);
        }
    }
}
