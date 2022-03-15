using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalanzaERP.Model
{
    public class Transaccion
    {
        public Int32 idtransaccion { get; set; }
        public Int32 establecimientoid { get; set; }
        public Int32 personaid { get; set; }
        public Int32 usuarioid { get; set; }
        public Int32 codigosistema { get; set; }
        public String codigotransaccion { get; set; }
        public String tipotransaccion { get; set; }
        public String fecharegistro { get; set; }
        public string fechadocumento { get; set; }
        public string fechaproduccion { get; set; }
        public string observacion { get; set; }
        public Double subtotal { get; set; }
        public Double subtotaliva { get; set; }
        public Double descuento { get; set; }
        public Double porcentajeiva { get; set; }
        public Double total { get; set; }
        public Int32 estado { get; set; }
        public Int32 secuencial { get; set; }
        public List<DetalleTransaccion> detalletransaccion { get; set; }
        public List<DetalleMarqueta> detallemarqueta { get; set; }

        public Transaccion()
        {
            idtransaccion = 0;
            establecimientoid = 0;
            personaid = 0;
            usuarioid = 0;
            codigosistema = 0;
            codigotransaccion = "";
            tipotransaccion = "";
            fecharegistro = "";
            fechadocumento = "";
            observacion = "";
            subtotal = 0;
            subtotaliva = 0;
            descuento = 0;
            porcentajeiva = 0;
            total = 0;
            estado = 0;
            secuencial = 0;
            fechaproduccion = "";
            detalletransaccion = new List<DetalleTransaccion>();
            detallemarqueta = new List<DetalleMarqueta>();
        }
    }
}
