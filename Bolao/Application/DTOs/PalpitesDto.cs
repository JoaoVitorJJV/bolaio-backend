using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class PalpitesDto
    {
        public record PalpitesAtivosDto(
            string nomeBolao,
            string descricaoJogo,
            string timeVencedor,
            string placarPalpite,
            string statusJogo,
            string valorApostado,
            string retornoPotencial            
            );
    }
}
