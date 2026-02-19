using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Times
    {
        public Guid Id { get; private set; }
        public string? Nome { get; private set; } = "";
        public string? Sigla { get; private set; } = "";
        public string? BandeiraUrl { get; private set; }
        public string? Confederacao { get; private set; }
        public string? CodigoExterno { get; private set; }
        public bool IsNacional { get; private set; } = false;
        public Times(string nome, string sigla, string bandeiraUrl,string confederacao)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Sigla = sigla;
            BandeiraUrl = bandeiraUrl;
            Confederacao = confederacao;
        }
        public Times(string nome, string bandeiraUrl, string codigoExterno)
        {
            Id = Guid.NewGuid();
            Nome = nome;            
            BandeiraUrl = bandeiraUrl;
            CodigoExterno = codigoExterno;

        }
        public Times(string nome, string bandeiraUrl, string codigoExterno,bool isNacional)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            BandeiraUrl = bandeiraUrl;
            CodigoExterno = codigoExterno;

        }
    }
}
