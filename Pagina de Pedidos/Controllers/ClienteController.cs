using Pagina_de_Pedidos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pagina_de_Pedidos.Controllers
{
    public class ClienteController : Controller
    {
 
        private PaginaPedidoEntities bd = new PaginaPedidoEntities();
        //MANTENEDOR CREAR
        public ActionResult Crear()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Crear(Cliente cliente)
        {
            bd.Cliente.Add(cliente);
            bd.SaveChanges();
            return RedirectToAction("Index");
        }
        //LISTADO
        public ActionResult Index()
        {
            var cliente = bd.Cliente.ToList();
            return View(cliente);
        }

        //MANTENEDOR EDITAR
        public ActionResult Editar(int? id)
        {
            if (id != null)
            {
                var cliente = bd.Cliente.Find(id);
                if (cliente != null)
                {
                    return View(cliente);
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
        public ActionResult Editar(Cliente cliente)
        {
            bd.Entry(cliente).State = System.Data.EntityState.Modified;
            bd.SaveChanges();

            return RedirectToAction("Index");
        }
        //MANTENEDOR ELIMINAR
        public ActionResult Eliminar(int? id)
        {
            if (id != null)
            {
                var cliente = bd.Cliente.Find(id);
                if (cliente != null)
                {
                    return View(cliente);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Eliminar(int id_cliente)
        {
            var cliente = bd.Cliente.Find(id_cliente);
            if (cliente != null)
            {
                bd.Cliente.Remove(cliente);
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        //OK
        [HttpPost]
        public ActionResult ClienteExiste(string cliente)
        {
            if (!string.IsNullOrEmpty(cliente))
            {
                var q = bd.Cliente.FirstOrDefault(e => e.Telefono.ToLower().Equals(cliente.ToLower()));
                if (q != null)
                {
                    string nombre = q.Nombre;
                    return Json(nombre);
                }
            }
            return Json("");
        }
    }
}