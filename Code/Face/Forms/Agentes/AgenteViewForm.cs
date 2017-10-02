using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library.Hipatia;
using moleQule.Face.Hipatia.Resources;

namespace moleQule.Face.Hipatia
{
	public partial class AgenteViewForm : moleQule.Face.Hipatia.AgenteForm
	{
        #region Business Methods

        protected override int BarSteps { get { return base.BarSteps + 2; } }

        /// <summary>
        /// Se trata de la Hipatia actual y que se va a editar.
        /// </summary>
        private AgenteInfo _entity;

        public override AgenteInfo EntityInfo { get { return _entity; } }

        #endregion

        #region Factory Methods

        public AgenteViewForm(long oid)
            : base(oid)
        {
            InitializeComponent();
            //Codigo_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Codigo", true));
			SetFormData();
            this.Text =  Labels.AGENTE_DETAIL_TITLE + " " + EntityInfo.Nombre.ToUpper();
            _mf_type = ManagerFormType.MFView;

		}

        protected override void GetFormSourceData(long oid)
        {
            _entity = AgenteInfo.Get(oid, true);
        }

        #endregion

        #region Style & Source

        /// <summary>Formatea los Controles del formulario
        /// <returns>void</returns>
        /// </summary>
        public override void FormatControls()
        {
			SetReadOnlyControls(this.Controls);
            Cancel_BT.Enabled = false;
            Cancel_BT.Visible = false;
            Tools_TS.Enabled = false;
			base.FormatControls();
        }

        /// <summary>
        /// Asigna el objeto principal al origen de datos 
        /// <returns>void</returns>
        /// </summary>
        protected override void RefreshMainData()
        {
			Datos.DataSource = _entity;
            Bar.Grow();

            DocumentoList documentos = DocumentoList.GetListByAgente(_entity);
            Datos_Documentos.DataSource = documentos;
		    Bar.Grow();

			base.RefreshMainData();
        }

        #endregion

		#region Validation & Format

		#endregion

        #region Actions

        protected override void SaveAction() { _action_result = DialogResult.Cancel; }

        #endregion

        #region Events

        #endregion
	}
}

