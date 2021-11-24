using System;

namespace ConsoleVersion.Messages
{
    internal class ClaimGraphMessage
    {
        public object ClaimerMessageToken { get; }

        public ClaimGraphMessage(Guid messageToken)
        {
            ClaimerMessageToken = messageToken;
        }
    }
}
