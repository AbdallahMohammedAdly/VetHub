using Microsoft.AspNetCore.SignalR;
using SignalR.Contexts;
using SignalR.Models;

namespace SignalR.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;
        private readonly ChatDbContext _context;

        public ChatHub(ILogger<ChatHub> logger,ChatDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task Send(string user , string message)
        {
            await Clients.All.SendAsync("ReceiveMessage",user, message);

            Message meg = new Message()
            {
                MessageText = message ,
                UserName = user 
            };
            _context.messages.Add(meg);

            await _context.SaveChangesAsync();
        }
    }
}
