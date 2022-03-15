using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;
using System.Windows.Forms;
using BalanzaERP.Model;
using BalanzaERP.Utils;
using System.Collections.Specialized;
using Newtonsoft.Json.Linq;
using System.Runtime;
namespace BalanzaERP.Controller
{
    public class UsuarioController
    {
        NpgsqlConnection cnn;
        EstablecimientoController establecimientoController = new EstablecimientoController();
        private bool Conectar()
        {
            bool retorno = false;
            try
            {
                cnn = new NpgsqlConnection("Server=localhost;Port=5432;User Id=posgres;Password=123456;Database=offlinepeso;");
                cnn.Open();
                retorno = true;
            }
            catch (Exception e)
            {
                cnn.Close();
            }
            return retorno;
        }
        public Usuario Login(String usuario, String clave) {
            Usuario retorno = null;
            try
            {
                using (var sql = new NpgsqlConnection(Global._connectionString))
                {
                    using (var cmd = new NpgsqlCommand("select * from public.sp_login(:tipoAccion, :codigo, :ausuario, :aclave)", sql))
                    {
                        cmd.Parameters.AddWithValue(":tipoAccion", "LOGIN");
                        cmd.Parameters.AddWithValue(":codigo", "0");
                        cmd.Parameters.AddWithValue(":ausuario", usuario);
                        cmd.Parameters.AddWithValue(":aclave", clave);
                        cmd.CommandType = System.Data.CommandType.Text;
                        sql.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                retorno = new Usuario();
                                retorno.idusuario = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                                retorno.razonsocial = reader.IsDBNull(1) ? "" : reader.GetString(1);
                                retorno.usuario = reader.IsDBNull(2) ? "" : reader.GetString(2);
                                retorno.clave = clave;
                                retorno.perfil = reader.IsDBNull(4) ? 0 : reader.GetInt32(4);
                                retorno.establecimiento = establecimientoController.getById(reader.IsDBNull(5) ? 0 : reader.GetInt32(5));
                                retorno.parroquiaid = reader.IsDBNull(6) ? 0 : reader.GetInt32(6);
                                retorno.nombreperfil = reader.IsDBNull(7) ? "" : reader.GetString(7);
                                retorno.nip = reader.IsDBNull(8) ? "" : reader.GetString(8);
                                retorno.puntoemisionid = reader.IsDBNull(9) ? 0 : reader.GetInt32(9);
                            }
                            return retorno;
                        }
                    }
                }
            }
            catch (NpgsqlException e) { MessageBox.Show(e.Message); }
            return retorno;
        }
        public Respuesta LoginWS(String usuario, String clave)
        {
            var retorno = new Respuesta();

            try
            {
                WebService webService = new WebService();
                NameValueCollection datos = new NameValueCollection();
                datos["usuario"] = usuario;
                datos["clave"] = clave;
                string str = webService.sendURL("registramovil", datos);
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
                        var jUser = new JObject();
                        jUser = jObject.GetValue("usuario").ToObject<JObject>();
                        user.idusuario = (int)(jUser.GetValue("idpersona"));
                        user.razonsocial = jUser.GetValue("razonsocial").ToString();
                        user.usuario = jUser.GetValue("usuario").ToString();
                        user.nip = jUser.GetValue("nip").ToString();
                        user.clave = clave;
                        user.parroquiaid = (int)jUser.GetValue("parroquiaid");
                        user.perfil = (int)jUser.GetValue("perfil");
                        user.nombreperfil = jUser.GetValue("nombreperfil").ToString();
                        JObject obj = jUser.GetValue("sucursal").ToObject<JObject>();
                        user.establecimiento = Establecimiento.AsignaDatos(obj);
                        user.puntoemisionid = (int)obj.GetValue("idpuntoemision");
                        retorno.data = user;
                    }
                }
            }
            catch (Exception e) { }

            return retorno;
        }
        public int Save(Usuario usuario)
        {
            int retorno = -1;
            try
            {
                using (var sql = new NpgsqlConnection(Global._connectionString))
                {
                    using (var cmd = new NpgsqlCommand("select * from public.Sp_Grab_Usuario(" +
                        ":eidusuario, :ausuario, :aclave, :arazonsocial, :anip, :eperfil, :eestablecimientoid, " +
                        ":eparroquiaid, :anombreperfil, :epuntoemisionid)", sql))
                    {
                        cmd.Parameters.AddWithValue(":eidusuario", usuario.idusuario);
                        cmd.Parameters.AddWithValue(":ausuario", usuario.usuario);
                        cmd.Parameters.AddWithValue(":aclave", usuario.clave);
                        cmd.Parameters.AddWithValue(":arazonsocial", usuario.razonsocial);
                        cmd.Parameters.AddWithValue(":anip", usuario.nip);
                        cmd.Parameters.AddWithValue(":eperfil", usuario.perfil);
                        cmd.Parameters.AddWithValue(":eestablecimientoid", usuario.establecimiento.idestablecimiento);
                        cmd.Parameters.AddWithValue(":eparroquiaid", usuario.parroquiaid);
                        cmd.Parameters.AddWithValue(":anombreperfil", usuario.nombreperfil);
                        cmd.Parameters.AddWithValue(":epuntoemisionid", usuario.puntoemisionid);
                        cmd.CommandType = System.Data.CommandType.Text;
                        sql.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                retorno = reader.IsDBNull(0) ? -1 : reader.GetInt32(0);
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
