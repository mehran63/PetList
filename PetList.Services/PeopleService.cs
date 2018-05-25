using Microsoft.eShopOnContainers.BuildingBlocks.Resilience.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PetList.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PetList.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly IHttpClient _httpClient;
        private readonly ServiceSettings _settings;
        private readonly string _remoteServiceUrl;

        public PeopleService(IHttpClient httpClient, IOptions<ServiceSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
            _remoteServiceUrl = _settings.PeopleUrl;
        }

        public async Task<Person[]> GetAll()
        {
            var dataString = await _httpClient.GetStringAsync(_remoteServiceUrl);

            var response = JsonConvert.DeserializeObject<Person[]>(dataString);

            return response;
        }
    }
}
