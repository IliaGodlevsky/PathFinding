using EnumerationValues.Attributes;
using ConsoleVersion.ViewModel;
using System;

namespace ConsoleVersion.Enums
{
    /// <summary>
    /// Tokens to channel messages
    /// </summary>
    [Flags]
    internal enum MessageTokens
    {
        /// <summary>
        /// Use this token to send message to <see cref="MainViewModel"/>
        /// </summary>
        MainModel = 2 << 0,
        /// <summary>
        /// Use this token to send message to <see cref="ConsoleVersion.View.MainView"/>
        /// </summary>
        MainView = 2 << 1,
        /// <summary>
        /// Use this token to send message to <see cref="ConsoleVersion.Model.EndPointsSelection"/>
        /// </summary>
        EndPointsSelection = 2 << 2,
        /// <summary>
        /// Use this token to send message to everybody
        /// </summary>
        [EnumValuesIgnore]
        Everyone = (2 << 3) - 1
    }
}
