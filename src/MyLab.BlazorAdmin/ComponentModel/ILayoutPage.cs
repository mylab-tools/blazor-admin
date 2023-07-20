namespace MyLab.BlazorAdmin.ComponentModel
{
    /// <summary>
    /// Provides interaction with layout page
    /// </summary>
    public interface ILayoutPage
    {
        /// <summary>
        /// Registers child component
        /// </summary>
        /// <returns>Uregistrar</returns>
        IDisposable RegisterChild(object child);
    }
}
