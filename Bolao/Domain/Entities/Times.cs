using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Times
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Sigla { get; private set; }
        public string BandeiraUrl { get; private set; }
        public Times(string nome, string sigla, string bandeiraUrl)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Sigla = sigla;
            BandeiraUrl = bandeiraUrl;
        }
    }
}
