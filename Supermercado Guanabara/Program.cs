using Supermercado_Guanabara.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermercado_Guanabara
{
    class Program
    {
        static void Main(string[] args)
        {
            var registroDeUsuarios = new RegistroDeUsuarios();
            var estoque = new Estoque();

            PassoDeAutenticacao(registroDeUsuarios);
            
            var fecharPrograma = false;
            while (!fecharPrograma)
            {
                Console.WriteLine("---------- Menu ----------");
                Console.WriteLine("1 - Gerir usuarios");
                Console.WriteLine("2 - Gerir estoque");
                Console.WriteLine("0 - Sair");

                string respostaDoMenu = Console.ReadLine();

                if (respostaDoMenu.Equals("1"))
                {
                    GerirUsuarios(registroDeUsuarios);
                }
                if (respostaDoMenu.Equals("2"))
                {
                    GerirEstoque(estoque);
                }
                else
                {
                    fecharPrograma = true;
                }




            }
        }

        private static void GerirEstoque(Estoque estoque)
        {
            bool voltarAoMenuAnterior = false;

            while (!voltarAoMenuAnterior)
            {

                Console.WriteLine("---------- Menu ----------");
                Console.WriteLine("1 - Adicionar artigo");
                Console.WriteLine("2 - Remover artigo");
                Console.WriteLine("3 - Gerar relatório do estoque");                
                Console.WriteLine("0 - Voltar ao menu anterior");
                string respostaDoMenu = Console.ReadLine();

                switch (respostaDoMenu)
                {
                    case "1":
                        AdicionarArtigoAoEstoque(estoque);
                        break;
                    case "2":
                        RemoverArtigoDoEstoque(estoque);
                        break;
                    case "3":
                        GerarRelatorioDoEstoque(estoque);
                        break;                   
                    default:
                        voltarAoMenuAnterior = true;
                        break;

                }

            }
        }

        private static void GerarRelatorioDoEstoque(Estoque estoque)
        {
            var todosOsArtigos = estoque.GerarRelatorioDoEstoque();
            Console.Clear();
            foreach (var artigo in todosOsArtigos)
            {                
                Console.WriteLine($"Nome:{artigo.NomeDoArtigo}     Quantidade: {artigo.Quantidade}");
            }
        }

        private static void RemoverArtigoDoEstoque(Estoque estoque)
        {
            Console.Write("\t Entre com o nome do artigo: ");
            string nome = Console.ReadLine();
                        
            estoque.RemoverArtigo(nome);

            Console.Clear();
            Console.WriteLine($"Artigo removido com sucesso.");
        }

        private static void AdicionarArtigoAoEstoque(Estoque estoque)
        {
            Console.Write("\t Entre com o nome do artigo: ");
            string nome = Console.ReadLine();
            Console.Write("\t Entre com o preço do artigo: ");
            string preco = Console.ReadLine();
            Console.Write("\t Entre com o tipo do artigo: ");
            Console.Write("\t Hortifruti-1, Talho-2, Peixaria-3, Papelaria-4, Higiene-5, Outros-6 ");
            var tipo = ObterOTipoDeArtigo(Console.ReadLine());
            Console.WriteLine("\n");

            var resultadoDecimal = decimal.TryParse(preco, out var precoEmDecimal);

            var novoArtigo = new Artigo(nome, precoEmDecimal, tipo);
            estoque.AdicinarArtigo(novoArtigo);

            Console.Clear();
            Console.WriteLine($"Artigo cadastrado com sucesso.");         
        }

        private static TipoArtigo ObterOTipoDeArtigo(string dado)
        {
            switch (dado)
            {
                case "1":
                    return TipoArtigo.Hortifruti;
                case "2":
                    return TipoArtigo.Talho;
                case "3":
                    return TipoArtigo.Peixaria;
                case "4":
                    return TipoArtigo.Papelaria;
                case "5":
                    return TipoArtigo.Higiene;
                default:
                    return TipoArtigo.Outros;
            }
        }

        private static void GerirUsuarios(RegistroDeUsuarios registroDeUsuarios)
        {
            bool voltarAoMenuAnterior = false;

            while (!voltarAoMenuAnterior)
            {

                Console.WriteLine("---------- Menu ----------");
                Console.WriteLine("1 - Adicionar usuario");                
                Console.WriteLine("2 - Bloquear usuario");
                Console.WriteLine("3 - Desbloquear usuario");
                Console.WriteLine("4 - Listar todos os usuarios");
                Console.WriteLine("0 - Voltar ao menu anterior");
                string respostaDoMenu = Console.ReadLine();

                switch (respostaDoMenu)
                {
                    case "1":
                        CriarUsuario(registroDeUsuarios);
                        break;
                    case "2":
                        BloquarUsuario(registroDeUsuarios);
                        break; 
                    case "3":
                        DesbloquarUsuario(registroDeUsuarios);
                        break; 
                    case "4":
                        ListarUsuarios(registroDeUsuarios);
                        break;
                    default:
                        voltarAoMenuAnterior = true;
                        break;
                }
            }
        }

        private static void ListarUsuarios(RegistroDeUsuarios registroDeUsuarios)
        {
            var todosOsUsuarios = registroDeUsuarios.ListarTodosUsuarios();
            
            Console.Clear();
            foreach (var usuario in todosOsUsuarios)
            {
                var status = usuario.Ativo ? "Ativo" : "Bloquado";
                Console.WriteLine($"Login:{usuario.Login}     Status: {status}");
            }

        }

        private static void PassoDeAutenticacao(RegistroDeUsuarios registroDeUsuarios)
        {
            Console.WriteLine("---------- Menu ----------");
            Console.WriteLine("1 - Primeiro Acesso");
            Console.WriteLine("2 - Fazer Login");
            string respostaDoMenu = Console.ReadLine();

            if (respostaDoMenu.Equals("1"))
            {
                CriarUsuario(registroDeUsuarios);
            }
            else
            {
                FazerLogin(registroDeUsuarios);
            }
        }

        private static void CriarUsuario(RegistroDeUsuarios registroDeUsuarios)
        {
            bool tentarNovamente = true;

            while (tentarNovamente)
            {                
                Console.Write("\t Entre com o nome do usuario: ");
                string nome = Console.ReadLine();
                Console.Write("\t Entre com o login do usuario: ");
                string login = Console.ReadLine();
                Console.Write("\t Entre com a senha do usuario: ");
                string senha = Console.ReadLine();
                Console.WriteLine("\n");

                var novoUsuario = new Usuario(nome, login, senha);
                var resultado = registroDeUsuarios.AdicinarUsuario(novoUsuario);
                
                
                if (!resultado)
                {
                    Console.Clear();
                    Console.WriteLine($"Login de usuario não disponível.");
                    Console.WriteLine($"Tente novamente!");
                }
                else
                {
                    Console.Clear();
                    registroDeUsuarios.SalvarRegistro();
                    Console.Write("Usuario criado com sucesso.");
                    tentarNovamente = false;
                 }
            }
        }

        private static void BloquarUsuario(RegistroDeUsuarios registroDeUsuarios)
        {
            bool tentarNovamente = true;

            while (tentarNovamente)
            {               
                Console.Write("\t Entre com o login do usuario: ");
                string login = Console.ReadLine();             
                var resultado = registroDeUsuarios.BloquearUsuario(login);


                if (!resultado)
                {
                    Console.Clear();
                    Console.WriteLine($"Login de usuario não encontrado.");
                    Console.WriteLine($"Tente novamente!");
                }
                else
                {
                    Console.Clear();
                    registroDeUsuarios.SalvarRegistro();
                    Console.Write("Usuario bloqueado com sucesso.");
                    tentarNovamente = false;
                }
            }
        }

        private static void DesbloquarUsuario(RegistroDeUsuarios registroDeUsuarios)
        {
            bool tentarNovamente = true;

            while (tentarNovamente)
            {
                Console.Write("\t Entre com o login do usuario: ");
                string login = Console.ReadLine();
                var resultado = registroDeUsuarios.DesbloquearUsuario(login);


                if (!resultado)
                {
                    Console.Clear();
                    Console.WriteLine($"Login de usuario não encontrado.");
                    Console.WriteLine($"Tente novamente!");
                }
                else
                {
                    Console.Clear();
                    registroDeUsuarios.SalvarRegistro();
                    Console.Write("Usuario desbloqueado com sucesso.");
                    tentarNovamente = false;
                }
            }
        }

        private static void FazerLogin(RegistroDeUsuarios registroDeUsuarios)
        {
            bool tentarNovamente = true;

            while (tentarNovamente)
            {

                Console.WriteLine("---------- Acesso ----------");
                Console.Write("\t Entre com o login do usuario: ");
                string login = Console.ReadLine();
                Console.Write("\t Entre com a senha do usuario: ");
                string senha = Console.ReadLine();
                Console.WriteLine("\n");

                var usuario = registroDeUsuarios.EfetuarLogin(login, senha);
                if (usuario == null)
                {
                    Console.Clear();
                    Console.WriteLine($"Login ou senha não conferam.");
                    Console.WriteLine($"Tente novamente!");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Olá {usuario.Login}!");
                    Console.WriteLine($"Seja bem vindo!");
                    tentarNovamente = false;
                }
            }


        }
    }
}
