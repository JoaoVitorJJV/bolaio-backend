using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Atributos
{
    // Este atributo serve apenas para marcar o código
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    internal class Atributos : Attribute
    {
    }

    // Se sua ferramenta suportar configuração de tipo de associação
    [AttributeUsage(AttributeTargets.Property)]
    internal class PlantUmlAssociationAttribute : Attribute
    {
        public string AssociationType { get; set; }
        public PlantUmlAssociationAttribute(string type = "Association")
        {
            AssociationType = type;
        }
    }
}
