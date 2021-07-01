using Pagina_de_Pedidos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pagina_de_Pedidos.Controllers
{
    public class DetalleController : Controller
    {
        private PaginaPedidoEntities bd = new PaginaPedidoEntities();

        //MANTENEDOR CREAR
        public ActionResult Crear()
        {
            ViewBag.Id_producto = new SelectList(bd.Producto, "Id_producto", "Nombre");
            ViewBag.Id_pedido = new SelectList(bd.Pedido, "Id_pedido", "Id_pedido");
            return View();
        }
        [HttpPost]
        public ActionResult Crear(Detalle detalle)
        {
            bd.Detalle.Add(detalle);
            bd.SaveChanges();
            ViewBag.Id_producto = new SelectList(bd.Producto, "Id_producto", "Nombre");
            ViewBag.Id_pedido = new SelectList(bd.Pedido, "Id_pedido", "Id_pedido");

            return RedirectToAction("Index");
        }
        public ActionResult Index()
        {
            var detalle = bd.Detalle.ToList();
            return View(detalle);
        }

        //MANTENEDOR EDITAR
        public ActionResult Editar(int? id)
        {
            if (id != null)
            {
                var detalle = bd.Detalle.Find(id);
                if (detalle != null)
                {
                    ViewBag.Id_producto = new SelectList(bd.Producto, "Id_producto", "Nombre");
                    ViewBag.Id_pedido = new SelectList(bd.Pedido, "Id_pedido", "Id_pedido");
                    return View(detalle);
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
        public ActionResult Editar(Detalle detalle)
        {
            bd.Entry(detalle).State = System.Data.EntityState.Modified;
            bd.SaveChanges();

            return RedirectToAction("Index");
        }

        //MANTENEDOR ELIMINAR
        public ActionResult Eliminar(int? id)
        {
            if (id != null)
            {
                var detalle = bd.Detalle.Find(id);
                if (detalle != null)
                {
                    return View(detalle);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Eliminar(int id_detalle)
        {
            var detalle = bd.Detalle.Find(id_detalle);
            if (detalle != null)
            {
                bd.Detalle.Remove(detalle);
                bd.SaveChanges();
            }
            return RedirectToAction("Index");

        }
    }
}