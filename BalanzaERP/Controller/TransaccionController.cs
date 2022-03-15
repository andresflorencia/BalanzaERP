using BalanzaERP.Model;
using BalanzaERP.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BalanzaERP.Controller
{
    public class TransaccionController
    {
        ProductoController productoController = new ProductoController();
        public int Secuencial(string tipoaccion, string periodo, string tipodocumento, string establecimiento,
                                string puntoemision, int valor = 0) {
            int retorno = 0;
            using (var cnn = new NpgsqlConnection(Global._connectionString))
            {
                cnn.Open();
                try
                {
                    using (var cmd = new NpgsqlCommand("select public.func_secuencial(" +
                        ":tipoaccion, :aperiodo, :atipodocumento, :aestablecimiento, " +
                        ":apuntoemision, :evalor)", cnn))
                    {
                        cmd.Parameters.AddWithValue(":tipoaccion", tipoaccion);
                        cmd.Parameters.AddWithValue(":aperiodo", periodo);
                        cmd.Parameters.AddWithValue(":atipodocumento", tipodocumento);
                        cmd.Parameters.AddWithValue(":aestablecimiento", establecimiento);
                        cmd.Parameters.AddWithValue(":apuntoemision", puntoemision);
                        cmd.Parameters.AddWithValue(":evalor", valor);
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandTimeout = 0;

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                retorno = reader.IsDBNull(0) ? -1 : reader.GetInt32(0);
                            }
                        }
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            return retorno;
        }

        public int Num_Transacciones_Fecha(string fecha)
        {
            int retorno = 0;
            using (var cnn = new NpgsqlConnection(Global._connectionString))
            {
                cnn.Open();
                try
                {
                    using (var cmd = new NpgsqlCommand("select public.func_num_transaccion(:fecha)", cnn))
                    {
                        cmd.Parameters.AddWithValue(":fecha", fecha);
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandTimeout = 0;

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                retorno = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            }
                        }
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            return retorno;
        }

        public Respuesta Save(string accion, Transaccion transaccion)
        {
            var retorno = new Respuesta();
            var band = false;

            using (var cnn = new NpgsqlConnection(Global._connectionString))
            {
                cnn.Open();
                var tran = cnn.BeginTransaction();
                try
                {
                    using (var cmd = new NpgsqlCommand(
                            "SELECT public.sp_grab_transaccion(:tipoaccion,:eidtransaccion,:eestablecimientoid,:epersonaid,:eusuarioid," + 
                            ":ecodigosistema, :acodigotransaccion, :atipotransaccion, :afechadocumento, :aobservacion, :eestado, :esecuencial, :afechaproduccion)",
                            cnn, tran))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue(":tipoaccion", accion);
                        cmd.Parameters.AddWithValue(":eidtransaccion", transaccion.idtransaccion);
                        cmd.Parameters.AddWithValue(":eestablecimientoid", transaccion.establecimientoid);
                        cmd.Parameters.AddWithValue(":epersonaid", transaccion.personaid);
                        cmd.Parameters.AddWithValue(":eusuarioid", transaccion.usuarioid);
                        cmd.Parameters.AddWithValue(":ecodigosistema", transaccion.codigosistema);
                        cmd.Parameters.AddWithValue(":acodigotransaccion", transaccion.codigotransaccion);
                        cmd.Parameters.AddWithValue(":atipotransaccion", transaccion.tipotransaccion);
                        cmd.Parameters.AddWithValue(":afechadocumento", transaccion.fechadocumento);
                        cmd.Parameters.AddWithValue(":aobservacion", transaccion.observacion);
                        cmd.Parameters.AddWithValue(":eestado", transaccion.estado);
                        cmd.Parameters.AddWithValue(":esecuencial", transaccion.secuencial);
                        cmd.Parameters.AddWithValue(":afechaproduccion", transaccion.fechaproduccion);
                        //await cnn.OpenAsync();
                        int idtransaccion = 0;
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                idtransaccion = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            }
                        }

                        if (idtransaccion > 0)
                        {
                            if (accion.Equals("UPDATE"))
                                idtransaccion = transaccion.idtransaccion;
                            band = SaveDetalle(idtransaccion, transaccion.detalletransaccion, cnn, tran);
                            band = band && SaveDetalleMarqueta(idtransaccion, transaccion.detallemarqueta, cnn, tran, transaccion.estado != 2);
                            if(accion.Equals("NUEVO"))
                                band = band && (Secuencial("UPDATE", "1", "19", transaccion.establecimientoid.ToString(), "0", transaccion.secuencial) > 0);
                        }

                        retorno.data = idtransaccion;
                        retorno.haserror = !band;
                        if (band)
                        {
                            tran.Commit();                            
                            retorno.message = "Datos guardados correctamente con el código «" + transaccion.codigotransaccion + "»";
                        }
                        else
                        {
                            tran.Rollback();
                            retorno.message = "Ocurrió un error al guardar los datos.";
                        }
                    }
                }
                catch(Exception e)
                {
                    retorno.haserror = true;
                    retorno.message = e.Message;
                    tran.Rollback();
                }
            }
            return retorno;
        }

        public bool SaveDetalle(int idtransaccion, List<DetalleTransaccion> lista, NpgsqlConnection cnn, NpgsqlTransaction tran)
        {
            var retorno = false;
            try
            {
                DeleteDetalle(idtransaccion, cnn, tran);
                foreach (var miDetalle in lista)
                {
                    using (var cmd = new NpgsqlCommand(
                            "SELECT public.sp_grab_detalletransaccion(:tipoaccion,:etransaccionid,:elinea,:eproductoid,:dcantidad,"+ 
	                        ":dtotal,:dprecio,:anumerolote,:afechavencimiento,:dstock,:dpreciocosto,:dprecioreferencia,:dmarquetas, :acodigoproducto)", 
                            cnn, tran))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue(":tipoaccion", "NUEVO");
                        cmd.Parameters.AddWithValue(":etransaccionid", idtransaccion);
                        cmd.Parameters.AddWithValue(":elinea", miDetalle.linea);
                        cmd.Parameters.AddWithValue(":eproductoid", miDetalle.producto.idproducto);
                        cmd.Parameters.AddWithValue(":dcantidad", miDetalle.cantidad);
                        cmd.Parameters.AddWithValue(":dtotal", miDetalle.total);
                        cmd.Parameters.AddWithValue(":dprecio", miDetalle.precio);
                        cmd.Parameters.AddWithValue(":anumerolote", miDetalle.numerolote);
                        cmd.Parameters.AddWithValue(":afechavencimiento", miDetalle.fechavencimiento);
                        cmd.Parameters.AddWithValue(":dstock", miDetalle.stock);
                        cmd.Parameters.AddWithValue(":dpreciocosto", miDetalle.preciocosto);
                        cmd.Parameters.AddWithValue(":dprecioreferencia", miDetalle.precioreferencia);
                        cmd.Parameters.AddWithValue(":dmarquetas", miDetalle.marquetas);
                        cmd.Parameters.AddWithValue(":acodigoproducto", miDetalle.codigoproducto);
                        //await cnn.OpenAsync();
                        int result = 0;
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                result = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                                retorno = result > 0;
                            }
                        }
                    }
                    if (!retorno)
                        return false;
                }
            }
            catch (Exception e)
            {
                retorno = false;
            }
            return retorno;
        }

        public bool DeleteDetalle(int idtransaccion, NpgsqlConnection cnn, NpgsqlTransaction tran)
        {
            var retorno = false;
            try
            {
                using (var cmd = new NpgsqlCommand(
                            "SELECT public.sp_grab_detalletransaccion(:tipoaccion,:etransaccionid, :elinea)",
                            cnn, tran))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue(":tipoaccion", "ELIMINAR");
                        cmd.Parameters.AddWithValue(":etransaccionid", idtransaccion);
                        cmd.Parameters.AddWithValue(":elinea", 1);
                        //await cnn.OpenAsync();
                        int result = 0;
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                result = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                                retorno = result > 0;
                            }
                        }
                    }
            }
            catch (Exception e)
            {
                retorno = false;
            }
            return retorno;
        }

        public bool SaveDetalleMarqueta(int idtransaccion, List<DetalleMarqueta> lista, NpgsqlConnection cnn, NpgsqlTransaction tran, bool deleteall = false)
        {
            var retorno = false;
            try
            {
                if(deleteall)
                    DeleteMarqueta(idtransaccion);
                foreach (var miDetalle in lista)
                {
                    using (var cmd = new NpgsqlCommand(
                            "SELECT public.sp_grab_detallemarqueta(:tipoaccion,:etransaccionid,:elinea,:eproductoid,:anumerolote,"+
                            ":acodigomarqueta, :dcantidad, :afechavencimiento, :enummarmita, :aorigenpesado)",
                            cnn, tran))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue(":tipoaccion", "NUEVO");
                        cmd.Parameters.AddWithValue(":etransaccionid", idtransaccion);
                        cmd.Parameters.AddWithValue(":elinea", miDetalle.linea);
                        cmd.Parameters.AddWithValue(":eproductoid", miDetalle.productoid);
                        cmd.Parameters.AddWithValue(":anumerolote", miDetalle.numerolote);
                        cmd.Parameters.AddWithValue(":acodigomarqueta", miDetalle.codigomarqueta);
                        cmd.Parameters.AddWithValue(":dcantidad", miDetalle.cantidad);
                        cmd.Parameters.AddWithValue(":afechavencimiento", miDetalle.fechavencimiento);
                        cmd.Parameters.AddWithValue(":enummarmita", miDetalle.nummarmita);
                        cmd.Parameters.AddWithValue(":aorigenpesado", miDetalle.origenpesado);
                        //await cnn.OpenAsync();
                        int result = 0;
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                result = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                                retorno = result > 0;
                            }
                        }
                    }
                    if (!retorno)
                        return false;
                }
            }
            catch (Exception e)
            {
                retorno = false;
            }
            return retorno;
        }

        public bool DeleteMarqueta(int idtransaccion, int linea = 0)
        {
            var retorno = false;
            using (var cnn = new NpgsqlConnection(Global._connectionString))
            {
                cnn.Open();
                var tran = cnn.BeginTransaction();
                try
                {
                    using (var cmd = new NpgsqlCommand(
                                "SELECT public.sp_grab_detallemarqueta(:tipoaccion,:etransaccionid, :elinea)",
                                cnn, tran))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue(":tipoaccion", linea > 0 ? "ELIMINAR_LINEA" : "ELIMINAR");
                        cmd.Parameters.AddWithValue(":etransaccionid", idtransaccion);
                        cmd.Parameters.AddWithValue(":elinea", linea);
                        //await cnn.OpenAsync();
                        int result = 0;
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                result = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                                retorno = result > 0;
                            }
                        }

                        if (retorno)
                            tran.Commit();
                        else
                            tran.Rollback();
                    }
                }
                catch (Exception e)
                {
                    retorno = false;
                    tran.Rollback();
                }
            }
            return retorno;
        }

        public List<Transaccion> getList(string accion, int id = 0, string codigo = "", bool buscardetalle=false)
        {
            var retorno = new List<Transaccion>();

            try
            {
                using (var sql = new NpgsqlConnection(Global._connectionString))
                {
                    using (var cmd = new NpgsqlCommand("select * from public.sp_bus_transaccion(:tipoaccion, :eidtransaccion, :acodigotransaccion)", sql))
                    {
                        cmd.Parameters.AddWithValue(":tipoaccion", accion);
                        cmd.Parameters.AddWithValue(":eidtransaccion", id);
                        cmd.Parameters.AddWithValue(":acodigotransaccion", codigo);
                        cmd.CommandType = System.Data.CommandType.Text;
                        sql.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var miE = new Transaccion();
                                miE.idtransaccion = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                                miE.establecimientoid = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                                miE.personaid= reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                                miE.usuarioid = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                                miE.codigosistema = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
                                miE.codigotransaccion = reader.IsDBNull(5) ? "" : reader.GetString(5);
                                miE.tipotransaccion = reader.IsDBNull(6) ? "" : reader.GetString(6);
                                miE.fecharegistro = reader.IsDBNull(7) ? "" : reader.GetDateTime(7).ToString("yyyy-MM-dd HH:mm:ss");
                                miE.fechadocumento = reader.IsDBNull(8) ? "" : reader.GetString(8);
                                miE.observacion = reader.IsDBNull(9) ? "" : reader.GetString(9);
                                miE.estado = reader.IsDBNull(15) ? 0 : reader.GetInt32(15);
                                miE.secuencial = reader.IsDBNull(16) ? 0 : reader.GetInt32(16);
                                miE.fechaproduccion = reader.IsDBNull(17) ? "" : reader.GetString(17);

                                if (buscardetalle)
                                {
                                    miE.detalletransaccion = BuscarDetalle(miE.idtransaccion);
                                    miE.detallemarqueta = BuscarDetalleMarqueta(miE.idtransaccion);
                                }

                                retorno.Add(miE);
                            }
                            return retorno;
                        }
                    }
                }
            }
            catch (NpgsqlException e) { MessageBox.Show(e.Message); }
            return retorno;
        }

        public List<DetalleTransaccion> BuscarDetalle(int idtransaccion)
        {
            var retorno = new List<DetalleTransaccion>();

            try
            {
                using (var sql = new NpgsqlConnection(Global._connectionString))
                {
                    using (var cmd = new NpgsqlCommand("select * from public.sp_bus_detalletransaccion(:eidtransaccion)", sql))
                    {
                        cmd.Parameters.AddWithValue(":eidtransaccion", idtransaccion);
                        cmd.CommandType = System.Data.CommandType.Text;
                        sql.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var miE = new DetalleTransaccion();
                                miE.transaccionid = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                                miE.linea = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                                miE.productoid = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                                miE.producto = productoController.BuscarProducto(reader.IsDBNull(2) ? 0 : reader.GetInt32(2));
                                miE.cantidad = reader.IsDBNull(3) ? 0 : reader.GetDouble(3);
                                miE.total = reader.IsDBNull(4) ? 0 : reader.GetDouble(4);
                                miE.precio = reader.IsDBNull(5) ? 0 : reader.GetDouble(5);
                                miE.numerolote = reader.IsDBNull(6) ? "" : reader.GetString(6);
                                miE.fechavencimiento = reader.IsDBNull(7) ? "" : reader.GetString(7);
                                miE.stock = reader.IsDBNull(8) ? 0 : reader.GetDouble(8);
                                miE.preciocosto = reader.IsDBNull(9) ? 0 : reader.GetDouble(9);
                                miE.precioreferencia = reader.IsDBNull(10) ? 0 : reader.GetDouble(10);
                                miE.valoriva = reader.IsDBNull(11) ? 0 : reader.GetDouble(11);
                                miE.valorice = reader.IsDBNull(12) ? 0 : reader.GetDouble(12);
                                miE.descuento = reader.IsDBNull(13) ? 0 : reader.GetDouble(13);
                                miE.marquetas = reader.IsDBNull(14) ? 0 : reader.GetDouble(14);
                                miE.codigoproducto = reader.IsDBNull(15) ? "" : reader.GetString(15);

                                retorno.Add(miE);
                            }
                            return retorno;
                        }
                    }
                }
            }
            catch (NpgsqlException e) { MessageBox.Show(e.Message); }
            return retorno;
        }

        public List<DetalleMarqueta> BuscarDetalleMarqueta(int idtransaccion)
        {
            var retorno = new List<DetalleMarqueta>();

            try
            {
                using (var sql = new NpgsqlConnection(Global._connectionString))
                {
                    using (var cmd = new NpgsqlCommand("select * from public.sp_bus_detallemarqueta(:eidtransaccion)", sql))
                    {
                        cmd.Parameters.AddWithValue(":eidtransaccion", idtransaccion);
                        cmd.CommandType = System.Data.CommandType.Text;
                        sql.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var miE = new DetalleMarqueta();
                                miE.transaccionid = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                                miE.linea = reader.IsDBNull(1) ? 0 : reader.GetInt32(1);
                                miE.productoid = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                                miE.numerolote = reader.IsDBNull(3) ? "" : reader.GetString(3);
                                miE.codigomarqueta = reader.IsDBNull(4) ? "" : reader.GetString(4);
                                miE.cantidad = reader.IsDBNull(5) ? 0 : reader.GetDouble(5);
                                miE.fechavencimiento = reader.IsDBNull(6) ? "" : reader.GetString(6);
                                miE.fecharegistro = reader.IsDBNull(7) ? "" : reader.GetDateTime(7).ToLongDateString();
                                miE.nummarmita= reader.IsDBNull(8) ? 0 : reader.GetInt32(8);
                                miE.origenpesado = reader.IsDBNull(9) ? "M" : reader.GetString(9);

                                retorno.Add(miE);
                            }
                            return retorno;
                        }
                    }
                }
            }
            catch (NpgsqlException e) { MessageBox.Show(e.Message); }
            return retorno;
        }


        public Transaccion BuscarTransaccion(string filter = "")
        {
            Transaccion retorno = null;
            var datos = getList("LISTA", 0, "");
            var frmBusqueda = new View.frmBusqueda();
            var column = new DataGridViewColumn();
            column = new DataGridViewTextBoxColumn();
            column.Width = 200;
            column.Name = "colCodigo";
            column.HeaderText = "CODIGO";
            frmBusqueda.dgvBusqueda.Columns.Add(column);
            column = new DataGridViewTextBoxColumn();
            column.Width = 200;
            column.Name = "colFechaProd";
            column.HeaderText = "FECHA PRODUCCION";
            frmBusqueda.dgvBusqueda.Columns.Add(column);
            column = new DataGridViewTextBoxColumn();
            column.Width = 200;
            column.Name = "colFecha";
            column.HeaderText = "FECHA DOCUMENTO";
            frmBusqueda.dgvBusqueda.Columns.Add(column);
            column = new DataGridViewTextBoxColumn();
            column.Width = 200;
            column.Name = "colFechaRegistro";
            column.HeaderText = "FECHA REGISTRO";
            frmBusqueda.dgvBusqueda.Columns.Add(column);
            column = new DataGridViewTextBoxColumn();
            column.Width = 200;
            column.Name = "colEstado";
            column.HeaderText = "ESTADO";
            frmBusqueda.dgvBusqueda.Columns.Add(column);
            frmBusqueda.dgvBusqueda.Rows.Clear();
            if (datos.Count <= 0)
                return null;
            frmBusqueda.dgvBusqueda.Rows.Add(datos.Count);
            for (var i = 0; i < datos.Count; i++)
            {
                frmBusqueda.dgvBusqueda.Rows[i].Cells[0].Tag = datos[i].idtransaccion;
                frmBusqueda.dgvBusqueda.Rows[i].Cells[0].Value = datos[i].codigotransaccion;
                frmBusqueda.dgvBusqueda.Rows[i].Cells[1].Value = datos[i].fechaproduccion;
                frmBusqueda.dgvBusqueda.Rows[i].Cells[2].Value = datos[i].fechadocumento;
                frmBusqueda.dgvBusqueda.Rows[i].Cells[3].Value = datos[i].fecharegistro;
                if (datos[i].estado == 2 && datos[i].codigosistema == 0)
                    frmBusqueda.dgvBusqueda.Rows[i].Cells[4].Value = "BORRADOR";
                else if (datos[i].estado > 0 && datos[i].codigosistema == 0)
                    frmBusqueda.dgvBusqueda.Rows[i].Cells[4].Value = "NO SINCRONIZADO";
                else
                    frmBusqueda.dgvBusqueda.Rows[i].Cells[4].Value = "SINCRONIZADO";
            }
            if (frmBusqueda.ShowDialog() == DialogResult.OK)
            {
                retorno = datos.Find(x => x.idtransaccion == int.Parse(frmBusqueda.codigo));
            }

            return retorno;
        }

        public bool UpdateEstado(int idtransaccion, int codigosistema)
        {
            var retorno = false;

            using (var cnn = new NpgsqlConnection(Global._connectionString))
            {
                cnn.Open();
                var tran = cnn.BeginTransaction();
                try
                {
                    using (var cmd = new NpgsqlCommand(
                            "SELECT public.sp_grab_transaccion(:tipoaccion,:eidtransaccion,:eestablecimientoid,:epersonaid,:eusuarioid," +
                            ":ecodigosistema, :acodigotransaccion, :atipotransaccion, :afechadocumento, :aobservacion, :eestado, :esecuencial)",
                            cnn, tran))
                    {
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue(":tipoaccion", "ESTADO");
                        cmd.Parameters.AddWithValue(":eidtransaccion",  idtransaccion);
                        cmd.Parameters.AddWithValue(":ecodigosistema", codigosistema);
                        cmd.Parameters.AddWithValue(":eestablecimientoid", 0);
                        cmd.Parameters.AddWithValue(":epersonaid", 0);
                        cmd.Parameters.AddWithValue(":eusuarioid", 0);
                        cmd.Parameters.AddWithValue(":acodigotransaccion", "");
                        cmd.Parameters.AddWithValue(":atipotransaccion", "");
                        cmd.Parameters.AddWithValue(":afechadocumento", "");
                        cmd.Parameters.AddWithValue(":aobservacion", "");
                        cmd.Parameters.AddWithValue(":eestado", 0);
                        cmd.Parameters.AddWithValue(":esecuencial", 0);
                        //await cnn.OpenAsync();
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                retorno = (reader.IsDBNull(0) ? 0 : reader.GetInt32(0)) > 0;
                            }
                        }

                        if (retorno)
                        {
                            tran.Commit();
                        }
                        else
                        {
                            tran.Rollback();
                        }
                    }
                }
                catch (Exception e)
                {
                    tran.Rollback();
                }
            }
            return retorno;
        }

        public Respuesta Sincronizar(String usuario, String clave)
        {
            var retorno = new Respuesta();

            try
            {
                var lista = getList("POR_SINCRONIZAR", 0, "", true);
                if (lista.Count <= 0)
                {
                    retorno.haserror = true;
                    retorno.message = "Estás al día, no hay registros por sincronizar.";
                    return retorno;
                }

                WebService webService = new WebService();
                NameValueCollection datos = new NameValueCollection();
                datos["usuario"] = usuario;
                datos["clave"] = clave;
                datos["puntoemision"] = Global.usuario.puntoemisionid.ToString();
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(lista);
                datos["transacciones"] = json;
                string str = webService.sendURL("loadingresoslote", datos);
                JObject jObject = null;
                try
                {
                    jObject = JObject.Parse(str);
                }
                catch { }
                if (jObject != null)
                {
                    if ((bool)jObject.GetValue("haserror"))
                    {
                        retorno.haserror = (bool)jObject.GetValue("haserror");
                        retorno.message = jObject.GetValue("message").ToString();
                    }
                    else
                    {
                        var user = new Usuario();
                        var jIngresosUpdate = new JArray();
                        jIngresosUpdate = jObject.GetValue("ingresosupdate").ToObject<JArray>();
                        
                        if(jIngresosUpdate != null && jIngresosUpdate.Count > 0)
                        {
                            for(var i=0; i < jIngresosUpdate.Count; i++)
                            {
                                var jIngreso = jIngresosUpdate[i].ToObject<JObject>();
                                UpdateEstado((int)jIngreso.GetValue("idtransaccion_movil"), (int)jIngreso.GetValue("codigosistema"));
                            }
                            retorno.haserror = false;
                            retorno.message = "Sincronización de datos sin contratiempos.";
                        }
                        else
                        {
                            retorno.haserror = true;
                            retorno.message = "El servidor no devolvió ningún dato. Contacte a soporte.";
                        }
                    }
                }
                else
                {
                    retorno.haserror = true;
                    retorno.message = "Error desconocido en el lado del servidor. Contacte a soporte.";
                }
            }
            catch (Exception e) {
                retorno.haserror = true;
                retorno.message = "Error: " + e.Message;
            }

            return retorno;
        }

        public void ImprimirTicketMarqueta(string producto, string lote, string codigo, string peso, string pathimg)
        {
            try
            {
                readNamePrinter();
                var impresora = new ImpresoraFormato(Global.name_printer, 40, 1);

                impresora.ConfiguraImpresora();
                impresora.ListImpresion.Add(new ImpresoraFormato.TextoFormato(codigo.PadLeft(30, ' '), 7, FontStyle.Regular));
                impresora.ListImpresion.Add(new ImpresoraFormato.TextoFormato(producto, 8, FontStyle.Bold));
                impresora.ListImpresion.Add(new ImpresoraFormato.TextoFormato("LOTE: " + lote, 7, FontStyle.Regular));
                impresora.ListImpresion.Add(new ImpresoraFormato.TextoFormato("Peso (lb): " + peso, 7, FontStyle.Regular));
                Image img = Image.FromFile(pathimg);
                impresora.image = img;

                impresora.Imprimir();
            }
            catch(Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }

        private void readNamePrinter()
        {
            string linea = "";
            var file = new StreamReader(Application.StartupPath + @"\config.txt");
            if (file != null)
            {
                linea = file.ReadToEnd();
                var objConfig = JsonConvert.DeserializeObject<JObject>(linea);
                if (objConfig != null)
                {
                    Global.name_printer = objConfig.GetValue("name_printer").ToString();
                }
                file.Close();
            }
        }
    }
}
