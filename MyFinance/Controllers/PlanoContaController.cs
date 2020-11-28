using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Controllers
{
    public class PlanoContaController : Controller
    {

        IHttpContextAccessor HttpContextAccessor;

        //Injeção de dependência: recebe o conexto para acesso às variáveis de sessão.
        public PlanoContaController(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            PlanoContaModel objConta = new PlanoContaModel(HttpContextAccessor);
            ViewBag.ListaPlano = objConta.ListaPlano();
            return View();
        }

        public IActionResult CriarPlano()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CriarPlano(PlanoContaModel formulario)
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
        public IActionResult CriarPlano(int? id)
        {
            if (id != null)
            {
                PlanoContaModel objPlano = new PlanoContaModel(HttpContextAccessor);
                ViewBag.Registro = objPlano.CarregarRegistro(id);
            }

            return View();
        }

        public IActionResult ExcluirPlano(int id)
        {
            PlanoContaModel objConta = new PlanoContaModel(HttpContextAccessor);
            objConta.Excluir(id);
            return RedirectToAction("Index");
        }

    }
}
