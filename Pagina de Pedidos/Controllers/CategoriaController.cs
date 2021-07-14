using Pagina_de_Pedidos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Pagina_de_Pedidos.Controllers
{
    public class CategoriaController : Controller
    {
    
        private PaginaPedidoEntities bd = new PaginaPedidoEntities();
        //MANTENEDOR CREAR
        public ActionResult Crear()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Crear(Categoria categoria )
        {
            bd.Categoria.Add(categoria);
            bd.SaveChanges();
            return RedirectToAction("Index");
        }
        //LISTADO
        public ActionResult Index()
        {
            var categoria = bd.Categoria.ToList();
            return View(categoria);
        }
        //MANTENEDOR EDITAR
        public ActionResult Editar(int? id)
        {
            if (id!=null)
            {
                var categoria = bd.Categoria.Find(id);
                if (categoria!=null)
                {
                    return View(categoria);
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
        public ActionResult Editar(Categoria categoria)
        {
            bd.Entry(categoria).State = System.Data.EntityState.Modified;
            bd.SaveChanges();

            return RedirectToAction("Index");
        }
        //MANTENEDOR ELIMINAR
        public ActionResult Eliminar(int? id)
        {
            if (id != null)
            {
                var categoria = bd.Categoria.Find(id);
                if (categoria != null)
                {
                    return View(categoria);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Eliminar(int id_categoria)
        {
            var categoria = bd.Categoria.Find(id_categoria);
            if (categoria != null)
            {
                bd.Categoria.Remove(categoria);
                bd.SaveChanges();
            }
            return RedirectToAction("Index");
        }    
    }
}