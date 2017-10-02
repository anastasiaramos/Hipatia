using System;
using System.Windows.Forms;

using moleQule.Library.Hipatia;

namespace moleQule.Face.Hipatia
{
	public partial class DocumentoSelectForm : DocumentoMngForm
    {
        #region Attributes & Properties

        private Agente _agente = null; 

        #endregion

        #region Factory Methods

        public DocumentoSelectForm()
            : this(null, null) {}

        public DocumentoSelectForm(Agente agente)
            : this(null, agente) {}

        public DocumentoSelectForm(Form parent)
            : this(parent, null) {}
		
        public DocumentoSelectForm(Form parent, Agente agente)
            : this(parent, agente, null) {}

        public DocumentoSelectForm(Form parent, Agente agente, DocumentoList lista)
            : base(true, parent, lista, string.Empty)
        {
			InitializeComponent(); 
			_view_mode = molView.Select;

			_action_result = DialogResult.Cancel;

            _agente = agente;
        }

        #endregion

        #region Style & Source

        #endregion

        #region Actions

        /// <summary>
        /// Asocia un documento existente a un agente
        /// </summary>
        protected void AttachAction()
        {
            if (ActiveItem == null)
            {
                MessageBox.Show("Debe elegir un documento.", 
                                Resources.Labels.ADVISE_TITLE, 
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Warning);
                return;
            }

            AgenteDocumento agente_documento = _agente.Documentos.NewItem(_agente);
            agente_documento.OidDocumento = ActiveOID;

            _agente.ApplyEdit();
            _agente.Save();

            _action_result = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Accion por defecto. Se usa para el Double_Click del Grid
        /// <returns>void</returns>
        /// </summary>
        protected override void DefaultAction() 
        {
            if (_agente != null)
                AttachAction();
            else
                ExecuteAction(molAction.Select);
        }

        #endregion
    }
}
