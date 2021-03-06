using Pagina_de_Pedidos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pagina_de_Pedidos.Controllers
{
    public class UsuarioController : Controller
    {
        private PaginaPedidoEntities bd = new PaginaPedidoEntities();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string contrasena)
        {
            var uss = bd.Usuario.FirstOrDefault(u => u.email == email && u.contrasena == contrasena);
            if (uss != null)
            {
                Session["Nombre"] = uss.nombre + " - " + uss.email;
                Session["iduss"] = uss.id_usuario;
                Session["tipoUss"] = uss.id_tipousuario;
                return RedirectToAction("Index", "Pedido");
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Login");
        }
        // MANTENEDOR CREAR
        public ActionResult Crear()
        {
            ViewBag.id_tipoUsuario = new SelectList(bd.TipoUsuario, "id_tipoUsuario", "nombre");
            return View();
        }
        [HttpPost]
        public ActionResult Crear(Usuario usuario)
        {
            bd.Usuario.Add(usuario);
            bd.SaveChanges();
            ViewBag.id_tipoUsuario = new SelectList(bd.TipoUsuario, "id_tipoUsuario", "nombre");
            return View();
        }
        public ActionResult Index()
        {
            var usuario = bd.Usuario.ToList();
            return View(usuario);
        }
        //MANTENEDOR EDITAR
        public ActionResult Editar(int? id)
        {
            if (id != null)
            {
                var usuario = bd.Usuario.Find(id);
                if (usuario != null)
                {
                    return View(usuario);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult Editar(Usuario usuario)
        {
            bd.Entry(usuario).State = System.Data.EntityState.Modified;
            bd.SaveChanges();
            return RedirectToAction("Index");
        }
        //MANTENEDOR ELIMINAR
        public ActionResult Eliminar(int? id)
        {
            if (id != null)
            {
                var usuario = bd.Usuario.Find(id);
                if (usuario != null)
                {
                    return View(usuario);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Eliminar(int id_usuario)
        {
            var usuario = bd.Usuario.Find(id_usuario);
            if (usuario != null)
            {
                bd.Usuario.Remove(usuario);
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult RutExiste(string existe)
        {
            if (!string.IsNullOrEmpty(existe))
            {
                var q = bd.Usuario.FirstOrDefault(e => e.rut.ToLower().Equals(existe.ToLower()));
                if (q != null)
                {
                    string nombre = q.nombre;
                    return Json(nombre);
                }
            }
            return Json("");
        }
        public ActionResult EmailExiste(string existe)
        {
            if (!string.IsNullOrEmpty(existe))
            {
                var q = bd.Usuario.FirstOrDefault(e => e.email.ToLower().Equals(existe.ToLower()));
                if (q != null)
                {
                    string nombre = q.nombre;
                    return Json(nombre);
                }
            }
            return Json("");
        }
    }
}