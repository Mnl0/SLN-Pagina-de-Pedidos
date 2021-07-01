using Pagina_de_Pedidos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pagina_de_Pedidos.Controllers
{
    public class PedidoController : Controller
    {
        private PaginaPedidoEntities bd = new PaginaPedidoEntities();

        //MANTENEDOR CREAR
        public ActionResult Crear()
        {
            ViewBag.Id_cliente = new SelectList(bd.Cliente, "Id_cliente", "Nombre");
            return View();
        }
        [HttpPost]
        public ActionResult Crear(Pedido pedido)
        {
            bd.Pedido.Add(pedido);
            bd.SaveChanges();
            ViewBag.Id_cliente = new SelectList(bd.Cliente, "Id_cliente", "Nombre");

            return RedirectToAction("Index");
        }
        public ActionResult Index()
        {
            var pedido = bd.Pedido.ToList();
            return View(pedido);
        }

        //MANTENEDOR EDITAR
        public ActionResult Editar(int? id)
        {
            if (id != null)
            {
                var pedido = bd.Pedido.Find(id);
                if (pedido != null)
                {
                    ViewBag.Id_cliente = new SelectList(bd.Cliente, "Id_cliente", "Nombre");
                    return View(pedido);
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
        public ActionResult Editar(Pedido pedido)
        {
            bd.Entry(pedido).State = System.Data.EntityState.Modified;
            bd.SaveChanges();

            return RedirectToAction("Index");
        }

        //MANTENEDOR ELIMINAR
        public ActionResult Eliminar(int? id)
        {
            if (id != null)
            {
                var pedido = bd.Pedido.Find(id);
                if (pedido != null)
                {
                    return View(pedido);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Eliminar(int id_pedido)
        {
            var pedido = bd.Pedido.Find(id_pedido);
            if (pedido != null)
            {
                bd.Pedido.Remove(pedido);
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
 
        }
    }
}