using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanzaERP.Model
{
    public class Producto
    {
        public Int32 idproducto { get; set; }
        public String codigoproducto { get; set; }
        public String nombreproducto { get; set; }
        public Double pvp { get; set; }
        public Int32 unidadid { get; set; }
        public Int32 unidadesporcaja { get; set; }
        public Int32 iva { get; set; }
        public Int32 ice { get; set; }
        public Double factorconversion { get; set; }
        public Double stock { get; set; }
        public Double porcentajeiva { get; set; }
    }
}
