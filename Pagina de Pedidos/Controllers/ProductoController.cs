using Pagina_de_Pedidos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pagina_de_Pedidos.Controllers
{
    public class ProductoController : Controller
    {
        private PaginaPedidoEntities bd = new PaginaPedidoEntities();
        //MANTENEDOR CREAR
        public ActionResult Crear()
        {
            ViewBag.Id_categoria = new SelectList(bd.Categoria, "Id_categoria", "Nombre");
            return View();
        }
        [HttpPost]
        public ActionResult Crear(Producto producto)
        {
            bd.Producto.Add(producto);
            bd.SaveChanges();
            ViewBag.Id_categoria = new SelectList(bd.Categoria, "Id_categoria", "Nombre");
            return RedirectToAction("Index");
        }
        //LISTADO
        public ActionResult Index()
        {
            ViewBag.categoria = new SelectList(bd.Categoria, "id_categoria", "nombre");
            return View();
        }
        //MANTENEDOR EDITAR
        public ActionResult Editar(int? id)
        {
            if (id != null)
            {
                var producto = bd.Producto.Find(id);
                if (producto != null)
                {
                    ViewBag.Id_categoria = new SelectList(bd.Categoria, "Id_categoria", "Nombre");
                    return View(producto);
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
        public ActionResult Editar(Producto producto)
        {
            bd.Entry(producto).State = System.Data.EntityState.Modified;
            bd.SaveChanges();
            ViewBag.Id_categoria = new SelectList(bd.Categoria, "Id_categoria", "Nombre");
            return RedirectToAction("Index");
        }
        //MANTENEDOR ELIMINAR
        public ActionResult Eliminar(int? id)
        {
            if (id != null)
            {
                var producto = bd.Producto.Find(id);
                if (producto != null)
                {
                    return View(producto);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Eliminar(int id_producto)
        {
            var producto = bd.Producto.Find(id_producto);
            if (producto != null)
            {
                bd.Producto.Remove(producto);
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        //Método buscar filtrar
        public ActionResult ListarProductos(int? categoria)
        {
            var productos = bd.Producto.ToList();
            if (categoria != null)
            {
                productos = productos.Where(p => p.Id_categoria == categoria).ToList();
            }
            return PartialView("_ListarProductos",productos);
        }
    }
}