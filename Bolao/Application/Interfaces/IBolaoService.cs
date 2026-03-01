using Application.DTOs;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using static Application.DTOs.BolaoDto;
using static Application.DTOs.PalpitesDto;

namespace Application.Interfaces
{
    public interface IBolaoService
    {
        Task<CriarBolaoResponseDto> CriarBolaoAsync(CriarBolaoDto bolaoDto);        
        Task<ListarBoloesDto> GetBolaoByIdNoTrackAsync(string id);
        Task<List<GetTimesDto>> GetTimes();            
        Task<List<GetPartidasDto>> ListarPartidas();
        List<TiposBolao> ObterTiposBolao();
        List<VisibilidadeDto> ObterVisibilidadeBolao();
        Task ProcessarPontuacaoPartidaAsync(Guid partidaId, int golsMandante, int golsVisitante);
        Task RegistrarPalpiteAsync(RegistrarPalpiteDto dto,Guid idUsuario);
    }
}
