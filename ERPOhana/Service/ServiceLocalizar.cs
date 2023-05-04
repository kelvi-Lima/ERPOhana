
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using MudBlazor;
using RestSharp;
using System.Diagnostics;
using System.IO.Ports;
using System.Net;
using System.Net.Mail;
using System.Text;
using static Category.Classes.Cadastro;
using static Category.Classes.Localizar;

namespace ERPOhama.Service
{
    public class ServiceLocalizar
    {
        public async Task<TB_USUARIO> CepUsuario(TB_USUARIO ListaRetorno)
        {
            var client = new HttpClient();

            TB_CEP DadosCep = await client.GetFromJsonAsync<TB_CEP>($"https://viacep.com.br/ws/{ListaRetorno.TX_CEP}/json/");

            if (DadosCep.Cep != null)
            {
                ListaRetorno.TX_CEP = DadosCep.Cep.Replace("-", "");
                ListaRetorno.TX_BAIRRO = DadosCep.Bairro;
                ListaRetorno.TX_ESTADO = DadosCep.Uf;
                ListaRetorno.TX_CIDADE = DadosCep.Localidade;
                ListaRetorno.TX_LOGRADOURO = DadosCep.Logradouro;

                return ListaRetorno;

            }
            else
            {
                ListaRetorno.TX_CEP = null;

                return ListaRetorno;
            }
        }

    }
}
