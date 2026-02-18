using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using static Application.DTOs.EventosDto;

namespace FutebolWorker
{ 
    public class FutebolWorkerService(IPublishEndpoint publishEndpoint, ILogger<FutebolWorkerService> logger, ApiService apiService) :BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                
                var resultadoApi = await apiService.GetJogosAoVivo();

                if (resultadoApi.Status == "Finalizado")
                {
                    
                    
                    await publishEndpoint.Publish(resultadoApi, stoppingToken);

                    logger.LogInformation("Evento de partida {PartidaId} publicado!", resultadoApi.PartidaId);
                }

                await Task.Delay(TimeSpan.FromMinutes(2), stoppingToken);
            }
        }
    }
}
