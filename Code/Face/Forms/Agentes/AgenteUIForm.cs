using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.Mail;

using Csla;
using moleQule.Face;
using moleQule.Face.Hipatia.Properties;
using moleQule.Library;
using moleQule.Library.CslaEx;
using moleQule.Library.Hipatia;

namespace moleQule.Face.Hipatia
{
    public partial class AgenteUIForm : AgenteForm, IBackGroundLauncher
    {
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps + 2; } }

        /// <summary>
        /// Se trata de la Hipatia actual y que se va a editar.
        /// </summary>
        protected Agente _entity;

        public override Agente Entity { get { return _entity; } set { _entity = value; } }
        public override AgenteInfo EntityInfo { get { return (_entity != null) ? _entity.GetInfo() : null; } }

        #endregion

        #region Factory Methods

        public AgenteUIForm()
            : this((Form)null) {}

		public AgenteUIForm(Form parent)
			: base(-1, parent)
		{
			InitializeComponent();
			SetFormData();
		}

        public AgenteUIForm(long oid)
            : this(oid, null) { }

		public AgenteUIForm(long oid, Form parent)
			: base(oid, parent)
		{
			InitializeComponent();
		}

        public AgenteUIForm(Agente Agente)
            : base()
        {
            InitializeComponent();
            _entity = Agente.Clone();
            _entity.BeginEdit();
            SetFormData();
        }

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            using (StatusBusy busy = new StatusBusy(moleQule.Face.Resources.Messages.SAVING))
            {
                this.Datos.RaiseListChangedEvents = false;

                Agente temp = _entity.Clone();
                temp.ApplyEdit();

                // do the save
                try
                {
                    _entity = temp.Save();
                    _entity.ApplyEdit();
                    return true;
                }
                catch (iQValidationException ex)
                {
                    MessageBox.Show(iQExceptionHandler.GetAllMessages(ex) +
                                    Environment.NewLine + ex.SysMessage,
                                    Resources.Labels.APP_TITLE,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return false;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(moleQule.Face.Resources.Messages.OPERATION_ERROR + Environment.NewLine +
                                    ex.Message,
                                    Resources.Labels.APP_TITLE,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    return false;
                }
                finally
                {
                    RefreshMainData();
                    this.Datos.RaiseListChangedEvents = true;
                }
            }

        }

        #endregion

        #region IBackGroundLauncher

        new enum BackJob { GetFormData, Download, Upload, Delete }
        new BackJob _back_job = BackJob.GetFormData;

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
                    case BackJob.Download:
                        DoDownload(bk);
                        break;

                    case BackJob.Upload:
                        DoUpload(bk);
                        break;

                    case BackJob.Delete:
                        DoDelete(bk);
                        break;

                    default:
                        base.BackGroundJob(bk);
                        return;
                }
            }
            catch (Exception ex)
            { 
                CancelBackGroundJob();
                PgMng.ShowInfoException(ex);
            }
        }

        private void DoDownload(BackgroundWorker bk)
        {
            PgMng.Grow(Resources.Messages.CONNECTING_SERVER);
            
            if (_ftp == null)
            {
				_ftp = new FtpClient(Principal.GetHipatiaFTPHost(), Principal.GetHipatiaFTPUser(), Principal.GetHipatiaFTPPwd(), Principal.GetHipatiaFTPRootPath());
				HipatiaCtl.InitHipatia(_ftp, Principal.GetHipatiaFTPRootPath(), Principal.GetHipatiaFTPHost());
            }
            PgMng.Grow();

			if (HipatiaCtl.ExistsDirectory(_ftp, CurrentDocumento.Ruta))
            {
                PgMng.Message = Resources.Messages.DOWNLOADING_FILE;
                PgMng.Grow();
                _ftp.DownloadFile(Path.GetDirectoryName(CurrentDocumento.Ruta), DestinationPath, Path.GetFileName(CurrentDocumento.Nombre));
                PgMng.Message = Resources.Messages.CLOSING_SERVER;
                PgMng.Grow();
				PgMng.Result = BGResult.OK;
            }
            else
            {
				PgMng.Result = BGResult.Error;
                throw new iQException(Resources.Errors.FILE_NOT_FOUND);
            }
        }

        private void DoUpload(BackgroundWorker bk)
        {
            PgMng.Grow(Resources.Messages.CONNECTING_SERVER);

            if (_ftp == null)
            {
				_ftp = new FtpClient(Principal.GetHipatiaFTPHost(), Principal.GetHipatiaFTPUser(), Principal.GetHipatiaFTPPwd(), Principal.GetHipatiaFTPRootPath());
				HipatiaCtl.InitHipatia(_ftp, Principal.GetHipatiaFTPRootPath(), Principal.GetHipatiaFTPHost());
            }
            PgMng.Grow();

			if (HipatiaCtl.ExistsDirectory(_ftp, CurrentDocumento.Ruta))
            {
                PgMng.Message = Resources.Messages.UPLOADING_FILE;
                PgMng.Grow();
				HipatiaCtl.Copy(_ftp, TempFile, CurrentDocumento.Ruta);
                PgMng.Message = Resources.Messages.CLOSING_SERVER;
                PgMng.Grow();
				PgMng.Result = BGResult.OK;
            }
            else
            {
				PgMng.Result = BGResult.Error;
                throw new iQException(Resources.Messages.OPERATION_ERROR);
            }
        }

        private void DoDelete(BackgroundWorker bk)
        {
            PgMng.Grow(Resources.Messages.CONNECTING_SERVER);

            if (_ftp == null)
            {
				_ftp = new FtpClient(Principal.GetHipatiaFTPHost(), Principal.GetHipatiaFTPUser(), Principal.GetHipatiaFTPPwd(), Principal.GetHipatiaFTPRootPath());
				HipatiaCtl.InitHipatia(_ftp, Principal.GetHipatiaFTPRootPath(), Principal.GetHipatiaFTPHost());
            }
            PgMng.Grow();

            _entity.Documentos.RemoveAgent(CurrentDocumento);

            PgMng.Grow();
  
            if (CurrentDocumento.AgenteDocumentos.Count == 1)
            {
                if (ProgressInfoMng.ShowQuestion(Resources.Messages.DELETE_LAST_FILE) == DialogResult.No)
                {
                    try
                    {
                        Documento.Delete(CurrentDocumento.OidDocumento);
                        PgMng.Grow();
						
						if (HipatiaCtl.ExistsFile(_ftp, CurrentDocumento.Ruta))
							_ftp.DeleteFile(CurrentDocumento.Ruta);
                        PgMng.Grow();
                    }
                    catch (Exception ex)
                    {
						PgMng.Result = BGResult.Error;
                        throw ex;
                    }
                }
            }

			PgMng.Result = BGResult.OK;
        }

        #endregion

        #region Style & Source

        public override void FormatControls()
        {
            base.FormatControls();
        }

        protected override void RefreshMainData()
        {
            Datos.DataSource = _entity;
            Bar.Grow();

            _documentos = DocumentoList.GetListByAgente(_entity);
            Datos_Documentos.DataSource = _documentos;
            Bar.Grow();

            base.RefreshMainData();
        }

        public override void RefreshSecondaryData()
        {
            if (_entity != null)
            {
                _documentos = DocumentoList.GetListByAgente(_entity);
                Datos_Documentos.DataSource = _documentos;
            }
        }

        #endregion

        #region Validation & Format

        /// <summary>
        /// Valida datos de entrada
        /// </summary>
        protected override void ValidateInput()
        {

        }

        #endregion

        #region Actions

		public void DoJob()
		{
			PgMng.Result = BGResult.Working;

			try
			{
				switch (_back_job)
				{
					case BackJob.Download:
						DoDownload(null);
						break;

					case BackJob.Upload:
						DoUpload(null);
						break;

					case BackJob.Delete:
						DoDelete(null);
						break;
				}
			}
			catch (Exception ex)
			{
				PgMng.ShowInfoException(ex);
			}
		}

		protected override void SaveAction()
        {
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

        protected override void AddAction() 
        { 
            DocumentoNewInputForm f = new DocumentoNewInputForm(_entity, this);
            f.ShowDialog(this);
            RefreshSecondaryData();
        }
        
        protected override void AttachAction() 
        {
            DocumentoList list = DocumentoList.GetListExceptAgente(_entity.Oid);
            DocumentoSelectForm f = new DocumentoSelectForm(this, _entity, list);
            f.ShowDialog();
            RefreshSecondaryData();
        }

        protected override void ViewAction()
        {
            if (CurrentDocumento == null) return;

            _destination_path = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            _back_job = BackJob.Download;
            PgMng.Reset(6, 1, Resources.Messages.CONNECTING_SERVER, this);
            //PgMng.StartBackJob(this);
			DoJob();

            PgMng.Grow();
            if (PgMng.Result == BGResult.OK)
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = TempFile;
                process.Start();
                PgMng.FillUp();
                try
                {
                    process.WaitForExit();
                }
                catch { }

                File.Delete(TempFile);
            }
            else
                PgMng.FillUp();
        }

        protected override void EditAction() 
        {
            if (CurrentDocumento == null) return;

            _destination_path = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            _back_job = BackJob.Download;
            PgMng.Reset(5, 1, Resources.Messages.CONNECTING_SERVER, this);
            //PgMng.StartBackJob(this);
			DoJob();

            if (PgMng.Result == BGResult.OK)
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = TempFile;
                PgMng.FillUp();
                process.Start();
                try
                {
                    process.WaitForExit();
                }
                catch { }

                _back_job = BackJob.Upload;
                PgMng.Reset(5, 1);
                PgMng.Message = Resources.Messages.CONNECTING_SERVER;
                PgMng.ShowForm();
                PgMng.Grow();

                if (PgMng.Result == BGResult.OK)
                {
                    PgMng.StartBackJob(this);
                    PgMng.FillUp();

                    File.Delete(TempFile);
                }
                else
                    PgMng.FillUp();
            }
            else
                PgMng.FillUp();
        }

        protected override void ReplaceAction() 
        {
            if (CurrentDocumento == null) return;

            DocumentoNewInputForm f = new DocumentoNewInputForm(_entity, CurrentDocumento);
            f.ShowDialog();
            RefreshSecondaryData();
        }

        protected override void DeleteAction()
        {
            if (CurrentDocumento == null) return;

            if (ProgressInfoMng.ShowQuestion(Resources.Messages.DELETE_CONFIRM) == DialogResult.No) return;

            _back_job = BackJob.Delete;
            PgMng.Reset(9, 1, Resources.Messages.CONNECTING_SERVER, this);
            //PgMng.StartBackJob(this);
			DoJob();

            PgMng.Grow();
            if (PgMng.Result == BGResult.OK)
            {
                _entity.ApplyEdit();
                _entity.Save();
            }

            RefreshSecondaryData();
            PgMng.FillUp();
        }

        protected override void DownloadAction() 
        {
            if (CurrentDocumento == null) return;

            if (Browser.ShowDialog() == DialogResult.OK)
            {
                _destination_path = Browser.SelectedPath;
                _back_job = BackJob.Download;
                PgMng.Reset(5, 1, Resources.Messages.CONNECTING_SERVER, this);
                //PgMng.StartBackJob(this);
				DoJob();
                PgMng.FillUp();
            }
        }

		protected override void SendEmailAction()
		{
			if (CurrentDocumento == null) return;

			_destination_path = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

			_back_job = BackJob.Download;
			PgMng.Reset(6, 1, Resources.Messages.CONNECTING_SERVER, this);
			//PgMng.StartBackJob(this);
			DoJob();

			PgMng.Grow();
			if (PgMng.Result == BGResult.OK)
			{
				PgMng.Grow(Face.Resources.Messages.SENDING_EMAIL);

				System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

				//mail.To.Add(new MailAddress(cliente.Email, cliente.Nombre));
                mail.From = new MailAddress(SettingsMng.Instance.GetSMTPMail(), AppContext.ActiveSchema.Name);
				mail.Body = String.Format(Resources.Messages.EMAIL_ATTACHMENT_BODY, AppContext.ActiveSchema.Name);
				mail.Subject = Resources.Messages.EMAIL_SUBJECT;
				mail.Attachments.Add(new Attachment(TempFile));

				PgMng.FillUp();
				try
				{
					EMailClient.Instance.SmtpCliente.Send(mail);
					PgMng.ShowInfoException(Resources.Messages.EMAIL_SUCCESS);
				}
				catch { }

				File.Delete(TempFile);
			}
			else
				PgMng.FillUp();
		}

        private void Aplicar_BT_Click(object sender, EventArgs e)
        {
            PgMng.Reset(BarSteps, 1);

            if (_ftp == null)
            {
				_ftp = new FtpClient(Principal.GetHipatiaFTPHost(), Principal.GetHipatiaFTPUser(), Principal.GetHipatiaFTPPwd(), Principal.GetHipatiaFTPRootPath());
				HipatiaCtl.InitHipatia(_ftp, Principal.GetHipatiaFTPRootPath(), Principal.GetHipatiaFTPHost());
            }

			if (!HipatiaCtl.ExistsAgentDirectory(_ftp, _entity.GetInfo(false)))
                HipatiaCtl.CreateAgentDirectory(_ftp, _entity.GetInfo(false));

            //_ftp.Close();

            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;

            PgMng.FillUp();
        }

        #endregion
    }
}

