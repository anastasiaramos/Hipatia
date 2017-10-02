using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection;

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
    public class Principal : moleQule.Library.PrincipalBase
    {
        #region Factory Methods

        /// <summary>
        /// Contructor 
        /// </summary>
        /// <param name="identity"></param>
        protected Principal(IIdentityEx identity)
            : base(identity)
        {
        }

        #endregion

		#region Application Settings

		public new static void SaveSettings() { Settings.Default.Save(); AppContext.Principal.SaveSettings(); }

		public static void UpgradeSettings()
		{
			Assembly ensamblado = System.Reflection.Assembly.GetExecutingAssembly();
			Version ver = ensamblado.GetName().Version;

			if (Properties.Settings.Default.MODULE_VERSION != ver.ToString())
			{
				Properties.Settings.Default.Upgrade();
				Properties.Settings.Default.MODULE_VERSION = ver.ToString();
			}
		}

		public static string GetFTPServer()
		{
			return (Settings.Default.FTP_HOST != string.Empty) ? Settings.Default.FTP_HOST : nHManager.Instance.Host;
		}
		public static void SetFTPServer(string server)
		{
			Settings.Default.FTP_HOST = server;
			Settings.Default.Save();
		}

		public static string GetHipatiaFTPHost() 
		{ 
			return (Settings.Default.FTP_HOST != string.Empty) ? Settings.Default.FTP_HOST : Settings.Default.FTP_HOST_DEFAULT; 
		}

		public static string GetHipatiaFTPUser() 
		{ 
			return Settings.Default.FTP_USER; 
		}

		public static string GetHipatiaFTPPwd() 
		{ 
			return Settings.Default.FTP_PWD; 
		}

		public static string GetHipatiaFTPRootPath() 
		{ 
			return Settings.Default.FTP_ROOT_PATH; 
		}

		#endregion

		#region User Settings

		public static string GetDBVersion() { return Settings.Default.DB_VERSION; }

		#endregion

        #region Business Methods

        /// <summary>
        /// Elimina todos los datos asociados al esquema activo
        /// tanto los propios como los de las entidades que contiene
        /// </summary>
        /// <param name="oid"></param>
        public override void DeleteSchema(ISchema schema)
        {
            base.DeleteSchema(schema);
        }

        #endregion
    }
}