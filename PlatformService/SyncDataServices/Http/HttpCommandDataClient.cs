﻿using PlatformService.Dtos;
using System.Text;
using System.Text.Json;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task SendPlatformToCommand(PlatformReadDto platformReadDto)
        {
            var baseUrl = _configuration["CommandServiceUrl"];
            var httpContent = new StringContent(JsonSerializer.Serialize(platformReadDto),
                Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{ baseUrl}/api/c/Platforms", httpContent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync post OK!");
            }
            else
            {
                Console.WriteLine("--> Sync post NOT OK!");
            }
        }
    }
}
