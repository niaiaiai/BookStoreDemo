using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace WebTest
{
    public class Ids4Test
    {
        private HttpClient _httpClient;
        private readonly DiscoveryDocumentResponse _disco;
        public Ids4Test()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();
            _httpClient = new ();
            _disco = _httpClient.GetDiscoveryDocumentAsync(configuration.GetValue<string>("IdentityServerOrigin")).Result;
        }

        [Theory]
        [InlineData("aaa@aa.com", "123")]
        public async Task Login_ShouldSuccess(string username, string password)
        {
            TokenResponse tokenResponse = await _httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = _disco.TokenEndpoint,
                ClientId = "js",
                UserName = username,
                Password = password,
                Scope = "openid bookstore profile userrole offline_access"
            });
            Assert.False(tokenResponse.IsError);
            Assert.NotNull(tokenResponse.AccessToken);
            Assert.NotNull(tokenResponse.Scope);
            Assert.NotNull(tokenResponse.RefreshToken);
        }
    }
}
