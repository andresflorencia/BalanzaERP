using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanzaERP.Model
{
    public class Secuencial
    {
        public Int32 idsecuencial { get; set; }
        public String periodo { get; set; }
        public String tipodocumento { get; set; }
        public String establecimiento { get; set; }
        public String puntoemision { get; set; }
        public Int32 valor { get; set; }
        public bool estado { get; set; }
    }
}
