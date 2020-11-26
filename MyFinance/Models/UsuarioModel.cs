using MyFinance.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class UsuarioModel
    {
        
        public int Id { get; set; }
        [Required(ErrorMessage ="Inform seu Nome")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Informe o seu Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Informe a sua Senha")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "Informa sua data de nascimento")]
        public String DataNascimento { get; set; }


        public bool ValidarLogin()
        {
            string sql = $"SELECT ID, NOME, DATA_NASCIMENTO FROM USUARIO WHERE EMAIL = '{Email}' AND SENHA = '{Senha}'";
            DAL objDAL = new DAL();
            DataTable dt = objDAL.RetDataTable(sql);

            if(dt != null)
            {
                if (dt.Rows.Count == 1)
                {
                    Id = int.Parse(dt.Rows[0]["ID"].ToString());
                    Nome = dt.Rows[0]["NOME"].ToString();
                    DataNascimento = dt.Rows[0]["DATA_NASCIMENTO"].ToString();
                    return true;
                }
            }

            return false;
        }

        public void RegistrarUsuario()
        {
            string dataNascimento = DateTime.Parse(DataNascimento).ToString("yyyy/MM/dd");
            string sql = $"INSERT INTO USUARIO (NOME, EMAIL, SENHA, DATA_NASCIMENTO) VALUES ('{Nome}', '{Email}', '{Senha}','{dataNascimento}')";
            DAL objDAL = new DAL();
            objDAL.ExecutarComandoSQL(sql);
        }

    }
}
