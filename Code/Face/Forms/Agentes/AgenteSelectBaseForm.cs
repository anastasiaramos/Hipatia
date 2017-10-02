using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Csla;
using moleQule.Library;
using moleQule.Library.CslaEx;
using moleQule.Library.Hipatia;
using moleQule.Face;

namespace moleQule.Face.Hipatia
{
    public partial class AgenteSelectBaseForm : Skin04.EntityMngSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 3; } }

        public const string ID = "AgenteSelectBaseForm";
        public static Type Type { get { return typeof(AgenteSelectBaseForm); } }
        public override Type EntityType { get { return typeof(Agente); } }

        protected IAgenteHipatia _entity;
		public EntidadInfo _entidad;

        private new SortedBindingList<IAgenteHipatia> _filter_results = null;
        private new SortedBindingList<IAgenteHipatia> _sorted_list = null;
        private new SortedBindingList<IAgenteHipatia> _search_results = null;

        /// <summary>
        ///  Lista de objetos de sólo lectura
        /// </summary>
        internal new IAgenteHipatiaList List
        {
            get { return _item_list as IAgenteHipatiaList; }
            set { _item_list = value; _sorted_list = (value as IAgenteHipatiaList); }
        }
        internal new SortedBindingList<IAgenteHipatia> SortedList { get { return _sorted_list; } }
        internal new IAgenteHipatiaList FilteredList { get { return IAgenteHipatiaList.GetList(_filter_results); } }
        internal SortedBindingList<IAgenteHipatia> CurrentList { get { return (Datos.List as SortedBindingList<IAgenteHipatia>); } }

        /// <summary>
        /// Devuelve el OID del objeto activo seleccionado de la tabla
        /// </summary>
        /// <returns></returns>
        public override long ActiveOID { get { return Datos.Current != null ? ((IAgenteHipatia)Datos.Current).Oid : -1; } }

        /// <summary>
        /// Devuelve el OID del objeto activo seleccionado de la tabla
        /// </summary>
        /// <returns></returns>
        public IAgenteHipatia ActiveItem { get { return Datos.Current != null ? Datos.Current as IAgenteHipatia : null; } }

        public override long ActiveFoundOID { get { return (DatosSearch.Current != null) ? ((IAgenteHipatia)(DatosSearch.Current)).Oid : -1; } }

        public override string SortProperty { get { return this.GetGridSortProperty(Tabla); } }
        public override ListSortDirection SortDirection { get { return this.GetGridSortDirection(Tabla); } }

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

        protected virtual long GetOid() { throw new iQImplementationException("AgenteSelectBaseForm::GetOid()"); }

        private bool AgenteExists()
        {
            AgenteList agentes = AgenteList.GetList(false);

            foreach (AgenteInfo item in agentes)
            {
                if ((item.OidEntidad == _entidad.Oid) && 
					(item.OidAgenteExt == ActiveItem.Oid))
                    return true;
            }

            return false;
        }

        #endregion

        #region Factory Methods

        public AgenteSelectBaseForm()
            : this(false) { }

        public AgenteSelectBaseForm(string schema)
            : this(null, false, null, schema) { }

        public AgenteSelectBaseForm(bool isModal)
            : this(isModal, null) { }

        public AgenteSelectBaseForm(bool isModal, Form parent)
            : this(null, isModal, parent, string.Empty) { }

        public AgenteSelectBaseForm(EntidadInfo entidad, bool isModal, Form parent)
            : this(entidad, isModal, parent, string.Empty) { }

        public AgenteSelectBaseForm(EntidadInfo entidad, bool isModal, Form parent, string schema)
            : base(isModal, parent)
        {
            InitializeComponent();

            SetView(molView.Select);
            _entidad = entidad;

            // Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
            DatosLocal_BS = Datos;
            Tabla.DataSource = DatosLocal_BS;

            SetMainDataGridView(Tabla);

            base.SortProperty = Nombre.DataPropertyName;
        }

        #endregion

        #region Style & Source

        /// <summary>Formatea los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            base.FormatControls();

            HideAction(molAction.Add);
            HideAction(molAction.Edit);
            HideAction(molAction.View);
            HideAction(molAction.Print);
            HideAction(molAction.Copy);
            HideAction(molAction.PrintDetail);
            HideAction(molAction.PrintListQR);
            HideAction(molAction.SelectAll);

            List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
            Nombre.Tag = 0.4;
            Observaciones.Tag = 0.6;

            cols.Add(Nombre);
            cols.Add(Observaciones);

            ControlsMng.MaximizeColumns(Tabla, cols);

            ControlsMng.OrderByColumn(Tabla, Nombre, ListSortDirection.Ascending);
            ControlsMng.MarkGridColumn(Tabla, ControlsMng.GetCurrentColumn(Tabla), ControlTools.Instance.HeaderSelectedStyle);

            Fields_CB.Text = ControlsMng.GetCurrentColumn(Tabla).HeaderText;
        }

        #endregion

        #region Source

        protected void SetMainList(SortedBindingList<IAgenteHipatia> list, bool order)
        {
            base.SortProperty = SortProperty;
            base.SortDirection = SortDirection;

            int currentColumn = (Tabla.CurrentCell != null) ? Tabla.CurrentCell.ColumnIndex : -1;

            Datos.DataSource = list;
            Datos.ResetBindings(true);

            if (order)
            {
                ControlsMng.OrderByColumn(Tabla, Tabla.Columns[base.SortProperty], base.SortDirection, true);
            }

            if (currentColumn != -1)
            {
                ControlsMng.SetCurrentCell(Tabla, currentColumn);
            }
        }

        protected override void RefreshMainData()
        {
            PgMng.Grow(string.Empty, "Agente");

            long oid = ActiveOID;

            List = GetAgentes(_entidad);

            PgMng.Grow(string.Empty, "Lista de Agentes");
        }
        
        protected override void RefreshSources()
        {
            switch (FilterType)
            {
                case IFilterType.None:
                    SetMainList(_sorted_list, true);
                    PgMng.Grow(string.Empty, "Ordenar Lista");
                    break;

                case IFilterType.Filter:
                    SetMainList(_filter_results, true);
                    PgMng.Grow(string.Empty, "Ordenar Lista");
                    break;
            }
            base.RefreshSources();
        }

        public override void UpdateList()
        {
            /*switch (_current_action)
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
            }*/

            _entity = null;
            RefreshSources();
        }

        /// <summary>
        /// Selecciona un elemento de la tabla
        /// </summary>
        /// <param name="oid">Identificar del elemento</param>
        protected override void Select(long oid)
        {
            int foundIndex = Datos.IndexOf(List[List.IndexOf(Datos.Current as IAgenteHipatia)]);
            Datos.Position = foundIndex;
        }

        /// <summary>
        /// Filtra la tabla
        /// </summary>
        /// <param name="oid">Identificar del elemento</param>
        protected override void SetFilter(bool on)
        {
            try
            {
                SetMainList(on ? _filter_results : _sorted_list, true);
            }
            catch (Exception)
            {
                SetMainList(_sorted_list, true);
            }

            base.SetFilter(on);
        }

        protected virtual IAgenteHipatiaList GetAgentes(EntidadInfo entidad) { throw new iQImplementationException("SelectAgenteInputBaseForm::GetAgentes()"); }

        public virtual void ShowFields() { }

        #endregion

        #region Actions

        /// <summary>
        /// Accion por defecto. Se usa para el Double_Click del Grid
        /// <returns>void</returns>
        /// </summary>
        protected override void DefaultAction() { ExecuteAction(molAction.Select); }

        public override void SelectObject()
        {
            if (_entidad == null)
            {
				PgMng.ShowInfoException(Library.Hipatia.Resources.Messages.NO_ENTIDAD_SELECTED);
                _action_result = DialogResult.Ignore;
                return;
            }
            if (ActiveItem == null)
            {
                MessageBox.Show("Debe seleccionar un agente.", Resources.Labels.ADVISE_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _action_result = DialogResult.Ignore;
                return;
            }

            if (AgenteExists())
            {
                MessageBox.Show("Ya existe un agente asociado a la entidad seleccionada.", Resources.Labels.ADVISE_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _action_result = DialogResult.Ignore;
                return;
            }

            base.SelectObject();
        }

        protected override bool DoFind(object value)
        {
            FilterItem fItem = new FilterItem();
            fItem.Column = ((DataGridViewColumn)(Fields_CB.SelectedItem)).Name;
            fItem.Value = value;
            fItem.FilterProperty = FilterProperty;
            fItem.Operation = _operation;
            _search_results = Localize(fItem);
            return _search_results != null;
        }

        protected override bool DoFilter(FilterItem fItem)
        {
            _filter_results = Localize(fItem);
            return _filter_results != null;
        }

        protected new SortedBindingList<IAgenteHipatia> Localize(FilterItem item)
        {
            SortedBindingList<IAgenteHipatia> list = null;
            IAgenteHipatiaList sourceList = null;

            switch (FilterType)
            {
                case IFilterType.None:
                    if (List == null)
                    {
                        MessageBox.Show(Face.Resources.Messages.NO_RESULTS);
                        return null;
                    }
                    sourceList = List;
                    break;

                case IFilterType.Filter:
                    if (FilteredList == null)
                    {
                        MessageBox.Show(Face.Resources.Messages.NO_RESULTS);
                        return null;
                    }
                    sourceList = FilteredList;
                    break;
            }

            if (item.FilterProperty == IFilterProperty.All)
            {
                FCriteria criteria = GetCriteria(string.Empty, item.Value, item.SecondValue, item.Operation);
                list = sourceList.GetSortedSubList(criteria, _properties_list);
            }
            else
            {
                FCriteria criteria = GetCriteria(item.Column, item.Value, item.SecondValue, item.Operation);
                list = sourceList.GetSortedSubList(criteria, _properties_list);
            }

            if (list.Count == 0)
            {
                PgMng.ShowInfoException(Face.Resources.Messages.NO_RESULTS);
                return sourceList;
            }

            DatosSearch.DataSource = list;
            DatosSearch.MoveFirst();

            AddFilterItem(item);

            return list;
        }

        #endregion

        #region Events

        private void Tabla_KeyPress(object sender, KeyPressEventArgs e)
        {
            FilterByKey(e.KeyChar.ToString());
        }

        private void Tabla_DoubleClick(object sender, EventArgs e)
        {
            ExecuteAction(molAction.Default);
        }

        private void Tabla_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ControlsMng.SetCurrentCell(Tabla);
            ControlsMng.MarkGridColumn(Tabla, ControlsMng.GetCurrentColumn(Tabla), ControlTools.Instance.HeaderSelectedStyle);
            Fields_CB.Text = ControlsMng.GetCurrentColumn(Tabla).HeaderText;
        }

        private void Tabla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ControlsMng.MarkGridColumn(Tabla, ControlsMng.GetCurrentColumn(Tabla), ControlTools.Instance.HeaderSelectedStyle);
            Fields_CB.Text = ControlsMng.GetCurrentColumn(Tabla).HeaderText;
        }

        private void Datos_DataSourceChanged(object sender, EventArgs e)
        {
            if (ActiveItem != null)
            {
                ShowFields();

                if (Datos.Count > 0)
                    Datos.MoveFirst();
            }
        }

        #endregion
    }
}
