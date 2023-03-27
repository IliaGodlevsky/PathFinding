using GalaSoft.MvvmLight.Messaging;
using Org.BouncyCastle.Cms;
using System;
using System.Diagnostics;
using System.Linq;

namespace Pathfinding.App.Console.Model
{
    internal sealed class MessengerDebug : IMessenger
    {
        private readonly IMessenger messenger;

        public MessengerDebug(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        public void Register<TMessage>(object recipient, Action<TMessage> action, bool keepTargetAlive = false)
        {
            Debug.WriteLine($"Registering {typeof(TMessage).Name}");
            Debug.WriteLine($"[{DateTime.Now.ToString("u")}]: [Recipient: {recipient}], [Action: {action.Method.Name}], [Keep target alive: {keepTargetAlive}]");
            messenger.Register<TMessage>(recipient, action, keepTargetAlive);
        }

        public void Register<TMessage>(object recipient, object token, Action<TMessage> action, bool keepTargetAlive = false)
        {
            Debug.WriteLine($"Registering {typeof(TMessage).Name}");
            Debug.WriteLine($"[{DateTime.Now.ToString("u")}]: [Recipient: {recipient}], [Token: {token}], [Action: {action.Method.Name}], [Keep target alive: {keepTargetAlive}]");
            messenger.Register<TMessage>(recipient, token, action, keepTargetAlive);
        }

        public void Register<TMessage>(object recipient, object token, bool receiveDerivedMessagesToo, Action<TMessage> action, bool keepTargetAlive = false)
        {
            Debug.WriteLine($"Registering {typeof(TMessage).Name}");
            Debug.WriteLine($"[{DateTime.Now.ToString("u")}]: [Recipient: {recipient}], [Token: {token}] [Action: {action.Method.Name}], [Recieve derived messages: {receiveDerivedMessagesToo}], [Keep target alive: {keepTargetAlive}]");
            messenger.Register<TMessage>(recipient, token, receiveDerivedMessagesToo, action, keepTargetAlive);
        }

        public void Register<TMessage>(object recipient, bool receiveDerivedMessagesToo, Action<TMessage> action, bool keepTargetAlive = false)
        {
            Debug.WriteLine($"Registering {typeof(TMessage).Name}");
            Debug.WriteLine($"[{DateTime.Now.ToString("u")}]: [Recipient: {recipient}], [Action: {action.Method.Name}], [Recieve derived messages: {receiveDerivedMessagesToo}], [Keep target alive: {keepTargetAlive}]");
            messenger.Register<TMessage>(recipient, receiveDerivedMessagesToo, action, keepTargetAlive);
        }

        public void Send<TMessage>(TMessage message)
        {
            Debug.WriteLine($"Sending {typeof(TMessage).Name}");
            Debug.WriteLine($"[{DateTime.Now.ToString("u")}]");
            messenger.Send<TMessage>(message);
        }

        public void Send<TMessage, TTarget>(TMessage message)
        {
            Debug.WriteLine($"Sending {typeof(TMessage).Name} to a target: {typeof(TTarget)}");
            Debug.WriteLine($"[{DateTime.Now.ToString("u")}]");
            messenger.Send<TMessage, TTarget>(message);
        }

        public void Send<TMessage>(TMessage message, object token)
        {
            Debug.WriteLine($"Sending {typeof(TMessage).Name}");
            Debug.WriteLine($"[{DateTime.Now.ToString("u")}]: [Token: {token}]");
            messenger.Send(message, token);
        }

        public void Unregister(object recipient)
        {
            messenger.Unregister(recipient);
        }

        public void Unregister<TMessage>(object recipient)
        {
            messenger.Unregister<TMessage>(recipient);
        }

        public void Unregister<TMessage>(object recipient, object token)
        {
            messenger.Unregister<TMessage>(recipient, token);
        }

        public void Unregister<TMessage>(object recipient, Action<TMessage> action)
        {
            messenger.Unregister<TMessage>(recipient, action);
        }

        public void Unregister<TMessage>(object recipient, object token, Action<TMessage> action)
        {
            messenger.Unregister<TMessage>(recipient, token, action);
        }
    }
}
