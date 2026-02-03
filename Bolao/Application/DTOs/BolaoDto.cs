using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class BolaoDto
    {
        public record CriarBolaoDto(Usuario Organizador, string Nome, TipoVisibilidade Visibilidade, decimal Valor, DateTime DtFechamento, TipoBolao TipoBolao,int maxParticipantes, Guid partidaId);
        public record CriarBolaoResponseDto(string Nome,DateTime DtFechamento,string Organizador);
        public record ListarBoloesDto(Guid Id, string Nome, TipoVisibilidade Visibilidade, decimal Valor, DateTime DtFechamento, TipoBolao TipoBolao, string Organizador,string qtdParticipantes,string premio);
        public record RegistrarPalpiteDto(Guid BolaoId, int GolsTimeA, int GolsTimeB);
    }
}
