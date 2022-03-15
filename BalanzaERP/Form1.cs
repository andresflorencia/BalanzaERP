using BalanzaERP.Controller;
using BalanzaERP.Model;
using BalanzaERP.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using BarcodeLib;
using BarcodeLib.Barcode;
using System.IO;
using TSCSDK;
using System.IO.Ports;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace BalanzaERP
{
    public partial class FrmPrincipal : Form
    {
        NumberFormatInfo nfi = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
        string SeparadorDecimal = ".";
        Controller.ProductoController productoController = new Controller.ProductoController();
        Controller.EstablecimientoController establecimientoController = new Controller.EstablecimientoController();
        Controller.TransaccionController transaccionController = new TransaccionController();
        Random rnd = new Random();
        Double _valorKG = 2.20462;
        int contMarqueta = 0, contMarmita = 0;

        TSCSDK.driver driver = new driver();
        View.FrmLoader frmLoader;

        private delegate void DelegadoAcceso(string accion);
        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            Global.separador_decimal = nfi.NumberDecimalSeparator;
            establecimientoController.LlenarCombo(cmbPlantaProduccion);
            cmbPlantaProduccion.SelectedValue = Global.usuario.establecimiento.idestablecimiento;
            establecimientoController.LlenarComboMarmita(cmbMarmita, Global.usuario.establecimiento.nummarmita);
            lblSucursal.Text = "    " + Global.usuario.razonsocial;
            btnLimpiar_Click(btnLimpiar, null);
            //cmbUnidad.SelectedIndex = 0;
            LlenarComboPuertos();
            LlenarComboImpresoras();

            string imagePath = Path.Combine(Application.StartupPath, @"images\off.png");
            btnConectaPuerto.Image = Image.FromFile(imagePath);
            cmbPrinters.Enabled = false;
            ckImpresion.Checked = true;
            VerificaBorrador();
        }

        private void LlenarComboImpresoras()
        {
            try
            {
                cmbPrinters.Items.Clear();
                foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                {
                    cmbPrinters.Items.Add(printer);
                }
                cmbPrinters.Text = Global.name_printer;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private void LlenarComboPuertos()
        {
            try
            {
                string[] puertos = SerialPort.GetPortNames();
                cmbPuerto.Items.Clear();
                foreach (var port in puertos)
                {
                    cmbPuerto.Items.Add(port);
                }
                ckbModoBalanza.Checked = false;
                if (cmbPuerto.Items.Count > 0)
                    cmbPuerto.SelectedIndex = 0;
            }catch(Exception e) { MessageBox.Show(e.Message); }
        }

        private string GeneraCodBar(string dato)
        {
            string ruta = "";
            try
            {
                //DIRECTORIO DONDE SE GUARDAN LAS IMAGENES DE LOS CODIGOS DE BARRA
                string path = Application.StartupPath + "\\codbar"; 
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //CONFIG PARA CODBAR
                BarcodeLib.Barcode.Linear code_128 = new BarcodeLib.Barcode.Linear();
                code_128.Type = BarcodeType.CODE128;
                code_128.Data = dato;

                code_128.UOM = UnitOfMeasure.CM;
                code_128.BarWidth = 0.001f;
                code_128.BarHeight = 0.7f;
                code_128.LeftMargin = 0.2f;
                code_128.RightMargin = 0.2f; 
                code_128.ShowText = false;

                code_128.ImageFormat = System.Drawing.Imaging.ImageFormat.Png;
                code_128.ImageWidth = 3;
                code_128.ImageHeight = 0.7f; 
                code_128.ResizeImage = true;
                ruta = path + "\\" + dato + ".png";
                code_128.drawBarcode(ruta);//GUARDA EL CODIGO DE BARRA
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return ruta;//RETORNO LA RUTA DONDE ESTÁ GUARDADO EL CODBAR PARA POSTERIOR IMPRESION
        }

        private void txtBuscarProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;
            BuscarProducto();
        }

        private void BuscarProducto() {
            var producto = productoController.BuscarProducto(txtBuscarProducto.Text.Trim());
            if (producto != null)
            {
                lblProducto.Text = producto.nombreproducto;
                lblProducto.Tag = producto;                
                txtCantidad.Enabled = true;
                txtCantidad.Focus();
            }
            txtBuscarProducto.Text = "";
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void Ticket()
        {
            try
            {
                driver.openport("TSC TTP-244 Pro");
                byte[] result_utf8 = System.Text.Encoding.UTF8.GetBytes("TEXT 40,620,\"ARIAL.TTF\",0,12,12,\"utf8 test Wörter auf Deutsch\"");
                driver.sendcommand("SIZE 51 mm, 25 mm");
                driver.sendcommand("SPEED 20");
                driver.sendcommand("DENSITY 15");
                driver.sendcommand("DIRECTION 1");
                driver.sendcommand("SET TEAR ON");
                driver.sendcommand("CODEPAGE UTF-8");
                driver.clearbuffer();
                driver.downloadpcx("UL.PCX", "UL.PCX");
                driver.windowsfont(40, 490, 48, 0, 0, 0, "Arial", "Windows Font Test");
                driver.windowsfontunicode(40, 550, 48, 0, 0, 0, "Arial", "Windows Unicode Test");
                driver.sendcommand("PUTPCX 40,40,\"UL.PCX\"");
                driver.sendcommand(result_utf8);
                driver.barcode("40", "300", "128", "80", "1", "0", "2", "2", "2345678");
                driver.printerfont("40", "440", "0", "0", "15", "15", "TEXTO");
                driver.printlabel("1", "1");
                driver.closeport();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(e.KeyCode.ToString() + " - " + e.KeyData.ToString() + " - " + e.KeyValue.ToString());
            if (ckbModoBalanza.Checked && e.KeyCode != Keys.Enter)
            {
                txtCantidad.Text = "";
                return;
            }
            //DETECTAR EL SEPARADOR DECIMAL
            if (e.KeyValue == 110 || e.KeyValue == 188 || e.KeyValue == 190)
            {
                txtCantidad.Text = txtCantidad.Text.Substring(0, txtCantidad.Text.Length) + Global.separador_decimal;
                txtCantidad.Select(txtCantidad.Text.Length, 0);
                e.SuppressKeyPress = true;
            }
            if (e.KeyCode == Keys.Enter && lblProducto.Tag != null && lblProducto.Text.Length > 0)
            {
                try
                {
                    Double cant = Double.Parse(txtCantidad.Text.Trim());
                    if (cant <= 0)
                        return;

                    if (ckImpresion.Checked && (Global.name_printer.Trim().Equals("") || cmbPrinters.Text.Trim().Equals("")))
                    {
                        MessageBox.Show("Asegúrese de especificar la impresora.", Application.ProductName);
                        cmbPrinters.Focus();
                        return;
                    }

                    string codigo = "";
                    do
                    {
                        codigo = txtLote.Text + "M"+(cmbMarmita.SelectedIndex+1).ToString().PadLeft(2,'0') + DateTime.Now.ToString("HHmmss");
                    } while (!VerificaCodigoMarqueta(codigo));

                    if (cmbUnidad.SelectedIndex == 0)
                        cant = cant * _valorKG;

                    dgvDetalle.Rows.Add(1);
                    Producto producto = (Producto)lblProducto.Tag;
                    dgvDetalle.Rows[dgvDetalle.Rows.Count - 1].Cells[0].Value = producto.nombreproducto;
                    dgvDetalle.Rows[dgvDetalle.Rows.Count - 1].Cells[0].Tag = producto;
                    dgvDetalle.Rows[dgvDetalle.Rows.Count - 1].Cells[1].Value = txtLote.Text;
                    dgvDetalle.Rows[dgvDetalle.Rows.Count - 1].Cells[1].Tag = dgvDetalle.Rows.Count;
                    dgvDetalle.Rows[dgvDetalle.Rows.Count - 1].Cells[2].Value = codigo;
                    dgvDetalle.Rows[dgvDetalle.Rows.Count - 1].Cells[2].Tag = cmbMarmita.SelectedIndex+1;
                    dgvDetalle.Rows[dgvDetalle.Rows.Count - 1].Cells[3].Value = cant.ToString("0.00");
                    dgvDetalle.Rows[dgvDetalle.Rows.Count - 1].Cells[3].Tag = ckbModoBalanza.Checked ? "B" : "M";
                    dgvDetalle.Rows[dgvDetalle.Rows.Count - 1].Cells[4].Value = "Imprimir";
                    dgvDetalle.Rows[dgvDetalle.Rows.Count - 1].Cells[5].Value = "Eliminar";
                    txtCantidad.Text = "";
                    Totales();
                    btnGuardar.Enabled = true;
                    txtBuscarProducto.Enabled = btnBuscarProducto.Enabled = dtpFechaLote.Enabled =  false;

                    if (ckImpresion.Checked)
                    {
                        string ruta = GeneraCodBar(dgvDetalle.Rows[dgvDetalle.Rows.Count - 1].Cells[2].Value.ToString());
                        transaccionController.ImprimirTicketMarqueta(producto.nombreproducto, txtLote.Text,
                            dgvDetalle.Rows[dgvDetalle.Rows.Count - 1].Cells[2].Value.ToString(),
                            cant.ToString("0.00"), ruta);
                    }

                    lblContMarqueta.Text = "Marqueta " + (contMarqueta + 1) + " de 8";
                    contMarqueta++;
                    if (contMarqueta == 8)
                    {
                        contMarqueta = 0;
                        contMarmita++;
                        if (contMarmita < cmbMarmita.Items.Count)
                        {
                            cmbMarmita.SelectedIndex = contMarmita;
                            lblContMarqueta.Text = "Marqueta 0 de 8";
                        } else {
                            cmbMarmita.SelectedIndex = 0;
                            contMarmita = 0;
                            lblContMarqueta.Text = "Marqueta 0 de 8";
                        }
                    }

                    dgvDetalle.FirstDisplayedScrollingRowIndex = dgvDetalle.RowCount - 1;
                    GuardarDatos(true);
                }
                catch {
                    MessageBox.Show("Ingrese un valor válido", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private bool VerificaCodigoMarqueta(string codigo)
        {
            bool band = true;
            try
            {
                for(var i=0; i < dgvDetalle.RowCount; i++)
                {
                    if (dgvDetalle.Rows[i].Cells[3].Value.ToString().Equals(codigo))
                    {
                        band = false;
                        break;
                    }
                }

            }catch(Exception e) { }
            return band;
        }

        private void dgvDetalle_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 5)
                {
                    if (MessageBox.Show("Desea eliminar éste registro?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        bool band = true;
                        if (((int)btnGuardar.Tag) > 0)
                            band = transaccionController.DeleteMarqueta((int)btnGuardar.Tag, (int)dgvDetalle.Rows[e.RowIndex].Cells[1].Tag);

                        if (band)
                        {
                            dgvDetalle.Rows.RemoveAt(e.RowIndex);
                            Totales();
                            if (dgvDetalle.Rows.Count <= 0)
                            {
                                txtBuscarProducto.Enabled = btnBuscarProducto.Enabled = dtpFechaLote.Enabled = true;
                                btnGuardar.Enabled = false;
                            }
                        }
                    }
                }
                else if (e.ColumnIndex == 4)
                {
                    if (Global.name_printer.Trim().Equals("") || cmbPrinters.Text.Trim().Equals(""))
                    {
                        MessageBox.Show("Asegúrese de especificar la impresora.", Application.ProductName);
                        cmbPrinters.Focus();
                        return;
                    }

                    string ruta = GeneraCodBar(dgvDetalle.Rows[e.RowIndex].Cells[2].Value.ToString());
                    transaccionController.ImprimirTicketMarqueta(
                        dgvDetalle.Rows[e.RowIndex].Cells[0].Value.ToString(),
                        dgvDetalle.Rows[e.RowIndex].Cells[1].Value.ToString(),
                        dgvDetalle.Rows[e.RowIndex].Cells[2].Value.ToString(),
                        dgvDetalle.Rows[e.RowIndex].Cells[3].Value.ToString(),
                        ruta);
                }
            }
        }

        private String GenerarCodigoLote() {
            String retorno = "";
            if (cmbPlantaProduccion.SelectedIndex >= 0) {
                retorno = ((Establecimiento)cmbPlantaProduccion.SelectedItem).codigoestablecimiento
                    + dtpFechaLote.Value.ToString("yyyyMMdd") + (transaccionController.Num_Transacciones_Fecha(dtpFechaLote.Value.ToShortDateString()) + 1).ToString().PadLeft(2, '0');
                //MessageBox.Show(cmbPlantaProduccion.SelectedValue.ToString());
                txtCantidad.Enabled = true;
            }
            return retorno;
        }
        private Double GetTotalLote() {
            Double retorno = 0;
            try
            {
                if (dgvDetalle.Rows.Count <= 0)
                    return 0;
                for (var i = 0; i < dgvDetalle.Rows.Count; i++)
                    retorno += Double.Parse(dgvDetalle.Rows[i].Cells[3].Value==null ? "0" : dgvDetalle.Rows[i].Cells[3].Value.ToString());
            }
            catch { }
            return retorno;
        }

        private void cmbPlantaProduccion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtLote.Text = GenerarCodigoLote();
            txtBuscarProducto.Focus();
            txtCantidad.Enabled = lblProducto.Tag != null;
        }

        private void dtpFechaLote_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                txtLote.Text = GenerarCodigoLote();
                txtBuscarProducto.Focus();
                dtpFechaVencimiento.Value = dtpFechaLote.Value.AddMonths(1);
                dtpFechaVencimiento.MinDate = dtpFechaLote.Value;
            }
            catch (Exception ex){ MessageBox.Show(ex.Message); }
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
        }

        private void FrmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Directory.Delete(Application.StartupPath + @"\codbar", true);
            }catch(Exception ex) { }
            finally
            {
                if (Global.usuario != null)
                    Program.frmLogin.Close();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            
        }

        private void dgvDetalle_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Totales();
        }

        private void Totales()
        {
            lblTotalLote.Text = "Total (lb): " + GetTotalLote().ToString("0.00").PadLeft(21, ' ') +
                                "\nNum. Marquetas: " + dgvDetalle.Rows.Count.ToString().PadLeft(6, ' ') +
                                "\nNum. Tinas: " + ((int)(dgvDetalle.Rows.Count / 4)).ToString().PadLeft(16, ' ');
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarDatos(false);
        }

        private void GuardarDatos(bool borrador)
        {
            try
            {
                if (!borrador)
                {
                    if (MessageBox.Show("¿Está seguro que desea guardar este registro?", "Guardar datos", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.No)
                        return;
                }

                if (!ValidarDatos())
                    return;
                var transaccion = new Transaccion();
                string accion = "NUEVO";
                transaccion.idtransaccion = (int)btnGuardar.Tag;
                if (transaccion.idtransaccion > 0)
                    accion = "UPDATE";
                transaccion.personaid = Global.usuario.idusuario;
                transaccion.usuarioid = Global.usuario.idusuario;
                transaccion.establecimientoid = ((Establecimiento)cmbPlantaProduccion.SelectedItem).idestablecimiento;
                transaccion.observacion = txtObservacion.Text;
                transaccion.tipotransaccion = "19";
                transaccion.fechadocumento = dtpFechaPesado.Value.ToString("yyyy-MM-dd");
                transaccion.fechaproduccion = dtpFechaLote.Value.ToString("yyyy-MM-dd");
                transaccion.estado = borrador ? 2 : 1;  //--2: BORRADOR , 1: FINALIZADO
                transaccion.secuencial = transaccionController.Secuencial("ULTIMO", "1", "19", transaccion.establecimientoid.ToString(), "0") + 1;
                transaccion.codigotransaccion = "INVLOTE-" + ((Establecimiento)cmbPlantaProduccion.SelectedItem).codigoestablecimiento +
                                                "-" + transaccion.secuencial.ToString().PadLeft(9, '0'); //GENERAR EL SECUENCIAL
                if (lblMessage.Tag != null)
                    transaccion.codigotransaccion = lblMessage.Tag.ToString();

                var midetalle = new DetalleTransaccion();

                if (borrador)
                {
                    var marqueta = new DetalleMarqueta();
                    int fila = dgvDetalle.Rows.Count - 1;
                    marqueta.linea = dgvDetalle.Rows.Count;
                    marqueta.productoid = ((Producto)dgvDetalle.Rows[fila].Cells[0].Tag).idproducto;
                    marqueta.numerolote = dgvDetalle.Rows[fila].Cells[1].Value.ToString();
                    marqueta.codigomarqueta = dgvDetalle.Rows[fila].Cells[2].Value.ToString();
                    marqueta.cantidad = Double.Parse(dgvDetalle.Rows[fila].Cells[3].Value.ToString());
                    marqueta.fechavencimiento = dtpFechaVencimiento.Value.ToString("yyyy-MM-dd");
                    marqueta.nummarmita = (int)dgvDetalle.Rows[fila].Cells[2].Tag;
                    marqueta.origenpesado = (string)dgvDetalle.Rows[fila].Cells[3].Tag;
                    transaccion.detallemarqueta.Add(marqueta);
                }

                for (var i = 0; i < dgvDetalle.Rows.Count; i++)
                {
                    if (!borrador)
                    {
                        var marqueta = new DetalleMarqueta();
                        marqueta.linea = i+1;
                        marqueta.productoid = ((Producto)dgvDetalle.Rows[i].Cells[0].Tag).idproducto;
                        marqueta.numerolote = dgvDetalle.Rows[i].Cells[1].Value.ToString();
                        marqueta.codigomarqueta = dgvDetalle.Rows[i].Cells[2].Value.ToString();
                        marqueta.cantidad = Double.Parse(dgvDetalle.Rows[i].Cells[3].Value.ToString());
                        marqueta.fechavencimiento = dtpFechaVencimiento.Value.ToString("yyyy-MM-dd");
                        marqueta.nummarmita = (int)dgvDetalle.Rows[i].Cells[2].Tag;
                        marqueta.origenpesado = (string)dgvDetalle.Rows[i].Cells[3].Tag;
                        transaccion.detallemarqueta.Add(marqueta);
                    }

                    midetalle.cantidad += Double.Parse(dgvDetalle.Rows[i].Cells[3].Value.ToString());
                }

                midetalle.linea = 1;
                midetalle.producto.idproducto = transaccion.detallemarqueta[0].productoid;
                midetalle.numerolote = transaccion.detallemarqueta[0].numerolote;
                midetalle.fechavencimiento = transaccion.detallemarqueta[0].fechavencimiento;
                midetalle.marquetas = transaccion.detallemarqueta.Count;
                midetalle.codigoproducto = ((Producto)dgvDetalle.Rows[0].Cells[0].Tag).codigoproducto;
                transaccion.detalletransaccion.Add(midetalle);

                var result = transaccionController.Save(accion, transaccion);

                btnGuardar.Tag = result.data;

                if(result.haserror || !borrador)
                    MessageBox.Show(result.message);
                if (!borrador && !result.haserror)
                {
                    btnGuardar.Tag = 0;
                    btnLimpiar_Click(btnLimpiar, null);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool ValidarDatos() {
            if (cmbPlantaProduccion.SelectedIndex < 0)
            {
                MessageBox.Show("Debe especificar la planta de producción.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (dgvDetalle.Rows.Count <= 0)
            {
                MessageBox.Show("No ha ingresado el detalle de las marquetas.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else
            {
                for(var i=0; i< dgvDetalle.Rows.Count; i++)
                {
                    double peso = dgvDetalle.Rows[i].Cells[3].Value == null || dgvDetalle.Rows[i].Cells[3].Value.ToString().Trim().Length == 0 ? 0 : Double.Parse(dgvDetalle.Rows[i].Cells[3].Value.ToString());
                    if (peso <= 0)
                    {
                        MessageBox.Show("Debe ingresar una cantidad válida en la fila " + (i + 1), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                }
            }
            return true;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.Rows.Count > 0 && (int)btnGuardar.Tag > 0)
            {
                if (MessageBox.Show("¿Está seguro que desea limpiar los datos?", "Limpiar datos", MessageBoxButtons.YesNo,
                   MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }
            
            dgvDetalle.Rows.Clear();
            lblProducto.Tag = null;
            lblProducto.Text = "";
            txtObservacion.Text = "";
            txtObservacion.Enabled = true;
            dgvDetalle.Columns[3].ReadOnly = false;
            dgvDetalle.Columns[5].Visible = true;
            dtpFechaLote.Enabled = true;
            dtpFechaVencimiento.Enabled = true;
            dtpFechaLote.Value = DateTime.Now.AddDays(-1);
            dtpFechaLote.MinDate = DateTime.Now.AddDays(-2);
            dtpFechaLote.MaxDate = DateTime.Now;
            dtpFechaPesado.Value = dtpFechaPesado.MinDate = dtpFechaPesado.MaxDate = DateTime.Now;
            //dtpFechaVencimiento.Value = DateTime.Now;
            Totales();
            lblConversion.Text = "";
            txtCantidad.Enabled = false;
            cmbMarmita.SelectedIndex = 0;
            cmbUnidad.SelectedIndex = 0; //cmbUnidad.Enabled = false;
            txtBuscarProducto.Enabled = btnBuscarProducto.Enabled = dtpFechaLote.Enabled = true;
            contMarqueta = contMarmita = 0;
            lblContMarqueta.Text = "Marqueta 0 de 8";
            btnGuardar.Tag = 0;
            lblMessage.Tag = null;

            txtLote.Text = GenerarCodigoLote();
            GetTotalLote();
            var secuencial = transaccionController.Secuencial("ULTIMO", "1", "19", cmbPlantaProduccion.SelectedValue.ToString(), "0") + 1;
            lblMessage.Text = "Nuevo Registro (" + "INVLOTE-" + ((Establecimiento)cmbPlantaProduccion.SelectedItem).codigoestablecimiento +
                                            "-" + (secuencial).ToString().PadLeft(9, '0') + ")";
        }

        private void VerificaBorrador()
        {
            try
            {
                var borradores = transaccionController.getList("BORRADOR", 0, "", true);
                if(borradores != null && borradores.Count > 0)
                {
                    if (MessageBox.Show("Existe un pesado sin finalizar, ¿Desea continuar con el registro?", "Pesado pendiente", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                        MostrarTransaccion(borradores[0]);
                }
            }catch(Exception ex) { }
        }

        private void FrmPrincipal_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void FrmPrincipal_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    if (MessageBox.Show("¿Está seguro que desea salir del sistema?", "Salir", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                        this.Close();
                        break;
                case Keys.F2:
                    if(btnGuardar.Enabled)
                        btnGuardar_Click(btnGuardar, null);
                    break;
                case Keys.F3:
                    btnLimpiar_Click(btnLimpiar, null);
                    break;
                case Keys.F7:
                    txtCantidad_KeyDown(txtCantidad, new KeyEventArgs(Keys.Enter));
                    break;
            }
        }

        private void tsBuscar_Click(object sender, EventArgs e)
        {
            string value = Interaction.InputBox("Ingrese el código de la transacción o el número del lote." +
                "\n\nEj.: INVLOTE-POR-000000001 ", "Búsqueda de transacción", "");
            if (!value.Trim().Equals(""))
            {
                var listTrans = transaccionController.getList("POR_CODIGO", 0, value, true);
                if (listTrans.Count <= 0)
                {
                    MessageBox.Show("No se encontró registro con el código " + value, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MostrarTransaccion(listTrans[0]);
                }
            }
        }

        private void MostrarTransaccion(Transaccion transaccion)
        {
            btnLimpiar_Click(btnLimpiar, null);
            if(transaccion.estado == 2 && transaccion.codigosistema == 0)
                lblMessage.Text = transaccion.codigotransaccion + " (BORRADOR)";
            else if (transaccion.estado > 0 && transaccion.codigosistema == 0)
                lblMessage.Text = transaccion.codigotransaccion + " (NO SINCRONIZADO)";
            else
                lblMessage.Text = transaccion.codigotransaccion + " (SINCRONIZADO)";
            
            lblMessage.Tag = transaccion.codigotransaccion;

            dtpFechaVencimiento.MinDate = dtpFechaLote.MinDate = dtpFechaPesado.MinDate = DateTime.Parse("01/01/1900");
            dtpFechaVencimiento.MaxDate = dtpFechaLote.MaxDate = dtpFechaPesado.MaxDate = DateTime.Parse("31/12/9998");

            dtpFechaPesado.Value = DateTime.Parse(transaccion.fechadocumento);
            dtpFechaVencimiento.Value = DateTime.Parse(transaccion.detalletransaccion[0].fechavencimiento);
            dtpFechaLote.Value = DateTime.Parse(transaccion.fechaproduccion);
            cmbPlantaProduccion.SelectedValue = transaccion.establecimientoid;
            txtLote.Text = transaccion.detalletransaccion[0].numerolote;
            txtObservacion.Text = transaccion.observacion;
            btnGuardar.Tag = transaccion.idtransaccion;
            if (transaccion.estado != 2)
            {
                txtObservacion.Enabled = false;
                btnGuardar.Enabled = false;
                dgvDetalle.Columns[3].ReadOnly = true;
                dgvDetalle.Columns[5].Visible = false;
                txtCantidad.Enabled = false;
                txtBuscarProducto.Enabled = btnBuscarProducto.Enabled = dtpFechaLote.Enabled = dtpFechaVencimiento.Enabled = false;
                cmbMarmita.SelectedIndex = -1;
            }
            else 
            {
                lblProducto.Text = transaccion.detalletransaccion[0].producto.nombreproducto;
                lblProducto.Tag = transaccion.detalletransaccion[0].producto;
                txtCantidad.Enabled = true;
                txtCantidad.Focus();
                btnGuardar.Enabled = true;
            }


            for (var i = 0; i < transaccion.detallemarqueta.Count; i++)
            {
                dgvDetalle.Rows.Add(1);
                dgvDetalle.Rows[dgvDetalle.Rows.Count - 1].Cells[0].Value = transaccion.detalletransaccion[0].producto.nombreproducto;
                dgvDetalle.Rows[dgvDetalle.Rows.Count - 1].Cells[0].Tag = transaccion.detalletransaccion[0].producto;
                dgvDetalle.Rows[dgvDetalle.Rows.Count - 1].Cells[1].Value = transaccion.detallemarqueta[i].numerolote;
                dgvDetalle.Rows[dgvDetalle.Rows.Count - 1].Cells[1].Tag = transaccion.detallemarqueta[i].linea;
                dgvDetalle.Rows[dgvDetalle.Rows.Count - 1].Cells[2].Value = transaccion.detallemarqueta[i].codigomarqueta;
                dgvDetalle.Rows[dgvDetalle.Rows.Count - 1].Cells[2].Tag = transaccion.detallemarqueta[i].nummarmita;
                dgvDetalle.Rows[dgvDetalle.Rows.Count - 1].Cells[3].Value = transaccion.detallemarqueta[i].cantidad;
                dgvDetalle.Rows[dgvDetalle.Rows.Count - 1].Cells[3].Tag = transaccion.detallemarqueta[i].origenpesado;
                dgvDetalle.Rows[dgvDetalle.Rows.Count - 1].Cells[4].Value = "Imprimir";
                dgvDetalle.Rows[dgvDetalle.Rows.Count - 1].Cells[5].Value = "Eliminar";
                dgvDetalle.FirstDisplayedScrollingRowIndex = dgvDetalle.RowCount - 1;
            }

            //VERIFICA EN QUE MARMITA Y MARQUETA SE QUEDÓ
            double div =  (double)((double)transaccion.detallemarqueta.Count / 8d);
            double ent =  Math.Truncate(div);
            double part = div - ent;
            if (cmbMarmita.Items.Count == 0)
                ent = 0;
            cmbMarmita.SelectedIndex = (int)ent;
            contMarqueta = (int)(part * 8);
            contMarmita = (int)(contMarqueta > 0 ? ent + 1 : ent);
            lblContMarqueta.Text = "Marqueta " + contMarqueta + " de 8";


            Totales();
        }

        private void tlUpload_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea subir la información al sistema?", "Sincronizar registros", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.No)
                return;
            ShowLoader();
            ThreadSafe(() =>
            {
                var respuesta = transaccionController.Sincronizar(Global.usuario.usuario, Global.usuario.clave);
                HideLoader();
                MessageBox.Show(respuesta.message, Application.ProductName,
                    MessageBoxButtons.OK, respuesta.haserror ? MessageBoxIcon.Warning : MessageBoxIcon.Information);

            }, this);
        }

        private void ShowLoader()
        {
            frmLoader = new View.FrmLoader();
            frmLoader.MdiParent = this.MdiParent;
            frmLoader.Width = this.Width;
            frmLoader.Height = this.Height;
            frmLoader.Show();
        }
        private void HideLoader()
        {
            if (frmLoader != null)
            {
                frmLoader.Close();
            }
        }
        private void ThreadSafe(Action callback, Form form)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.RunWorkerCompleted += (obj, e) =>
            {
                if (form.InvokeRequired)
                    form.Invoke(callback);
                else
                    callback();
            };
            worker.RunWorkerAsync();
        }

        private void tlListado_Click(object sender, EventArgs e)
        {
            var transaccion = transaccionController.BuscarTransaccion();
            if (transaccion != null)
            {
                transaccion = transaccionController.getList("POR_ID", transaccion.idtransaccion, "", true)[0];
                MostrarTransaccion(transaccion);
            }
        }

        private void tsCerrarSesion_Click(object sender, EventArgs e)
        {

        }

        private void tsCerrarSesion_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea cerrar la sesión?", "Cerrar Sesión", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.No)
                return;

            Global.usuario = null;
            Program.frmLogin.Show();
            this.Close();
        }

        private void cmbPuerto_SelectionChangeCommitted(object sender, EventArgs e)
        {
        }

        private void DatoRecibido(object sender, SerialDataReceivedEventArgs e)
        {
            AccesoInterrupcion(spPuerto.ReadExisting());
        }
        private void AccesoForm(string accion)
        {
            try
            {
                if (accion.Contains("="))
                    accion = accion.Replace('=', ' ');
                if (accion.Contains("."))
                {
                    if (Global.translate_data.Equals("yes"))
                    {
                        var temp = "";
                        foreach (var c in accion)
                            temp = c + temp;

                        accion = temp;
                    }
                    string part1 = accion.Split('.')[0];
                    part1 = Int32.Parse(part1).ToString();
                    part1 = part1.Trim();
                    if (part1.Equals(""))
                        part1 = "0";
                    accion = part1 + Global.separador_decimal + accion.Split('.')[1];
                }
                txtCantidad.Text = accion.Trim();
                txtCantidad.SelectAll();
                //txtCantidad.Focus();
                lblConversion.Text = "";
                if (cmbUnidad.SelectedIndex == 0)//PARA EL CASO DE QUE SEA KG
                    lblConversion.Text = (Double.Parse(accion) * _valorKG).ToString("0.00") + " lb";
                else if(cmbUnidad.SelectedIndex == 1)
                    lblConversion.Text = (Double.Parse(accion) / _valorKG).ToString("0.00") + " kg";
            }
            catch (Exception ex) {
                lblConversion.Text = "";
            }
        }
        private void AccesoInterrupcion(string accion)
        {
            DelegadoAcceso delegado = new DelegadoAcceso(AccesoForm);
            object[] arg = { accion };
            base.Invoke(delegado, arg);
        }

        private void btnConectaPuerto_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnConectaPuerto.Tag.ToString() == "OFF")
                {
                    try
                    {
                        if (cmbPuerto.Text.Trim().Equals("")) {
                            MessageBox.Show("No hay puertos disponibles. Asegúres de conectar la balanza.");
                            return;
                        }
                        
                        spPuerto.BaudRate = Global.configPort.baudrate;//2400; // 9600;
                        spPuerto.DataBits = Global.configPort.databits;// 8;
                        spPuerto.Parity = (Parity)Enum.ToObject(typeof(Parity), Global.configPort.parity); //Parity.None;
                        spPuerto.StopBits = (StopBits)Enum.ToObject(typeof(StopBits), Global.configPort.stopbit); //StopBits.One; 
                        spPuerto.Handshake = (Handshake)Enum.ToObject(typeof(Handshake), Global.configPort.handshake); //Handshake.None;
                        spPuerto.PortName = cmbPuerto.Text;

                        spPuerto.Open();
                        btnConectaPuerto.Tag = "ON";
                        cmbPuerto.Enabled = false;
                        string imagePath = Path.Combine(Application.StartupPath, @"images\on.png");
                        btnConectaPuerto.Image = Image.FromFile(imagePath);
                        ckbModoBalanza.Checked = true;
                        txtCantidad.Enabled = false;
                        btnConfigPort.Enabled = false;
                        txtCantidad.Focus();
                    }
                    catch (Exception ex) { MessageBox.Show("1.- " + ex.Message); }
                }
                else
                {
                    spPuerto.Close();
                    cmbPuerto.Enabled = true;
                    btnConectaPuerto.Tag = "OFF";
                    string imagePath = Path.Combine(Application.StartupPath, @"images\off.png");
                    btnConectaPuerto.Image = Image.FromFile(imagePath);
                    ckbModoBalanza.Checked = false;
                    txtCantidad.Enabled = true;
                    btnConfigPort.Enabled = true;
                    txtCantidad.Focus();
                }
                txtCantidad.Focus();
            }catch(Exception ex) { MessageBox.Show("2.- " + ex.Message); }
            txtCantidad.Focus();
        }

        private void txtCantidad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ttMessages.SetToolTip(lblConversion, "");
                ttMessages.ToolTipTitle = "Info:";
                ttMessages.ToolTipIcon = ToolTipIcon.Info;
                lblConversion.ForeColor = Color.DarkGreen;
                if (ckbModoBalanza.Checked && !spPuerto.IsOpen)
                {
                    txtCantidad.Text = "";
                }
                else
                {
                    lblConversion.Text = "";
                    if (cmbUnidad.SelectedIndex == 0)//PARA EL CASO DE QUE SEA KG
                        lblConversion.Text = (Double.Parse(txtCantidad.Text.Trim()) * _valorKG).ToString("0.00") + " lb";
                    else if (cmbUnidad.SelectedIndex == 1)
                        lblConversion.Text = (Double.Parse(txtCantidad.Text.Trim()) / _valorKG).ToString("0.00") + " kg";
                }
            }
            catch (Exception ex) {
                lblConversion.ForeColor = Color.Red;
                lblConversion.Text = "¡Error!";
                ttMessages.ToolTipTitle = "Error!";
                ttMessages.SetToolTip(lblConversion, "Ingrese un valor válido!");
                ttMessages.ToolTipIcon = ToolTipIcon.Error;
            }
        }

        private void ckbModoBalanza_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ckImpresion_CheckedChanged(object sender, EventArgs e)
        {
            cmbPrinters.Enabled = ckImpresion.Checked;
        }

        private void dgvDetalle_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //dgvDetalle.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
        }

        private void dgvDetalle_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgvDetalle.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void cmbPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string linea = "";
                var file = new StreamReader(Application.StartupPath + @"\config.txt");
                if (file != null)
                {
                    linea = file.ReadToEnd();
                    var objConfig = JsonConvert.DeserializeObject<JObject>(linea);
                    file.Close();
                    if (objConfig != null)
                    {
                        Global.name_printer = cmbPrinters.Text;
                        objConfig.Property("name_printer").Value = Global.name_printer;
                        var f = new StreamWriter(Application.StartupPath + @"\config.txt");
                        f.Write(objConfig.ToString()); 
                        f.Flush();
                        f.Close();
                    }
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnConfigPort_Click(object sender, EventArgs e)
        {
            btnConectaPuerto.Enabled = false;
            new View.FrmConfigPort().ShowDialog();
            btnConectaPuerto.Enabled = true;
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            BuscarProducto();
        }
    }
}
