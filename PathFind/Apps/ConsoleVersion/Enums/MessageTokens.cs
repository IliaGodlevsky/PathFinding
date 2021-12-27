using ConsoleVersion.ViewModel;
using EnumerationValues.Attributes;
using System;

namespace ConsoleVersion.Enums
{
    /// <summary>
    /// Tokens to channel messages
    /// </summary>
    [Flags]
    [EnumValuesIgnore(MessageTokens.None, MessageTokens.Everyone)]
    internal enum MessageTokens
    {
        None = 0,
        /// <summary>
        /// Use this token to send message to <see cref="MainViewModel"/>
        /// </summary>
        MainModel = 2 << 0,
        /// <summary>
        /// Use this token to send message to <see cref="ConsoleVersion.Views.MainView"/>
        /// </summary>
        MainView = 2 << 1,
        /// <summary>
        /// Use this token to send message to <see cref="ConsoleVersion.ViewModel.EndPointsViewModel"/>
        /// </summary>
        EndPointsViewModel = 2 << 2,
        /// <summary>
        /// Use this token to send message to everybody
        /// </summary>
        Everyone = (2 << 3) - 1
    }
}
