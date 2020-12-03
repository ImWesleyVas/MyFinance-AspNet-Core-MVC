using System;
using Xunit;
using MyFinance.Models;

namespace ProjetoTeste
{
    public class UnitTestModels
    {
        [Fact]
        public void Test1()
        {
            UsuarioModel usuarioModel = new UsuarioModel();

            usuarioModel.Email = "wesley@financas.com.br";
            usuarioModel.Senha = "123456";
            bool result = usuarioModel.ValidarLogin();
            Assert.True(result);

        }

        [Fact]
        public void TesteRegistrarUsuario()
        {
            UsuarioModel usuarioModel = new UsuarioModel();
            usuarioModel.Nome = "Teste";
            usuarioModel.DataNascimento = "1987/05/22";
            usuarioModel.Email = "usurio@teste.com.br";
            usuarioModel.Senha = "123123";
            usuarioModel.RegistrarUsuario();
            bool result = usuarioModel.ValidarLogin();
            Assert.True(result);

        }
    }
}
