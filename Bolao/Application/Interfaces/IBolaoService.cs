using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static Application.DTOs.BolaoDto;

namespace Application.Interfaces
{
    public interface IBolaoService
    {
        Task<CriarBolaoResponseDto> CriarBolaoAsync(CriarBolaoDto bolaoDto);
        Task RegistrarPalpiteAsync(RegistrarPalpiteDto dto,Guid idUsuario);
    }


}
