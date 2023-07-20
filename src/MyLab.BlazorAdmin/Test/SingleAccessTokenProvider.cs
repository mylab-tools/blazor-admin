using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace MyLab.BlazorAdmin.Test
{
    class SingleAccessTokenProvider : IAccessTokenProvider
    {
        private readonly string _accessToken;

        public SingleAccessTokenProvider(string accessToken)
        {
            _accessToken = accessToken;
        }

        public ValueTask<AccessTokenResult> RequestAccessToken()
        {
            return RequestAccessTokenCore();
        }

        public ValueTask<AccessTokenResult> RequestAccessToken(AccessTokenRequestOptions options)
        {
            return RequestAccessTokenCore();
        }

        ValueTask<AccessTokenResult> RequestAccessTokenCore()
        {
            AccessToken t = new AccessToken
            {
                Value = _accessToken
            };

            var tokenResult = new AccessTokenResult(AccessTokenResultStatus.Success, t, "", null);

            return new ValueTask<AccessTokenResult>(tokenResult);
        }
    }
}
