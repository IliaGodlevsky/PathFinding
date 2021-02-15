namespace ConsoleVersion.Enums
{
    /// <summary>
    /// Represents the priority of a menu item in the menu
    /// </summary>
    internal enum MenuItemPriority
    {
        /// <summary>
        /// Menu item will be in the top of menu
        /// </summary>
        Highest,
        /// <summary>
        /// Menu item will be higher the most of menu items in menu
        /// </summary>
        High,
        /// <summary>
        /// Menu item will be somewhere in the middle of menu
        /// </summary>
        Normal,
        /// <summary>
        /// Menu item will be lower  the most of menu items in menu
        /// </summary>
        Low,
        /// <summary>
        /// Menu item will be in the bottom of menu
        /// </summary>
        Lowest
    }
}
