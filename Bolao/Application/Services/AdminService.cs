using Application.DTOs;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using static Application.DTOs.EventosDto;

namespace Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AdminService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public Task<bool> AtualizaTimes()
        {
            var paises = GetPaises().Result;
            foreach (var pais in paises)
            {
                
            }
            return Task.FromResult(true);
        }

        public async Task<List<CountriesResponse>> GetPaises()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Add("x-apisports-key", "b713b686ba4bb6b8190e016436d541d9");
                var respostaMessage = await client.GetAsync("https://v3.football.api-sports.io/countries");

                if (respostaMessage.IsSuccessStatusCode)
                {
                    var dados = await respostaMessage.Content.ReadFromJsonAsync<List<CountriesResponse>>();
                    return dados;
                }
                return new List<CountriesResponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return new List<CountriesResponse>();
            }
        }

        Task IAdminService.AtualizaTimes()
        {
            return AtualizaTimes();
        }
    }
}
