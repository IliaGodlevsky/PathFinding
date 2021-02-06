namespace Common.Handlers
{
    /// <summary>
    /// A handler for constructor
    /// </summary>
    /// <typeparam name="TReturnType"></typeparam>
    /// <param name="args"></param>
    /// <returns><typeparamref name="TReturnType"></typeparamref></returns>
    public delegate TReturnType ActivatorHandler<out TReturnType>(params object[] args) where TReturnType : class;
}
