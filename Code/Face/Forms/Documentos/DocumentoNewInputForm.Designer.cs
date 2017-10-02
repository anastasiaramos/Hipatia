namespace moleQule.Face.Hipatia
{
    partial class DocumentoNewInputForm
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
            System.Windows.Forms.Label fechaLabel;
            System.Windows.Forms.Label fechaAltaLabel;
            System.Windows.Forms.Label tipoLabel;
            System.Windows.Forms.Label observacionesLabel;
            System.Windows.Forms.Label nombreLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentoNewInputForm));
            this.OpenFile_BT = new System.Windows.Forms.Button();
            this.Fecha_DTP = new System.Windows.Forms.DateTimePicker();
            this.FechaAlta_DTP = new System.Windows.Forms.DateTimePicker();
            this.Tipo_CB = new System.Windows.Forms.ComboBox();
            this.Datos_TipoDocumento = new System.Windows.Forms.BindingSource(this.components);
            this.Observaciones_RTB = new System.Windows.Forms.RichTextBox();
            this.Nombre_TB = new System.Windows.Forms.TextBox();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.Tipo_Documento_Add_BT = new System.Windows.Forms.Button();
            fechaLabel = new System.Windows.Forms.Label();
            fechaAltaLabel = new System.Windows.Forms.Label();
            tipoLabel = new System.Windows.Forms.Label();
            observacionesLabel = new System.Windows.Forms.Label();
            nombreLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
            this.Source_GB.SuspendLayout();
            this.PanelesV.Panel1.SuspendLayout();
            this.PanelesV.Panel2.SuspendLayout();
            this.PanelesV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_TipoDocumento)).BeginInit();
            this.SuspendLayout();
            // 
            // Datos
            // 
            this.Datos.DataSource = typeof(moleQule.Library.Hipatia.Documento);
            // 
            // Submit_BT
            // 
            this.Submit_BT.Location = new System.Drawing.Point(147, 7);
            this.HelpProvider.SetShowHelp(this.Submit_BT, true);
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Location = new System.Drawing.Point(237, 7);
            this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
            // 
            // Source_GB
            // 
            this.Source_GB.Controls.Add(this.Tipo_Documento_Add_BT);
            this.Source_GB.Controls.Add(this.OpenFile_BT);
            this.Source_GB.Controls.Add(fechaLabel);
            this.Source_GB.Controls.Add(this.Fecha_DTP);
            this.Source_GB.Controls.Add(fechaAltaLabel);
            this.Source_GB.Controls.Add(this.FechaAlta_DTP);
            this.Source_GB.Controls.Add(tipoLabel);
            this.Source_GB.Controls.Add(this.Tipo_CB);
            this.Source_GB.Controls.Add(observacionesLabel);
            this.Source_GB.Controls.Add(this.Observaciones_RTB);
            this.Source_GB.Controls.Add(nombreLabel);
            this.Source_GB.Controls.Add(this.Nombre_TB);
            this.Source_GB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Source_GB.Location = new System.Drawing.Point(0, 0);
            this.HelpProvider.SetShowHelp(this.Source_GB, true);
            this.Source_GB.Size = new System.Drawing.Size(470, 325);
            this.Source_GB.Text = "";
            // 
            // PanelesV
            // 
            // 
            // PanelesV.Panel1
            // 
            this.PanelesV.Panel1.AutoScroll = true;
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
            // 
            // PanelesV.Panel2
            // 
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
            this.HelpProvider.SetShowHelp(this.PanelesV, true);
            this.PanelesV.Size = new System.Drawing.Size(472, 367);
            this.PanelesV.SplitterDistance = 327;
            // 
            // fechaLabel
            // 
            fechaLabel.AutoSize = true;
            fechaLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            fechaLabel.Location = new System.Drawing.Point(72, 144);
            fechaLabel.Name = "fechaLabel";
            fechaLabel.Size = new System.Drawing.Size(40, 13);
            fechaLabel.TabIndex = 32;
            fechaLabel.Text = "Fecha:";
            // 
            // fechaAltaLabel
            // 
            fechaAltaLabel.AutoSize = true;
            fechaAltaLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            fechaAltaLabel.Location = new System.Drawing.Point(50, 117);
            fechaAltaLabel.Name = "fechaAltaLabel";
            fechaAltaLabel.Size = new System.Drawing.Size(62, 13);
            fechaAltaLabel.TabIndex = 30;
            fechaAltaLabel.Text = "Fecha Alta:";
            // 
            // tipoLabel
            // 
            tipoLabel.AutoSize = true;
            tipoLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            tipoLabel.Location = new System.Drawing.Point(81, 75);
            tipoLabel.Name = "tipoLabel";
            tipoLabel.Size = new System.Drawing.Size(31, 13);
            tipoLabel.TabIndex = 28;
            tipoLabel.Text = "Tipo:";
            // 
            // observacionesLabel
            // 
            observacionesLabel.AutoSize = true;
            observacionesLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            observacionesLabel.Location = new System.Drawing.Point(30, 196);
            observacionesLabel.Name = "observacionesLabel";
            observacionesLabel.Size = new System.Drawing.Size(82, 13);
            observacionesLabel.TabIndex = 26;
            observacionesLabel.Text = "Observaciones:";
            // 
            // nombreLabel
            // 
            nombreLabel.AutoSize = true;
            nombreLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            nombreLabel.Location = new System.Drawing.Point(78, 48);
            nombreLabel.Name = "nombreLabel";
            nombreLabel.Size = new System.Drawing.Size(34, 13);
            nombreLabel.TabIndex = 24;
            nombreLabel.Text = "Ruta:";
            // 
            // OpenFile_BT
            // 
            this.OpenFile_BT.Image = global::moleQule.Face.Hipatia.Properties.Resources.folder_16;
            this.OpenFile_BT.Location = new System.Drawing.Point(381, 44);
            this.OpenFile_BT.Name = "OpenFile_BT";
            this.OpenFile_BT.Size = new System.Drawing.Size(32, 22);
            this.OpenFile_BT.TabIndex = 34;
            this.OpenFile_BT.UseVisualStyleBackColor = true;
            this.OpenFile_BT.Click += new System.EventHandler(this.OpenFile_BT_Click);
            // 
            // Fecha_DTP
            // 
            this.Fecha_DTP.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.Datos, "Fecha", true));
            this.Fecha_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Fecha_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Fecha_DTP.Location = new System.Drawing.Point(118, 140);
            this.Fecha_DTP.Name = "Fecha_DTP";
            this.Fecha_DTP.ShowCheckBox = true;
            this.Fecha_DTP.Size = new System.Drawing.Size(116, 21);
            this.Fecha_DTP.TabIndex = 33;
            // 
            // FechaAlta_DTP
            // 
            this.FechaAlta_DTP.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.Datos, "FechaAlta", true));
            this.FechaAlta_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FechaAlta_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.FechaAlta_DTP.Location = new System.Drawing.Point(118, 113);
            this.FechaAlta_DTP.Name = "FechaAlta_DTP";
            this.FechaAlta_DTP.ShowCheckBox = true;
            this.FechaAlta_DTP.Size = new System.Drawing.Size(116, 21);
            this.FechaAlta_DTP.TabIndex = 31;
            // 
            // Tipo_CB
            // 
            this.Tipo_CB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Tipo", true));
            this.Tipo_CB.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.Datos, "Tipo", true));
            this.Tipo_CB.DataSource = this.Datos_TipoDocumento;
            this.Tipo_CB.DisplayMember = "Valor";
            this.Tipo_CB.FormattingEnabled = true;
            this.Tipo_CB.Location = new System.Drawing.Point(118, 71);
            this.Tipo_CB.Name = "Tipo_CB";
            this.Tipo_CB.Size = new System.Drawing.Size(257, 21);
            this.Tipo_CB.TabIndex = 29;
            this.Tipo_CB.ValueMember = "Valor";
            this.Tipo_CB.SelectedIndexChanged += new System.EventHandler(this.Tipo_CB_SelectedIndexChanged);
            // 
            // Datos_TipoDocumento
            // 
            this.Datos_TipoDocumento.DataSource = typeof(moleQule.Library.Hipatia.TipodocumentoInfo);
            // 
            // Observaciones_RTB
            // 
            this.Observaciones_RTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Observaciones", true));
            this.Observaciones_RTB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Observaciones_RTB.Location = new System.Drawing.Point(118, 193);
            this.Observaciones_RTB.Name = "Observaciones_RTB";
            this.Observaciones_RTB.Size = new System.Drawing.Size(322, 96);
            this.Observaciones_RTB.TabIndex = 27;
            this.Observaciones_RTB.Text = "";
            // 
            // Nombre_TB
            // 
            this.Nombre_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Nombre", true));
            this.Nombre_TB.Location = new System.Drawing.Point(118, 44);
            this.Nombre_TB.Name = "Nombre_TB";
            this.Nombre_TB.Size = new System.Drawing.Size(257, 21);
            this.Nombre_TB.TabIndex = 25;
            // 
            // Tipo_Documento_Add_BT
            // 
            this.Tipo_Documento_Add_BT.Image = global::moleQule.Face.Hipatia.Properties.Resources.add_16;
            this.Tipo_Documento_Add_BT.Location = new System.Drawing.Point(381, 71);
            this.Tipo_Documento_Add_BT.Name = "Tipo_Documento_Add_BT";
            this.Tipo_Documento_Add_BT.Size = new System.Drawing.Size(32, 22);
            this.Tipo_Documento_Add_BT.TabIndex = 35;
            this.Tipo_Documento_Add_BT.UseVisualStyleBackColor = true;
            this.Tipo_Documento_Add_BT.Click += new System.EventHandler(this.Tipo_Documento_Add_BT_Click);
            // 
            // DocumentoNewInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(472, 367);
            this.ControlBox = false;
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DocumentoNewInputForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.ShowInTaskbar = false;
            this.Text = "DocumentoNewInputForm";
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
            this.Source_GB.ResumeLayout(false);
            this.Source_GB.PerformLayout();
            this.PanelesV.Panel1.ResumeLayout(false);
            this.PanelesV.Panel2.ResumeLayout(false);
            this.PanelesV.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Datos_TipoDocumento)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Button OpenFile_BT;
        protected System.Windows.Forms.DateTimePicker Fecha_DTP;
        protected System.Windows.Forms.DateTimePicker FechaAlta_DTP;
        protected System.Windows.Forms.ComboBox Tipo_CB;
        protected System.Windows.Forms.RichTextBox Observaciones_RTB;
        protected System.Windows.Forms.TextBox Nombre_TB;
        private System.Windows.Forms.BindingSource Datos_TipoDocumento;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.Button Tipo_Documento_Add_BT;

    }
}
