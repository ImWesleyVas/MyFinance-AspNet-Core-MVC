using Microsoft.AspNetCore.Http;
using MyFinance.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class TransacaoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Informa a data!")]
        public string Data { get; set; }
        public string DataFinal { get; set; }//para uso no filtro dos relatórios
        public string Tipo { get; set; }
        public double Valor { get; set; }
        [Required(ErrorMessage = "Informa a descricão!")]
        public string Descricao { get; set; }
        public int ContaId { get; set; }
        public string NomeConta { get; set; }
        public int PlanoContasId { get; set; }
        public string DescricaoPlanoConta { get; set; }
        public int UsuarioId { get; set; }

        //obter o contexto
        public IHttpContextAccessor HttpContextAccessor { get; set; }

        //injeção de dependência
        public TransacaoModel(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public TransacaoModel()
        {

        }

        public List<TransacaoModel> ListaTransacao()
        {

            List<TransacaoModel> lista = new List<TransacaoModel>();
            TransacaoModel item;

            // Filtro - utilizado pela View Extrato

            string filtro = "";

            if (Data != null && DataFinal != null)
            {
                filtro += $"AND T.DATA >= '{DateTime.Parse(Data).ToString("yyyy/MM/dd")}' AND T.DATA <= '{DateTime.Parse(DataFinal).ToString("yyyy/MM/dd")}' ";
            }

            if (Tipo != null)
            {
                if (Tipo != "A")
                {
                    filtro += $" and T.TIPO = '{Tipo}' ";
                }
            }

            if (ContaId != 0)
            {
                filtro += $" and T.CONTA_ID = '{ContaId}' ";
            }

            // Fim Filtro - View Extrato



            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = "SELECT T.ID, T.DATA, T.TIPO, T.VALOR, T.DESCRICAO HISTORICO, T.CONTA_ID, C.NOME CONTA, " +
                    "T.PLANO_CONTAS_ID, P.DESCRICAO AS PLANO_CONTA FROM TRANSACAO T INNER JOIN CONTA C " +
                    $"ON T.CONTA_ID = C.ID INNER JOIN PLANO_CONTAS P ON T.PLANO_CONTAS_ID = P.ID WHERE T.USUARIO_ID = {id_usuario_logado} " +
                    $"{filtro} ORDER BY T.DATA DESC LIMIT 10 ";

            DAL objDAL = new DAL();

            DataTable dt = objDAL.RetDataTable(sql);


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new TransacaoModel();
                item.Id = int.Parse(dt.Rows[i]["ID"].ToString());
                item.Data = DateTime.Parse(dt.Rows[i]["DATA"].ToString()).ToString("dd/MM/yyyy");
                item.Tipo = dt.Rows[i]["TIPO"].ToString();
                item.Valor = double.Parse(dt.Rows[i]["VALOR"].ToString());
                item.Descricao = dt.Rows[i]["HISTORICO"].ToString();
                item.ContaId = int.Parse(dt.Rows[i]["CONTA_ID"].ToString());
                item.NomeConta = dt.Rows[i]["CONTA"].ToString();
                item.PlanoContasId = int.Parse(dt.Rows[i]["PLANO_CONTAS_ID"].ToString());
                item.DescricaoPlanoConta = dt.Rows[i]["PLANO_CONTA"].ToString();

                lista.Add(item);
            }

            return lista;
        }


        public TransacaoModel CarregarRegistro(int? id)
        {
            TransacaoModel item;

            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = "SELECT T.ID, T.DATA, T.TIPO, T.VALOR, T.DESCRICAO HISTORICO, T.CONTA_ID, C.NOME CONTA, " +
                    "T.PLANO_CONTAS_ID, P.DESCRICAO AS PLANO_CONTA FROM TRANSACAO T INNER JOIN CONTA C " +
                    $"ON T.CONTA_ID = C.ID INNER JOIN PLANO_CONTAS P ON T.PLANO_CONTAS_ID = P.ID WHERE T.USUARIO_ID = {id_usuario_logado} " +
                    $"AND T.ID = '{id}'";

            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            item = new TransacaoModel();
            item.Id = int.Parse(dt.Rows[0]["ID"].ToString());
            item.Data = DateTime.Parse(dt.Rows[0]["DATA"].ToString()).ToString("dd/MM/yyyy");
            item.Tipo = dt.Rows[0]["TIPO"].ToString();
            item.Valor = double.Parse(dt.Rows[0]["VALOR"].ToString());
            item.Descricao = dt.Rows[0]["HISTORICO"].ToString();
            item.ContaId = int.Parse(dt.Rows[0]["CONTA_ID"].ToString());
            item.NomeConta = dt.Rows[0]["CONTA"].ToString();
            item.PlanoContasId = int.Parse(dt.Rows[0]["PLANO_CONTAS_ID"].ToString());
            item.DescricaoPlanoConta = dt.Rows[0]["PLANO_CONTA"].ToString();

            return item;
        }

        public void Insert()
        {
            string id_usuario_logado = HttpContextAccessor.HttpContext.Session.GetString("IdUsuarioLogado");
            string sql = "";
            if (Id == 0)
            {
                sql = $"INSERT INTO TRANSACAO (DATA, TIPO, VALOR, DESCRICAO, CONTA_ID, PLANO_CONTAS_ID, " +
                    $"USUARIO_ID) VALUES ('{DateTime.Parse(Data).ToString("yyyy/MM/dd")}', '{Tipo}', '{Valor}', " +
                    $"'{Descricao}', '{ContaId}', '{PlanoContasId}', '{id_usuario_logado}')";
            }
            else
            {
                sql = $"UPDATE TRANSACAO " +
                        $"SET DATA = '{Data}', " +
                        $"TIPO = '{Tipo}', " +
                        $"VALOR = '{Valor}', " +
                        $"DESCRICAO = '{Descricao}', " +
                        $"CONTA_ID = '{ContaId}', " +
                        $"PLANO_CONTAS_ID = '{PlanoContasId}' " +
                        $"WHERE USUARIO_ID = '{id_usuario_logado}' AND ID = '{Id}' ";
            }

            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
        }

        public void Excluir(int id)
        {
            new DAL().ExecutarComandoSQL($"DELETE FROM TRANSACAO WHERE ID = '{id}'");
        }
    }

    public class Dashboard
    {
        public double Total { get; set; }
        public string PlanoConta { get; set; }

        public List<Dashboard> RetornaGraficoPie()
        {
            List<Dashboard> lista = new List<Dashboard>();
            Dashboard item;
            string sql = "select sum(T.VALOR) as TOTAL, P.DESCRICAO " +
                        "from TRANSACAO AS T inner join PLANO_CONTAS as P " +
                        "on T.PLANO_CONTAS_ID = P.ID " +
                        "where T.TIPO = 'D' " +
                        "group by P.DESCRICAO; ";

            DAL objDAL = new DAL();
            DataTable dt = new DataTable();
            dt = objDAL.RetDataTable(sql);

            for(int i = 0; i <  dt.Rows.Count; i++)
            {
                item = new Dashboard();
                item.Total = double.Parse(dt.Rows[i]["Total"].ToString());
                item.PlanoConta = dt.Rows[i]["Descricao"].ToString();
                lista.Add(item);
            }

            return lista;
        }
    }

}

