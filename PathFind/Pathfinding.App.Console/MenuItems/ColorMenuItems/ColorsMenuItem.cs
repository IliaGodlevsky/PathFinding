using GalaSoft.MvvmLight.Messaging;
using Pathfinding.App.Console.Extensions;
using Pathfinding.App.Console.Interface;
using Pathfinding.App.Console.Model;
using Shared.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Pathfinding.App.Console.MenuItems.ColorMenuItems
{
    internal abstract class ColorsMenuItem : IMenuItem, ICanRecieveMessage
    {
        private readonly IMessenger messenger;
        private readonly IInput<int> intInput;
        
        private IReadOnlyList<ConsoleColor> AllColors { get; }

        private MenuList ColorsMenuList { get; }

        protected ColorsMenuItem(IMessenger messenger,
            IInput<int> intInput)
        {
            this.messenger = messenger;
            this.intInput = intInput;
            AllColors = Enum.GetValues(typeof(ConsoleColor))
                .Cast<ConsoleColor>()
                .ToReadOnly();
            ColorsMenuList = AllColors.CreateMenuList(color => color.GetName());
        }

        public virtual void Execute()
        {
            SendAskMessage();
            var colorProps = GetPropertiesInfo<ConsoleColor>();
            var menuList = colorProps.CreateMenuList(prop => prop.GetAttributeOrDefault<DescriptionAttribute>().Description);
            var colors = GetPropertiesValues<ConsoleColor>(colorProps);
            menuList.Display();
            SendColorsMessage();
        }

        private IEnumerable<PropertyInfo> GetPropertiesInfo<TPropType>()
        {
            return GetType()
                .GetProperties(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(prop => prop.PropertyType == typeof(TPropType));
        }

        private IReadOnlyList<TPropType> GetPropertiesValues<TPropType>(IEnumerable<PropertyInfo> infos)
        {
            return infos
                .Select(prop => prop.GetValue(this))
                .OfType<TPropType>()
                .ToReadOnly();
        }

        protected abstract void SendAskMessage();

        protected abstract void SendColorsMessage();

        public abstract void RegisterHanlders(IMessenger messenger);
    }
}
