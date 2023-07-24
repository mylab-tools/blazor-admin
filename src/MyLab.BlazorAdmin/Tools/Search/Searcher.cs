using System.IdentityModel.Tokens.Jwt;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Text;
using System.Runtime.Serialization;
using Microsoft.IdentityModel.Tokens;

namespace MyLab.BlazorAdmin.Tools.Search
{
    /// <summary>
    /// Contains logic for searching interaction with backend
    /// </summary>
    public class Searcher<T>
    {
        private readonly HttpClient _httpClient;
        private readonly string _urlPath;
        private string? _searchToken;
        
        /// <summary>
        /// Initializes a new instance of <see cref="Searcher{T}"/>
        /// </summary>
        public Searcher(
            HttpClient httpClient,
            string urlPath)
        {
            _httpClient = httpClient;
            _urlPath = urlPath.TrimEnd('/');
        }

        /// <summary>
        /// Performs a searching
        /// </summary>
        public async Task<SearchResult<T>?> SearchAsync(SearchRequest request)
        {
            if (_searchToken == null || !IsTokenValid(_searchToken))
            {
                var tokenReqRes = await _httpClient.GetAsync(_urlPath + "/" + "token");

                if (!tokenReqRes.IsSuccessStatusCode)
                {
                    throw new BackendResponseException("Unable to get search token", tokenReqRes);
                }

                _searchToken = await tokenReqRes.Content.ReadAsStringAsync();
            }

            var apiRequest = new ApiSearchRequest
            {
                Offset = request.PageIndex * request.PageSize,
                Limit = request.PageSize,
                QueryMode = SearchRequestQueryMode.Must,
                Query = request.Query,
            };

            if (request.SortId != null)
                apiRequest.Sort = new SearchRequestSort { Id = request.SortId };

            if (request.FilterId != null)
                apiRequest.Filters = new SearchRequestFilter[] { new() { Id = request.FilterId } };

            var jsonReq = JsonConvert.SerializeObject(apiRequest);

            var reqContent = new StringContent(jsonReq, Encoding.UTF8, "application/json")
            {
                Headers =
            {
                { "X-Search-Token", _searchToken }
            }
            };

            var searchReqRes = await _httpClient.PostAsync(_urlPath, reqContent);

            if (!searchReqRes.IsSuccessStatusCode)
            {
                throw new BackendResponseException("Search performing error", searchReqRes);
            }

            var searchResStr = await searchReqRes.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<SearchResult<T>>(searchResStr);
        }

        bool IsTokenValid(string? token)
        {
            if (token == null)
                return false;

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out _);

                return true;
            }
            catch
            {
                return false;
            }
        }

        class ApiSearchRequest
        {
            [JsonProperty("query")]
            public string? Query { get; set; }
            [JsonProperty("sort")]
            public SearchRequestSort? Sort { get; set; }
            [JsonProperty("offset")]
            public int Offset { get; set; }
            [JsonProperty("limit")]
            public int Limit { get; set; }
            [JsonProperty("queryMode")]
            [JsonConverter(typeof(StringEnumConverter))]
            public SearchRequestQueryMode QueryMode { get; set; }
            [JsonProperty("filters")]
            public SearchRequestFilter[]? Filters { get; set; }
        }

        public class SearchRequestSort
        {
            [JsonProperty("id")]
            public string? Id { get; set; }
            [JsonProperty("args")]
            public Dictionary<string, string>? Args { get; set; }
        }

        public enum SearchRequestQueryMode
        {
            [EnumMember(Value = null)]
            Undefined,
            [EnumMember(Value = "should")]
            Should,
            [EnumMember(Value = "must")]
            Must
        }

        public class SearchRequestFilter
        {
            [JsonProperty("id")]
            public string? Id { get; set; }
            [JsonProperty("args")]
            public Dictionary<string, string>? Args { get; set; }
        }
    }
}
