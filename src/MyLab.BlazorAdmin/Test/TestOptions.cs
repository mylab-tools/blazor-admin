namespace MyLab.BlazorAdmin.Test
{
    /// <summary>
    /// Contains options for testing
    /// </summary>
    public class TestOptions
    {
        /// <summary>
        /// Configuration section name
        /// </summary>
        public const string SectionName = "Test";
        /// <summary>
        /// The base URL of backend API
        /// </summary>
        public string BaseApiUrl { get; set; }

        /// <summary>
        /// Specifies identity from <see cref="Identities"/>
        /// </summary>
        public string UseIdentity { get; set; }
        /// <summary>
        /// Test identities for startup authentication
        /// </summary>
        public Dictionary<string, TestIdentity> Identities { get; set; }
    }

    /// <summary>
    /// Describes test identity
    /// </summary>
    public class TestIdentity
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Access token
        /// </summary>
        public string Token { get; set; }
    }
}
