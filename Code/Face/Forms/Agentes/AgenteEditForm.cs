using System;
using System.Windows.Forms;

using moleQule.Library.Hipatia;
using moleQule.Face.Hipatia.Resources;
using moleQule.Face;

namespace moleQule.Face.Hipatia
{
	public partial class AgenteEditForm : moleQule.Face.Hipatia.AgenteUIForm
    {
        #region Factory Methods

		public AgenteEditForm(Type tipoEntidad, IAgenteHipatia agenteH)
			: this(tipoEntidad, agenteH, null) { }

		public AgenteEditForm(Type tipoEntidad, IAgenteHipatia agenteH, Form parent)
            : base(-1, parent)
        {
            InitializeComponent();
			_entity = Agente.Get(tipoEntidad, agenteH);
            _entity.BeginEdit();
            SetFormData();

            this.Text = Resources.Labels.AGENTE_EDIT_TITLE + " " + Entity.Nombre.ToUpper();
            _mf_type = ManagerFormType.MFEdit;
        }

        public AgenteEditForm(long oid, Form parent)
            : base(oid, parent)
		{
			InitializeComponent();
            if (Entity != null)
            {
                SetFormData();
                this.Text = Resources.Labels.AGENTE_EDIT_TITLE + " " + Entity.Nombre.ToUpper();
            }
            _mf_type = ManagerFormType.MFEdit;
        }

		protected override void GetFormSourceData()
		{
		}

        protected override void GetFormSourceData(long oid)
        {
            _entity = Agente.Get(oid);
            _entity.CloseSessions = false;
            _entity.BeginEdit();
        }

        #endregion

        #region Style & Source

        public override void FormatControls()
        {
            base.FormatControls();
        }

        #endregion

        #region Buttons

		#endregion 

		#region Events

		private void AgenteEditForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (_entity != null) _entity.CloseSession();
		}

		#endregion
	}
}

