using Common.Extensions;
using Common.Extensions.EnumerableExtensions;
using Common.Interface;
using ConsoleVersion.Attributes;
using ConsoleVersion.Commands;
using ConsoleVersion.Extensions;
using ConsoleVersion.Interface;
using ConsoleVersion.Model.DelegateExtractors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using static System.Reflection.BindingFlags;

namespace ConsoleVersion.Model
{
    internal sealed class Menu : IMenu
    {
        private const BindingFlags MethodAccessModificators = NonPublic | Instance | Public;

        private readonly IViewModel target;
        private readonly Type targetType;
        private readonly IDelegateExtractor<Func<bool>> validationExtractor;
        private readonly IDelegateExtractor<Action<Action>> safeActionExtractor;

        private readonly Lazy<IReadOnlyDictionary<string, IMenuCommand>> menuActions;

        public IReadOnlyDictionary<string, IMenuCommand> MenuCommands => menuActions.Value;

        public Menu(IViewModel target)
        {
            safeActionExtractor = new SafeMethodExtractor();
            validationExtractor = new ValidationMethodExtractor();
            this.target = target;
            targetType = target.GetType();
            menuActions = new Lazy<IReadOnlyDictionary<string, IMenuCommand>>(GetMenuCommands);
        }

        private IReadOnlyDictionary<string, IMenuCommand> GetMenuCommands()
        {
            return targetType
                .GetMethods(MethodAccessModificators)
                .Where(method => Attribute.IsDefined(method, typeof(MenuItemAttribute)))
                .OrderByOrderAttribute()
                .SelectMany(CreateNameCommandPair)
                .ToReadOnlyDictionary();
        }

        private IEnumerable<KeyValuePair<string, IMenuCommand>> CreateNameCommandPair(MethodInfo methodInfo)
        {
            if (methodInfo.TryCreateDelegate(target, out Action action))
            {
                var safeAction = safeActionExtractor.ExtractFirstOrEmpty(methodInfo, target);
                var validationMethods = validationExtractor.Extract(methodInfo, target).ToArray();
                var command = new Action(() => safeAction.Invoke(action));
                string header = methodInfo.GetAttributeOrNull<MenuItemAttribute>().Header;
                var menuCommand = new MenuCommand(command, validationMethods);
                yield return new KeyValuePair<string, IMenuCommand>(header, menuCommand);
            }
        }
    }
}