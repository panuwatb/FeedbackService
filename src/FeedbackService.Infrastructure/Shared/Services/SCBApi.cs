using Abp.Dependency;
using AutoMapper;
using AutoMapper.Configuration;
using FeedbackService.Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackService.Infrastructure.Shared.Services
{
    public class SCBApi : ITransientDependency, ISCBApi
    {
        private readonly IObjectMapper _objectMapper;
        private readonly IConfigurationRoot _appConfiguration;

        public string App_key { get; set; }
        public string App_secret { get; set; }
        public string Grant_type { get; set; }
        public string Api_url { get; set; }
        public const string contentTypeValue = "application/json";

        public SCBApi(IObjectMapper objectMapper, IHostingEnvironment env)
        {
            _objectMapper = objectMapper;

            _appConfiguration = AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName);

            // TODO: Configured by tenant.
            App_key = "l7d6119a1db7e2457fa7f7b05d4733882b";
            App_secret = "3216e745283c4a368e533b084af24457";
            Api_url = "https://api.partners.scb/partners";
        }

        public async Task<string> GetToken(string client_id, string client_secretkey)
        {
            var url = Api_url + "/v1/oauth/token";
            string data = "{\"applicationKey\": \"" + App_key + "\"," + "\"applicationSecret\": \"" + App_secret + "\"" + "}";

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Accept", "*/*");
            client.DefaultRequestHeaders.Add("requestUId", Guid.NewGuid().ToString());
            client.DefaultRequestHeaders.Add("resourceOwnerId", App_key);
            client.DefaultRequestHeaders.Add("Accept-Language", "EN");


            var content = new StringContent(data, Encoding.UTF8, contentTypeValue);
            var response = await client.PostAsync(url, content);

            var result = await response.Content.ReadAsStringAsync();
            // var scbModel = JsonConvert.DeserializeObject<ScbTokenModel>(result);

            return result;
        }
    }
}
