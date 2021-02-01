using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supermercado_Guanabara.Modelo
{
    public class Artigo
    {
        public Artigo(string nome, decimal preco, TipoArtigo tipo)
        {            
            Nome = nome;
            Preco = preco;
            Tipo = tipo;
        }
                
        public string  Nome { get; }      
        public decimal  Preco { get; }        
        public TipoArtigo  Tipo { get; }
    }
}
