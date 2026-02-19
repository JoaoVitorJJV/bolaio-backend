using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Application.DTOs
{
    public class TeamsDto
    {
        public record TeamResponse(
        [property: JsonPropertyName("response")] List<TeamItem> Response
    );

        public record TeamItem(
            [property: JsonPropertyName("team")] TeamDetails Team
        );

        public record TeamDetails(
            [property: JsonPropertyName("id")] int Id,
            [property: JsonPropertyName("name")] string Name,
            [property: JsonPropertyName("code")] string Code,
            [property: JsonPropertyName("country")] string Country,
            [property: JsonPropertyName("logo")] string Logo,
            [property: JsonPropertyName("national")] bool IsNational
        );
    }
}
