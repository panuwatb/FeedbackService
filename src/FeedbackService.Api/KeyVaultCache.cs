using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeedbackService.Api
{
    public static class KeyVaultCache
    {
        public static string BaseUri { get; set; }

        private static KeyVaultClient _KeyVaultClient = null;
        public static KeyVaultClient KeyVaultClient
        {
            get
            {
                if (_KeyVaultClient == null)
                {
                    var provider = new AzureServiceTokenProvider();
                    _KeyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(provider.KeyVaultTokenCallback));
                }

                return _KeyVaultClient;
            }
        }

        public static void GetAzureKeyVaultSecrets(HostBuilderContext context, IConfigurationBuilder config)
        {
            var buildConfig = config.Build();
            var keyVaultName = buildConfig[$"AppSettings:KeyVaultName"];
            BaseUri = $"https://{keyVaultName}.vault.azure.net";
            var secretClient = new SecretClient(new Uri(BaseUri), new DefaultAzureCredential());
            config.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
        }

        private static Dictionary<string, string> SecretCache = new Dictionary<string, string>();
        public async static Task<string> GetCacheSecret(string secretName)
        {
            if (!SecretCache.ContainsKey(secretName))
            {
                var secretBundle = await KeyVaultClient.GetSecretAsync($"{BaseUri}/secrets/{secretName}").ConfigureAwait(false);
                SecretCache.Add(secretName, secretBundle.Value);
            }

            return SecretCache.ContainsKey(secretName) ? SecretCache[secretName] : string.Empty;
        }
    }

    public static class GetSecret
    {
        public static async Task<string> FeedbackDbConnectionString() => await KeyVaultCache.GetCacheSecret($"{KeyVaultKeys.FeedbackDbConnectionStringKVKey}").ConfigureAwait(false);
        public static async Task<string> StorageAccountSecret() => await KeyVaultCache.GetCacheSecret($"{KeyVaultKeys.StorageAccountKVKey}").ConfigureAwait(false);
    }
}
