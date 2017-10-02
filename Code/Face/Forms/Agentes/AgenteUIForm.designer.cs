namespace moleQule.Face.Hipatia
{
	partial class AgenteUIForm
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
            this.Aplicar_BT = new System.Windows.Forms.Button();
            this.Identificador_GB.SuspendLayout();
            this.Documentos_GB.SuspendLayout();
            this.PanelDocs.Panel1.SuspendLayout();
            this.PanelDocs.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Documentos)).BeginInit();
            this.PanelesV.Panel1.SuspendLayout();
            this.PanelesV.Panel2.SuspendLayout();
            this.PanelesV.SuspendLayout();
            this.Paneles2.Panel1.SuspendLayout();
            this.Paneles2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
            this.SuspendLayout();
            // 
            // Aplicar_BT
            // 
            this.Aplicar_BT.Location = new System.Drawing.Point(589, 7);
            this.Aplicar_BT.Name = "Aplicar_BT";
            this.Aplicar_BT.Size = new System.Drawing.Size(75, 23);
            this.Aplicar_BT.TabIndex = 207;
            this.Aplicar_BT.Text = "Guardar";
            this.Aplicar_BT.UseVisualStyleBackColor = true;
            this.Aplicar_BT.Click += new System.EventHandler(this.Aplicar_BT_Click);
            // 
            // AgenteUIForm
            // 
            this.HelpProvider.SetHelpKeyword(this, "60");
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Name = "AgenteUIForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "AgenteUIForm";
            this.Identificador_GB.ResumeLayout(false);
            this.Identificador_GB.PerformLayout();
            this.Documentos_GB.ResumeLayout(false);
            this.PanelDocs.Panel1.ResumeLayout(false);
            this.PanelDocs.Panel1.PerformLayout();
            this.PanelDocs.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Documentos)).EndInit();
            this.PanelesV.Panel1.ResumeLayout(false);
            this.PanelesV.Panel2.ResumeLayout(false);
            this.PanelesV.ResumeLayout(false);
            this.Paneles2.Panel1.ResumeLayout(false);
            this.Paneles2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Button Aplicar_BT;
	}
}
