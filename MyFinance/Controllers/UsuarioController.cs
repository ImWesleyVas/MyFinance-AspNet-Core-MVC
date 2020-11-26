using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public IActionResult Login(int? id)
        {
            if(id != null)
            {
                if(id == 0)
                {
                    // HttpContext.Session.SetString("NomeUsuarioLogado", string.Empty);  /// professor felipe passou essa linha de comando
                    // HttpContext.Session.SetString("IdUsuarioLogado", string.Empty);
                    
                    // preferi esta que achei mais elegante e segura, pois remove a chave completa
                    HttpContext.Session.Remove("NomeUsuarioLogado");   
                    HttpContext.Session.Remove("IdUsuarioLogado");

                }
            }
            return View();
        }

        [HttpPost]
        public IActionResult ValidarLogin(UsuarioModel usuario)
        {
            bool login = usuario.ValidarLogin();
            if (login)
            {
                HttpContext.Session.SetString("NomeUsuarioLogado", usuario.Nome);
                HttpContext.Session.SetString("IdUsuarioLogado", usuario.Id.ToString());
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["MensagemLoginInvalido"] = "Dados de login inválidos!";
                return RedirectToAction("Login");
            }
        }
    }
}
