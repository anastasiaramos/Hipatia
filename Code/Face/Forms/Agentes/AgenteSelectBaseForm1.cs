using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Csla;
using CslaEx;

using moleQule.Face;
using moleQule.Library;

using moleQule.Library.Hipatia;
using moleQule.Face.Hipatia.Resources;

namespace moleQule.Face.Hipatia
{
    public partial class AgenteSelectBaseForm : moleQule.Face.Skin03.EntityMngSkinForm
    {
        #region Attributes & Properties

        protected override int BarSteps { get { return base.BarSteps + 3; } }

        /// <summary>
        ///  Lista de objetos de sólo lectura
        /// </summary>
        private IAgenteHipatiaList _list;

        /// <summary>
        ///  Lista resultado del filtro
        /// </summary>
        private new IAgenteHipatiaList _filter_results = null;

        /// <summary>
        ///  Lista resultado de la busqueda
        /// </summary>
        private new IAgenteHipatiaList _search_results = null;

        /// <summary>
        /// Devuelve el OID del objeto activo seleccionado de la tabla
        /// </summary>
        /// <returns></returns>
        public override long ActiveOID { get { return Datos.Current != null ? ((IAgenteHipatia)Datos.Current).Oid : -1; } }

        /// <summary>
        /// Devuelve el OID del objeto activo seleccionado de la tabla
        /// </summary>
        /// <returns></returns>
        public IAgenteHipatia ActiveItem { get { return Datos.Current != null ? Datos.Current as IAgenteHipatia: null; } }

        public override long ActiveFoundOID { get { return (DatosSearch.Current != null) ? ((IAgenteHipatia)(DatosSearch.Current)).Oid : -1; } }

        public override string SortProperty { get { return this.GetGridSortProperty(Tabla); } }
        public override ListSortDirection SortDirection { get { return this.GetGridSortDirection(Tabla); } }

        public EntidadInfo CurrentEntidad { get { return Datos_Entidades.Current != null ? Datos_Entidades.Current as EntidadInfo : null; } } 

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
                if (item.OidEntidad == CurrentEntidad.Oid
                    && item.OidAgenteExt == ActiveItem.Oid)
                    return true;
            }

            return false;
        }

        #endregion

        #region Factory Methods

        public AgenteSelectBaseForm()
            : this(false, null) { }

        public AgenteSelectBaseForm(Form parent)
            : this(true, parent) {}

        public AgenteSelectBaseForm(bool isModal, Form parent)
            : base(isModal, parent)
        {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;

            // Parche para poder abrir el formulario en modo diseño y no perder la configuracion de columnas
            DatosLocal_BS = Datos;
            Tabla.DataSource = DatosLocal_BS;
        }

        #endregion

        #region Autorizacion

        /// <summary>Aplica las reglas de validación de usuarios al formulario.
        /// <returns>void</returns>
        /// </summary>
        protected override void ApplyAuthorizationRules()
        {
            Tabla.Visible = Agente.CanGetObject();
            Add_Button.Enabled = Agente.CanAddObject();
            Edit_Button.Enabled = Agente.CanEditObject();
            Delete_Button.Enabled = Agente.CanDeleteObject();
            View_Button.Enabled = Agente.CanGetObject();
        }

        #endregion

        #region Style & Source

        /// <summary>Formatea los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            base.FormatControls();

            Tool_Strip.Visible = false;
            PanelesV.Panel1MinSize = 50;
            PanelesV.SplitterDistance = 50;
            Letras_Panel.Top = (PanelesV.SplitterDistance - Letras_Panel.Height) / 2;

            List<DataGridViewColumn> cols = new List<DataGridViewColumn>();
            Nombre.Tag = 0.4;
            Observaciones.Tag = 0.6;

            cols.Add(Nombre);
            cols.Add(Observaciones);

            ControlsMng.MaximizeColumns(Tabla, cols);

            ControlsMng.OrderByColumn(Tabla, Nombre, ListSortDirection.Ascending);
            ControlsMng.MarkGridColumn(Tabla, ControlsMng.GetCurrentColumn(Tabla));
            
            Fields_CB.Text = Nombre.HeaderText;
            SetGridFormat();
        }

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData()
        {
            PgMng.Grow(string.Empty, "Agentes");

            long oid = ActiveOID;

            _list = GetAgentes(CurrentEntidad);
            Datos.DataSource = _list;

            DatosSearch.DataSource = Datos.DataSource;

            Fields_CB.DataSource = Tabla.Columns;
            Fields_CB.DisplayMember = "HeaderText";
            Fields_CB.ValueMember = "DataPropertyName";

            if (oid > 0) Select(oid);
        }

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        public override void RefreshSecondaryData()
        {
            Datos_Entidades.DataSource = EntidadList.GetList(true);
        }

        /// <summary>
        /// Selecciona un elemento de la tabla
        /// </summary>
        /// <param name="oid">Identificar del elemento</param>
        protected override void Select(long oid)
        {
            int foundIndex = Datos.IndexOf(_list[_list.IndexOf(Datos.Current as IAgenteHipatia)]);
            Datos.Position = foundIndex;
        }

        /// <summary>
        /// Filtra la tabla
        /// </summary>
        /// <param name="oid">Identificar del elemento</param>
        protected override void SetFilter(bool on)
        {
            DataGridViewColumn col = ControlsMng.GetCurrentColumn(Tabla);

            try
            {
                _list.ApplySort(SortProperty, SortDirection);
                Datos.DataSource = on ? _filter_results : _list;
            }
            catch (Exception)
            {
                _list.ApplySort(SortProperty, SortDirection);
                Datos.DataSource = _list;
            }

            ControlsMng.OrderByColumn(Tabla, col, SortDirection);
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
            if (CurrentEntidad == null)
            {
                MessageBox.Show("Debe seleccionar un tipo de entidad.", Resources.Labels.ADVISE_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ActiveItem == null)
            {
                MessageBox.Show("Debe seleccionar un agente.", Resources.Labels.ADVISE_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (AgenteExists())
            {
                MessageBox.Show("Ya existe un agente asociado a la entidad seleccionada.", Resources.Labels.ADVISE_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            base.SelectObject();
        }

        protected override bool DoFind(object value)
        {
            _search_results = Localize(value, ((DataGridViewColumn)(Fields_CB.SelectedItem)).Name);
            return _search_results != null;
        }

        protected override bool DoFilter(object value)
        {
            _filter_results = Localize(value, ((DataGridViewColumn)(Fields_CB.SelectedItem)).Name);
            return _filter_results != null;
        }

        protected override bool DoFilterByFirst(string value, string column_name)
        {
            if (column_name == null)
                column_name = ControlsMng.GetCurrentColumn(Tabla).Name;

            _filter_results = Localize(value, column_name);
            return _filter_results != null;
        }

        protected new IAgenteHipatiaList Localize(object value, string column_name)
        {
            IAgenteHipatiaList list = null;

            if (_list == null)
            {
                MessageBox.Show(Messages.NO_RESULTS);
                return null;
            }

            FCriteria criteria = null;
            string related = "none";

            switch (column_name)
            {
                default:
                    {
                        criteria = GetCriteria(column_name, value, _operation);
                    } break;
            }

            switch (related)
            {
                case "none":
                    {
                        list = _list.GetSortedSubList(criteria, SortProperty, SortDirection);
                    } break;
            }

            if (list.Count == 0)
            {
                MessageBox.Show(Messages.NO_RESULTS);
                return null;
            }

            DatosSearch.DataSource = list;
            DatosSearch.MoveFirst();

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
            ControlsMng.MarkGridColumn(Tabla, ControlsMng.GetCurrentColumn(Tabla));
            Fields_CB.Text = ControlsMng.GetCurrentColumn(Tabla).HeaderText;
            SetGridFormat();
        }

        private void Tabla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ControlsMng.MarkGridColumn(Tabla, ControlsMng.GetCurrentColumn(Tabla));
            Fields_CB.Text = ControlsMng.GetCurrentColumn(Tabla).HeaderText;
        }

        private void Datos_Entidades_CurrentChanged(object sender, EventArgs e)
        {
            if (Datos_Entidades.Current != null)
            {
                TipoEntidadList tipos = TipoEntidadList.GetList(false);

                if (!tipos.GetItem(CurrentEntidad.Tipo).UserCreated)
                {
                    RefreshMainData();
                    ControlsMng.OrderByColumn(Tabla, Nombre, ListSortDirection.Ascending);
                    ControlsMng.MarkGridColumn(Tabla, ControlsMng.GetCurrentColumn(Tabla));
                    Fields_CB.Text = Nombre.HeaderText;
                }
                else
                    Datos.DataSource = null;
            }
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

