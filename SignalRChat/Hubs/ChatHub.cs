using Microsoft.AspNet.SignalR;
using SignalRChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        private static SignalRChatContext db = new SignalRChatContext();

        // Отправка сообщений
        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
            db.Messages.Add(new Message()
            {
                SenderName = name,
                Text = message
            });
            db.SaveChanges();
        }

        // Показать историю сообщений
        public void ShowHistory()
        {
            foreach(var msg in db.Messages.ToList())
            {
                Clients.Caller.addMessage(msg.SenderName, msg.Text);
            }
        }

        // Подключение нового пользователя
        public void Connect(string userName)
        {
            var users = db.Users.ToList();
            var id = Context.ConnectionId;

            if (!users.Any(x => x.ConnectionId == id))
            {
                db.Users.Add(new User { ConnectionId = id, Name = userName });
                db.SaveChanges();

                // Посылаем сообщение текущему пользователю
                Clients.Caller.onConnected(id, userName, users);

                // Посылаем сообщение всем пользователям, кроме текущего
                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }

        // Отключение пользователя
        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = db.Users.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (item != null)
            {
                db.Users.Remove(item);
                db.SaveChanges();
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.Name);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}