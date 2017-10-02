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
	public partial class AgenteMngBaseForm : AgenteMngBaseBaseForm
	{
		#region Attributes & Properties

		protected override int BarSteps { get { return base.BarSteps + 4; } }
        public override Type EntityType { get { return typeof(Agente); } }

		protected Agente _entity;
		TipoEntidadList _tipos;

		public EntidadInfo CurrentEntidad { get { return Entidades_TV.SelectedNode != null ? Entidades_TV.SelectedNode.Tag as EntidadInfo : null; } }

		#endregion

		#region Factory Methods

		public AgenteMngBaseForm()
			: this(false) { }

		public AgenteMngBaseForm(string schema)
			: this(false, null, schema) { }

		public AgenteMngBaseForm(bool isModal)
			: this(isModal, null) { }

		public AgenteMngBaseForm(bool isModal, Form parent)
			: this(isModal, parent, string.Empty) { }

		public AgenteMngBaseForm(bool isModal, Form parent, string schema)
			: base(isModal, parent, null)
		{
			InitializeComponent();

			SetView(molView.Normal);

			// Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
			DatosLocal_BS = Datos;
			Tabla.DataSource = DatosLocal_BS;

			SetMainDataGridView(Tabla);
			Datos.DataSource = AgenteList.NewList().GetSortedList();
			SortProperty = Nombre.DataPropertyName;

			base.SortProperty = Nombre.DataPropertyName;
		}

		#endregion

		#region Business Methods

		protected override Type GetColumnType(string column_name)
		{
			return Tabla.Columns[column_name] != null ? Tabla.Columns[column_name].ValueType : null;
		}

		protected override string GetColumnProperty(string column_name)
		{
			return Tabla.Columns[column_name] != null ? Tabla.Columns[column_name].DataPropertyName : null;
		}

		#endregion

		#region Style

		public override void FitColumns()
		{
			List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
			Nombre.Tag = 0.4;
			Observaciones.Tag = 0.6;

			cols.Add(Nombre);
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
					
					HideAction(molAction.Copy);
					HideAction(molAction.Delete);
					HideAction(molAction.Print);	

					break;

				case molView.Normal:

					HideAction(molAction.Copy);
					HideAction(molAction.Delete);
					HideAction(molAction.Print);	

					break;
			}
		}

		#endregion

		#region Source

		protected override void RefreshMainData()
		{
			PgMng.Grow(string.Empty, "Agente");

			long oid = ActiveOID;

			if (CurrentEntidad != null)
				List = AgenteList.GetListByEntidad(CurrentEntidad);
			else
				List = AgenteList.GetList(false);

			PgMng.Grow(string.Empty, "Lista de Agentes");
		}

		public override void RefreshSecondaryData()
		{
			_tipos = TipoEntidadList.GetList(false);

			SortedBindingList<EntidadInfo> elist = EntidadList.GetSortedList(EntidadList.GetList(true), "Tipo", ListSortDirection.Ascending);

			foreach (EntidadInfo item in elist)
			{
				TreeNode node = new TreeNode();
				node.Name = item.Tipo;
				node.Text = item.Observaciones;
				node.Tag = item;
				node.ImageIndex = 1;
				node.SelectedImageIndex = 2;

				Entidades_TV.Nodes[0].Nodes.Add(node);
			}

			Entidades_TV.ExpandAll();
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
						AgenteList listA = AgenteList.GetList(_filter_results);
						listA.AddItem(_entity.GetInfo(false));
						_filter_results = listA.GetSortedList();
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
						AgenteList listD = AgenteList.GetList(_filter_results);
						listD.RemoveItem(ActiveOID);
						_filter_results = listD.GetSortedList();
					}
					break;
			}

			_entity = null;
			RefreshSources();
		}

		#endregion

		#region Actions

		public override void OpenViewForm() { AddForm(new AgenteViewForm(ActiveOID)); }

		public override void OpenEditForm()
		{
			AgenteEditForm form = new AgenteEditForm(ActiveOID, this);

			if (form.Entity != null)
			{
				AddForm(form);
				_entity = form.Entity;
			}
		}

		public override void DeleteAction()
		{
			Agente.Delete(ActiveOID);
			_action_result = DialogResult.OK;
		}

		#endregion

		#region Events

		private void Entidades_TV_AfterSelect(object sender, TreeViewEventArgs e)
		{
			ExecuteAction(molAction.FilterOff);

			if (CurrentEntidad != null)
			{
				Fields_CB.Text = TipoEntidad.HeaderText;
				_search_value = CurrentEntidad.Tipo;
				_show_filter_msg = false;
				ExecuteAction(molAction.FilterOn);
			}

			/*if (!_tipos.GetItem(CurrentEntidad.Tipo).UserCreated)
			{
				RefreshMainData();
				RefreshSources();
				ControlsMng.OrderByColumn(Tabla, Nombre, ListSortDirection.Ascending);
				ControlsMng.MarkGridColumn(Tabla, ControlsMng.GetCurrentColumn(Tabla), ControlTools.Instance.HeaderSelectedStyle);
				Fields_CB.Text = Nombre.HeaderText;
			}
			else
			{
				Datos.DataSource = null;
			}*/
		}

		#endregion
	}

	public partial class AgenteMngBaseBaseForm : Skin07.EntityMngSkinForm<AgenteList, AgenteInfo>
	{
		public AgenteMngBaseBaseForm()
			: this(false, null, null) { }

		public AgenteMngBaseBaseForm(bool isModal, Form parent, AgenteList lista)
			: base(isModal, parent, lista) { }
	}
}
