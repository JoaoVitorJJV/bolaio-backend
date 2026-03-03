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

        public bool Admin { get; private set; } = false;
        public bool Ativo { get; private set; }
        public Carteira? Carteira { get; set; }


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

        public void AlterarUsuario(string nome,string email)
        {
            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(email))
            {
                throw new DomainException("Informações inválidas!");
            }

            this.Nome = nome;
            this.Email = email;

        }

        public void Ativar(bool ativo)
        {
            this.Ativo = ativo;
        }
    }
}
