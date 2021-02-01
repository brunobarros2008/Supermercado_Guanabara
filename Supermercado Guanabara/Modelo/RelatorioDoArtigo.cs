using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermercado_Guanabara.Modelo
{
    public class RelatorioDoArtigo
    {
        public RelatorioDoArtigo(string nomeDoArtigo, int quantidade)
        {
            NomeDoArtigo = nomeDoArtigo;
            Quantidade = quantidade;
        }

        public string NomeDoArtigo { get; }
        public int Quantidade { get; }
    }
}
