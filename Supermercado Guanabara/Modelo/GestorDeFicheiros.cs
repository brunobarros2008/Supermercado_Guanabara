using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermercado_Guanabara.Modelo
{
    public class GestorDeFicheiros
    {
        private const string NomeFicheiroDeUsuarios = "usuarios.csv";
        private const string NomeFicheiroDeArtigos = "artigos.csv";
              

        public static List<Usuario> LerUsuarios()
        {
            var listaDeUsuarios = new List<Usuario>();

            if (File.Exists(NomeFicheiroDeArtigos))
            {
                using (var streamReader = File.OpenText(NomeFicheiroDeUsuarios))
                using (var leitorDeCsv = new CsvReader(streamReader, CultureInfo.CurrentCulture))
                {
                    while (leitorDeCsv.Read())
                    {
                        leitorDeCsv.TryGetField<string>(0, out string nome);
                        leitorDeCsv.TryGetField<string>(1, out string login);
                        leitorDeCsv.TryGetField<string>(2, out string senha);
                        leitorDeCsv.TryGetField<string>(3, out string ativo);


                        var usuario = new Usuario(nome, login, senha, bool.Parse(ativo));
                        listaDeUsuarios.Add(usuario);
                    }
                }
            }

            return listaDeUsuarios;
        }

        public static void EscreverUsuarios(List<Usuario> usuarios)
        {
            using (var memoria = new MemoryStream())
            using (var writer = new StreamWriter(memoria))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.CurrentCulture))
            {
                foreach (var usuario in usuarios)
                {
                    csvWriter.WriteField(usuario.Nome);
                    csvWriter.WriteField(usuario.Login);
                    csvWriter.WriteField(usuario.Senha);
                    csvWriter.WriteField(usuario.Ativo);
                    csvWriter.NextRecord();
                }

                writer.Flush();
                var resultado = Encoding.UTF8.GetString(memoria.ToArray());
                File.WriteAllText(NomeFicheiroDeUsuarios, resultado);
            }          
        }
        
        public static void EscreverArtigos(List<Artigo> artigos)
        {
            using (var memoria = new MemoryStream())
            using (var writer = new StreamWriter(memoria))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.CurrentCulture))
            {
                foreach (var artigo in artigos)
                {                    
                    csvWriter.WriteField(artigo.Nome);
                    csvWriter.WriteField(artigo.Preco);
                    csvWriter.WriteField(artigo.Tipo.ToString());
                    csvWriter.NextRecord();
                }

                writer.Flush();
                var resultado = Encoding.UTF8.GetString(memoria.ToArray());
                File.WriteAllText(NomeFicheiroDeArtigos, resultado);
            }          
        }

        public static List<Artigo> LerArtigos()
        {
            var listaDeArtigos = new List<Artigo>();

            if (File.Exists(NomeFicheiroDeArtigos))
            {
                using (var streamReader = File.OpenText(NomeFicheiroDeArtigos))
                using (var leitorDeCsv = new CsvReader(streamReader, CultureInfo.CurrentCulture))
                {
                    while (leitorDeCsv.Read())
                    {
                        leitorDeCsv.TryGetField<string>(0, out string nome);
                        leitorDeCsv.TryGetField<string>(1, out string preco);
                        leitorDeCsv.TryGetField<string>(2, out string tipo);

                        var artigo = new Artigo(nome, decimal.Parse(preco), ObterOTipoDeArtigo(tipo));
                        listaDeArtigos.Add(artigo);
                    }
                }
            }
            return listaDeArtigos;
        }
                
        private static TipoArtigo ObterOTipoDeArtigo(string dado)
        {
            switch (dado)
            {
                case "Hortifruti":
                    return TipoArtigo.Hortifruti; 
                case "Talho":
                    return TipoArtigo.Talho; 
                case "Peixaria":
                    return TipoArtigo.Peixaria; 
                case "Papelaria":
                    return TipoArtigo.Papelaria;
                case "Higiene":
                    return TipoArtigo.Higiene; 
                default:
                    return TipoArtigo.Outros;
            }
        }
    }
}
