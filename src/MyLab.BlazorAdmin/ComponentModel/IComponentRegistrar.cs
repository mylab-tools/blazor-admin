namespace MyLab.BlazorAdmin.ComponentModel
{
    /// <summary>
    /// Registers a component 
    /// </summary>
    public interface IComponentRegistrar
    {
        /// <summary>
        /// Registers child component
        /// </summary>
        /// <returns>Uregistrar</returns>
        IDisposable RegisterChild(object child);
    }
}
