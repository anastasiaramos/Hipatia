using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library;
using moleQule.Library.CslaEx;
using moleQule.Library.Hipatia;

namespace moleQule.Face.Hipatia
{
	public partial class AgenteForm : Skin01.ItemMngSkinForm
    {
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps; } }

		public virtual Agente Entity { get { return null; } set { } }
		public virtual AgenteInfo EntityInfo { get { return null; } }

        public DocumentoList _documentos = null;

        protected FtpClient _ftp;

        protected string _destination_path = string.Empty;

        protected DocumentoInfo CurrentDocumento { get { return Datos_Documentos.Current != null ? Datos_Documentos.Current as DocumentoInfo : null; } }
        protected string DestinationPath { get { return _destination_path; } }
		protected string TempFile { get { return DestinationPath + "\\" + System.Web.HttpUtility.UrlEncode(CurrentDocumento.Nombre); } }

        #endregion

        #region Factory Methods

        public AgenteForm() 
			: this(-1) {}

        public AgenteForm(long oid)
            : this(oid, null) {}

		public AgenteForm(long oid, Form parent)
			: base(oid, true, parent)
		{
			InitializeComponent();

			_ftp = new FtpClient(Principal.GetHipatiaFTPHost(), Principal.GetHipatiaFTPUser(), Principal.GetHipatiaFTPPwd(), Principal.GetHipatiaFTPRootPath());
			//HipatiaCtl.InitHipatia(_ftp, Controler.GetHipatiaFTPRootPath(), Controler.GetHipatiaFTPHost());
		}

        #endregion

        #region Style

		public override void FormatControls()
        {
            base.FormatControls();

            List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
            Nombre.Tag = 0.4;
            Observaciones.Tag = 0.6;

            cols.Add(Nombre);
            cols.Add(Observaciones);

            ControlsMng.MaximizeColumns(Tabla, cols);
            Tabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        #endregion

		#region Validation & Format

		#endregion

        #region Actions
        
        protected virtual void AddAction() {}
        protected virtual void AttachAction() {}        
        protected virtual void ViewAction() {}
        protected virtual void EditAction() {}
        protected virtual void ReplaceAction() {}
        protected virtual void DeleteAction() {}        
        protected virtual void DownloadAction() {}
		protected virtual void SendEmailAction() { }

        #endregion

        #region Buttons

        private void New_TI_Click(object sender, EventArgs e)
        {
            AddAction();
        }

        private void Attach_TI_Click(object sender, EventArgs e)
        {
            AttachAction();
        }

        private void View_TI_Click(object sender, EventArgs e)
        {
            ViewAction();
        }

        private void Edit_TI_Click(object sender, EventArgs e)
        {
            EditAction();
        }

        private void Borrar_TI_Click(object sender, EventArgs e)
        {
            DeleteAction();
        }

        private void Replace_TI_Click(object sender, EventArgs e)
        {
            ReplaceAction();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DownloadAction();
        }

		private void Email_TI_Click(object sender, EventArgs e)
		{
			SendEmailAction();
		}

        #endregion

        #region Events

		private void AgenteForm_FormClosing(object sender, FormClosingEventArgs e)
		{
            if (!(this is AgenteViewForm))
            {
                Entity.CloseSession();
            }

			Cerrar();
		}

		private void Tabla_DoubleClick(object sender, EventArgs e)
		{
			ViewAction();
		}

        #endregion
     }
}

