using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Localization;
using Pathfinding.App.Console.Messages;
using Shared.Primitives.ValueRange;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Pathfinding.App.Console.MenuItems.MainMenuItems
{
    internal sealed class LanguageMenuItem : IMenuItem
    {
        private readonly IInput<int> intInput;
        private readonly IMessenger messenger;
        private readonly IReadOnlyList<CultureInfo> languages;

        public int Order => 3;

        public LanguageMenuItem(IReadOnlyList<CultureInfo> languages, 
            IInput<int> intInput, IMessenger messenger)
        {
            this.intInput = intInput;
            this.messenger = messenger;
            this.languages = languages;
        }

        public bool CanBeExecuted() => true;

        public void Execute()
        {
            var menu = languages
                .Select(lang => lang.NativeName)
                .CreateMenuList(1);
            var range = new InclusiveValueRange<int>(languages.Count, 1);
            string message = menu + "\n" + Languages.LanguageMsg;
            using (Cursor.CleanUpAfter())
            {
                int index = intInput.Input(message, range) - 1;
                var language = languages[index];
                CultureInfo.CurrentCulture = language;
                CultureInfo.CurrentUICulture = language;
            }
            messenger.Send(new GraphChangedMessage());
        }

        public override string ToString()
        {
            return Languages.Language;
        }
    }
}
