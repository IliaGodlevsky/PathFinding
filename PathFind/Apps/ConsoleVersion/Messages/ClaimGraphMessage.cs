using ConsoleVersion.Enums;
using System;

namespace ConsoleVersion.Messages
{
    internal class ClaimGraphMessage
    {
        public MessageTokens ClaimerMessageToken { get; }

        public ClaimGraphMessage(MessageTokens messageToken)
        {
            ClaimerMessageToken = messageToken;
        }
    }
}
