using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Face;

namespace moleQule.Face.Hipatia
{
    /// <summary>
    /// Clase base para manejo (apertura y cierre) de formularios
    /// Es único en el sistema (singleton)
    /// </summary>
    /// <remarks>
    /// Para utilizar el FormMng es necesario indicar cual será el MainForm padre de los formularios
    /// Este MainForm deberá ser un formulario heredado de MainFormBase
    /// </remarks>
	public class FormMng : IFormMng
    {
		#region Factory Methods

		/// <summary>
		/// Única instancia de la clase MainBaseForm (Singleton)
		/// </summary>
		protected static FormMng _main;

		/// <summary>
		/// Unique FormMng Class Instance
		/// </summary>
		/// <remarks>
		/// Para utilizar el FormMng es necesario inicializar el MainForm padre de los formularios
		/// </remarks>
		public static FormMng Instance { get { return (_main != null) ? _main : new FormMng(); } }

		/// <summary>
		/// Constructor
		/// </summary>
		public FormMng()
		{
			// Singleton
			_main = this;
		}

		#endregion

        #region Business Methods

		/// <summary>
		/// Abre un nuevo manager para la entidad. Si no está abierto, lo crea, y si 
		/// lo está, lo muestra 
		/// </summary>
		/// <param name="formID">Identificador del formulario que queremos abrir</param>
		public void OpenForm(string formID) { OpenForm(formID, null); }
		public void OpenForm(string formID, object param) { OpenForm(formID, new object[1] { param }); }

        /// <summary>
        /// Abre un nuevo manager para la entidad. Si no está abierto, lo crea, y si 
        /// lo está, lo muestra 
        /// </summary>
        /// <param name="formID">Identificador del formulario que queremos abrir</param>
        /// <param name="param">Parámetro para el formulario</param>
        public void OpenForm(string formID, object[] parameters, Form parent)
        {
            try
            {
                switch (formID)
                {
                    case DocumentoMngForm.ID:
                        {
							if (!FormMngBase.Instance.BuscarFormulario(DocumentoMngForm.Type))
							{
								DocumentoMngForm em = new DocumentoMngForm(parent);
								FormMngBase.Instance.ShowFormulario(em);
							}
                        }
                        break;

					default:
						{
							throw new iQImplementationException(string.Format(moleQule.Face.Resources.Messages.FORM_NOT_FOUND, formID), string.Empty);
						} 
                }
#if TRACE
                Globals.Instance.ProgressInfoMng.ShowCronos();
                MessageBox.Show(Globals.Instance.Timer.GetCronos());
                //if (cform != null) cform.Bar.ShowCronos();
#endif
            }
			catch (iQImplementationException ex)
			{
				throw ex;
			}
			catch (Exception ex)
			{
				MessageBox.Show(iQExceptionHandler.GetAllMessages(ex), Application.ProductName);
			}
        }

		/// <summary>
		/// Devuelve un formulario hijo del tipo pasado como parámetro
		/// </summary>
		/// <param name="childType">Tipo de formulario</param>
		public object GetFormulario(Type childType) { return FormMngBase.Instance.GetFormulario(childType); }

        #endregion
    }
}
