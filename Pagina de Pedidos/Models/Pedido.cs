//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pagina_de_Pedidos.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pedido
    {
        public Pedido()
        {
            this.Detalle = new HashSet<Detalle>();
        }
    
        public int Id_pedido { get; set; }
        public int Id_cliente { get; set; }
        public System.DateTime Fecha { get; set; }
        public int Precio_total { get; set; }
        public int Numero_pedido { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual ICollection<Detalle> Detalle { get; set; }
    }
}
