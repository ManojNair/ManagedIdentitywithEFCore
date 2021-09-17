using System.Threading.Tasks;
using Azure.Core;
using Azure.Identity;
using Microsoft.Data.SqlClient;

namespace ConsoleApp1
{
    public class CustomAzureSqlAuthProvider : SqlAuthenticationProvider
    {
        private static readonly string[] AzureSqlScopes = new[]
        {
            "https://database.windows.net//.default"
        };

        private static readonly TokenCredential _credential = new DefaultAzureCredential();

        public override async Task<SqlAuthenticationToken> AcquireTokenAsync(SqlAuthenticationParameters parameters)
        {
            var tokenRequestContext = new TokenRequestContext(AzureSqlScopes);
            var tokenResult = await _credential.GetTokenAsync(tokenRequestContext, default);
            return new SqlAuthenticationToken(tokenResult.Token, tokenResult.ExpiresOn);
        }

        public override bool IsSupported(SqlAuthenticationMethod authenticationMethod) =>
            authenticationMethod.Equals(SqlAuthenticationMethod.ActiveDirectoryDeviceCodeFlow);
    }
}