using Application.DTOs;
using Application.Interfaces;
using Application.Mappings;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using static Application.DTOs.EventosDto;
using static Application.DTOs.TeamsDto;

namespace Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAdminRepository _adminRepository;
        private readonly IBolaoRepository _bolaoRepository;
        public AdminService(IHttpClientFactory httpClientFactory,IAdminRepository adminRepo,IBolaoRepository bolaoRepository)
        {
            _httpClientFactory = httpClientFactory;
            _adminRepository = adminRepo;
            _bolaoRepository = bolaoRepository;
        }
        public async Task AtualizaTimes()
        {
            var paises = GetPaises().Result;
            foreach (var pais in paises.Response)
            {
                await _adminRepository.NovoTime(new Domain.Entities.Times(pais.Team.Name, pais.Team.Logo, pais.Team.Id.ToString(), pais.Team.IsNational));
            } 
        }

        public async Task CriarPartida(BolaoDto.CriarPartidaDto criarPartidaDto)
        {
            Times timeA = await _bolaoRepository.GetTimesByIdAsync(criarPartidaDto.idTimeA);
            Times timeB = await _bolaoRepository.GetTimesByIdAsync(criarPartidaDto.idTimeB);

            if (timeA == null || timeB == null)
            {
                throw new Exception("Times não encontrados. Atualize os times antes de criar a partida.");
            }




            Partida novaPartida = new Partida(timeA, timeB, criarPartidaDto.dataPartida);
            //Partida novaPartida = new Partida(timeA, timeB, DateTime.UtcNow);
            await _adminRepository.CriarPartida(novaPartida);
        }

        public async Task<TeamResponse> GetPaises()
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Add("x-apisports-key", "b713b686ba4bb6b8190e016436d541d9");
                var respostaMessage = await client.GetAsync("https://v3.football.api-sports.io/teams?league=72&season=2022");
            /*//https://v3.football.api-sports.io/countries*/
                if (respostaMessage.IsSuccessStatusCode)
                {
                    var dados = await respostaMessage.Content.ReadFromJsonAsync<TeamResponse>();
                    //var dados = await respostaMessage.Content.ReadAsStringAsync();
                     return dados;
                    //var a = dados;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return null;
            }
        }

        async Task IAdminService.AtualizaTimes()
        {
            await AtualizaTimes();
        }
    }

   
}
