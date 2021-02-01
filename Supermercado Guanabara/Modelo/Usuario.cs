using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermercado_Guanabara.Modelo
{
    public class Usuario
    {
        public Usuario(string nome, string login, string senha)
        {
            Nome = nome;
            Login = login;
            Senha = senha;
            Ativo = true;
        }
        
        public Usuario(string nome, string login, string senha, bool ativo)
        {
            Nome = nome;
            Login = login;
            Senha = senha;
            Ativo = ativo;
        }

        public string Nome { get; }
        public string Login { get; }
        public string Senha { get; }
        public bool Ativo { get; private set; }

        public void Bloquar()
        {
            Ativo = false;
        }
        
        public void Desbloquar()
        {
            Ativo = true;
        }

        public bool ConferirLogin(string login, string senha)
        {
            return login.Equals(Login) && senha.Equals(Senha);
        }
    }
}
