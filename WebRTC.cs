using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace facechat
{
    public class WebRTC : Hub
    {
        public async Task Offer(string connectionId, string sdpOffer)
        {
            await Clients.Client(connectionId).SendAsync("ReceiveOffer", Context.ConnectionId, sdpOffer);
        }

        public async Task Answer(string connectionId, string sdpOffer)
        {
            await Clients.Client(connectionId).SendAsync("ReceiveAnswer", Context.ConnectionId, sdpOffer);
        }

        public async Task IceCandidate(string connectionId, string candidate)
        {
            await Clients.Client(connectionId).SendAsync("ReceiveIceCandidate", Context.ConnectionId, candidate);
        }
    }
}
