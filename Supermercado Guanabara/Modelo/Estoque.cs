using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermercado_Guanabara.Modelo
{
    public class Estoque
    {
        private readonly List<Artigo> listaDeArtigos;

        public Estoque()
        {
            listaDeArtigos = GestorDeFicheiros.LerArtigos();
        }

        public void AdicinarArtigo(Artigo novoArtigo)
        {
            
            listaDeArtigos.Add(novoArtigo);            
        }

        public void RemoverArtigo(string nomeDoArtigo)
        {
            var artigoParaSerRemovido = listaDeArtigos.FirstOrDefault(artigo => artigo.Nome.Equals(nomeDoArtigo));
            if(artigoParaSerRemovido != null)
            {
                listaDeArtigos.Remove(artigoParaSerRemovido);
            }
        }

        public List<RelatorioDoArtigo> GerarRelatorioDoEstoque()
        {
            var relatorio = new List<RelatorioDoArtigo>();
            var artigosAgrupadosPorNome = listaDeArtigos.GroupBy(artigo => artigo.Nome);

            foreach(var grupo in artigosAgrupadosPorNome)
            {
                relatorio.Add(new RelatorioDoArtigo(grupo.Key, grupo.Count()));
            }

            return relatorio;
        }

        public void SalvarEstoque()
        {
            GestorDeFicheiros.EscreverArtigos(listaDeArtigos);
        }


    }
}
