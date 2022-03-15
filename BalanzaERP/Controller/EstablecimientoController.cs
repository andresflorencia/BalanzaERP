using BalanzaERP.Model;
using BalanzaERP.Utils;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BalanzaERP.Controller
{
    public class EstablecimientoController
    {
        public List<Establecimiento> getList()
        {
            var retorno = new List<Establecimiento>();

            try
            {
                using (var sql = new NpgsqlConnection(Global._connectionString))
                {
                    using (var cmd = new NpgsqlCommand("select * from public.sp_bus_establecimiento(:tipoaccion)", sql))
                    {
                        cmd.Parameters.AddWithValue(":tipoaccion", "LISTA");
                        cmd.CommandType = System.Data.CommandType.Text;
                        sql.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var miE = new Establecimiento();
                                miE.idestablecimiento = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                                miE.codigoestablecimiento = reader.IsDBNull(1) ? "" : reader.GetString(1);
                                miE.nombreestablecimiento = reader.IsDBNull(3) ? "" : reader.GetString(3);
                                miE.direccion = reader.IsDBNull(4) ? "" : reader.GetString(4);
                                miE.parroquiaid = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);
                                miE.tipo = reader.IsDBNull(6) ? "" : reader.GetString(6);
                                miE.nombrecomercial = reader.IsDBNull(7) ? "" : reader.GetString(7);
                                miE.codigoestablecimientosri = reader.IsDBNull(8) ? "" : reader.GetString(8);
                                miE.nummarmita = reader.IsDBNull(9) ? 0 : reader.GetInt32(9);
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

        public Establecimiento getById(int codigo)
        {
            var retorno = new Establecimiento();

            try
            {
                using (var sql = new NpgsqlConnection(Global._connectionString))
                {
                    using (var cmd = new NpgsqlCommand("select * from public.sp_bus_establecimiento(:tipoaccion, :codigo)", sql))
                    {
                        cmd.Parameters.AddWithValue(":tipoaccion", "FILL");
                        cmd.Parameters.AddWithValue(":codigo", codigo);
                        cmd.CommandType = System.Data.CommandType.Text;
                        sql.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                retorno.idestablecimiento = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                                retorno.codigoestablecimiento = reader.IsDBNull(1) ? "" : reader.GetString(1);
                                retorno.nombreestablecimiento = reader.IsDBNull(3) ? "" : reader.GetString(3);
                                retorno.direccion = reader.IsDBNull(4) ? "" : reader.GetString(4);
                                retorno.parroquiaid = reader.IsDBNull(5) ? 0 : reader.GetInt32(5);
                                retorno.tipo = reader.IsDBNull(6) ? "" : reader.GetString(6);
                                retorno.nombrecomercial = reader.IsDBNull(7) ? "" : reader.GetString(7);
                                retorno.codigoestablecimientosri = reader.GetString(8) ?? "";
                                retorno.nummarmita = reader.IsDBNull(9) ? 0 : reader.GetInt32(9);
                            }
                            return retorno;
                        }
                    }
                }
            }
            catch (NpgsqlException e) { MessageBox.Show(e.Message); }
            return retorno;
        }
        public void LlenarCombo(ComboBox combo) {
            List<Establecimiento> datos = getList();
            combo.ValueMember = "idestablecimiento";
            combo.DisplayMember = "nombreestabelcimiento";
            combo.DataSource = datos;
            combo.SelectedIndex = -1;
            /*foreach (var e in datos)
            {
                combo.Items.Add(e);
            }*/
        }

        public int Save(Establecimiento establecimiento)
        {
            var retorno = 0;
            using (var cnn = new NpgsqlConnection(Global._connectionString))
            {
                cnn.Open();
                try
                {
                    using (var cmd = new NpgsqlCommand("select * from public.Sp_Grab_Establecimiento(" +
                        ":eidestablecimiento, :acodigoestablecimiento, :arucempresa, :anombreestablecimiento, " +
                        ":adireccion, :atipo, :anombrecomercial, :acodigoestablecimientosri, :enummarmita)", cnn))
                    {
                        cmd.Parameters.AddWithValue(":eidestablecimiento", establecimiento.idestablecimiento);
                        cmd.Parameters.AddWithValue(":acodigoestablecimiento", establecimiento.codigoestablecimiento);
                        cmd.Parameters.AddWithValue(":arucempresa", "");
                        cmd.Parameters.AddWithValue(":anombreestablecimiento", establecimiento.nombreestablecimiento);
                        cmd.Parameters.AddWithValue(":adireccion", establecimiento.direccion);
                        cmd.Parameters.AddWithValue(":atipo", establecimiento.tipo);
                        cmd.Parameters.AddWithValue(":anombrecomercial", establecimiento.nombrecomercial);
                        cmd.Parameters.AddWithValue(":acodigoestablecimientosri", establecimiento.codigoestablecimientosri);
                        cmd.Parameters.AddWithValue(":enummarmita", establecimiento.nummarmita);
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

        public Respuesta SaveList(List<Establecimiento> establecimientos)
        {
            var retorno = new Respuesta();
            var band = false;
            using (var cnn = new NpgsqlConnection(Global._connectionString))
            {
                cnn.Open();
                var trans = cnn.BeginTransaction();
                try
                {
                    foreach (var miE in establecimientos)
                    {
                        using (var cmd = new NpgsqlCommand("select * from public.Sp_Grab_Establecimiento(" +
                            ":eidestablecimiento, :acodigoestablecimiento, :arucempresa, :anombreestablecimiento, " +
                            ":adireccion, :atipo, :anombrecomercial, :acodigoestablecimientosri, :enummarmita)", cnn))
                        {
                            cmd.Parameters.AddWithValue(":eidestablecimiento", miE.idestablecimiento);
                            cmd.Parameters.AddWithValue(":acodigoestablecimiento", miE.codigoestablecimiento);
                            cmd.Parameters.AddWithValue(":arucempresa", "");
                            cmd.Parameters.AddWithValue(":anombreestablecimiento", miE.nombreestablecimiento);
                            cmd.Parameters.AddWithValue(":adireccion", miE.direccion);
                            cmd.Parameters.AddWithValue(":atipo", miE.tipo);
                            cmd.Parameters.AddWithValue(":anombrecomercial", miE.nombrecomercial);
                            cmd.Parameters.AddWithValue(":acodigoestablecimientosri", miE.codigoestablecimientosri);
                            cmd.Parameters.AddWithValue(":enummarmita", miE.nummarmita);
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
                                retorno.message = "Ocurrió un error al intentar guardar los datos de los establecimientos.";
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

        public void LlenarComboMarmita(ComboBox combo, int nummarmita)
        {
            if (nummarmita == 0)
                nummarmita = 1;
            for(int i= 0; i < nummarmita; i++)
            {
                combo.Items.Add("Marmita " + (i + 1).ToString() + " (M" + (i + 1).ToString() + ")");
            }
            combo.SelectedIndex = 0;
        }
    }
}
