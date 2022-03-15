using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanzaERP.Model
{
    public class Usuario
    {
        public Int32 idusuario { get; set; }
        public String usuario { get; set; }
        public String clave { get; set; }
        public String razonsocial { get; set; }
        public String nip { get; set; }
        public Int32 perfil { get; set; }
        public Establecimiento establecimiento { get; set; }
        public Int32 parroquiaid { get; set; }
        public String nombreperfil { get; set; }
        public Int32 puntoemisionid { get; set; }
    }
}
