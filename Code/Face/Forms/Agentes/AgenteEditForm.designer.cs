namespace moleQule.Face.Hipatia
{
    partial class AgenteEditForm
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
			this.Identificador_GB.SuspendLayout();
			this.Documentos_GB.SuspendLayout();
			this.PanelDocs.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Documentos)).BeginInit();
			this.PanelesV.Panel1.SuspendLayout();
			this.PanelesV.Panel2.SuspendLayout();
			this.PanelesV.SuspendLayout();
			this.Paneles2.Panel1.SuspendLayout();
			this.Paneles2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			this.ProgressBK_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.SuspendLayout();
			// 
			// Codigo_TB
			// 
			this.Codigo_TB.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Codigo_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Codigo_TB.ForeColor = System.Drawing.Color.Black;
			this.Codigo_TB.Size = new System.Drawing.Size(80, 21);
			this.Codigo_TB.TabStop = false;
			this.Codigo_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// Nombre_TB
			// 
			this.Nombre_TB.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Nombre_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Nombre_TB.ForeColor = System.Drawing.Color.Black;
			this.Nombre_TB.TabStop = false;
			// 
			// Fecha_DTP
			// 
			this.Fecha_DTP.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.Fecha_DTP.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Fecha_DTP.ShowCheckBox = true;
			this.Fecha_DTP.Size = new System.Drawing.Size(115, 21);
			// 
			// Observaciones_TB
			// 
			this.Observaciones_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.Observaciones_TB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.textBox1.Font = new System.Drawing.Font("Tahoma", 8.25F);
			this.textBox1.ForeColor = System.Drawing.Color.Black;
			this.textBox1.TabStop = false;
			// 
			// PanelesV
			// 
			// 
			// PanelesV.Panel1
			// 
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
			// 
			// PanelesV.Panel2
			// 
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
			this.HelpProvider.SetShowHelp(this.PanelesV, true);
			// 
			// Submit_BT
			// 
			this.Submit_BT.Location = new System.Drawing.Point(429, 7);
			this.HelpProvider.SetShowHelp(this.Submit_BT, true);
			// 
			// Cancel_BT
			// 
			this.Cancel_BT.Location = new System.Drawing.Point(515, 7);
			this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
			// 
			// Paneles2
			// 
			// 
			// Paneles2.Panel1
			// 
			this.HelpProvider.SetShowHelp(this.Paneles2.Panel1, true);
			// 
			// Paneles2.Panel2
			// 
			this.HelpProvider.SetShowHelp(this.Paneles2.Panel2, true);
			this.HelpProvider.SetShowHelp(this.Paneles2, true);
			// 
			// Imprimir_Button
			// 
			this.Imprimir_Button.Location = new System.Drawing.Point(691, 7);
			this.HelpProvider.SetShowHelp(this.Imprimir_Button, true);
			// 
			// Docs_BT
			// 
			this.Docs_BT.Location = new System.Drawing.Point(601, 7);
			this.HelpProvider.SetShowHelp(this.Docs_BT, true);
			// 
			// AgenteEditForm
			// 
			this.ClientSize = new System.Drawing.Size(1194, 672);
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.HelpProvider.SetHelpKeyword(this, "60");
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Name = "AgenteEditForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.Text = "Modificar Hipatia";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AgenteEditForm_FormClosing);
			this.Identificador_GB.ResumeLayout(false);
			this.Identificador_GB.PerformLayout();
			this.Documentos_GB.ResumeLayout(false);
			this.PanelDocs.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Datos_Documentos)).EndInit();
			this.PanelesV.Panel1.ResumeLayout(false);
			this.PanelesV.Panel2.ResumeLayout(false);
			this.PanelesV.ResumeLayout(false);
			this.Paneles2.Panel1.ResumeLayout(false);
			this.Paneles2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			this.ProgressBK_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion
    }
}
