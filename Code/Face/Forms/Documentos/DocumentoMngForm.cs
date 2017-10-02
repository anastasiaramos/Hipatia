using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

using Csla;
using moleQule.Library;
using moleQule.Library.CslaEx;
using moleQule.Library.Hipatia;
using moleQule.Face;

namespace moleQule.Face.Hipatia
{
	public partial class DocumentoMngForm : DocumentoMngBaseForm
	{
		#region Attributes & Properties

		public const string ID = "DocumentoMngForm";
		public static Type Type { get { return typeof(DocumentoMngForm); } }
		public override Type EntityType { get { return typeof(Documento); } }

		protected override int BarSteps { get { return base.BarSteps + 4; } }

		protected Documento _entity;
		FtpClient _ftp;

		public EntidadInfo CurrentEntidad { get { return Entidades_TV.SelectedNode != null ? Entidades_TV.SelectedNode.Tag as EntidadInfo : null; } }
		public AgenteInfo CurrentAgente { get { return Entidades_TV.SelectedNode != null ? Entidades_TV.SelectedNode.Tag as AgenteInfo : null; } }

		#endregion

		#region Factory Methods

		public DocumentoMngForm()
			: this(false) { }

		public DocumentoMngForm(string schema)
			: this(false, null, null, schema) { }

		public DocumentoMngForm(bool isModal)
			: this(isModal, null, null, string.Empty) { }

		public DocumentoMngForm(Form parent)
			: this(false, parent, null, string.Empty) { }

		public DocumentoMngForm(bool isModal, Form parent, DocumentoList list, string schema)
			: base(isModal, parent, list)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla);
			Datos.DataSource = DocumentoList.NewList().GetSortedList();
			SortProperty = Nombre.DataPropertyName;

			_ftp = new FtpClient(Principal.GetHipatiaFTPHost(), Principal.GetHipatiaFTPUser(), Principal.GetHipatiaFTPPwd(), Principal.GetHipatiaFTPRootPath());
		}

		#endregion

		#region Style

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Nombre.Tag = 0.3;
			Agente.Tag = 0.3;
			Observaciones.Tag = 0.4;

			cols.Add(Nombre);
			cols.Add(Agente);
			cols.Add(Observaciones);

			ControlsMng.MaximizeColumns(Tabla, cols);
		}

		public override void FormatControls()
		{
			if (Tabla == null) return;

			base.FormatControls();
		}

		protected override void SetView(molView view)
		{
			base.SetView(view);

			switch (_view_mode)
			{
				case molView.Select:

					HideAction(molAction.Add);
					HideAction(molAction.Copy);
					HideAction(molAction.Edit);
					HideAction(molAction.Print);
					ShowAction(molAction.Save);

					break;

				case molView.Normal:
					
					HideAction(molAction.Add);
					HideAction(molAction.Copy);
					HideAction(molAction.Edit);
					HideAction(molAction.Print);
					ShowAction(molAction.Save);

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Documento");

			_selectedOid = ActiveOID;

			switch (DataType)
			{
				case EntityMngFormTypeData.Default:
					if (CurrentEntidad != null)
						List = DocumentoList.GetListByEntidad(CurrentEntidad);
					else
						List = DocumentoList.GetListByEntidad(null);
					break;

				case EntityMngFormTypeData.ByParameter:
					_sorted_list = List.GetSortedList();
					break;
			}

			PgMng.Grow(string.Empty, "Lista de Documentos");
		}

		public override void RefreshSecondaryData()
		{
			SortedBindingList<EntidadInfo> elist = EntidadList.GetSortedList(EntidadList.GetList(true), "Tipo", ListSortDirection.Ascending);
			List<AgenteList> agentes = new List<AgenteList>();

			foreach (EntidadInfo item in elist)
			{
				TreeNode node = new TreeNode();
				node.Name = item.Tipo;
				node.Text = item.Observaciones;
				node.Tag = item;
				node.ImageIndex = 1;
				node.SelectedImageIndex = 2;

				Entidades_TV.Nodes[0].Nodes.Add(node);

				agentes.Add(AgenteList.GetListByEntidad(item));
			}

			Entidades_TV.ExpandAll();

			int i = -1;

			foreach (TreeNode item in Entidades_TV.Nodes[0].Nodes)
			{
				i++;

				foreach (AgenteInfo agente in agentes[i])
				{
					TreeNode node = new TreeNode();
					node.Name = agente.Oid.ToString();
					node.Text = agente.Nombre;
					node.Tag = agente;
					node.ImageIndex = 1;
					node.SelectedImageIndex = 2;

					item.Nodes.Add(node);
				}
			}
		}

		public override void UpdateList()
		{
			switch (_current_action)
			{
				case molAction.Add:
					if (_entity == null) return;
					List.AddItem(_entity.GetInfo(false));
					if (FilterType == IFilterType.Filter)
					{
						DocumentoList list = DocumentoList.GetList(_filter_results);
						list.AddItem(_entity.GetInfo(false));
						_filter_results = list.GetSortedList();
					}
					break;

				case molAction.Edit:
				case molAction.Lock:
				case molAction.Unlock:
					if (_entity == null) return;
					ActiveItem.CopyFrom(_entity);
					break;

				case molAction.Delete:
					if (ActiveItem == null) return;
					List.RemoveItem(ActiveOID);
					if (FilterType == IFilterType.Filter)
					{
						DocumentoList list = DocumentoList.GetList(_filter_results);
						list.RemoveItem(ActiveOID);
						_filter_results = list.GetSortedList();
					}
					break;
			}

			_entity = null;
			RefreshSources();
		}

		#endregion

		#region Actions

		protected override void DefaultAction() { ExecuteAction(molAction.View); }

		public override void OpenViewForm()
		{
			try
			{
				string destinationPath = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
				string tempFile = destinationPath + "\\" + System.Web.HttpUtility.UrlEncode(ActiveItem.Nombre);

				PgMng.Reset(5, 1, Resources.Messages.CONNECTING_SERVER, this);

				HipatiaCtl.InitHipatia(_ftp, Principal.GetHipatiaFTPRootPath(), Principal.GetHipatiaFTPHost());
				PgMng.Grow();

				if (HipatiaCtl.ExistsFile(_ftp, ActiveItem.Ruta))
				{
					PgMng.Grow(Resources.Messages.DOWNLOADING_FILE);
					_ftp.DownloadFile(Path.GetDirectoryName(ActiveItem.Ruta), destinationPath, Path.GetFileName(ActiveItem.Nombre));
					PgMng.Grow(Resources.Messages.CLOSING_SERVER);

					if (File.Exists(tempFile))
					{
						System.Diagnostics.Process process = new System.Diagnostics.Process();
						process.StartInfo.FileName = tempFile;
						process.Start();
						PgMng.FillUp();
						process.WaitForExit();

						File.Delete(tempFile);
					}
					else
						PgMng.ShowInfoException(Resources.Errors.FILE_NOT_FOUND);
				}
				else
				{
					PgMng.Result = BGResult.Error;
					PgMng.ShowInfoException(Resources.Errors.FILE_NOT_FOUND);
				}
			}
			finally { PgMng.FillUp(); }
		}

		public override void DeleteObject(long oid)
		{
			if (ProgressInfoMng.ShowQuestion(moleQule.Face.Resources.Messages.DELETE_CONFIRM) == DialogResult.Yes)
			{
				if (ActiveItem.Shared)
				{
					if (ProgressInfoMng.ShowQuestion(Resources.Messages.DOC_SHARED) != DialogResult.OK)
					{
						_action_result = DialogResult.Cancel;
						return;
					}
				}

				try
				{
					PgMng.Reset(5, 1, Resources.Messages.CONNECTING_SERVER, this);

					HipatiaCtl.InitHipatia(_ftp, Principal.GetHipatiaFTPRootPath(), Principal.GetHipatiaFTPHost());

					PgMng.Grow(moleQule.Face.Resources.Messages.DELETING);

					Documento item = Documento.Get(ActiveItem.OidDocumento);
					item.AgenteDocumentos.Clear();
					item.Save();
					item.CloseSession();
					PgMng.Grow();

					Documento.Delete(ActiveItem.OidDocumento);
					PgMng.Grow();

					if (HipatiaCtl.ExistsFile(_ftp, ActiveItem.Ruta))
						_ftp.DeleteFile(ActiveItem.Ruta);
					PgMng.Grow();
				}
				catch (Exception ex)
				{
					_action_result = DialogResult.Cancel;
					throw ex;
				}
				finally
				{
					PgMng.FillUp();
				}

				_action_result = DialogResult.OK;								
			}
		}

		public override void DownloadAction()
		{
			PgMng.Reset(5, 1, Resources.Messages.CONNECTING_SERVER, this);

			HipatiaCtl.InitHipatia(_ftp, Principal.GetHipatiaFTPRootPath(), Principal.GetHipatiaFTPHost());
			PgMng.Grow();

			if (HipatiaCtl.ExistsDirectory(_ftp, ActiveItem.Ruta))
			{
				if (Browser.ShowDialog() == DialogResult.OK)
				{
					string destinationPath = Browser.SelectedPath;
					string tempFile = destinationPath + "\\" + System.Web.HttpUtility.UrlEncode(ActiveItem.Nombre);
					
					PgMng.Message = Resources.Messages.DOWNLOADING_FILE;
					PgMng.Grow();
					_ftp.DownloadFile(Path.GetDirectoryName(ActiveItem.Ruta), destinationPath, Path.GetFileName(ActiveItem.Nombre));
					PgMng.Message = Resources.Messages.CLOSING_SERVER;
					PgMng.Grow();
					PgMng.Result = BGResult.OK;
				}
			}
			else
			{
				PgMng.Result = BGResult.Error;
				throw new iQException(Resources.Errors.FILE_NOT_FOUND);
			}

			PgMng.FillUp();
		}

		#endregion

		#region Events

		private void Entidades_TV_AfterSelect(object sender, TreeViewEventArgs e)
		{
			ExecuteAction(molAction.FilterOff);

			switch (e.Node.Level)
			{
				case 1:
					if (CurrentEntidad != null)
					{
						Fields_CB.Text = Entidad.HeaderText;
						_search_value = CurrentEntidad.Tipo;
						_show_filter_msg = false;
						ExecuteAction(molAction.FilterOn);
					}
					break;

				case 2:
					if (CurrentAgente != null)
					{
						Fields_CB.Text = Agente.HeaderText;
						_search_value = CurrentAgente.Nombre;
						_show_filter_msg = false;
						ExecuteAction(molAction.FilterOn);
					}
					break;
			}
		}

		#endregion
	}

	public partial class DocumentoMngBaseForm : Skin07.EntityMngSkinForm<DocumentoList, DocumentoInfo>
	{
		public DocumentoMngBaseForm()
			: this(false, null, null) { }

		public DocumentoMngBaseForm(bool isModal, Form parent, DocumentoList lista)
			: base(isModal, parent, lista) { }
	}
}
