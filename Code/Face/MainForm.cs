using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;

using moleQule.Library;
using moleQule.Face;
using moleQule.Library.Hipatia;

namespace moleQule.Face.Hipatia
{
    public partial class MainForm : moleQule.Face.MainBaseForm,
                                    moleQule.Library.IBackGroundLauncher
    {

        #region Attributes

        const bool DEBUG_TIME = false;

        //bool _decimalPressed = false;

        #endregion

        #region Business Methods

		public override void AutoPilot(bool log)
		{
			//Controler.AutoPilot();
		}

        public override void SetFormSkin()
        {
            int pos = Application.ProductVersion.IndexOf(".", 3);
            string version = Application.ProductVersion;
            this.Text = Application.ProductName + " " + version;
            if (AppContext.ActiveSchema != null)
                this.Text = this.Text + " - " + AppContext.ActiveSchema.Name;

            Leyenda_SL.Text = Resources.Labels.LEYENDA;
            Status_Label.Text = Resources.Labels.POWEREDBY;
        }

        /// <summary>
        /// Reacciona ante la pulsación de teclas
        /// </summary>
        /// <param name="key_code">Código de la tecla pulsada</param>
        protected void KeysDriver(Keys key_code) { }

        #endregion

        #region IBackGroundLauncher

        bool _finished = false;
        string _param = string.Empty;
        BGResult _result = BGResult.Working;

        /// <summary>
        /// La llama el backgroundworker para avisar que ha terminado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool Finished { get { return _finished; } set { _finished = value; } }
        public BGResult Result { get { return _result; } set { _result = value; } }

        /// <summary>
        /// La llama el backgroundworker para ejecutar codigo en segundo plano
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BackGroundJob()
        {
            try
            {
                
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        /// <summary>
        /// La llama el backgroundworker para ejecutar codigo en segundo plano
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BackGroundJob(BackgroundWorker bk)
        {
            try
            {
                
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        /// <summary>
        /// La llama el backgroundworker para ejecutar codigo en primer plano
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ForeGroundJob() { }

        #endregion

        #region Factory Methods

        public MainForm()
        {
            InitializeComponent();

            Globals.Instance.MainForm = this;
            Globals.Instance.Cursor = this.Cursor;
            Globals.Instance.StatusBar = this.Principal_StatusBar;
            Globals.Instance.StatusLabel = this.Status_Label;
            Globals.Instance.AnimLabel = this.Anim_Label;
            Globals.Instance.ProgressBar = this.Progress_Bar;
            Globals.Instance.ProgressInfoMng = ProgressInfoMng.Instance;

            //Fichero de ayuda
            HelpProvider.HelpNamespace = Application.StartupPath + ModuleController.HELP_PATH;
        }

        #endregion

        #region ApplyAuthorizationRules

        /// <summary>
        /// Aplica las reglas de autorización con el objetivo de habilitar o
        /// deshabilitar botones del menú según los permisos
        /// </summary>
        protected override void ApplyAuthorizationRules()
        {
            Main_MenuStrip.Enabled = true;
        }

        #endregion


        // Menus Response

        #region Archivo

        private void Salir_MI_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void CerrarSesion_MI_Click(object sender, EventArgs e)
        {
            DoLogin();
        }

        #endregion

        #region Herramientas


        #endregion

        #region Menus


        #endregion

        #region Login/Logout

        #endregion



    }
}

