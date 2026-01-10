using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }

        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }


        public bool Ativo { get; private set; }


        protected Usuario() { }


        public Usuario(string nome, string email)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Email = email;
            Ativo = true;

        }
        public void DefinirSenha(byte[] passwordHash, byte[] passwordSalt)
        {
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }

        public void AlterarNome(string novoNome)
        {
            if (string.IsNullOrWhiteSpace(novoNome))
            {
                throw new DomainException("O nome do usuário não pode ser vazio.");
            }

            if (novoNome.Length < 3)
            {
                throw new DomainException("O nome deve ter pelo menos 3 caracteres.");
            }

            Nome = novoNome;
        }
    }
}
