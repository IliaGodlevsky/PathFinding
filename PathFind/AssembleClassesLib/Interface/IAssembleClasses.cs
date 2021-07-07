using System.Collections.Generic;

namespace AssembleClassesLib.Interface
{
    /// <summary>
    /// An interface, that provides 
    /// methods for loading types from assembly(s)
    /// </summary>
    public interface IAssembleClasses
    {
        /// <summary>
        /// A collection of classes names, loaded from assembly(s)
        /// </summary>
        IReadOnlyCollection<string> ClassesNames { get; }

        /// <summary>
        /// Creates an objects according to its class name
        /// </summary>
        /// <param name="name">a name of class, 
        /// loaded from assembly</param>
        /// <param name="ctorParametres"> constructor 
        /// paramtres to initialize object</param>
        /// <returns>An instance of object</returns>
        object Get(string name, params object[] ctorParametres);

        /// <summary>
        /// Loads classes from assembly
        /// </summary>
        void LoadClasses();
    }
}