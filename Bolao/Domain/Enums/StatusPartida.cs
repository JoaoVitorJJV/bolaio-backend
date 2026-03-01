using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Domain.Enums
{
    public enum StatusPartida
    {
        Agendada,
        [Description("Em Andamento")]
        EmAndamento,
        Concluida
    }
}
