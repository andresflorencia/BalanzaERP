using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanzaERP.Model
{
    public class Respuesta
    {
        public bool haserror { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}
