using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanzaERP.Model
{
    public class Establecimiento
    {
        public Int32 idestablecimiento { get; set; }
        public String codigoestablecimiento { get; set; }
        public String nombreestablecimiento { get; set; }
        public String direccion { get; set; }
        public Int32 parroquiaid { get; set; }
        public String tipo { get; set; }
        public String nombrecomercial { get; set; }
        public String codigoestablecimientosri { get; set; }
        public Int32 nummarmita { get; set; }

        public Establecimiento() {
            this.idestablecimiento = 0;
            this.codigoestablecimiento = "";
            this.nombreestablecimiento = "";
            this.direccion = "";
            this.parroquiaid = 0;
            this.tipo = "";
            this.nombrecomercial = "";
            this.codigoestablecimientosri = "";
            this.nummarmita = 0;
        }

        public static Establecimiento AsignaDatos(JObject jObject) {
            Establecimiento sucursal = new Establecimiento();
            sucursal.idestablecimiento = (int)jObject.GetValue("idestablecimiento");
            sucursal.nombrecomercial = jObject.GetValue("nombrecomercial").ToString();
            sucursal.direccion = jObject.GetValue("direccion").ToString();
            sucursal.codigoestablecimiento = jObject.GetValue("idsucursal").ToString();
            sucursal.codigoestablecimientosri = jObject.GetValue("codigoestablecimiento").ToString();
            sucursal.nombreestablecimiento = jObject.GetValue("nombreestablecimiento").ToString();
            sucursal.nummarmita = (int)jObject.GetValue("nummarmita");
            return sucursal;
        }
        public override string ToString()
        {
            return codigoestablecimiento + " - " + nombreestablecimiento;
        }
    }
}
