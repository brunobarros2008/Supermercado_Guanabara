using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermercado_Guanabara.Modelo
{
    public class RegistroDeUsuarios
    {
        private readonly List<Usuario> usuariosCadastrados;

        public RegistroDeUsuarios()
        {
            usuariosCadastrados = GestorDeFicheiros.LerUsuarios();
        }

        public Usuario EfetuarLogin(string login, string senha)
        {
            var usuario = usuariosCadastrados.FirstOrDefault(user => user.ConferirLogin(login, senha));
            2return usuario;
        }

        public bool AdicinarUsuario(Usuario novoUsario)
        {
            var esseUsuarioExiste = usuariosCadastrados.Any(user => user.Login.Equals(novoUsario.Login));

            if (!esseUsuarioExiste)
            {
                usuariosCadastrados.Add(novoUsario);
            }

            return !esseUsuarioExiste;
        }

        public bool BloquearUsuario(string login)
        {
            var usuarioParaBloquear = usuariosCadastrados.FirstOrDefault(usuario => usuario.Login.Equals(login));

            var usuarioEncontrado = usuarioParaBloquear != null;

            if (usuarioEncontrado)
            {
                usuarioParaBloquear.Bloquar();
            }

            return usuarioEncontrado;
        }
        
        public bool DesbloquearUsuario(string login)
        {
            var usuarioParaDesbloquear = usuariosCadastrados.FirstOrDefault(usuario => usuario.Login.Equals(login));

            var usuarioEncontrado = usuarioParaDesbloquear != null;

            if (usuarioEncontrado)
            {
                usuarioParaDesbloquear.Desbloquar();
            }

            return usuarioEncontrado;
        }

        public List<Usuario> ListarTodosUsuarios()
        {           
            return usuariosCadastrados;
        }

        public void SalvarRegistro()
        {
            GestorDeFicheiros.EscreverUsuarios(usuariosCadastrados);
        }
    }
}
