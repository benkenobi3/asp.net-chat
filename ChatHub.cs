using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp.net_chat.DataModel;
using Microsoft.EntityFrameworkCore;

namespace asp.net_chat
{
    public class ChatHub : Hub
    {
        private ApplicationContext db = new ApplicationContext();

        public override Task OnConnectedAsync()
        {
            Console.WriteLine("New Connection Started: " + Context.ConnectionId);
            Clients.All.SendAsync("NewConnection", "New Connection Successfull", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(System.Exception exception)
        {
            db.Database.ExecuteSqlRaw("TRUNCATE TABLE public.message");
            
            Console.WriteLine("Discconnected: " + Context.ConnectionId);
            Clients.All.SendAsync("DisconnectConnection", "Connection Destoryed", Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string message)
        {
            Console.WriteLine("Message received " + message);
            
            var m = new Message(message);
            db.Message.Add(m);
            db.SaveChanges();
            
            await Clients.All.SendAsync("SendMessage", message);
        }

        public async Task JoinRoom(string name)
        {
            Console.WriteLine("User Joined");
            await Clients.All.SendAsync("JoinRoom", name);
        }
    }
}
        


