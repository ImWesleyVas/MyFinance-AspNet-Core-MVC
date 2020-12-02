using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Controllers
{
    public class TransacaoController : Controller
    {
        IHttpContextAccessor HttpContextAccessor;

        //Injeção de dependência: recebe o conexto para acesso às variáveis de sessão.
        public TransacaoController(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            TransacaoModel objConta = new TransacaoModel(HttpContextAccessor);
            ViewBag.ListaTransacao = objConta.ListaTransacao();
            return View();
        }


        [HttpPost]
        public IActionResult Registrar(TransacaoModel formulario)
        {
            if (ModelState.IsValid)
            {
                formulario.HttpContextAccessor = HttpContextAccessor;
                formulario.Insert();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Registrar(int? id)
        {
            if (id != null)
            {
                TransacaoModel objTransacao = new TransacaoModel(HttpContextAccessor);
                ViewBag.Registro = objTransacao.CarregarRegistro(id);
            }
            ViewBag.ListaContas = new ContaModel(HttpContextAccessor).ListaConta();
            ViewBag.ListaPlano = new PlanoContaModel(HttpContextAccessor).ListaPlano();

            return View();
        }


        [HttpGet]
        public IActionResult ExcluirTransacao(int id)
        {
            TransacaoModel objTransacao = new TransacaoModel(HttpContextAccessor);
            ViewBag.Registro = objTransacao.CarregarRegistro(id);
            return View();
        }

        public IActionResult Excluir(int id)
        {
            TransacaoModel objConta = new TransacaoModel(HttpContextAccessor);
            objConta.Excluir(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [HttpPost]
        public IActionResult Extrato(TransacaoModel formulario)
        {
            formulario.HttpContextAccessor = HttpContextAccessor;
            ViewBag.ListaTransacao = formulario.ListaTransacao();
            ViewBag.ListaConta = new ContaModel(HttpContextAccessor).ListaConta();
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

    }
}
