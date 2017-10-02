namespace moleQule.Face.Hipatia
{
    partial class AgenteForm
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
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
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgenteForm));
			this.Identificador_GB = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.Fecha_DTP = new System.Windows.Forms.DateTimePicker();
			this.Nombre_TB = new System.Windows.Forms.TextBox();
			this.Codigo_TB = new System.Windows.Forms.TextBox();
			this.Documentos_GB = new System.Windows.Forms.GroupBox();
			this.PanelDocs = new System.Windows.Forms.SplitContainer();
			this.Tools_TS = new System.Windows.Forms.ToolStrip();
			this.New_TI = new System.Windows.Forms.ToolStripButton();
			this.Attach_TI = new System.Windows.Forms.ToolStripButton();
			this.View_TI = new System.Windows.Forms.ToolStripButton();
			this.Edit_TI = new System.Windows.Forms.ToolStripButton();
			this.Borrar_TI = new System.Windows.Forms.ToolStripButton();
			this.Replace_TI = new System.Windows.Forms.ToolStripButton();
			this.Download_TI = new System.Windows.Forms.ToolStripButton();
			this.Email_TI = new System.Windows.Forms.ToolStripButton();
			this.Tabla = new System.Windows.Forms.DataGridView();
			this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.FechaAlta = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Shared = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.Observaciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Datos_Documentos = new System.Windows.Forms.BindingSource(this.components);
			this.Observaciones_TB = new System.Windows.Forms.RichTextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.Browser = new System.Windows.Forms.FolderBrowserDialog();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.PanelesV)).BeginInit();
			this.PanelesV.Panel1.SuspendLayout();
			this.PanelesV.Panel2.SuspendLayout();
			this.PanelesV.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Paneles2)).BeginInit();
			this.Paneles2.Panel1.SuspendLayout();
			this.Paneles2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.Progress_Panel.SuspendLayout();
			this.ProgressBK_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
			this.Identificador_GB.SuspendLayout();
			this.Documentos_GB.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PanelDocs)).BeginInit();
			this.PanelDocs.Panel1.SuspendLayout();
			this.PanelDocs.Panel2.SuspendLayout();
			this.PanelDocs.SuspendLayout();
			this.Tools_TS.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Tabla)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Documentos)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// PanelesV
			// 
			// 
			// PanelesV.Panel1
			// 
			this.PanelesV.Panel1.AutoScroll = true;
			this.PanelesV.Panel1.Controls.Add(this.groupBox1);
			this.PanelesV.Panel1.Controls.Add(this.Documentos_GB);
			this.PanelesV.Panel1.Controls.Add(this.Identificador_GB);
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
			// 
			// PanelesV.Panel2
			// 
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
			this.HelpProvider.SetShowHelp(this.PanelesV, true);
			this.PanelesV.Size = new System.Drawing.Size(1194, 672);
			this.PanelesV.SplitterDistance = 617;
			// 
			// Submit_BT
			// 
			this.Submit_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Submit_BT.Location = new System.Drawing.Point(490, 7);
			this.HelpProvider.SetShowHelp(this.Submit_BT, true);
			// 
			// Cancel_BT
			// 
			this.Cancel_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Cancel_BT.Location = new System.Drawing.Point(579, 7);
			this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
			// 
			// Paneles2
			// 
			this.ErrorMng_EP.SetError(this.Paneles2, "F1 Ayuda        ");
			// 
			// Paneles2.Panel1
			// 
			this.HelpProvider.SetShowHelp(this.Paneles2.Panel1, true);
			// 
			// Paneles2.Panel2
			// 
			this.HelpProvider.SetShowHelp(this.Paneles2.Panel2, true);
			this.HelpProvider.SetShowHelp(this.Paneles2, true);
			this.Paneles2.Size = new System.Drawing.Size(1192, 52);
			this.Paneles2.SplitterDistance = 27;
			// 
			// Imprimir_Button
			// 
			this.Imprimir_Button.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Imprimir_Button.Location = new System.Drawing.Point(668, 7);
			this.HelpProvider.SetShowHelp(this.Imprimir_Button, true);
			// 
			// Docs_BT
			// 
			this.Docs_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Docs_BT.Location = new System.Drawing.Point(282, 9);
			this.HelpProvider.SetShowHelp(this.Docs_BT, true);
			// 
			// Datos
			// 
			this.Datos.DataSource = typeof(moleQule.Library.Hipatia.Agente);
			// 
			// Progress_Panel
			// 
			this.Progress_Panel.Location = new System.Drawing.Point(418, 227);
			// 
			// ProgressBK_Panel
			// 
			this.ProgressBK_Panel.Size = new System.Drawing.Size(1194, 672);
			// 
			// ProgressInfo_PB
			// 
			this.ProgressInfo_PB.Location = new System.Drawing.Point(560, 384);
			// 
			// Progress_PB
			// 
			this.Progress_PB.Location = new System.Drawing.Point(560, 299);
			// 
			// Identificador_GB
			// 
			this.Identificador_GB.Controls.Add(this.label4);
			this.Identificador_GB.Controls.Add(this.textBox1);
			this.Identificador_GB.Controls.Add(this.label3);
			this.Identificador_GB.Controls.Add(this.label2);
			this.Identificador_GB.Controls.Add(this.label1);
			this.Identificador_GB.Controls.Add(this.Fecha_DTP);
			this.Identificador_GB.Controls.Add(this.Nombre_TB);
			this.Identificador_GB.Controls.Add(this.Codigo_TB);
			this.Identificador_GB.Location = new System.Drawing.Point(7, 15);
			this.Identificador_GB.Name = "Identificador_GB";
			this.Identificador_GB.Size = new System.Drawing.Size(268, 240);
			this.Identificador_GB.TabIndex = 0;
			this.Identificador_GB.TabStop = false;
			this.Identificador_GB.Text = "Datos de la Carpeta";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(12, 180);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(65, 13);
			this.label4.TabIndex = 10;
			this.label4.Text = "Tipo Agente";
			// 
			// textBox1
			// 
			this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Entidad", true));
			this.textBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBox1.Location = new System.Drawing.Point(15, 197);
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(242, 21);
			this.textBox1.TabIndex = 9;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(12, 130);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(58, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "Fecha Alta";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(12, 76);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(42, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Agente";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(12, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "Código";
			// 
			// Fecha_DTP
			// 
			this.Fecha_DTP.Checked = false;
			this.Fecha_DTP.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.Datos, "Fecha", true));
			this.Fecha_DTP.Enabled = false;
			this.Fecha_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Fecha_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.Fecha_DTP.Location = new System.Drawing.Point(15, 146);
			this.Fecha_DTP.Name = "Fecha_DTP";
			this.Fecha_DTP.Size = new System.Drawing.Size(127, 21);
			this.Fecha_DTP.TabIndex = 5;
			// 
			// Nombre_TB
			// 
			this.Nombre_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Nombre", true));
			this.Nombre_TB.Location = new System.Drawing.Point(15, 93);
			this.Nombre_TB.Name = "Nombre_TB";
			this.Nombre_TB.ReadOnly = true;
			this.Nombre_TB.Size = new System.Drawing.Size(242, 21);
			this.Nombre_TB.TabIndex = 1;
			// 
			// Codigo_TB
			// 
			this.Codigo_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Codigo", true));
			this.Codigo_TB.Location = new System.Drawing.Point(15, 42);
			this.Codigo_TB.Name = "Codigo_TB";
			this.Codigo_TB.ReadOnly = true;
			this.Codigo_TB.Size = new System.Drawing.Size(138, 21);
			this.Codigo_TB.TabIndex = 0;
			// 
			// Documentos_GB
			// 
			this.Documentos_GB.Controls.Add(this.PanelDocs);
			this.Documentos_GB.Location = new System.Drawing.Point(281, 15);
			this.Documentos_GB.Name = "Documentos_GB";
			this.Documentos_GB.Size = new System.Drawing.Size(900, 597);
			this.Documentos_GB.TabIndex = 1;
			this.Documentos_GB.TabStop = false;
			this.Documentos_GB.Text = "Documentos";
			// 
			// PanelDocs
			// 
			this.PanelDocs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelDocs.Location = new System.Drawing.Point(3, 17);
			this.PanelDocs.Name = "PanelDocs";
			this.PanelDocs.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// PanelDocs.Panel1
			// 
			this.PanelDocs.Panel1.Controls.Add(this.Tools_TS);
			// 
			// PanelDocs.Panel2
			// 
			this.PanelDocs.Panel2.AutoScroll = true;
			this.PanelDocs.Panel2.Controls.Add(this.Tabla);
			this.PanelDocs.Size = new System.Drawing.Size(894, 577);
			this.PanelDocs.SplitterDistance = 57;
			this.PanelDocs.TabIndex = 0;
			// 
			// Tools_TS
			// 
			this.Tools_TS.AutoSize = false;
			this.Tools_TS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.Tools_TS.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Tools_TS.GripMargin = new System.Windows.Forms.Padding(3);
			this.Tools_TS.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.Tools_TS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.New_TI,
            this.Attach_TI,
            this.View_TI,
            this.Edit_TI,
            this.Borrar_TI,
            this.Replace_TI,
            this.Download_TI,
            this.Email_TI});
			this.Tools_TS.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
			this.Tools_TS.Location = new System.Drawing.Point(0, 0);
			this.Tools_TS.Name = "Tools_TS";
			this.Tools_TS.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.Tools_TS.Size = new System.Drawing.Size(894, 57);
			this.Tools_TS.TabIndex = 6;
			// 
			// New_TI
			// 
			this.New_TI.Image = global::moleQule.Face.Hipatia.Properties.Resources.document_add;
			this.New_TI.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.New_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.New_TI.Margin = new System.Windows.Forms.Padding(0);
			this.New_TI.Name = "New_TI";
			this.New_TI.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.New_TI.Size = new System.Drawing.Size(56, 57);
			this.New_TI.Text = "Nuevo";
			this.New_TI.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.New_TI.Click += new System.EventHandler(this.New_TI_Click);
			// 
			// Attach_TI
			// 
			this.Attach_TI.Image = global::moleQule.Face.Hipatia.Properties.Resources.document_attachment;
			this.Attach_TI.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.Attach_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.Attach_TI.Margin = new System.Windows.Forms.Padding(0);
			this.Attach_TI.Name = "Attach_TI";
			this.Attach_TI.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.Attach_TI.Size = new System.Drawing.Size(60, 57);
			this.Attach_TI.Text = "Asociar";
			this.Attach_TI.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.Attach_TI.Click += new System.EventHandler(this.Attach_TI_Click);
			// 
			// View_TI
			// 
			this.View_TI.Image = global::moleQule.Face.Hipatia.Properties.Resources.document_view;
			this.View_TI.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.View_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.View_TI.Margin = new System.Windows.Forms.Padding(0);
			this.View_TI.Name = "View_TI";
			this.View_TI.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.View_TI.Size = new System.Drawing.Size(46, 57);
			this.View_TI.Text = "Ver";
			this.View_TI.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.View_TI.Click += new System.EventHandler(this.View_TI_Click);
			// 
			// Edit_TI
			// 
			this.Edit_TI.Image = global::moleQule.Face.Hipatia.Properties.Resources.document_edit;
			this.Edit_TI.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.Edit_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.Edit_TI.Margin = new System.Windows.Forms.Padding(0);
			this.Edit_TI.Name = "Edit_TI";
			this.Edit_TI.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.Edit_TI.Size = new System.Drawing.Size(51, 57);
			this.Edit_TI.Text = "Editar";
			this.Edit_TI.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.Edit_TI.Click += new System.EventHandler(this.Edit_TI_Click);
			// 
			// Borrar_TI
			// 
			this.Borrar_TI.Image = global::moleQule.Face.Hipatia.Properties.Resources.document_delete;
			this.Borrar_TI.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.Borrar_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.Borrar_TI.Margin = new System.Windows.Forms.Padding(0);
			this.Borrar_TI.Name = "Borrar_TI";
			this.Borrar_TI.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.Borrar_TI.Size = new System.Drawing.Size(53, 57);
			this.Borrar_TI.Text = "Borrar";
			this.Borrar_TI.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.Borrar_TI.Click += new System.EventHandler(this.Borrar_TI_Click);
			// 
			// Replace_TI
			// 
			this.Replace_TI.Image = global::moleQule.Face.Hipatia.Properties.Resources.document_exchange;
			this.Replace_TI.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.Replace_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.Replace_TI.Margin = new System.Windows.Forms.Padding(0);
			this.Replace_TI.Name = "Replace_TI";
			this.Replace_TI.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.Replace_TI.Size = new System.Drawing.Size(82, 57);
			this.Replace_TI.Text = "Reemplazar";
			this.Replace_TI.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.Replace_TI.Click += new System.EventHandler(this.Replace_TI_Click);
			// 
			// Download_TI
			// 
			this.Download_TI.Image = global::moleQule.Face.Hipatia.Properties.Resources.document_into;
			this.Download_TI.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.Download_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.Download_TI.Margin = new System.Windows.Forms.Padding(0);
			this.Download_TI.Name = "Download_TI";
			this.Download_TI.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
			this.Download_TI.Size = new System.Drawing.Size(73, 57);
			this.Download_TI.Text = "Descargar";
			this.Download_TI.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.Download_TI.Click += new System.EventHandler(this.toolStripButton1_Click);
			// 
			// Email_TI
			// 
			this.Email_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.Email_TI.Image = global::moleQule.Face.Hipatia.Properties.Resources.send_document;
			this.Email_TI.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.Email_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.Email_TI.Name = "Email_TI";
			this.Email_TI.Size = new System.Drawing.Size(36, 54);
			this.Email_TI.Text = "Enviar por email";
			this.Email_TI.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.Email_TI.Visible = false;
			this.Email_TI.Click += new System.EventHandler(this.Email_TI_Click);
			// 
			// Tabla
			// 
			this.Tabla.AllowUserToAddRows = false;
			this.Tabla.AllowUserToDeleteRows = false;
			this.Tabla.AllowUserToOrderColumns = true;
			this.Tabla.AutoGenerateColumns = false;
			this.Tabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Codigo,
            this.Nombre,
            this.Tipo,
            this.Fecha,
            this.FechaAlta,
            this.Shared,
            this.Observaciones});
			this.Tabla.DataSource = this.Datos_Documentos;
			this.Tabla.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Tabla.Location = new System.Drawing.Point(0, 0);
			this.Tabla.Name = "Tabla";
			this.Tabla.ReadOnly = true;
			this.Tabla.Size = new System.Drawing.Size(894, 516);
			this.Tabla.TabIndex = 0;
			this.Tabla.DoubleClick += new System.EventHandler(this.Tabla_DoubleClick);
			// 
			// Codigo
			// 
			this.Codigo.DataPropertyName = "Codigo";
			this.Codigo.HeaderText = "Código";
			this.Codigo.Name = "Codigo";
			this.Codigo.ReadOnly = true;
			this.Codigo.Width = 80;
			// 
			// Nombre
			// 
			this.Nombre.DataPropertyName = "Nombre";
			this.Nombre.HeaderText = "Nombre";
			this.Nombre.Name = "Nombre";
			this.Nombre.ReadOnly = true;
			// 
			// Tipo
			// 
			this.Tipo.DataPropertyName = "Tipo";
			this.Tipo.HeaderText = "Tipo";
			this.Tipo.Name = "Tipo";
			this.Tipo.ReadOnly = true;
			// 
			// Fecha
			// 
			this.Fecha.DataPropertyName = "Fecha";
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.Format = "d";
			dataGridViewCellStyle1.NullValue = null;
			this.Fecha.DefaultCellStyle = dataGridViewCellStyle1;
			this.Fecha.HeaderText = "Fecha";
			this.Fecha.Name = "Fecha";
			this.Fecha.ReadOnly = true;
			this.Fecha.Width = 75;
			// 
			// FechaAlta
			// 
			this.FechaAlta.DataPropertyName = "FechaAlta";
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.Format = "d";
			dataGridViewCellStyle2.NullValue = null;
			this.FechaAlta.DefaultCellStyle = dataGridViewCellStyle2;
			this.FechaAlta.HeaderText = "Fecha Alta";
			this.FechaAlta.Name = "FechaAlta";
			this.FechaAlta.ReadOnly = true;
			this.FechaAlta.Width = 75;
			// 
			// Shared
			// 
			this.Shared.DataPropertyName = "Shared";
			this.Shared.HeaderText = "Compartido";
			this.Shared.Name = "Shared";
			this.Shared.ReadOnly = true;
			this.Shared.Width = 80;
			// 
			// Observaciones
			// 
			this.Observaciones.DataPropertyName = "Observaciones";
			this.Observaciones.HeaderText = "Observaciones";
			this.Observaciones.Name = "Observaciones";
			this.Observaciones.ReadOnly = true;
			// 
			// Datos_Documentos
			// 
			this.Datos_Documentos.DataSource = typeof(moleQule.Library.Hipatia.DocumentoInfo);
			// 
			// Observaciones_TB
			// 
			this.Observaciones_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Observaciones", true));
			this.Observaciones_TB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Observaciones_TB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Observaciones_TB.Location = new System.Drawing.Point(3, 17);
			this.Observaciones_TB.Name = "Observaciones_TB";
			this.Observaciones_TB.Size = new System.Drawing.Size(262, 323);
			this.Observaciones_TB.TabIndex = 5;
			this.Observaciones_TB.Text = "";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.Observaciones_TB);
			this.groupBox1.Location = new System.Drawing.Point(7, 269);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(268, 343);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Observaciones";
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.DataPropertyName = "Codigo";
			this.dataGridViewTextBoxColumn1.HeaderText = "Código";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.DataPropertyName = "Nombre";
			this.dataGridViewTextBoxColumn2.HeaderText = "Nombre";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			// 
			// dataGridViewTextBoxColumn3
			// 
			this.dataGridViewTextBoxColumn3.DataPropertyName = "Tipo";
			this.dataGridViewTextBoxColumn3.HeaderText = "Tipo";
			this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
			// 
			// dataGridViewTextBoxColumn4
			// 
			this.dataGridViewTextBoxColumn4.DataPropertyName = "Fecha";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle3.Format = "d";
			dataGridViewCellStyle3.NullValue = null;
			this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle3;
			this.dataGridViewTextBoxColumn4.HeaderText = "Fecha";
			this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
			// 
			// dataGridViewTextBoxColumn5
			// 
			this.dataGridViewTextBoxColumn5.DataPropertyName = "FechaAlta";
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle4.Format = "d";
			dataGridViewCellStyle4.NullValue = null;
			this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle4;
			this.dataGridViewTextBoxColumn5.HeaderText = "Fecha de alta";
			this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
			// 
			// dataGridViewTextBoxColumn6
			// 
			this.dataGridViewTextBoxColumn6.DataPropertyName = "Observaciones";
			this.dataGridViewTextBoxColumn6.HeaderText = "Observaciones";
			this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
			// 
			// AgenteForm
			// 
			this.ClientSize = new System.Drawing.Size(1194, 672);
			this.HelpProvider.SetHelpKeyword(this, "60");
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "AgenteForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.Text = "AgenteForm";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AgenteForm_FormClosing);
			this.PanelesV.Panel1.ResumeLayout(false);
			this.PanelesV.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.PanelesV)).EndInit();
			this.PanelesV.ResumeLayout(false);
			this.Paneles2.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Paneles2)).EndInit();
			this.Paneles2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			this.Progress_Panel.ResumeLayout(false);
			this.Progress_Panel.PerformLayout();
			this.ProgressBK_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).EndInit();
			this.Identificador_GB.ResumeLayout(false);
			this.Identificador_GB.PerformLayout();
			this.Documentos_GB.ResumeLayout(false);
			this.PanelDocs.Panel1.ResumeLayout(false);
			this.PanelDocs.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.PanelDocs)).EndInit();
			this.PanelDocs.ResumeLayout(false);
			this.Tools_TS.ResumeLayout(false);
			this.Tools_TS.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Tabla)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Documentos)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.GroupBox Identificador_GB;
        protected System.Windows.Forms.TextBox Codigo_TB;
        protected System.Windows.Forms.GroupBox Documentos_GB;
        protected System.Windows.Forms.SplitContainer PanelDocs;
        protected System.Windows.Forms.TextBox Nombre_TB;
        protected System.Windows.Forms.DateTimePicker Fecha_DTP;
        protected System.Windows.Forms.GroupBox groupBox1;
        protected System.Windows.Forms.RichTextBox Observaciones_TB;
        protected System.Windows.Forms.DataGridView Tabla;
        protected System.Windows.Forms.BindingSource Datos_Documentos;
        protected System.Windows.Forms.FolderBrowserDialog Browser;
        private System.Windows.Forms.ToolStripButton New_TI;
        private System.Windows.Forms.ToolStripButton Edit_TI;
        private System.Windows.Forms.ToolStripButton Attach_TI;
        private System.Windows.Forms.ToolStripButton Borrar_TI;
        private System.Windows.Forms.ToolStripButton Replace_TI;
        private System.Windows.Forms.ToolStripButton Download_TI;
        private System.Windows.Forms.ToolStripButton View_TI;
        protected System.Windows.Forms.ToolStrip Tools_TS;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.Label label4;
        protected System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaAlta;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Shared;
        private System.Windows.Forms.DataGridViewTextBoxColumn Observaciones;
		private System.Windows.Forms.ToolStripButton Email_TI;

    }
}
