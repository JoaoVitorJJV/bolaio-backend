using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Grupo
    {
        public Guid Id { get; private set; }
        public string Letra { get; private set; } // Ex: "A", "B", "C"
        private readonly List<Times> _selecoes = new();
        public IReadOnlyCollection<Times> Selecoes => _selecoes.AsReadOnly();

        public Grupo(string letra)
        {
            if (string.IsNullOrWhiteSpace(letra)) throw new ArgumentException("Letra do grupo é obrigatória.");
            Letra = letra.ToUpper();
        }

        public void AdicionarSelecao(Times selecao)
        {
            if (_selecoes.Count >= 4)
                throw new DomainException($"O Grupo {Letra} já possui o limite de 4 seleções.");

            if (_selecoes.Any(s => s.Id == selecao.Id))
                throw new DomainException("Esta seleção já está neste grupo.");

            _selecoes.Add(selecao);
        }
    }
}
