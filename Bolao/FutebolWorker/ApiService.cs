using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using static Application.DTOs.EventosDto;
using static MassTransit.ValidationResultExtensions;

namespace FutebolWorker
{
    public class ApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /* public async Task GetJogosAoVivo(int leagueId, int season)
         {
             var client = _httpClientFactory.CreateClient();
             client.DefaultRequestHeaders.Add("x-apisports-key", "b713b686ba4bb6b8190e016436d541d9");

             try
             {
                 var resposta = await client.GetAsync($"https://v3.football.api-sports.io/countries");

                 if (resposta.IsSuccessStatusCode)
                 {
                     foreach (var item in resposta.Response)
                     {
                         // Lógica para salvar no seu DbContext (PostgreSQL)
                         var novoTime = new Times()
                         {
                             IdExterno = item.Team.Id,
                             Nome = item.Team.Name,
                             EscudoUrl = item.Team.Logo
                         };
                         _context.Times.Add(novoTime);
                     }
                 }
             }
             catch (Exception ex)
             {
                 Console.WriteLine($"Falha na comunicação: {ex.Message}");
             }
         }*/

        

        internal async Task<PartidaAtualizadaEvent> GetJogosAoVivo()
        {
            return new PartidaAtualizadaEvent(1, 1, 1, "", DateTime.Now);
        }
    }
}
