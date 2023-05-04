
using System.Net;
using RestSharp;
using Newtonsoft.Json;

using static Category.Classes.Cadastro;
using static Category.Classes.Localizar;


namespace ERPOhama.Service
{
    public class ServiceCadastro
    {
        private readonly RestClient client = new("https://localhost:7278/");

        #region Usuario
        public async Task<RestResponse> Adicionar_Conta(TB_USUARIO Dados)
        {
            var Parametro = JsonConvert.SerializeObject(Dados);

            var request = new RestRequest("ApiCadastro/Adicionar", Method.Post);
            request.AddJsonBody(Parametro);

            var response = await client.ExecuteAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
                return new RestResponse { StatusCode = HttpStatusCode.OK, Content = response.Content };
            else
                return new RestResponse { StatusCode = HttpStatusCode.BadRequest, Content = response.Content };
        }


        public async Task<bool> Alterar_Conta(TB_USUARIO Dados)
        {
            var Parametro = JsonConvert.SerializeObject(Dados);

            var request = new RestRequest("ApiCadastro/Alterar", Method.Put);
            request.AddJsonBody(Parametro);

            var result = await client.ExecuteAsync(request);

            return (result.StatusCode == HttpStatusCode.OK);
        }


        public async Task<bool> Deletar_Conta(string Id)
        {
            var request = new RestRequest("ApiCadastro/Deletar", Method.Delete);
            request.AddHeader("Id", Id);

            var result = await client.ExecuteAsync(request);

            return (result.StatusCode == HttpStatusCode.OK);
        }


        public async Task<TB_USUARIO> Consultar_Conta(string Id)
        {
            var request = new RestRequest("ApiCadastro/Consultar", Method.Get);
            request.AddHeader("Id", Id);

            var result = await client.ExecuteAsync(request);

            var ListaRetorno = new TB_USUARIO();

            if (result.StatusCode == HttpStatusCode.OK)
            {
                ListaRetorno = JsonConvert.DeserializeObject<TB_USUARIO>(result.Content.Substring(1, result.Content.Length - 2).Replace(@"\", ""), new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });

                return ListaRetorno;
            }
            else
            {
                ListaRetorno = new();

                return ListaRetorno;
            }
        }


        public async Task<List<TB_USUARIO>> Listar_Conta()
        {
            var request = new RestRequest("ApiCadastro/Listar", Method.Get);

            var result = await client.ExecuteAsync(request);

            var ListaRetorno = new List<TB_USUARIO>();

            if (result.StatusCode == HttpStatusCode.OK)
            {
                ListaRetorno = JsonConvert.DeserializeObject<List<TB_USUARIO>>(result.Content);

                return ListaRetorno;
            }
            else
            {
                ListaRetorno = new();

                return ListaRetorno;
            }
        }


        public async Task<List<TB_USUARIO>> Pesquisar_Conta(TipoPesquisa Tipo, string Id)
        {
            var request = new RestRequest("ApiCadastro/Pesquisar", Method.Get);
            request.AddHeader("Tipo", Tipo);
            request.AddHeader("Id", WebUtility.UrlEncode(Id));

            var result = await client.ExecuteAsync(request);

            var ListaRetorno = JsonConvert.DeserializeObject<List<TB_USUARIO>>(result.Content);

            return ListaRetorno;
        }

        #endregion
    }
}
