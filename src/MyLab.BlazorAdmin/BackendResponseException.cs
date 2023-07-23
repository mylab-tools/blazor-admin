namespace MyLab.BlazorAdmin
{
    internal class BackendResponseException : Exception
    {
        public BackendResponseException(string message, HttpResponseMessage responseMessage)
            : base(message)
        {

        }
    }
}
