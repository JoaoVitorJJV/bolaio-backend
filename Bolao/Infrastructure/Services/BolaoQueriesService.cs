using Application.DTOs;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using UFRA.Bolaio.API.Data;
using static Application.DTOs.BolaoDto;
using static Application.DTOs.PalpitesDto;
using static Application.DTOs.UsuarioDtos;

namespace Infrastructure.Services
{
    public class BolaoQueriesService : IBolaoQueries
    {
        private readonly AppDbContext _appDbContext;
        public BolaoQueriesService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<GetExtratoResponseDto>> GetExtrato(Guid id)
        {            
            var transacoes = await _appDbContext.Transacoes
                .Where(w=>w.Carteira.UsuarioId == id)
                .Select(
                        s => new GetExtratoResponseDto(
                            s.DataHora.ToString(),
                            s.Bolao != null? $"({s.Tipo.ToString()}) "+s.Bolao.Partida.TimeA.Sigla + " x " + s.Bolao.Partida.TimeB.Sigla: $"{s.Tipo.ToString()}",
                            //"A x B",
                            s.Status.ToString(),
                            s.Valor)
                     ).AsNoTracking().ToListAsync();
            
            return transacoes;
        }

        public async Task<List<ListarBoloesDto>> ListarBoloes()
        {
            return await _appDbContext.Boloes.Include(b=>b.Partida)
            .Where(x => x.Visibilidade == TipoVisibilidade.Publico)
            .Select(b => new ListarBoloesDto(
                b.Id,
                b.Nome,
                b.Visibilidade,
                b.Valor,
                b.DataFechamento,
                b.TipoBolao,
                b.Organizador.Nome,
                b.Palpites.Select(p => p.Participante.Id).Distinct().Count().ToString(),
                b.Premio,
                b.Partida.TimeA.Nome,
                b.Partida.TimeB.Nome,
                b.Partida.ResultadoTimeA + " - " + b.Partida.ResultadoTimeB,
                b.MaxParticipantes.ToString(),
                b.Partida.Id.ToString()
                //"timeA","timeB", "0 - 0"
                ))
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task GetLucroPotencial(string usuarioId)
        {
            //List<Carteira> carteiras = await _appDbContext.Carteiras.ToListAsync();

        }

        public async Task<ListarBoloesDto> GetBolaoByIdNoTrackAsync(string id)
        {
            Guid i = Guid.Parse(id);

            return await _appDbContext.Boloes
                .AsNoTracking()
                .Where(x => x.Id == i) // Filtra pelo ID
                .Select(b => new ListarBoloesDto(
                     b.Id,
                     b.Nome,
                     b.Visibilidade,
                     b.Valor,
                     b.DataFechamento,
                     b.TipoBolao,
                     b.Organizador.Nome, // O EF resolve o Join sozinho aqui
                     b.Palpites.Select(p => p.Participante.Id).Distinct().Count().ToString(),
                     b.Premio,
                     b.Partida.TimeA.Nome,
                     b.Partida.TimeB.Nome,
                     b.Partida.ResultadoTimeA + " - " + b.Partida.ResultadoTimeB,
                     b.MaxParticipantes.ToString(),
                     b.Partida.Id.ToString()
                 ))
                .FirstOrDefaultAsync(); // Executa e retorna 1 ou null
        }

        public async Task<List<PalpitesAtivosDto>> ListarPalpitesPorUsuarioAsync(Guid guid)
        {
            return await _appDbContext.Palpites.AsNoTracking()
                .Where(b => b.Participante.Id == guid)
                .Select(b => new PalpitesAtivosDto(
                        b.Bolao.Nome,
                        b.Bolao.Partida.TimeA.Nome + " x " + b.Bolao.Partida.TimeB.Nome,
                        "time teste a implementar",
                        b.PalpiteGolsA + " x " + b.PalpiteGolsB,
                        b.Bolao.Partida.StatusPartida.ToString(),
                        (b.Bolao.Valor * b.QtdCotas).ToString("C2"),
                        "retorno a implementar"
                    ))
                .ToListAsync();
        }

        public async Task<List<string>> GetLogs()
        {
            var random = new Random();
            var logs = new List<string>();

            string[] acoes = {
        "Transação Efetuada", "Palpite Cadastrado", "Bolão Criado",
        "Partida Criada", "Partida Encerrada", "Login Realizado",
        "Erro de Conexão RabbitMQ", "Sincronização SGD", "Ajuste de Saldo"
    };

            string[] tiposTransacao = { "Débito", "Depósito" };
            string[] usuarios = { "xiconha", "admin_tester", "user_99", "dev_back" };

            for (int i = 1; i <= 1000; i++)
            {
                var data = DateTime.Now.AddSeconds(-i * 10); // Logs retroativos a cada 10 segundos
                var acao = acoes[random.Next(acoes.Length)];
                var usuario = usuarios[random.Next(usuarios.Length)];

                string logEntry = $"[{data:yyyy-MM-dd HH:mm:ss}] ";

                // Lógica customizada baseada na ação sorteada
                logEntry += acao switch
                {
                    "Transação Efetuada" => $"FINANCE: {acao} - Tipo: {tiposTransacao[random.Next(tiposTransacao.Length)]} - Valor: R$ {random.Next(5, 600)},00 - Usuário: {usuario}",
                    "Palpite Cadastrado" => $"GAME: {acao} - Usuário: {usuario} - Partida ID: {random.Next(100, 500)} - Placar: {random.Next(0, 5)}x{random.Next(0, 5)}",
                    "Bolão Criado" => $"USER: {acao} - Nome: 'Torneio {random.Next(2025, 2027)}' - Criado por: {usuario}",
                    "Erro de Conexão RabbitMQ" => $"ERROR: Falha na fila 'processamento_bolao' - Tentativa: {random.Next(1, 5)}",
                    "Partida Encerrada" => $"SYSTEM: Partida ID {random.Next(100, 500)} encerrada. Processando resultados no PostgreSQL.",
                    _ => $"INFO: {acao} executada com sucesso por {usuario}."
                };

                logs.Add(logEntry);
            }

            // Inverte para que o log mais recente apareça primeiro (comum em dashboards)
            logs.Reverse();

            return await Task.FromResult(logs);
        }
    }
}
