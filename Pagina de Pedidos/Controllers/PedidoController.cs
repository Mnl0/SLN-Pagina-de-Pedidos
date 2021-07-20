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
        public ActionResult Index()
        {
            ViewBag.Id_cliente = new SelectList(bd.Cliente, "Id_cliente", "Nombre");
            ViewBag.Id_producto = new SelectList(bd.Producto, "Id_producto", "Nombre");
            var fecha = DateTime.Now;
            return View(fecha);
        }
        [HttpPost]
        public JsonResult buscarPrecio(int? id)
        {
            var producto = bd.Producto.Find(id);
            if (producto != null)
            {
                int pp = producto.Precio; 
                return Json(pp);
            }
            return Json("");
        }
        [HttpPost]
        public JsonResult buscarCliente(int? id)
        {
            var cliente = bd.Cliente.Find(id);
            if (cliente != null)
            {

                return Json(cliente);
            }
            return Json("");
        }
        [HttpPost]
        public ActionResult PedidoExiste(string pedido)
        {
            if (!string.IsNullOrEmpty(pedido))
            {
                var q = bd.Pedido.FirstOrDefault(e => e.Id_pedido.Equals(pedido));
                if (q != null)
                {
                    int ped = q.Id_pedido;
                    return Json(ped);
                }
            }
            return Json("");
        }
        [HttpPost]
        public JsonResult GuardarPedido(Pedido pedido, List<Detalle> detalle)
        {
            pedido.Fecha = DateTime.Now;
            pedido.id_usuario = int.Parse(Session["iduss"].ToString());
            bd.Pedido.Add(pedido);
            bd.SaveChanges();
            var id_pedido = pedido.Id_pedido;
            var total = pedido.Precio_total;

            foreach (var item in detalle)
            {
                item.Id_pedido = id_pedido;
                bd.Detalle.Add(item);
                var producto = bd.Producto.Find(item.Id_producto);
                //var cliente = bd.Cliente.Find(item.Id_pedido);
                item.Cantidad = item.Cantidad;
                bd.Entry(producto).State = System.Data.EntityState.Modified;
                bd.SaveChanges();
                //var multi = item.Cantidad * producto.Precio;
            }
            return Json("");
        }
        public ActionResult HistorialPedido()
        {
            return View();
        }
        public ActionResult ListaPedidos(DateTime? inicio, DateTime? fin)
        {
            //obtiene toda la lista de ordenes de entrada
            var ordenes = bd.Pedido.ToList();
            if (inicio != null)
            {
                //select * from OrdenEntrada o WHERE o.fecha >= inicio AND o.fecha<=fin
                ordenes = ordenes.Where(o => o.Fecha >= inicio && o.Fecha <= fin).ToList();
            }
            return PartialView("_ListaPedidos",ordenes);
        }
    }
}