using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanzaERP.Model
{
    public class DetalleTransaccion
    {
        public Int32 transaccionid { get; set; }
        public Int32 linea { get; set; }
        public Producto producto { get; set; }
        public Double cantidad { get; set; }
        public Double total { get; set; }
        public Double precio { get; set; }
        public String numerolote { get; set; }
        public String fechavencimiento { get; set; }
        public Double stock { get; set; }
        public Double preciocosto { get; set; }
        public Double precioreferencia { get; set; }
        public Double valoriva { get; set; }
        public Double valorice { get; set; }
        public Double descuento { get; set; }
        public Double marquetas { get; set; }
        public string codigoproducto { get; set; }
        public Int32 productoid { get; set; }
        public DetalleTransaccion()
        {
            transaccionid = 0;
            linea = 0;
            producto = new Producto();
            cantidad = 0;
            total = 0;
            precio = 0;
            numerolote = "";
            fechavencimiento = "";
            stock = 0;
            preciocosto = 0;
            precioreferencia = 0;
            valoriva = 0;
            valorice = 0;
            descuento = 0;
            marquetas = 0;
            codigoproducto = "";
            productoid = 0;
        }
    }
}
