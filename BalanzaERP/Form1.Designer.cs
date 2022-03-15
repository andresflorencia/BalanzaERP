
namespace BalanzaERP
{
    partial class FrmPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipal));
            this.grbInfoLote = new System.Windows.Forms.GroupBox();
            this.lblContMarqueta = new System.Windows.Forms.Label();
            this.cmbMarmita = new System.Windows.Forms.ComboBox();
            this.txtLote = new System.Windows.Forms.TextBox();
            this.dtpFechaPesado = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaVencimiento = new System.Windows.Forms.DateTimePicker();
            this.dtpFechaLote = new System.Windows.Forms.DateTimePicker();
            this.cmbPlantaProduccion = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblConversion = new System.Windows.Forms.Label();
            this.cmbUnidad = new System.Windows.Forms.ComboBox();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblProducto = new System.Windows.Forms.Label();
            this.txtBuscarProducto = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.ckImpresion = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbPuerto = new System.Windows.Forms.ComboBox();
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.colProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMarqueta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colImprimir = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colEliminar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lblTotalLote = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtObservacion = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.menuOpciones = new System.Windows.Forms.MenuStrip();
            this.lblMessage = new System.Windows.Forms.Label();
            this.spPuerto = new System.IO.Ports.SerialPort(this.components);
            this.btnConectaPuerto = new System.Windows.Forms.Button();
            this.ckbModoBalanza = new System.Windows.Forms.CheckBox();
            this.grbOpciones = new System.Windows.Forms.GroupBox();
            this.cmbPrinters = new System.Windows.Forms.ComboBox();
            this.grbProducto = new System.Windows.Forms.GroupBox();
            this.btnConfigPort = new System.Windows.Forms.Button();
            this.btnBuscarProducto = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.lblSucursal = new System.Windows.Forms.Label();
            this.tlOpciones = new System.Windows.Forms.ToolStripMenuItem();
            this.tsBuscar = new System.Windows.Forms.ToolStripMenuItem();
            this.tlListado = new System.Windows.Forms.ToolStripMenuItem();
            this.tlUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCerrarSesion = new System.Windows.Forms.ToolStripMenuItem();
            this.ttMessages = new System.Windows.Forms.ToolTip(this.components);
            this.grbInfoLote.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).BeginInit();
            this.menuOpciones.SuspendLayout();
            this.grbOpciones.SuspendLayout();
            this.grbProducto.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbInfoLote
            // 
            this.grbInfoLote.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.grbInfoLote.BackColor = System.Drawing.SystemColors.Control;
            this.grbInfoLote.Controls.Add(this.lblContMarqueta);
            this.grbInfoLote.Controls.Add(this.cmbMarmita);
            this.grbInfoLote.Controls.Add(this.txtLote);
            this.grbInfoLote.Controls.Add(this.dtpFechaPesado);
            this.grbInfoLote.Controls.Add(this.dtpFechaVencimiento);
            this.grbInfoLote.Controls.Add(this.dtpFechaLote);
            this.grbInfoLote.Controls.Add(this.cmbPlantaProduccion);
            this.grbInfoLote.Controls.Add(this.label12);
            this.grbInfoLote.Controls.Add(this.label5);
            this.grbInfoLote.Controls.Add(this.label7);
            this.grbInfoLote.Controls.Add(this.label4);
            this.grbInfoLote.Controls.Add(this.label3);
            this.grbInfoLote.Controls.Add(this.label11);
            this.grbInfoLote.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbInfoLote.Location = new System.Drawing.Point(58, 61);
            this.grbInfoLote.Name = "grbInfoLote";
            this.grbInfoLote.Size = new System.Drawing.Size(526, 147);
            this.grbInfoLote.TabIndex = 0;
            this.grbInfoLote.TabStop = false;
            this.grbInfoLote.Text = "Información Lote";
            // 
            // lblContMarqueta
            // 
            this.lblContMarqueta.AutoSize = true;
            this.lblContMarqueta.Font = new System.Drawing.Font("Arial Rounded MT Bold", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContMarqueta.Location = new System.Drawing.Point(379, 120);
            this.lblContMarqueta.Name = "lblContMarqueta";
            this.lblContMarqueta.Size = new System.Drawing.Size(0, 17);
            this.lblContMarqueta.TabIndex = 15;
            // 
            // cmbMarmita
            // 
            this.cmbMarmita.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMarmita.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMarmita.FormattingEnabled = true;
            this.cmbMarmita.Location = new System.Drawing.Point(371, 91);
            this.cmbMarmita.Name = "cmbMarmita";
            this.cmbMarmita.Size = new System.Drawing.Size(137, 24);
            this.cmbMarmita.TabIndex = 10;
            this.ttMessages.SetToolTip(this.cmbMarmita, "Marmita de producción");
            // 
            // txtLote
            // 
            this.txtLote.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLote.Location = new System.Drawing.Point(316, 44);
            this.txtLote.Name = "txtLote";
            this.txtLote.ReadOnly = true;
            this.txtLote.Size = new System.Drawing.Size(192, 23);
            this.txtLote.TabIndex = 7;
            this.txtLote.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dtpFechaPesado
            // 
            this.dtpFechaPesado.Enabled = false;
            this.dtpFechaPesado.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaPesado.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaPesado.Location = new System.Drawing.Point(128, 101);
            this.dtpFechaPesado.Name = "dtpFechaPesado";
            this.dtpFechaPesado.Size = new System.Drawing.Size(132, 23);
            this.dtpFechaPesado.TabIndex = 6;
            this.ttMessages.SetToolTip(this.dtpFechaPesado, "Fecha en la que se realiza el pesado de la producción");
            // 
            // dtpFechaVencimiento
            // 
            this.dtpFechaVencimiento.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaVencimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaVencimiento.Location = new System.Drawing.Point(128, 73);
            this.dtpFechaVencimiento.Name = "dtpFechaVencimiento";
            this.dtpFechaVencimiento.Size = new System.Drawing.Size(132, 23);
            this.dtpFechaVencimiento.TabIndex = 6;
            this.ttMessages.SetToolTip(this.dtpFechaVencimiento, "Fecha de vencimiento de la producción pesada");
            // 
            // dtpFechaLote
            // 
            this.dtpFechaLote.Enabled = false;
            this.dtpFechaLote.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaLote.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaLote.Location = new System.Drawing.Point(128, 44);
            this.dtpFechaLote.Name = "dtpFechaLote";
            this.dtpFechaLote.Size = new System.Drawing.Size(131, 23);
            this.dtpFechaLote.TabIndex = 6;
            this.ttMessages.SetToolTip(this.dtpFechaLote, "Fecha en la que se produjo el producto.");
            this.dtpFechaLote.ValueChanged += new System.EventHandler(this.dtpFechaLote_ValueChanged);
            // 
            // cmbPlantaProduccion
            // 
            this.cmbPlantaProduccion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlantaProduccion.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPlantaProduccion.FormattingEnabled = true;
            this.cmbPlantaProduccion.Location = new System.Drawing.Point(128, 15);
            this.cmbPlantaProduccion.Name = "cmbPlantaProduccion";
            this.cmbPlantaProduccion.Size = new System.Drawing.Size(380, 23);
            this.cmbPlantaProduccion.TabIndex = 5;
            this.cmbPlantaProduccion.SelectionChangeCommitted += new System.EventHandler(this.cmbPlantaProduccion_SelectionChangeCommitted);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(39, 106);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 12);
            this.label12.TabIndex = 4;
            this.label12.Text = "Fecha Pesado:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(276, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 15);
            this.label5.TabIndex = 4;
            this.label5.Text = "Lote:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 12);
            this.label7.TabIndex = 4;
            this.label7.Text = "Fecha Vencimiento:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "Fecha producción:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Planta Producción:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Maroon;
            this.label11.Location = new System.Drawing.Point(372, 77);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(135, 14);
            this.label11.TabIndex = 11;
            this.label11.Text = "Especifique la marmita:";
            // 
            // lblConversion
            // 
            this.lblConversion.AutoSize = true;
            this.lblConversion.Font = new System.Drawing.Font("Arial Rounded MT Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConversion.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblConversion.Location = new System.Drawing.Point(259, 114);
            this.lblConversion.Name = "lblConversion";
            this.lblConversion.Size = new System.Drawing.Size(0, 28);
            this.lblConversion.TabIndex = 12;
            // 
            // cmbUnidad
            // 
            this.cmbUnidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUnidad.FormattingEnabled = true;
            this.cmbUnidad.Items.AddRange(new object[] {
            "Kilos (kg)",
            "Libras (lb)"});
            this.cmbUnidad.Location = new System.Drawing.Point(253, 84);
            this.cmbUnidad.Name = "cmbUnidad";
            this.cmbUnidad.Size = new System.Drawing.Size(117, 24);
            this.cmbUnidad.TabIndex = 10;
            this.ttMessages.SetToolTip(this.cmbUnidad, "Unidad de pesado");
            // 
            // txtCantidad
            // 
            this.txtCantidad.Enabled = false;
            this.txtCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantidad.Location = new System.Drawing.Point(117, 84);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(126, 40);
            this.txtCantidad.TabIndex = 9;
            this.txtCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtCantidad.TextChanged += new System.EventHandler(this.txtCantidad_TextChanged);
            this.txtCantidad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCantidad_KeyDown);
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidad_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(48, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 15);
            this.label6.TabIndex = 8;
            this.label6.Text = "Cantidad:";
            // 
            // lblProducto
            // 
            this.lblProducto.AutoSize = true;
            this.lblProducto.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProducto.ForeColor = System.Drawing.Color.MidnightBlue;
            this.lblProducto.Location = new System.Drawing.Point(86, 50);
            this.lblProducto.Name = "lblProducto";
            this.lblProducto.Size = new System.Drawing.Size(0, 24);
            this.lblProducto.TabIndex = 3;
            // 
            // txtBuscarProducto
            // 
            this.txtBuscarProducto.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarProducto.Location = new System.Drawing.Point(117, 17);
            this.txtBuscarProducto.Name = "txtBuscarProducto";
            this.txtBuscarProducto.Size = new System.Drawing.Size(227, 26);
            this.txtBuscarProducto.TabIndex = 2;
            this.txtBuscarProducto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBuscarProducto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscarProducto_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Buscar producto:";
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label10.Location = new System.Drawing.Point(86, 127);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(160, 16);
            this.label10.TabIndex = 11;
            this.label10.Text = "Presione F7 para confirmar";
            // 
            // ckImpresion
            // 
            this.ckImpresion.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ckImpresion.AutoSize = true;
            this.ckImpresion.Location = new System.Drawing.Point(28, 88);
            this.ckImpresion.Margin = new System.Windows.Forms.Padding(2);
            this.ckImpresion.Name = "ckImpresion";
            this.ckImpresion.Size = new System.Drawing.Size(134, 17);
            this.ckImpresion.TabIndex = 13;
            this.ckImpresion.Text = "Impresión de ticket";
            this.ttMessages.SetToolTip(this.ckImpresion, "Cuando está marcado imprime el ticket de la marqueta");
            this.ckImpresion.UseVisualStyleBackColor = true;
            this.ckImpresion.CheckedChanged += new System.EventHandler(this.ckImpresion_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(999, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Puerto:";
            // 
            // cmbPuerto
            // 
            this.cmbPuerto.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cmbPuerto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPuerto.FormattingEnabled = true;
            this.cmbPuerto.Location = new System.Drawing.Point(1045, 35);
            this.cmbPuerto.Name = "cmbPuerto";
            this.cmbPuerto.Size = new System.Drawing.Size(70, 21);
            this.cmbPuerto.TabIndex = 3;
            this.cmbPuerto.SelectionChangeCommitted += new System.EventHandler(this.cmbPuerto_SelectionChangeCommitted);
            // 
            // dgvDetalle
            // 
            this.dgvDetalle.AllowUserToAddRows = false;
            this.dgvDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProducto,
            this.colLote,
            this.colMarqueta,
            this.colCantidad,
            this.colImprimir,
            this.colEliminar});
            this.dgvDetalle.Location = new System.Drawing.Point(47, 214);
            this.dgvDetalle.Name = "dgvDetalle";
            this.dgvDetalle.Size = new System.Drawing.Size(1158, 163);
            this.dgvDetalle.TabIndex = 4;
            this.dgvDetalle.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalle_CellClick);
            this.dgvDetalle.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalle_CellValueChanged);
            this.dgvDetalle.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalle_RowEnter);
            this.dgvDetalle.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvDetalle_RowPostPaint);
            // 
            // colProducto
            // 
            this.colProducto.HeaderText = "Producto";
            this.colProducto.Name = "colProducto";
            this.colProducto.ReadOnly = true;
            this.colProducto.Width = 300;
            // 
            // colLote
            // 
            this.colLote.HeaderText = "Lote";
            this.colLote.Name = "colLote";
            this.colLote.ReadOnly = true;
            this.colLote.Width = 150;
            // 
            // colMarqueta
            // 
            this.colMarqueta.HeaderText = "Cód. Marqueta";
            this.colMarqueta.Name = "colMarqueta";
            this.colMarqueta.ReadOnly = true;
            this.colMarqueta.Width = 220;
            // 
            // colCantidad
            // 
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = "0";
            this.colCantidad.DefaultCellStyle = dataGridViewCellStyle1;
            this.colCantidad.HeaderText = "Peso (lb)";
            this.colCantidad.Name = "colCantidad";
            // 
            // colImprimir
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.colImprimir.DefaultCellStyle = dataGridViewCellStyle2;
            this.colImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.colImprimir.HeaderText = "Imprimir";
            this.colImprimir.Name = "colImprimir";
            this.colImprimir.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colImprimir.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colEliminar
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.AntiqueWhite;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.NavajoWhite;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.colEliminar.DefaultCellStyle = dataGridViewCellStyle3;
            this.colEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.colEliminar.HeaderText = "Eliminar";
            this.colEliminar.Name = "colEliminar";
            this.colEliminar.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // lblTotalLote
            // 
            this.lblTotalLote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalLote.AutoSize = true;
            this.lblTotalLote.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalLote.Location = new System.Drawing.Point(599, 387);
            this.lblTotalLote.Name = "lblTotalLote";
            this.lblTotalLote.Size = new System.Drawing.Size(142, 22);
            this.lblTotalLote.TabIndex = 9;
            this.lblTotalLote.Text = "Total (lb): 0.00";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(69, 377);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 12);
            this.label8.TabIndex = 1;
            this.label8.Text = "Observación:";
            // 
            // txtObservacion
            // 
            this.txtObservacion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtObservacion.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObservacion.Location = new System.Drawing.Point(66, 392);
            this.txtObservacion.Multiline = true;
            this.txtObservacion.Name = "txtObservacion";
            this.txtObservacion.Size = new System.Drawing.Size(503, 40);
            this.txtObservacion.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(10, 440);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(367, 14);
            this.label9.TabIndex = 4;
            this.label9.Text = "F2 = Guardar;   F3 = Nuevo; F7 = Ingresa cantidad;   Esc = Salir";
            this.ttMessages.SetToolTip(this.label9, "Teclas de acceso rápido a las funciones del sistema");
            // 
            // menuOpciones
            // 
            this.menuOpciones.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.menuOpciones.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuOpciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlOpciones,
            this.tsCerrarSesion});
            this.menuOpciones.Location = new System.Drawing.Point(0, 0);
            this.menuOpciones.Name = "menuOpciones";
            this.menuOpciones.Size = new System.Drawing.Size(1254, 24);
            this.menuOpciones.TabIndex = 10;
            this.menuOpciones.Text = "Opciones";
            // 
            // lblMessage
            // 
            this.lblMessage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblMessage.Location = new System.Drawing.Point(564, 36);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(106, 15);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "Nuevo Registro";
            // 
            // spPuerto
            // 
            this.spPuerto.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.DatoRecibido);
            // 
            // btnConectaPuerto
            // 
            this.btnConectaPuerto.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnConectaPuerto.Location = new System.Drawing.Point(1150, 34);
            this.btnConectaPuerto.Name = "btnConectaPuerto";
            this.btnConectaPuerto.Size = new System.Drawing.Size(41, 23);
            this.btnConectaPuerto.TabIndex = 11;
            this.btnConectaPuerto.Tag = "OFF";
            this.ttMessages.SetToolTip(this.btnConectaPuerto, "Conectar/Desconectar balanza");
            this.btnConectaPuerto.UseVisualStyleBackColor = true;
            this.btnConectaPuerto.Click += new System.EventHandler(this.btnConectaPuerto_Click);
            // 
            // ckbModoBalanza
            // 
            this.ckbModoBalanza.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ckbModoBalanza.AutoSize = true;
            this.ckbModoBalanza.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ckbModoBalanza.Enabled = false;
            this.ckbModoBalanza.Font = new System.Drawing.Font("Arial Rounded MT Bold", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbModoBalanza.Location = new System.Drawing.Point(894, 37);
            this.ckbModoBalanza.Name = "ckbModoBalanza";
            this.ckbModoBalanza.Size = new System.Drawing.Size(102, 16);
            this.ckbModoBalanza.TabIndex = 12;
            this.ckbModoBalanza.Text = "Modo Balanza";
            this.ckbModoBalanza.UseVisualStyleBackColor = true;
            this.ckbModoBalanza.CheckedChanged += new System.EventHandler(this.ckbModoBalanza_CheckedChanged);
            // 
            // grbOpciones
            // 
            this.grbOpciones.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.grbOpciones.BackColor = System.Drawing.SystemColors.Control;
            this.grbOpciones.Controls.Add(this.cmbPrinters);
            this.grbOpciones.Controls.Add(this.btnLimpiar);
            this.grbOpciones.Controls.Add(this.ckImpresion);
            this.grbOpciones.Controls.Add(this.btnGuardar);
            this.grbOpciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbOpciones.Location = new System.Drawing.Point(999, 61);
            this.grbOpciones.Name = "grbOpciones";
            this.grbOpciones.Size = new System.Drawing.Size(195, 147);
            this.grbOpciones.TabIndex = 13;
            this.grbOpciones.TabStop = false;
            this.grbOpciones.Text = "Opciones";
            // 
            // cmbPrinters
            // 
            this.cmbPrinters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrinters.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPrinters.FormattingEnabled = true;
            this.cmbPrinters.Location = new System.Drawing.Point(11, 110);
            this.cmbPrinters.Name = "cmbPrinters";
            this.cmbPrinters.Size = new System.Drawing.Size(173, 24);
            this.cmbPrinters.TabIndex = 14;
            this.ttMessages.SetToolTip(this.cmbPrinters, "Impresora de ticket");
            this.cmbPrinters.SelectedIndexChanged += new System.EventHandler(this.cmbPrinters_SelectedIndexChanged);
            // 
            // grbProducto
            // 
            this.grbProducto.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.grbProducto.BackColor = System.Drawing.SystemColors.Control;
            this.grbProducto.Controls.Add(this.txtCantidad);
            this.grbProducto.Controls.Add(this.btnBuscarProducto);
            this.grbProducto.Controls.Add(this.label10);
            this.grbProducto.Controls.Add(this.lblConversion);
            this.grbProducto.Controls.Add(this.label1);
            this.grbProducto.Controls.Add(this.txtBuscarProducto);
            this.grbProducto.Controls.Add(this.cmbUnidad);
            this.grbProducto.Controls.Add(this.lblProducto);
            this.grbProducto.Controls.Add(this.label6);
            this.grbProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbProducto.Location = new System.Drawing.Point(601, 61);
            this.grbProducto.Name = "grbProducto";
            this.grbProducto.Size = new System.Drawing.Size(382, 147);
            this.grbProducto.TabIndex = 14;
            this.grbProducto.TabStop = false;
            this.grbProducto.Text = "Información Producto";
            // 
            // btnConfigPort
            // 
            this.btnConfigPort.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnConfigPort.Image = global::BalanzaERP.Properties.Resources.settings2;
            this.btnConfigPort.Location = new System.Drawing.Point(1119, 33);
            this.btnConfigPort.Name = "btnConfigPort";
            this.btnConfigPort.Size = new System.Drawing.Size(25, 25);
            this.btnConfigPort.TabIndex = 15;
            this.ttMessages.SetToolTip(this.btnConfigPort, "Configurar la conexión de la balanza");
            this.btnConfigPort.UseVisualStyleBackColor = true;
            this.btnConfigPort.Click += new System.EventHandler(this.btnConfigPort_Click);
            // 
            // btnBuscarProducto
            // 
            this.btnBuscarProducto.Image = global::BalanzaERP.Properties.Resources.search;
            this.btnBuscarProducto.Location = new System.Drawing.Point(346, 16);
            this.btnBuscarProducto.Margin = new System.Windows.Forms.Padding(2);
            this.btnBuscarProducto.Name = "btnBuscarProducto";
            this.btnBuscarProducto.Size = new System.Drawing.Size(26, 29);
            this.btnBuscarProducto.TabIndex = 14;
            this.ttMessages.SetToolTip(this.btnBuscarProducto, "Buscar producto");
            this.btnBuscarProducto.UseVisualStyleBackColor = true;
            this.btnBuscarProducto.Click += new System.EventHandler(this.btnBuscarProducto_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLimpiar.BackColor = System.Drawing.SystemColors.Control;
            this.btnLimpiar.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiar.ForeColor = System.Drawing.Color.Teal;
            this.btnLimpiar.Image = global::BalanzaERP.Properties.Resources.add_file;
            this.btnLimpiar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnLimpiar.Location = new System.Drawing.Point(12, 25);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(85, 40);
            this.btnLimpiar.TabIndex = 0;
            this.btnLimpiar.Text = "NUEVO";
            this.btnLimpiar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMessages.SetToolTip(this.btnLimpiar, "Limpiar los datos mostrados");
            this.btnLimpiar.UseVisualStyleBackColor = false;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.BackColor = System.Drawing.SystemColors.Control;
            this.btnGuardar.Enabled = false;
            this.btnGuardar.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnGuardar.Image = global::BalanzaERP.Properties.Resources.floppy_disk;
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnGuardar.Location = new System.Drawing.Point(103, 25);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(85, 40);
            this.btnGuardar.TabIndex = 0;
            this.btnGuardar.Text = "GUARDAR";
            this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ttMessages.SetToolTip(this.btnGuardar, "Guardar datos");
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSalir.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.ForeColor = System.Drawing.Color.Crimson;
            this.btnSalir.Image = global::BalanzaERP.Properties.Resources.logout;
            this.btnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.Location = new System.Drawing.Point(1324, 0);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(128, 26);
            this.btnSalir.TabIndex = 0;
            this.btnSalir.Text = "Cerrar Sesión";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Visible = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // lblSucursal
            // 
            this.lblSucursal.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblSucursal.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSucursal.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblSucursal.Image = global::BalanzaERP.Properties.Resources.profile_user;
            this.lblSucursal.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSucursal.Location = new System.Drawing.Point(103, 28);
            this.lblSucursal.Name = "lblSucursal";
            this.lblSucursal.Size = new System.Drawing.Size(436, 31);
            this.lblSucursal.TabIndex = 1;
            this.lblSucursal.Text = "    Usuario";
            this.lblSucursal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlOpciones
            // 
            this.tlOpciones.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBuscar,
            this.tlListado,
            this.tlUpload});
            this.tlOpciones.Image = global::BalanzaERP.Properties.Resources.menu;
            this.tlOpciones.Name = "tlOpciones";
            this.tlOpciones.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.O)));
            this.tlOpciones.Size = new System.Drawing.Size(85, 20);
            this.tlOpciones.Text = "Opciones";
            // 
            // tsBuscar
            // 
            this.tsBuscar.Image = global::BalanzaERP.Properties.Resources.search;
            this.tsBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsBuscar.Name = "tsBuscar";
            this.tsBuscar.ShortcutKeyDisplayString = "Ctrl+F";
            this.tsBuscar.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.tsBuscar.Size = new System.Drawing.Size(216, 22);
            this.tsBuscar.Text = "Buscar Registro";
            this.tsBuscar.Click += new System.EventHandler(this.tsBuscar_Click);
            // 
            // tlListado
            // 
            this.tlListado.Image = global::BalanzaERP.Properties.Resources.portapapeles;
            this.tlListado.Name = "tlListado";
            this.tlListado.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
            this.tlListado.Size = new System.Drawing.Size(216, 22);
            this.tlListado.Text = "Listado de registros";
            this.tlListado.Click += new System.EventHandler(this.tlListado_Click);
            // 
            // tlUpload
            // 
            this.tlUpload.Image = global::BalanzaERP.Properties.Resources.cloud_computing;
            this.tlUpload.Name = "tlUpload";
            this.tlUpload.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.tlUpload.Size = new System.Drawing.Size(216, 22);
            this.tlUpload.Text = "Subir información";
            this.tlUpload.Click += new System.EventHandler(this.tlUpload_Click);
            // 
            // tsCerrarSesion
            // 
            this.tsCerrarSesion.Image = global::BalanzaERP.Properties.Resources.logout;
            this.tsCerrarSesion.Name = "tsCerrarSesion";
            this.tsCerrarSesion.Size = new System.Drawing.Size(104, 20);
            this.tsCerrarSesion.Text = "Cerrar Sesión";
            this.tsCerrarSesion.Click += new System.EventHandler(this.tsCerrarSesion_Click_1);
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1254, 457);
            this.Controls.Add(this.btnConfigPort);
            this.Controls.Add(this.grbProducto);
            this.Controls.Add(this.grbOpciones);
            this.Controls.Add(this.ckbModoBalanza);
            this.Controls.Add(this.btnConectaPuerto);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtObservacion);
            this.Controls.Add(this.lblTotalLote);
            this.Controls.Add(this.dgvDetalle);
            this.Controls.Add(this.cmbPuerto);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblSucursal);
            this.Controls.Add(this.grbInfoLote);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.menuOpciones);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuOpciones;
            this.Name = "FrmPrincipal";
            this.Text = "Principal";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmPrincipal_FormClosed);
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmPrincipal_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmPrincipal_KeyPress);
            this.grbInfoLote.ResumeLayout(false);
            this.grbInfoLote.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalle)).EndInit();
            this.menuOpciones.ResumeLayout(false);
            this.menuOpciones.PerformLayout();
            this.grbOpciones.ResumeLayout(false);
            this.grbOpciones.PerformLayout();
            this.grbProducto.ResumeLayout(false);
            this.grbProducto.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grbInfoLote;
        private System.Windows.Forms.TextBox txtLote;
        private System.Windows.Forms.DateTimePicker dtpFechaLote;
        private System.Windows.Forms.ComboBox cmbPlantaProduccion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.TextBox txtBuscarProducto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.Label lblSucursal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbPuerto;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTotalLote;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.DateTimePicker dtpFechaVencimiento;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtObservacion;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.MenuStrip menuOpciones;
        private System.Windows.Forms.ToolStripMenuItem tlOpciones;
        private System.Windows.Forms.ToolStripMenuItem tsBuscar;
        private System.Windows.Forms.ToolStripMenuItem tlUpload;
        private System.Windows.Forms.ToolStripMenuItem tlListado;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.ToolStripMenuItem tsCerrarSesion;
        private System.IO.Ports.SerialPort spPuerto;
        private System.Windows.Forms.Button btnConectaPuerto;
        private System.Windows.Forms.CheckBox ckbModoBalanza;
        private System.Windows.Forms.ComboBox cmbUnidad;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblConversion;
        private System.Windows.Forms.ComboBox cmbMarmita;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox ckImpresion;
        private System.Windows.Forms.Button btnBuscarProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLote;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMarqueta;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCantidad;
        private System.Windows.Forms.DataGridViewButtonColumn colImprimir;
        private System.Windows.Forms.DataGridViewButtonColumn colEliminar;
        private System.Windows.Forms.DateTimePicker dtpFechaPesado;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox grbOpciones;
        private System.Windows.Forms.Label lblContMarqueta;
        private System.Windows.Forms.ComboBox cmbPrinters;
        private System.Windows.Forms.GroupBox grbProducto;
        private System.Windows.Forms.Button btnConfigPort;
        private System.Windows.Forms.ToolTip ttMessages;
    }
}

