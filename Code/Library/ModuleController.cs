using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using Csla;
using Csla.Security;
using moleQule.Library;
using moleQule.Library.CslaEx;
using moleQule.Library.Hipatia.Properties;

namespace moleQule.Library.Hipatia
{
    /// <summary>
    /// 
    /// </summary>
	[Serializable()]
	public class ModuleController : moleQule.Library.AppControllerBase
	{
        #region Paths

        public static string HELP_PATH { get { return Resources.Paths.HELP; } }

        #endregion

		#region Factory Methods

        /// <summary>
        /// Única instancia de la clase ControlerBase (Singleton)
        /// </summary>
        protected static ModuleController _main;

        /// <summary>
        /// Unique Controler Class Instance
        /// </summary>
        public static ModuleController Instance { get { return (_main != null) ? _main : new ModuleController(); } }
        
        /// <summary>
        /// Contructor 
        /// </summary>
		protected ModuleController()
        {
            // Singleton
            _main = this;
        }

		public new static void CheckDBVersion()
		{
			ApplicationSettingInfo dbVersion = ApplicationSettingInfo.Get(Settings.Default.DB_VERSION_VARIABLE);

			//Version de base de datos equivalente o no existe la variable
			if ((dbVersion.Value == string.Empty) ||
				(String.CompareOrdinal(dbVersion.Value, Principal.GetDBVersion()) == 0))
			{
				return;
			}
			//Version de base de datos superior
			else if (String.CompareOrdinal(dbVersion.Value, Principal.GetDBVersion()) > 0)
			{
				throw new iQException(String.Format(Library.Resources.Messages.DB_VERSION_HIGHER,
													dbVersion.Value,
													Principal.GetDBVersion(),
													Settings.Default.NAME),
													iQExceptionCode.DB_VERSION_MISSMATCH);
			}
			//Version de base de datos inferior
			else if (String.CompareOrdinal(dbVersion.Value, Principal.GetDBVersion()) < 0)
			{
				throw new iQException(String.Format(Library.Resources.Messages.DB_VERSION_LOWER,
													dbVersion.Value,
													Principal.GetDBVersion(),
													Settings.Default.NAME),
													iQExceptionCode.DB_VERSION_MISSMATCH);
			}
		}

		/*private static void SetDBName()
		{
#if DEBUG
			SettingsMng.Instance.SetDBName(Settings.Default.DB_NAME);
#endif
#if DEMO
            SettingsMng.Instance.SetDBName(Settings.Default.DB_NAME);  
#endif
#if PREPRO
            SettingsMng.Instance.SetDBName(Settings.Default.DB_NAME); 
#endif
#if RELEASE
			SettingsMng.Instance.SetDBName(Settings.Default.DB_NAME);
#endif

		}*/

		private static void SetDBPassword()
		{
#if DEBUG
			SettingsMng.Instance.SetDBPassword("TebaP2G_1998");
#endif
#if DEMO
            SettingsMng.Instance.SetDBPassword("zl3Ge4uEn");    
#endif
#if PREPRO
            SettingsMng.Instance.SetDBPassword("TebaP2G_1998");    
#endif
#if RELEASE
			SettingsMng.Instance.SetDBPassword("TebaP2G_1998");
#endif
		}

		private static void SetDBVersion()
		{
			SettingsMng.Instance.SetDBVersion(Settings.Default.DB_VERSION);
		}

		public new static void UpgradeSettings() { Principal.UpgradeSettings(); }

		#endregion

		#region Settings

		public static void SetHipatiaFTPDocs(string user)
		{
			Settings.Default.FTP_USER = user;
			Settings.Default.Save();
		}

		public static void SetHipatiaFTPParams(string ftp_host, string ftp_user, string ftp_pwd, string ftp_root_path)
		{
			if (ftp_host != string.Empty) Settings.Default.FTP_HOST = ftp_host;
			if (ftp_user != string.Empty) Settings.Default.FTP_USER = ftp_user;
			if (ftp_pwd != string.Empty) Settings.Default.FTP_PWD = ftp_pwd;
			if (ftp_root_path != string.Empty) Settings.Default.FTP_ROOT_PATH = ftp_root_path;
			Settings.Default.Save();
		}

		#endregion

		#region Updates

		/// <summary>
        /// Busca actualizaciones disponibles en el FTP
        /// </summary>
        public static string LookForUpdates()
        {
			return AppControllerBase.LookForUpdates(Resources.Conf.FTPHost,
                                                Resources.Conf.FTPUser,
                                                Resources.Conf.FTPPwd,
                                                Application.ProductName,
                                                Resources.Conf.FTPFile,
                                                Application.ProductName + ".exe");
        }

        /// <summary>
        /// Busca actualizaciones disponibles en el FTP
        /// </summary>
        public static string DownloadUpdate(string remote_file, BackgroundWorker bk)
        {
			return AppControllerBase.DownloadUpdate(Resources.Conf.FTPHost,
                                                Resources.Conf.FTPUser,
                                                Resources.Conf.FTPPwd,
                                                Application.ProductName + "//" + remote_file,
                                                System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory), bk);
        }

        /// <summary>
        /// Ejecuta el fichero de actualización
        /// </summary>
        public new static void Update(string remote_file)
        {
			AppControllerBase.Update(System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory) +
                                 "\\" + remote_file);
        }

        /// <summary>
        /// Ejecuta el fichero de actualización
        /// </summary>
        public new static void Update(string remote_file, string user_name, string password, string domain)
        {
			AppControllerBase.Update(System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory) +
                                 "\\" + remote_file, user_name, password, domain);
        }

        #endregion

        #region Backup

		/*public static void AutoBackup(bool forceBackup)
		{
			ModuleController.SetDBPassword();
			AppControllerBase.AutoBackup(nHManager.Instance.Host,
										nHManager.Instance.Database,
										nHManager.Instance.User,
										forceBackup);
		}

		public static void CreateBackup(string outputFile)
		{
			if (!AppContext.Principal.Identity.IsAdmin)
				throw new iQException(Library.Resources.Messages.USER_NOT_ALLOWED);

			ModuleController.SetDBPassword();
			AppControllerBase.DoCreateBackup(nHManager.Instance.Host,
										nHManager.Instance.Database,
										nHManager.Instance.User,
										outputFile);
		}*/

		public static void RestoreBackup(string filename)
		{
			if (!AppContext.Principal.Identity.IsAdmin)
				throw new iQException(Library.Resources.Messages.USER_NOT_ALLOWED);

			ModuleController.SetDBPassword();
			AppControllerBase.RestoreBackup(nHManager.Instance.Host,
										nHManager.Instance.Database,
										nHManager.Instance.User,
										filename);
		}

        #endregion

        #region Automatico

        /// <summary>
        /// Realiza acciones automáticas al principio de la ejecución
        /// </summary>
        public override List<string> Autopilot(bool log)
        {
            List<string> results = new List<string>();

            results.Add("LookForUpdates");

            return results;
        }

        #endregion
	}

	public class ModuleDef : IModuleDef
	{
		public string Name { get { return "Hipatia"; } }
		public Type Type { get { return typeof(Hipatia.ModuleController); } }
		public Type[] Mappings
		{
			get
			{
				return new Type[] 
                {   
					typeof(AgentMap),
                    typeof(AgentDocumentMap),
                    typeof(DocumentMap),
					typeof(DocumentTypeMap),
                    typeof(EntityMap),
                    typeof(EntityTypeMap)
                };
			}
		}

		public void GetEntities(Dictionary<Type, Type> recordEntities)
		{
			if (recordEntities.ContainsKey(typeof(Agente))) return;

			recordEntities.Add(typeof(Agente), typeof(AgentRecord));
			recordEntities.Add(typeof(AgenteDocumento), typeof(AgentDocumentRecord));
			recordEntities.Add(typeof(Documento), typeof(DocumentRecord));
			recordEntities.Add(typeof(Tipodocumento), typeof(DocumentTypeRecord));
			recordEntities.Add(typeof(Entidad), typeof(EntityRecord));
			recordEntities.Add(typeof(TipoEntidad), typeof(EntityTypeRecord));
		}
	}
}
