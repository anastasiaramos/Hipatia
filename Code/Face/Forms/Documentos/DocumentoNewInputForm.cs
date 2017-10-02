using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Security.Principal;

using moleQule.Library;
using moleQule.Face;
using moleQule.Face.Skin01;

using moleQule.Library.Hipatia;

namespace moleQule.Face.Hipatia
{
    public partial class DocumentoNewInputForm : InputSkinForm,
                                                moleQule.Library.IBackGroundLauncher
    {
        #region Business Methods

        protected override int BarSteps { get { return 15; } }

        public const string ID = "DocumentoNewInputForm";
        public static Type Type { get { return typeof(DocumentoNewInputForm); } }

        private Agente _agente;
        private DocumentoInfo _doc;
        private Documento _entity;

        public Documento Entity
        {
            get { return _entity; }
            set { _entity = value; }
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        public DocumentoNewInputForm(Agente agente, DocumentoInfo doc)
            : this(true, agente, doc) {}

        /// <summary>
        /// Constructor
        /// </summary>
        public DocumentoNewInputForm(Agente agente)
            : this(true, agente, null) { }

        public DocumentoNewInputForm(Agente agente, Form parent)
            : this(true, agente, null, parent) { }

        public DocumentoNewInputForm(bool IsModal, Agente agente, DocumentoInfo doc)
            : this(true, agente, doc, null) { }

        public DocumentoNewInputForm(bool IsModal, Agente agente, DocumentoInfo doc, Form parent)
            : base(IsModal, parent)
        {
            InitializeComponent();
            _agente = agente;
            _doc = doc;
            if (doc != null)
            {
                _entity = Documento.Get(doc.Oid);
                _entity.Nombre = string.Empty;
                _entity.Ruta = string.Empty;
                this.Text = Resources.Labels.DOCUMENTO_EDIT_TITLE + " " + doc.Nombre;
            }
            else
            {
                _entity = Documento.New();//_agente.Documentos.NewItem(agente);
                //AgenteDocumento agente_documento = _entity.AgenteDocumentos.NewItem(_entity);
                //agente_documento.OidAgente = _agente.Oid;
                this.Text = Resources.Labels.DOCUMENTO_ADD_TITLE;
            }
            SetFormData();           
        }

        #endregion

        #region IBackGroundLauncher

        new enum BackJob { Submit, Update }
        new BackJob _back_job = BackJob.Submit;
 
        /// <summary>
        /// La llama el backgroundworker para ejecutar codigo en segundo plano
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public new void BackGroundJob(BackgroundWorker bk)
        {
            try
            {
                switch (_back_job)
                {
                    case BackJob.Update:
                        DoUpdate();
                        break;

                    default:
                        base.BackGroundJob(bk);
                        return;
                }
                
            }
            catch (Exception ex)
            {
                PgMng.Result = BGResult.Error;
                CancelBackGroundJob();
				PgMng.ShowErrorException(ex);
            }
        }

        private void DoUpdate()
        {
			PgMng.Result = BGResult.OK;

            PgMng.Message = Resources.Messages.CONNECTING_SERVER;

            if (Nombre_TB.Text == string.Empty)
            {
                throw new iQException(Resources.Messages.NO_NAME);
            }
            PgMng.Grow();

            if (_doc != null)
            {
                //Los nombres de los ficheros deben coincidir
                if (_doc.Nombre != Nombre_TB.Text)
                    throw new iQException(Resources.Messages.NOT_EQUAL_DOC_NAME);
            }
            else if (HipatiaCtl.CheckDuplicate(_agente, _entity))
            {
                throw new iQException(Resources.Messages.DOC_REPEATED);
            }
            PgMng.Grow();

			FtpClient ftp = new FtpClient(Principal.GetHipatiaFTPHost(), Principal.GetHipatiaFTPUser(), Principal.GetHipatiaFTPPwd(), Principal.GetHipatiaFTPRootPath());
			HipatiaCtl.InitHipatia(ftp, Principal.GetHipatiaFTPRootPath(), Principal.GetHipatiaFTPHost());
            PgMng.Grow();

            PgMng.Message = Resources.Messages.UPLOADING_FILE;

            AgenteInfo _ag_info = _agente.GetInfo(false);

            if (!HipatiaCtl.ExistsAgentDirectory(ftp, _ag_info)) HipatiaCtl.CreateAgentDirectory(ftp, _ag_info);
            PgMng.Grow();

            try
            {
                //Estamos añadiendo uno nuevo
                if (_doc == null)
                {
                    if (HipatiaCtl.ExistsFile(ftp, _ag_info, _entity.GetInfo(false)))
                    {
                        if (ProgressInfoMng.ShowQuestion(Resources.Messages.OVERWRITE_FILE) == DialogResult.Yes)
                            HipatiaCtl.Copy(ftp, _ag_info, _entity.Ruta);
                    }
                    else
                    {
                        HipatiaCtl.Copy(ftp, _ag_info, _entity.Ruta);
                    }
                }
                //Estamos reemplazando uno existente
                else
                {
					HipatiaCtl.ExistsDirectory(ftp, _doc.Ruta);
                    HipatiaCtl.Copy(ftp, _ag_info, _entity.Ruta);
                }
            }
            catch (Exception ex)
            {
				PgMng.Result = BGResult.Error;
				throw new iQException(Resources.Messages.OPERATION_ERROR + Environment.NewLine + ex.Message);
            }

            _entity.Ruta = HipatiaCtl.GetAgenteDirectory(ftp, _ag_info) + "/" + _entity.Nombre;
            PgMng.Grow();

            PgMng.Message = Resources.Messages.CLOSING_SERVER;
            PgMng.Grow();

            _entity.ApplyEdit();
            _entity.Save();
            PgMng.Grow();
        }

        #endregion

        #region Style & Source

        /// <summary>Formatea los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            base.FormatControls();
        }

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
        }

        public override void RefreshSecondaryData()
        {
            Datos_TipoDocumento.DataSource = TipodocumentoList.GetList(false);
        }

        #endregion

        #region Actions

        protected override void SubmitAction()
        {
            if (_entity.Nombre == string.Empty || _entity.Ruta == string.Empty)
            {
                PgMng.ShowInfoException(Resources.Messages.MISSING_FILE);

                _action_result = DialogResult.Ignore;
                return;
            }

            if (Fecha_DTP.Checked == false)
            {
				PgMng.ShowInfoException(Resources.Messages.MISSING_FECHA);

                _action_result = DialogResult.Ignore;
                return;
            }

            if (FechaAlta_DTP.Checked == false)
            {
				PgMng.ShowInfoException(Resources.Messages.MISSING_FECHA_ALTA);

                _action_result = DialogResult.Ignore;
                return;
            }

            if (Tipo_CB.SelectedItem == null)
            {
				PgMng.ShowInfoException(Resources.Messages.MISSING_TIPO);

                _action_result = DialogResult.Ignore;
                return;
            }

            this.Enabled = false;

            PgMng.Reset(9, 1, Resources.Messages.CONNECTING_SERVER, this);
            _back_job = BackJob.Update;
            //Hide();
			DoJob();
            //PgMng.StartBackJob(this);

            if (PgMng.Result == BGResult.OK)
            {
                //Si es nuevo añadimos la entrada en la base de datos
                if (_doc == null)
                {
                    AgenteDocumento agente_documento = _agente.Documentos.NewItem(_agente);
                    agente_documento.OidDocumento = _entity.Oid;
                    PgMng.Grow();

                    _agente.ApplyEdit();
                    _agente.Save();
                }
            }
            PgMng.FillUp();

            this.Enabled = true;

            _entity.CloseSession();
            _action_result = DialogResult.OK;
        }

        protected override void CancelAction()
        {
            _entity.CloseSession();
            _action_result = DialogResult.Cancel;
        }

		public void DoJob()
		{
			PgMng.Result = BGResult.OK;

			try
			{
				switch (_back_job)
				{
					case BackJob.Update:
						DoUpdate();
						break;
				}
			}
			catch (Exception ex)
			{
				PgMng.Result = BGResult.Error;
				PgMng.ShowErrorException(ex);
			}
		}

        #endregion

        #region Events

        private void OpenFile_BT_Click(object sender, EventArgs e)
        {
            // Si no deshacemos el Impersonate temporalmente, las carpetas
            // y archivos disponibles localmente seran los del usuario IQ.
            //PrincipalBase.Context.Undo();

            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                string _filePath = OpenFileDialog.FileName;
                string _fileName = _filePath.Substring(_filePath.LastIndexOf('\\') + 1);
                _entity.Nombre = _fileName;
                _entity.Ruta = _filePath;
            }
            //PrincipalBase.Context = PrincipalBase.ImpersonatedId.Impersonate(); 
        }

        private void Tipo_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*if (Tipo_CB.SelectedItem == null) return;

            _entity.Tipo = Tipo_CB.SelectedText;*/
        }

        private void Tipo_Documento_Add_BT_Click(object sender, EventArgs e)
        {
            TipoDocumentoUIForm form = new TipoDocumentoUIForm(true);
            form.ShowDialog(this);

            RefreshSecondaryData();
        }

        #endregion
    }
}

