namespace moleQule.Face.Hipatia
{
    partial class TipoDocumentoUIForm
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
            this.Datos_Grid = new System.Windows.Forms.DataGridView();
            this.Valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserCreated = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.PanelesV.Panel1.SuspendLayout();
            this.PanelesV.Panel2.SuspendLayout();
            this.PanelesV.SuspendLayout();
            this.Paneles2.Panel1.SuspendLayout();
            this.Paneles2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Grid)).BeginInit();
            this.SuspendLayout();
            // 
            // PanelesV
            // 
            // 
            // PanelesV.Panel1
            // 
            this.PanelesV.Panel1.Controls.Add(this.Datos_Grid);
            this.PanelesV.Size = new System.Drawing.Size(349, 266);
            this.PanelesV.SplitterDistance = 225;
            // 
            // Submit_BT
            // 
            this.Submit_BT.Location = new System.Drawing.Point(251, 6);
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Location = new System.Drawing.Point(341, 6);
            // 
            // Paneles2
            // 
            this.Paneles2.Size = new System.Drawing.Size(347, 38);
            this.Paneles2.SplitterDistance = 37;
            // 
            // Imprimir_Button
            // 
            this.Imprimir_Button.Location = new System.Drawing.Point(161, 6);
            // 
            // Datos
            // 
            this.Datos.DataSource = typeof(moleQule.Library.Hipatia.Tipodocumentos);
            // 
            // Datos_Grid
            // 
            this.Datos_Grid.AutoGenerateColumns = false;
            this.Datos_Grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Datos_Grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Valor,
            this.UserCreated});
            this.Datos_Grid.DataSource = this.Datos;
            this.Datos_Grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Datos_Grid.Location = new System.Drawing.Point(0, 0);
            this.Datos_Grid.Name = "Datos_Grid";
            this.Datos_Grid.Size = new System.Drawing.Size(347, 223);
            this.Datos_Grid.TabIndex = 0;
            // 
            // Valor
            // 
            this.Valor.DataPropertyName = "Valor";
            this.Valor.HeaderText = "Valor";
            this.Valor.Name = "Valor";
            // 
            // UserCreated
            // 
            this.UserCreated.DataPropertyName = "UserCreated";
            this.UserCreated.HeaderText = "User Created";
            this.UserCreated.Name = "UserCreated";
            // 
            // TipoDocumentoUIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(349, 266);
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Name = "TipoDocumentoUIForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TipoDocumentoUIForm_FormClosing);
            this.PanelesV.Panel1.ResumeLayout(false);
            this.PanelesV.Panel2.ResumeLayout(false);
            this.PanelesV.ResumeLayout(false);
            this.Paneles2.Panel1.ResumeLayout(false);
            this.Paneles2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView Datos_Grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Valor;
        private System.Windows.Forms.DataGridViewCheckBoxColumn UserCreated;
    }
}
