using GalaSoft.MvvmLight.Messaging;
using Pathfinding.AlgorithmLib.Core.Abstractions;
using Pathfinding.App.Console.Messages.DataMessages;
using Pathfinding.App.Console.Model;
using Pathfinding.GraphLib.Core.Realizations.Graphs;
using Pathfinding.Logging.Loggers;
using System;
using System.Linq;

namespace Pathfinding.App.Console.Extensions
{
    internal static class IMessengerExtensions
    {
        private static Tokens[] Tokens { get; }

        static IMessengerExtensions()
        {
            Tokens = Enum.GetValues(typeof(Tokens))
                .Cast<Tokens>()
                .OrderBy(_ => _)
                .ToArray();
        }

        public static void SendData<TData>(this IMessenger messenger,
            TData data, Tokens token)
        {
            var msg = new DataMessage<TData>(data);
            for (int i = 0; i < Tokens.Length; i++)
            {
                if (token.ContainsFlag(Tokens[i]))
                {
                    messenger.Send(msg, Tokens[i]);
                }
            }
        }

        public static void SendData<TData>(this IMessenger messenger, 
            PathfindingProcess algorithm, TData data, Tokens token)
        {
            var msg = new AlgorithmMessage<TData>(algorithm, data);
            for (int i = 0; i < Tokens.Length; i++)
            {
                if (token.ContainsFlag(Tokens[i]))
                {
                    messenger.Send(msg, Tokens[i]);
                }
            }
        }

        public static void RegisterData<TData>(this IMessenger messenger, object recipient,
            Tokens token, Action<DataMessage<TData>> action)
        {
            messenger.Register<DataMessage<TData>>(recipient, token, action);
        }

        public static void RegisterGraph(this IMessenger messenger, object recipient,
            Tokens token, Action<DataMessage<Graph2D<Vertex>>> action)
        {
            messenger.RegisterData<Graph2D<Vertex>>(recipient, token, action);
        }

        public static void RegisterAlgorithmData<TData>(this IMessenger messenger, object recipient,
            Tokens token, Action<AlgorithmMessage<TData>> action)
        {
            messenger.Register<AlgorithmMessage<TData>>(recipient, token, action);
        }

        private static bool ContainsFlag(this Tokens flags, Tokens flag)
        {
            return (flags & flag) != 0;
        }
    }
}
