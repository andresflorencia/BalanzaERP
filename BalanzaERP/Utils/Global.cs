using BalanzaERP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanzaERP.Utils
{
    public class Global
    {
        public static Usuario usuario { get; set; }
        public static string _connectionString { get; set; }
        public static string URL { get; set; }
        public static string separador_decimal { get; set; }
        public static string name_printer { get; set; }
        public static string translate_data { get; set; }
        public static ConfigPort configPort { get; set; }
        //public const string _connectionString= "Server=localhost;Port=5432;User Id=postgres;Password=123456;Database=offlinepeso;";
        //public const string URL = "http://localhost/erpproduccion/index.php/wsmovil2/";
    }

    public class ConfigPort
    {
        public int baudrate { get; set; }
        public int databits { get; set; }
        public int parity { get; set; }
        public int stopbit { get; set; }
        public int handshake { get; set; }

        public ConfigPort()
        {
            baudrate = 9600;
            databits = 8;
            parity = 0;
            stopbit = 1;
            handshake = 0;
        }
    }

}
