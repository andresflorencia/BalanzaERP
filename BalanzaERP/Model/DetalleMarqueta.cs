using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanzaERP.Model
{
    public class DetalleMarqueta
    {
        public Int32 transaccionid { get; set; }
        public Int32 linea { get; set; }
        public Int32 productoid { get; set; }
        public String numerolote { get; set; }
        public String codigomarqueta { get; set; }
        public Double cantidad { get; set; }
        public String fechavencimiento { get; set; }
        public String fecharegistro { get; set; }
        public Int32 nummarmita { get; set; }
        public String origenpesado { get; set; }

        public DetalleMarqueta() {
            transaccionid = 0;
            linea = 0;
            productoid = 0;
            numerolote = "";
            codigomarqueta = "";
            cantidad = 0;
            fechavencimiento = "";
            fecharegistro = "";
            nummarmita = 0;
            origenpesado = "";
        }
    }
}
