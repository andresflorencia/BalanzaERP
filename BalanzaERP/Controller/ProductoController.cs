using BalanzaERP.Model;
using BalanzaERP.Utils;
using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BalanzaERP.Controller
{
    public class ProductoController
    {
        public Producto BuscarProducto(string filter = "") {
            var retorno = new Producto();
            var datos = getList(filter);
            var frmBusqueda = new View.frmBusqueda();
            var column = new DataGridViewColumn();
            column = new DataGridViewTextBoxColumn();
            column.Width = 200;
            column.Name = "colCodigo";
            column.HeaderText = "CODIGO";
            frmBusqueda.dgvBusqueda.Columns.Add(column);
            column = new DataGridViewTextBoxColumn();
            column.Width = 350;
            column.Name = "colNombre";
            column.HeaderText = "NOMBRE";
            frmBusqueda.dgvBusqueda.Columns.Add(column);
            frmBusqueda.dgvBusqueda.Rows.Clear();
            if (datos.Count <= 0)
                return null;
            frmBusqueda.dgvBusqueda.Rows.Add(datos.Count);
            for (var i=0;i<datos.Count;i++) {
                frmBusqueda.dgvBusqueda.Rows[i].Cells[0].Tag = datos[i].idproducto;
                frmBusqueda.dgvBusqueda.Rows[i].Cells[0].Value = datos[i].codigoproducto;
                frmBusqueda.dgvBusqueda.Rows[i].Cells[1].Value = datos[i].nombreproducto;
            }
            if(frmBusqueda.ShowDialog() == DialogResult.OK)
            {
                 retorno = datos.Find(x => x.idproducto == int.Parse(frmBusqueda.codigo));
            }

            return retorno;
        }

        public Respuesta DownloadProducts(String usuario, String clave)
        {
            var retorno = new Respuesta();

            try
            {
                WebService webService = new WebService();
                NameValueCollection datos = new NameValueCollection();
                datos["usuario"] = usuario;
                datos["clave"] = clave;
                string str = webService.sendURL("getproductos", datos);
                JObject jObject = null;
                var lista = new List<Producto>();
                try
                {
                    jObject = JObject.Parse(str);
                }
                catch(Exception e) { retorno.message = e.Message; retorno.haserror = true; }
                if (jObject != null)
                {
                    if ((bool)jObject.GetValue("haserror"))
                    {
                        retorno.haserror = (bool)jObject.GetValue("haserror");
                        retorno.message = jObject.GetValue("message").ToString();
                    }
                    else
                    {
                        var producto = new Producto();
                        var jProductos = new JArray();
                        jProductos = jObject.GetValue("productos").ToObject<JArray>();
                        if (jProductos != null)
                        {
                            foreach (var miJP in jProductos)
                            {
                                var jProduct = miJP.ToObject<JObject>();
                                producto = new Producto();
                                producto.idproducto = (int)(jProduct.GetValue("idproducto"));
                                producto.codigoproducto = jProduct.GetValue("codigoproducto").ToString();
                                producto.nombreproducto = jProduct.GetValue("nombreproducto").ToString();
                                producto.pvp = jProduct.GetValue("pvp")==null?0:(double)jProduct.GetValue("pvp");
                                producto.unidadid = jProduct.GetValue("unidadid").ToString().Equals("") ? 0 : (int)jProduct.GetValue("unidadid");
                                producto.unidadesporcaja = jProduct.GetValue("unidadesporcaja").ToString().Equals("") ? 0 : (int)jProduct.GetValue("unidadesporcaja");
                                producto.iva = jProduct.GetValue("iva").ToString().Equals("") ? 0 : (int)jProduct.GetValue("iva");
                                producto.ice = jProduct.GetValue("ice").ToString().Equals("") ? 0 : (int)jProduct.GetValue("ice");
                                producto.factorconversion = jProduct.GetValue("factorconversion").ToString().Equals("") ? 0 : (double)jProduct.GetValue("factorconversion");
                                producto.stock = 0;
                                producto.porcentajeiva = 0;
                                lista.Add(producto);
                            }
                        }
                        retorno.data = lista;
                    }
                }
            }
            catch (Exception e) { retorno.message = e.Message; retorno.haserror = true; }

            return retorno;
        }

        public Respuesta SaveList(List<Producto> productos)
        {
            var retorno = new Respuesta();
            var band = false;
            using (var cnn = new NpgsqlConnection(Global._connectionString))
            {
                cnn.Open();
                var trans = cnn.BeginTransaction();
                try
                {
                    foreach (var miE in productos)
                    {
                        using (var cmd = new NpgsqlCommand("select * from public.Sp_Grab_Producto(" +
                            ":eidproducto,:acodigoproducto,:anombreproducto,:dpvp,:eunidadid,"+
                            ":eunidadesporcaja, :eiva, :eice, :dfactorconversion, :dstock, :dporcentajeiva)", cnn))
                        {
                            cmd.Parameters.AddWithValue(":eidproducto", miE.idproducto);
                            cmd.Parameters.AddWithValue(":acodigoproducto", miE.codigoproducto);
                            cmd.Parameters.AddWithValue(":anombreproducto", miE.nombreproducto);
                            cmd.Parameters.AddWithValue(":dpvp", miE.pvp);
                            cmd.Parameters.AddWithValue(":eunidadid", miE.unidadid);
                            cmd.Parameters.AddWithValue(":eunidadesporcaja", miE.unidadesporcaja);
                            cmd.Parameters.AddWithValue(":eiva", miE.iva);
                            cmd.Parameters.AddWithValue(":eice", miE.ice);
                            cmd.Parameters.AddWithValue(":dfactorconversion", miE.factorconversion);
                            cmd.Parameters.AddWithValue(":dstock", miE.stock);
                            cmd.Parameters.AddWithValue(":dporcentajeiva", miE.porcentajeiva);
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.CommandTimeout = 0;

                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows && reader.Read())
                                {
                                    band = reader.IsDBNull(0) ? false : reader.GetInt32(0) > 0;
                                }
                            }
                            if (!band)
                            {
                                trans.Rollback();
                                retorno.haserror = true;
                                retorno.message = "Ocurrió un error al intentar guardar los datos de los productos.";
                                break;
                            }
                        }
                    }
                    if (band)
                    {
                        trans.Commit();
                        retorno.haserror = false;
                        retorno.message = "Datos guardados correctamente";
                    }

                }
                catch (Exception e)
                {
                    trans.Rollback();
                    retorno.haserror = true;
                    retorno.message = e.Message;
                }
            }
            return retorno;
        }

        public List<Producto> getList(string filter = "")
        {
            var retorno = new List<Producto>();

            try
            {
                using (var sql = new NpgsqlConnection(Global._connectionString))
                {
                    using (var cmd = new NpgsqlCommand("select * from public.sp_bus_producto(:tipoaccion, :codigo, :filtro)", sql))
                    {
                        cmd.Parameters.AddWithValue(":tipoaccion", filter.Length > 0 ? "FILTER" : "LISTA");
                        cmd.Parameters.AddWithValue(":codigo", 0);
                        cmd.Parameters.AddWithValue(":filtro", filter);
                        cmd.CommandType = System.Data.CommandType.Text;
                        sql.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var miE = new Producto();
                                miE.idproducto = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                                miE.codigoproducto = reader.IsDBNull(1) ? "" : reader.GetString(1);
                                miE.nombreproducto = reader.IsDBNull(2) ? "" : reader.GetString(2);
                                miE.pvp = reader.IsDBNull(3) ? 0 : reader.GetDouble(3);
                                miE.unidadid = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
                                miE.unidadesporcaja = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);
                                miE.iva = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);
                                miE.ice = reader.IsDBNull(7) ? 0 : reader.GetInt32(7);
                                miE.factorconversion = reader.IsDBNull(8) ? 0 : reader.GetDouble(8);
                                miE.stock = reader.IsDBNull(9) ? 0 : reader.GetDouble(9);
                                miE.porcentajeiva = reader.IsDBNull(10) ? 0 : reader.GetDouble(10);
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



        public Producto BuscarProducto(int codigo)
        {
            var retorno = new Producto();

            try
            {
                using (var sql = new NpgsqlConnection(Global._connectionString))
                {
                    using (var cmd = new NpgsqlCommand("select * from public.sp_bus_producto(:tipoaccion, :codigo)", sql))
                    {
                        cmd.Parameters.AddWithValue(":tipoaccion", "FILL");
                        cmd.Parameters.AddWithValue(":codigo", codigo);
                        cmd.CommandType = System.Data.CommandType.Text;
                        sql.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                retorno.idproducto = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                                retorno.codigoproducto = reader.IsDBNull(1) ? "" : reader.GetString(1);
                                retorno.nombreproducto = reader.IsDBNull(2) ? "" : reader.GetString(2);
                                retorno.pvp = reader.IsDBNull(3) ? 0 : reader.GetDouble(3);
                                retorno.unidadid = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
                                retorno.unidadesporcaja = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);
                                retorno.iva = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);
                                retorno.ice = reader.IsDBNull(7) ? 0 : reader.GetInt32(7);
                                retorno.factorconversion = reader.IsDBNull(8) ? 0 : reader.GetDouble(8);
                                retorno.stock = reader.IsDBNull(9) ? 0 : reader.GetDouble(9);
                                retorno.porcentajeiva = reader.IsDBNull(10) ? 0 : reader.GetDouble(10);
                            }
                            return retorno;
                        }
                    }
                }
            }
            catch (NpgsqlException e) { MessageBox.Show(e.Message); }
            return retorno;
        }
    }
}
