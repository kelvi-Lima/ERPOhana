
namespace Category.Classes
{
    public static class Cadastro
    {
        public class TB_USUARIO
        {
            public int ID { get; set; }
            public string TX_NOME { get; set; } = string.Empty;
            public int? NR_IDADE { get; set; }
            public string OP_ESTADO_CIVIL { get; set; } = string.Empty;
            public string TX_CPF { get; set; } = string.Empty;
            public string TX_EMAIL { get; set; } = string.Empty;
            public string TX_TELEFONE { get; set; } = string.Empty;
            public string TX_CEP { get; set; } = string.Empty;
            public string TX_LOGRADOURO { get; set; } = string.Empty;
            public string TX_NUMERO { get; set; } = string.Empty;
            public string TX_BAIRRO { get; set; } = string.Empty;
            public string TX_CIDADE { get; set; } = string.Empty;
            public string TX_ESTADO { get; set; } = string.Empty;
        }

    }
}
