using System;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Face;
using moleQule.Library.Hipatia;

namespace moleQule.Face.Hipatia
{
	public partial class AgenteAddForm : moleQule.Face.Hipatia.AgenteUIForm
    {
        #region Factory Methods

		public AgenteAddForm(Type tipoEntidad, IAgenteHipatia agente_h)
			: this(tipoEntidad, agente_h, null) {}

		public AgenteAddForm(Type entityType, IAgenteHipatia agent, Form parent)
			: base(-1, parent)
		{
			InitializeComponent();

			EntidadInfo entity = EntidadInfo.Get(entityType);

			_entity.CopyFrom(entity, agent);
			_entity.Codigo = _entity.GetCode();
			SetFormData();

			_mf_type = ManagerFormType.MFAdd;
			this.Text = Resources.Labels.AGENTE_ADD_TITLE;
		}

		public AgenteAddForm(EntidadInfo entity, IAgenteHipatia agent, Form parent) 
			: base(-1, parent)
        {
            InitializeComponent();

			_entity.CopyFrom(entity, agent);
			_entity.Codigo = _entity.GetCode();
			SetFormData();

            _mf_type = ManagerFormType.MFAdd;
            this.Text =  Resources.Labels.AGENTE_ADD_TITLE;
		}

		protected override void GetFormSourceData()
		{
            _entity = Agente.New();
            _entity.CloseSessions = false;
            _entity.BeginEdit();
		}

		#endregion

		#region Buttons

		#endregion	
	}
}

