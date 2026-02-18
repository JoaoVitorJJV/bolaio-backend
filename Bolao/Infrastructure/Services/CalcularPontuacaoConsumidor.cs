using Application.Interfaces;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using static Application.DTOs.EventosDto;

namespace Infrastructure.Services
{
    public class CalcularPontuacaoConsumidor(IBolaoService bolaoService)/* : IConsumer<PartidaFinalizadaEvent>*/
    {
        /*public async Task Consume(ConsumeContext<PartidaFinalizadaEvent> context)
        {
            var evento = context.Message;
            // Aqui entra sua lógica de DDD: busca o bolão, calcula pontos, salva no Postgres
            await bolaoService.ProcessarPontuacaoPartidaAsync(evento.PartidaId, evento.GolsMandante, evento.GolsVisitante);
        }*/
    }
}
