namespace MyLab.BlazorAdmin.Services
{
    /// <summary>
    /// Provides the base address of a backend
    /// </summary>
    /// <remarks>
    /// Use it interface in extensions to generate backend URLs
    /// </remarks>
    public interface IBackendAddrProvider
    {
        /// <summary>
        /// Provides  Url
        /// </summary>
        Uri Provide();
    }

    class BackendAddrProvider : IBackendAddrProvider
    {
        private readonly Uri _url;

        public BackendAddrProvider(string baseAddr)
        {
            _url = new Uri(baseAddr);
        }

        public Uri Provide()
        {
            return _url;
        }
    }
}
