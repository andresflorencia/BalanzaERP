using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BalanzaERP.Utils;

namespace BalanzaERP.View
{
    public partial class FrmConfigPort : Form
    {
        public FrmConfigPort()
        {
            InitializeComponent();
        }

        private void FrmConfigPort_Load(object sender, EventArgs e)
        {
            cbParity.DataSource = Enum.GetValues(typeof(Parity));
            cbStopBits.DataSource = Enum.GetValues(typeof(StopBits));
            cbHandshake.DataSource = Enum.GetValues(typeof(Handshake));

            cbParity.SelectedItem = (Parity)Enum.ToObject(typeof(Parity), Global.configPort.parity);
            cbStopBits.SelectedItem = (StopBits)Enum.ToObject(typeof(StopBits), Global.configPort.stopbit);
            cbHandshake.SelectedItem = (Handshake)Enum.ToObject(typeof(Handshake), Global.configPort.handshake);

            cbBaudRate.SelectedItem = Global.configPort.baudrate.ToString();
            cbDataBits.SelectedItem = Global.configPort.databits.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Global.configPort.baudrate = Int32.Parse(cbBaudRate.SelectedItem.ToString());
                Global.configPort.databits = Int32.Parse(cbDataBits.SelectedItem.ToString());
                Global.configPort.parity = (int)cbParity.SelectedItem;
                Global.configPort.stopbit = (int)cbStopBits.SelectedItem;
                Global.configPort.handshake = (int)cbHandshake.SelectedItem;
                string linea = "";
                var file = new StreamReader(Application.StartupPath + @"\config.txt");
                if (file != null)
                {
                    linea = file.ReadToEnd();
                    var objConfig = JsonConvert.DeserializeObject<JObject>(linea);
                    file.Close();
                    if (objConfig != null)
                    {
                        JToken token = JToken.FromObject(Global.configPort);
                        if (!objConfig.ContainsKey("config_port"))
                            objConfig.Add("config_port", token);
                        else
                            objConfig.Property("config_port").Value = token;
                        var f = new StreamWriter(Application.StartupPath + @"\config.txt");
                        f.Write(objConfig.ToString());
                        f.Flush();
                        f.Close();
                    }
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FrmConfigPort_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
