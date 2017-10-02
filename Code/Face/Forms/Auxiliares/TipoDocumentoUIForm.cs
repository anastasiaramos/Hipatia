using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Face.Skin01;

using moleQule.Library.Hipatia;
using moleQule.Face.Hipatia.Resources;

using moleQule.Face;

namespace moleQule.Face.Hipatia
{
    public partial class TipoDocumentoUIForm : ItemMngSkinForm
    {

        #region Business Methods

        public const string ID = "TipoDocumentoUIForm";
        public static Type Type { get { return typeof(TipoDocumentoUIForm); } }

        /// <summary>
        /// Se trata de la empresa actual y que se va a editar.
        /// </summary>
        private Tipodocumentos _tipo_documentos;

        public Tipodocumentos TipoDocumentos
        {
            get { return _tipo_documentos; }
            set { _tipo_documentos = value; }
        }

        #endregion

        #region Factory Methods

        public TipoDocumentoUIForm()
            : this(true)
        {
        }

        public TipoDocumentoUIForm(bool IsModal)
            : base(IsModal)
        {
            InitializeComponent();
            SetFormData();
            this.Text = Resources.Labels.TIPO_DOCUMENTO_TITLE;
        }

        protected override void GetFormSourceData()
        {
            _tipo_documentos = Tipodocumentos.GetList();
        }

        /// <summary>
        /// Guarda en la bd el objeto actual
        /// </summary>
        protected override bool SaveObject()
        {
            //using (StatusBusy busy = new StatusBusy(Resources.Messages.SAVING))
            //{
            this.Datos.RaiseListChangedEvents = false; ;

            // do the save
            try
            {
                _tipo_documentos.Save();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(Messages.OPERATION_ERROR + Environment.NewLine +
                                ex.Message,
                                Labels.APP_TITLE,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return false;
            }
            finally
            {
                RefreshMainData();
                this.Datos.RaiseListChangedEvents = true;
            }
            //}
        }

        #endregion

        #region Style & Source

        /// <summary>Formatea los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
            base.FormatControls();

            List<string> visibles = new List<string>();

            visibles.Add(UserCreated.Name);
            visibles.Add(Valor.Name);

            ControlTools.ShowDataGridColumns(Datos_Grid, visibles);

            VScrollBar vs = new VScrollBar();

            int rowWidth = (int)(Datos_Grid.Width - vs.Width
                                                - Datos_Grid.RowHeadersWidth
                                                - Datos_Grid.Columns[UserCreated.Name].Width);

            Datos_Grid.Columns[Valor.Name].Width = (int)(rowWidth * 0.995);
        }

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData()
        {
            Datos.DataSource = Tipodocumentos.SortList(_tipo_documentos, "Valor", ListSortDirection.Ascending);
            Bar.FillUp();
        }

        #endregion

        #region Buttons

        /// <summary>
        /// Implementa Save_button_Click
        /// </summary>
        protected override void SaveAction()
        {
            _action_result = SaveObject() ? DialogResult.OK : DialogResult.Ignore;
        }

        #endregion

        #region Events

        private void TipoDocumentoUIForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _tipo_documentos.CloseSession();
            Cerrar();
        }

        #endregion

    }
}