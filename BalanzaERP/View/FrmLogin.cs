using BalanzaERP.Controller;
using BalanzaERP.Model;
using BalanzaERP.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace BalanzaERP.View
{
    public partial class FrmLogin : Form
    {
        UsuarioController userController;
        EstablecimientoController establecimientoController;
        ProductoController productoController;
        FrmLoader frmLoader;
        public FrmLogin()
        {
            InitializeComponent();
            userController = new UsuarioController();
            establecimientoController = new EstablecimientoController();
            productoController = new ProductoController();
            LeerConfig();

            if (!VerifyInstance())
                this.Close();
        }

        private bool VerifyInstance()
        {
            bool appNewInstance;

            Mutex m = new Mutex(true, "ApplicationName", out appNewInstance);

            if (!appNewInstance)
            {
                MessageBox.Show("El programa ya se encuentra abierto.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                return false;
            }
            GC.KeepAlive(m);
            return true;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        public void LeerConfig()
        {
            string linea = "";
            var file = new StreamReader(Application.StartupPath+@"\config.txt");
            if (file != null)
            {
                linea = file.ReadToEnd();
                var objConfig =  JsonConvert.DeserializeObject<JObject>(linea);
                if (objConfig != null)
                {
                    var objCnn = objConfig.GetValue("connection_db").ToObject<JObject>();
                    Global._connectionString = "Server=" + objCnn.GetValue("Server")
                                                + ";Port=" + objCnn.GetValue("Port")
                                                + ";User Id=" + objCnn.GetValue("User")
                                                + ";Password=" + objCnn.GetValue("Password")
                                                + ";Database=" + objCnn.GetValue("Database") + ";";
                    Global.URL = objConfig.GetValue("webservice").ToString();
                    Global.separador_decimal = objConfig.ContainsKey("separador_decimal") ? objConfig.GetValue("separador_decimal").ToString() : ".";
                    Global.name_printer = objConfig.ContainsKey("name_printer") ? objConfig.GetValue("name_printer").ToString() : "";
                    Global.translate_data = objConfig.ContainsKey("translate_data") ? objConfig.GetValue("translate_data").ToString() : "no";

                    Global.configPort = objConfig.ContainsKey("config_port") ? objConfig.GetValue("config_port").ToObject<ConfigPort>() : null;
                    if (Global.configPort == null)
                        Global.configPort = new ConfigPort();
                }
                file.Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            ShowLoader();
            ThreadSafe(() =>
            {
                ProcesoLogin();
            }, this);
            /*ShowLoader();
            Task oTask = new Task(delegate () {
                if (lblMessage.InvokeRequired)
                {
                    lblMessage.Invoke(new MethodInvoker(delegate
                    {
                        lblMessage.Text = "";
                    }));
                }
                ProcesoLogin();
            });
            oTask.Start();
            HideLoader();*/
        }

        private void ShowLoader() {
            frmLoader = new FrmLoader();
            frmLoader.MdiParent = this.MdiParent;
            frmLoader.Width = this.Width;
            frmLoader.Height = this.Height;
            frmLoader.Show();
        }
        private void HideLoader() {
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

        private void ProcesoLogin() {
            if (txtUsuario.Text.Trim().Length == 0 || txtClave.Text.Trim().Length == 0)
            {
                lblMessage.Text = "Complete los campos.";
                HideLoader();
                return;
            }

            if (!LoginWS())
            {
                var user = userController.Login(txtUsuario.Text, txtClave.Text);
                if (user != null)
                {
                    Global.usuario = user;
                    txtClave.Text = "";
                    HideLoader();
                    Program.frmLogin.Hide();
                    FrmPrincipal frmPrincipal = new FrmPrincipal();
                    frmPrincipal.Show();
                }
                else
                {
                    lblMessage.Text = "Usuario o contraseña incorrecto.";
                    HideLoader();
                    //LoginWS();
                }
            }
        }

        private bool LoginWS() {
            bool retorno = false;
            try
            {
                var respuesta = userController.LoginWS(txtUsuario.Text, txtClave.Text);
                if (respuesta.haserror || respuesta.data == null)
                {
                    lblMessage.Text = respuesta.message;
                    HideLoader();
                }
                else
                {
                    Global.usuario = (Usuario)respuesta.data;
                    if (userController.Save(Global.usuario) > 0 && establecimientoController.Save(Global.usuario.establecimiento) > 0)
                    {
                        var resProd = productoController.DownloadProducts(Global.usuario.usuario, Global.usuario.clave);

                        if (!resProd.haserror && resProd.data != null)
                        {
                            resProd = productoController.SaveList((List<Producto>)resProd.data);
                            if (!resProd.haserror)
                            {
                                retorno = true;
                                txtClave.Text = "";
                                HideLoader();
                                Program.frmLogin.Hide();
                                FrmPrincipal frmPrincipal = new FrmPrincipal();
                                frmPrincipal.Show();
                            }
                            else
                            {
                                lblMessage.Text = resProd.message;
                                HideLoader();
                            }
                        }
                        else
                        {
                            lblMessage.Text = resProd.message;
                            HideLoader();
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Ocurrió un error al intentar guardar los datos.";
                        HideLoader();
                    }
                }
            }
            catch (Exception e)
            {
                HideLoader();
                MessageBox.Show(e.Message);
            }
            return retorno;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtClave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnLogin_Click(btnLogin, null);
        }

        private void FrmLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void FrmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                btnCancelar_Click(btnCancelar, null);
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            lblMessage.Text = "";
        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Control c;
                c = ((TextBox)sender).Parent;
                c.SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            string imagePath = Path.Combine(Application.StartupPath, @"images\visible.png");
            btnShowPassword.Image = Image.FromFile(imagePath);
            btnShowPassword.Tag = "I";
        }

        private void FrmLogin_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnShowPassword_Click(object sender, EventArgs e)
        {
            string imagePath = Path.Combine(Application.StartupPath, @"images\invisible.png");
            if (btnShowPassword.Tag.ToString().Equals("V"))
            {
                imagePath = Path.Combine(Application.StartupPath, @"images\visible.png");
                btnShowPassword.Tag = "I";
                txtClave.PasswordChar = '*';
            }
            else
            {
                btnShowPassword.Tag = "V";
                txtClave.PasswordChar = '\0';
            }
            btnShowPassword.Image = Image.FromFile(imagePath);
            txtClave.Focus();
        }
    }
}
