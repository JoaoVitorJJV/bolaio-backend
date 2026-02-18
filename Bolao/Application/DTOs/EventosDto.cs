using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class EventosDto
    {
        public record PartidaAtualizadaEvent(int PartidaId, int GolsMandante, int GolsVisitante,string Status,DateTime DataOcorrencia);
        public record CountriesResponse(string name, string code, string flag);
    }
}
