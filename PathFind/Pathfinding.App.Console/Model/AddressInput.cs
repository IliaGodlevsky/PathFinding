using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;

namespace Pathfinding.App.Console.Model
{
    internal sealed class AddressInput
    {
        private readonly IInput<int> intInput;
        private readonly IInput<string> stringInput;

        public AddressInput(IInput<int> intInput, IInput<string> stringInput)
        {
            this.intInput = intInput;
            this.stringInput = stringInput;
        }

        public (string Host, int Port) InputAddress()
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                string host = stringInput.Input("Input host name: ");
                while (string.IsNullOrEmpty(host))
                {
                    host = stringInput.Input("Input host name: ");
                }
                int port = intInput.Input("Input port: ");
                return (host, port);
            }
        }
    }
}
