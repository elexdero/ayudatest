using PA.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PA.Controllers
{
    public class PAController : Controller
    {

        public IActionResult Regis()
        {
            return View();
        }

        public IActionResult Nosotros()
        {
            return View();
        }

        public IActionResult Mision()
        {
            return View();
        }

        public IActionResult RegisConfirm(string Nombre, string ApPaterno, string ApMaterno, int Boleta, string TelUser, string Correo)
        {
            ViewBag.Nombre = Nombre;
            ViewBag.ApPaterno = ApPaterno;
            ViewBag.ApMaterno = ApMaterno;
            ViewBag.Boleta = Boleta;
            ViewBag.TelUser = TelUser;
            ViewBag.Correo = Correo;
            IDModel ID = new IDModel();
            ID._Nombre = Nombre;
            ID._ApPaterno = ApPaterno;
            ID._ApMaterno = ApMaterno;
            ID._Boleta = Boleta;
            ID._TelUser = TelUser;
            ID._Correo = Correo;
            ViewBag.ID = ID.ObtenerID();
            return View();
        }

        public ContentResult error()
        {
            ContentResult content = new ContentResult()
            {
                StatusCode = 422
            };
            return content;
        } 
    }
}
