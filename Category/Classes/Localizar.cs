using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Category.Classes
{
    public static class Localizar
    {
        public class TB_CEP
        {
            public string Cep { get; set; } = string.Empty;
            public string Logradouro { get; set; } = string.Empty;
            public string Bairro { get; set; } = string.Empty;
            public string Localidade { get; set; } = string.Empty;
            public string Uf { get; set; } = string.Empty;
        }

        public enum TipoPesquisa
        {
            PorCpf,
            PorNome
        };
    }
}
