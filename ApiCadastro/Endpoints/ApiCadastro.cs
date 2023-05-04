
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using Dapper;
using System.Net;

using static Category.Classes.Cadastro;
using static Category.Classes.Localizar;

namespace ApiCadastro.Endpoints
{
    public static class ApiCadastro
    {
        public static void MapCadastro(this WebApplication app, string Con)
        {
            app.MapPost("ApiCadastro/Adicionar", async (TB_USUARIO Tarefa) =>
            {
                using (MySqlConnection Lig = new MySqlConnection(Con))
                {
                    await Lig.OpenAsync();

                    var Resultado = true;

                    try
                    {
                        var Dados = await Lig.ExecuteAsync(@$"INSERT INTO TB_USUARIO (TX_NOME, NR_IDADE, OP_ESTADO_CIVIL, TX_CPF, TX_EMAIL, TX_TELEFONE, TX_CEP, TX_LOGRADOURO, TX_NUMERO, TX_BAIRRO, TX_CIDADE, TX_ESTADO)
                                                                               VALUES('{Tarefa.TX_NOME}', {Tarefa.NR_IDADE}, '{Tarefa.OP_ESTADO_CIVIL}', '{Tarefa.TX_CPF}', 
                                                                                      '{Tarefa.TX_EMAIL}', '{Tarefa.TX_TELEFONE}', '{Tarefa.TX_CEP}', '{Tarefa.TX_LOGRADOURO}', 
                                                                                      '{Tarefa.TX_NUMERO}', '{Tarefa.TX_BAIRRO}', '{Tarefa.TX_CIDADE}', '{Tarefa.TX_ESTADO}')");
                    }
                    catch (Exception ex)
                    {
                        Resultado = false;

                        Debug.WriteLine(ex.Message);
                    }
                    finally
                    {
                        await Lig.CloseAsync();
                    }

                    return Resultado ? Results.Ok("Insert com Sucesso!") : Results.StatusCode(400);
                }

            }).WithTags("Cadastro");



            app.MapPut("ApiCadastro/Alterar", async (TB_USUARIO Tarefa) =>
            {
                using (MySqlConnection Lig = new MySqlConnection(Con))
                {
                    await Lig.OpenAsync();

                    var Resultado = true;

                    try
                    {
                        var Dados = await Lig.ExecuteAsync(@$"UPDATE TB_USUARIO SET TX_NOME = '{Tarefa.TX_NOME}', NR_IDADE = '{Tarefa.NR_IDADE}', OP_ESTADO_CIVIL = '{Tarefa.OP_ESTADO_CIVIL}', TX_CPF = '{Tarefa.TX_CPF}', 
                                                                                    TX_EMAIL = '{Tarefa.TX_EMAIL}', TX_TELEFONE = '{Tarefa.TX_TELEFONE}', TX_CEP = '{Tarefa.TX_CEP}', TX_LOGRADOURO = '{Tarefa.TX_LOGRADOURO}', 
                                                                                    TX_NUMERO  = '{Tarefa.TX_NUMERO}', TX_BAIRRO = '{Tarefa.TX_BAIRRO}', TX_CIDADE = '{Tarefa.TX_CIDADE}', TX_ESTADO = '{Tarefa.TX_ESTADO}'
                                                                                    WHERE ID = {Tarefa.ID}");
                    }
                    catch (Exception ex)
                    {
                        Resultado = false;

                        Debug.WriteLine(ex.Message);
                    }
                    finally
                    {
                        await Lig.CloseAsync();
                    }

                    return Resultado ? Results.Ok("Update com Sucesso!") : Results.StatusCode(400);
                }

            }).WithTags("Cadastro");



            app.MapDelete("ApiCadastro/Deletar", async ([FromHeader] string Id) =>
            {
                using (MySqlConnection Lig = new MySqlConnection(Con))
                {
                    await Lig.OpenAsync();

                    var Resultado = true;

                    try
                    {
                        var Dados = await Lig.ExecuteAsync($"DELETE FROM TB_USUARIO WHERE ID = '{Id}'");
                    }
                    catch (Exception ex)
                    {
                        Resultado = false;

                        Debug.WriteLine(ex.Message);
                    }
                    finally
                    {
                        await Lig.CloseAsync();
                    }

                    return Resultado ? Results.Ok("Delete com Sucesso!") : Results.StatusCode(400);
                }

            }).WithTags("Cadastro");



            app.MapGet("ApiCadastro/Consultar", async ([FromHeader] string Id) =>
            {
                using (MySqlConnection Lig = new MySqlConnection(Con))
                {
                    await Lig.OpenAsync();

                    var Resultado = true;

                    IEnumerable<TB_USUARIO> Dados = new List<TB_USUARIO>();

                    try
                    {
                        Dados = await Lig.QueryAsync<TB_USUARIO>($"SELECT * FROM TB_USUARIO WHERE ID = {Id}");
                    }
                    catch (Exception ex)
                    {
                        Resultado = false;

                        Debug.WriteLine(ex.Message);
                    }
                    finally
                    {
                        await Lig.CloseAsync();
                    }

                    return Resultado ? Results.Ok(Dados) : Results.StatusCode(400);
                }

            }).WithTags("Cadastro");



            app.MapGet("ApiCadastro/Listar", async () =>
            {
                using (MySqlConnection Lig = new MySqlConnection(Con))
                {
                    await Lig.OpenAsync();

                    var Resultado = true;

                    IEnumerable<TB_USUARIO> Dados = new List<TB_USUARIO>();

                    try
                    {
                        Dados = await Lig.QueryAsync<TB_USUARIO>("SELECT * FROM TB_USUARIO");
                    }
                    catch (Exception ex)
                    {
                        Resultado = false;

                        Debug.WriteLine(ex.Message);
                    }
                    finally
                    {
                        await Lig.CloseAsync();
                    }

                    return Resultado ? Results.Ok(Dados.ToList()) : Results.StatusCode(400);
                }

            }).WithTags("Cadastro");



            app.MapGet("ApiCadastro/Pesquisar", async ([FromHeader] string Tipo, [FromHeader] string Id) =>
            {
                Id = WebUtility.UrlDecode(Id);

                using (MySqlConnection Lig = new MySqlConnection(Con))
                {
                    await Lig.OpenAsync();

                    if (Tipo == TipoPesquisa.PorCpf.ToString())
                    {
                        var Resultado = await Lig.QueryAsync<TB_USUARIO>($"SELECT * FROM TB_USUARIO WHERE TX_CPF LIKE '%" + Id + "%'");

                        await Lig.CloseAsync();

                        return Results.Ok(Resultado.ToList());
                    }

                    else
                    {
                        var Resultado = await Lig.QueryAsync<TB_USUARIO>($"SELECT * FROM TB_USUARIO WHERE UPPER(TX_NOME) LIKE '%" + Id.ToUpper() + "%'");

                        await Lig.CloseAsync();

                        return Results.Ok(Resultado.ToList());
                    }
                }

            }).WithTags("Cadastro");
        }
    }
}
