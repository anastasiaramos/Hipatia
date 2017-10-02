namespace moleQule.Face.Hipatia
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Main_MenuStrip = new System.Windows.Forms.MenuStrip();
            this.Archivo_MI = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Salir_MI = new System.Windows.Forms.ToolStripMenuItem();
            this.Ayuda_MI = new System.Windows.Forms.ToolStripMenuItem();
            this.contenidoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.índiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.acercaDeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Principal_StatusBar = new System.Windows.Forms.StatusStrip();
            this.Leyenda_SL = new System.Windows.Forms.ToolStripStatusLabel();
            this.Progress_Bar = new System.Windows.Forms.ToolStripProgressBar();
            this.Anim_Label = new System.Windows.Forms.ToolStripStatusLabel();
            this.Status_Label = new System.Windows.Forms.ToolStripStatusLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.Main_MenuStrip.SuspendLayout();
            this.Principal_StatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // Main_MenuStrip
            // 
            this.Main_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Archivo_MI,
            this.Ayuda_MI});
            this.Main_MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.Main_MenuStrip.Name = "Main_MenuStrip";
            this.Main_MenuStrip.Size = new System.Drawing.Size(884, 24);
            this.Main_MenuStrip.TabIndex = 1;
            this.Main_MenuStrip.Text = "menuStrip1";
            // 
            // Archivo_MI
            // 
            this.Archivo_MI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.Salir_MI});
            this.Archivo_MI.Name = "Archivo_MI";
            this.Archivo_MI.Size = new System.Drawing.Size(55, 20);
            this.Archivo_MI.Text = "Archivo";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(102, 6);
            // 
            // Salir_MI
            // 
            this.Salir_MI.Name = "Salir_MI";
            this.Salir_MI.Size = new System.Drawing.Size(105, 22);
            this.Salir_MI.Text = "Salir";
            this.Salir_MI.Click += new System.EventHandler(this.Salir_MI_Click);
            // 
            // Ayuda_MI
            // 
            this.Ayuda_MI.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contenidoToolStripMenuItem,
            this.índiceToolStripMenuItem,
            this.toolStripSeparator2,
            this.acercaDeToolStripMenuItem});
            this.Ayuda_MI.Name = "Ayuda_MI";
            this.Ayuda_MI.Size = new System.Drawing.Size(50, 20);
            this.Ayuda_MI.Text = "Ayuda";
            // 
            // contenidoToolStripMenuItem
            // 
            this.contenidoToolStripMenuItem.Name = "contenidoToolStripMenuItem";
            this.contenidoToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.contenidoToolStripMenuItem.Text = "Contenido";
            // 
            // índiceToolStripMenuItem
            // 
            this.índiceToolStripMenuItem.Name = "índiceToolStripMenuItem";
            this.índiceToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.índiceToolStripMenuItem.Text = "Índice";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(131, 6);
            // 
            // acercaDeToolStripMenuItem
            // 
            this.acercaDeToolStripMenuItem.Name = "acercaDeToolStripMenuItem";
            this.acercaDeToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.acercaDeToolStripMenuItem.Text = "Acerca de";
            // 
            // Principal_StatusBar
            // 
            this.Principal_StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Leyenda_SL,
            this.Progress_Bar,
            this.Anim_Label,
            this.Status_Label});
            this.Principal_StatusBar.Location = new System.Drawing.Point(0, 525);
            this.Principal_StatusBar.Name = "Principal_StatusBar";
            this.Principal_StatusBar.Size = new System.Drawing.Size(884, 22);
            this.Principal_StatusBar.TabIndex = 3;
            this.Principal_StatusBar.Text = "statusStrip1";
            // 
            // Leyenda_SL
            // 
            this.Leyenda_SL.Name = "Leyenda_SL";
            this.Leyenda_SL.Size = new System.Drawing.Size(0, 17);
            this.Leyenda_SL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Progress_Bar
            // 
            this.Progress_Bar.Name = "Progress_Bar";
            this.Progress_Bar.Size = new System.Drawing.Size(100, 16);
            // 
            // Anim_Label
            // 
            this.Anim_Label.Font = new System.Drawing.Font("Tahoma", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Anim_Label.Name = "Anim_Label";
            this.Anim_Label.Size = new System.Drawing.Size(0, 17);
            this.Anim_Label.Visible = false;
            // 
            // Status_Label
            // 
            this.Status_Label.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Status_Label.Name = "Status_Label";
            this.Status_Label.Size = new System.Drawing.Size(767, 17);
            this.Status_Label.Spring = true;
            this.Status_Label.Text = "Powered by iQ";
            this.Status_Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 54);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.ActiveSchemaType = typeof(moleQule.Face.SchemaMngForm);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(884, 547);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Principal_StatusBar);
            this.Controls.Add(this.Main_MenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Controls.SetChildIndex(this.Main_MenuStrip, 0);
            this.Controls.SetChildIndex(this.Principal_StatusBar, 0);
            this.Controls.SetChildIndex(this.button1, 0);
            this.Main_MenuStrip.ResumeLayout(false);
            this.Main_MenuStrip.PerformLayout();
            this.Principal_StatusBar.ResumeLayout(false);
            this.Principal_StatusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip Main_MenuStrip;
        private System.Windows.Forms.StatusStrip Principal_StatusBar;
        private System.Windows.Forms.ToolStripProgressBar Progress_Bar;
        private System.Windows.Forms.ToolStripStatusLabel Status_Label;
        private System.Windows.Forms.ToolStripMenuItem Archivo_MI;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem Salir_MI;
        private System.Windows.Forms.ToolStripMenuItem Ayuda_MI;
        private System.Windows.Forms.ToolStripMenuItem contenidoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem índiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem acercaDeToolStripMenuItem;
        public System.Windows.Forms.ToolStripStatusLabel Anim_Label;
        public System.Windows.Forms.ToolStripStatusLabel Leyenda_SL;
        private System.Windows.Forms.Button button1;

    }
}
