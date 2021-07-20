using Pagina_de_Pedidos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pagina_de_Pedidos.Controllers
{
    public class TipoUsuarioController : Controller
    {
        private PaginaPedidoEntities bd = new PaginaPedidoEntities();
        //MANTENEDOR CREAR
        public ActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Crear(TipoUsuario tipoUsuario)
        {
            bd.TipoUsuario.Add(tipoUsuario);
            bd.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Index()
        {
            var tipoUsuario = bd.TipoUsuario.ToList();
            return View(tipoUsuario);
        }
        //MANTENEDOR EDITAR
        public ActionResult Editar(int? id)
        {
            if (id != null)
            {
                var tipoUsuario = bd.TipoUsuario.Find(id);
                if (tipoUsuario != null)
                {
                    return View(tipoUsuario);
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
        public ActionResult Editar(TipoUsuario tipoUsuario)
        {
            bd.Entry(tipoUsuario).State = System.Data.EntityState.Modified;
            bd.SaveChanges();
            return RedirectToAction("Index");
        }
        //MANTENEDOR ELIMINAR
        public ActionResult Eliminar(int? id)
        {
            if (id != null)
            {
                var tipoUsuario = bd.TipoUsuario.Find(id);
                if (tipoUsuario != null)
                {
                    return View(tipoUsuario);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Eliminar(int id_tipoUsuario)
        {
            var tipoUsuario = bd.TipoUsuario.Find(id_tipoUsuario);
            if (tipoUsuario != null)
            {
                bd.TipoUsuario.Remove(tipoUsuario);
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult TipoExiste(string tipo)
        {
            if (!string.IsNullOrEmpty(tipo))
            {
                var q = bd.TipoUsuario.FirstOrDefault(e => e.nombre.ToLower().Equals(tipo.ToLower()));
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