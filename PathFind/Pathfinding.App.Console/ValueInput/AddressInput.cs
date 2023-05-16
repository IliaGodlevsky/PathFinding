using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;

namespace Pathfinding.App.Console.ValueInput
{
    internal sealed class AddressInput : IInput<(string Host, int Port)>
    {
        private readonly IInput<int> intInput;
        private readonly IInput<string> stringInput;

        public AddressInput(IInput<int> intInput,
            IInput<string> stringInput)
        {
            this.intInput = intInput;
            this.stringInput = stringInput;
        }

        public (string Host, int Port) Input()
        {
            using (Cursor.UseCurrentPositionWithClean())
            {
                string host = stringInput.Input(Languages.InputHostName);
                while (string.IsNullOrEmpty(host))
                {
                    host = stringInput.Input(Languages.InputHostName);
                }
                int port = intInput.Input(Languages.InputPort);
                return (host, port);
            }
        }
    }
}
