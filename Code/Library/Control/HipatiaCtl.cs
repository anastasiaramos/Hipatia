using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security.AccessControl;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Net;

using Csla;
using Csla.Validation;
using moleQule.Library;
using moleQule.Library.CslaEx;
using NHibernate;

namespace moleQule.Library.Hipatia
{
    public static class HipatiaCtl
    {
		#region Attributes & Properties

		private static string _ruta = string.Empty;
		//private static string _user = string.Empty;
		//private static string _pass = string.Empty;
		private static string _server = string.Empty;
		private static TipoEntidadList _entidades = TipoEntidadList.GetList(false);

		#endregion

		#region Business Methods

        //public static string User { get { return _user; } }

        //public static string Password { get { return _pass; } }

        public static string Server { get { return _server; } }

        public static string Ruta { get { return _ruta; } }

        public static void Copy(FtpClient ftp, string origen, string destino)
        {
            ftp.UploadFile(origen, destino + "/" + Path.GetFileName(origen));
        }

		public static void Copy(FtpClient ftp, AgenteInfo agente, string origen)
		{
			EntidadInfo entidad = EntidadInfo.Get(agente.OidEntidad, false);
			ftp.UploadFile(origen, entidad.Schema + "/" + agente.Codigo + "/" + Path.GetFileName(origen));
		}

		public static bool ExistsDirectory(FtpClient ftp, string path)
		{
			string remote_path = path;
			return ftp.ExistsDir(remote_path);
		}

		public static bool ExistsFile(FtpClient ftp, string file)
		{
			return ftp.ExistsFile(file);
		}

		public static bool ExistsFile(FtpClient ftp, string dir, string file)
		{
			return ftp.ExistsFile(dir, file);
		}

		public static bool ExistsFile(FtpClient ftp, AgenteInfo agente, DocumentoInfo doc)
		{
			return ExistsFile(ftp, GetAgenteDirectory(ftp, agente), doc.Nombre);
		}

		public static void CreateDirectory(FtpClient ftp, string path)
		{
			ftp.MakeDir(path);
		}

        #endregion

        #region Factory Methods

		public static void InitHipatia(FtpClient ftp, string ruta, string server)
		{
			try
			{
				_ruta = ruta;

				if (server.StartsWith("/"))
					_server = server.Substring(2);
				else
					_server = server;

				if (!ftp.ExistsDir(AppContext.CommonSchema)) ftp.MakeDir(AppContext.CommonSchema);
				if (!ftp.ExistsDir(AppContext.ActiveSchema.SchemaCode)) ftp.MakeDir(AppContext.ActiveSchema.SchemaCode);
			}
			catch (Exception ex)
			{
				throw new iQException(iQExceptionHandler.GetAllMessages(ex) + Environment.NewLine + ftp.Parameters);
			}
		}

		public static string CreateAgentDirectory(FtpClient ftp, AgenteInfo agente)
		{
			if (!ExistsAgentDirectory(ftp, agente))
			{
				EntidadInfo entidad = EntidadInfo.Get(agente.OidEntidad, false);

				CreateDirectory(ftp, entidad.Schema + "/" + agente.Codigo);
				return entidad.Schema + "/" + agente.Codigo;
			}

			return string.Empty;
		}

        public static void CreateCommonDirectory(FtpClient ftp)
        {
            CreateDirectory(ftp, AppContext.CommonSchema);
        }

		public static void CreateSchemaDirectory(FtpClient ftp)
		{
			HipatiaCtl.CreateDirectory(ftp, AppContext.ActiveSchema.Code);
		}

        public static bool ExistsCommonDirectory(FtpClient ftp)
        {
            return ExistsDirectory(ftp, Ruta + "/" + AppContext.CommonSchema);
        }

        public static bool ExistsSchemaDirectory(FtpClient ftp)
        {
            return ExistsDirectory(ftp, Ruta + "/" + AppContext.ActiveSchema.Code);
        }

        public static bool ExistsAgentDirectory(FtpClient ftp, AgenteInfo agente)
        {
            EntidadInfo entidad = EntidadInfo.Get(agente.OidEntidad, false);

            return ExistsDirectory(ftp, entidad.Schema + "/" + agente.Codigo);
        }

        public static string GetAgentDirectory(FtpClient ftp, AgenteInfo agente)
        {
            EntidadInfo entidad = EntidadInfo.Get(agente.OidEntidad, false);

            return entidad.Schema + "/" + agente.Codigo;
        }

        public static string GetAgenteDirectory(FtpClient ftp, AgenteInfo agente)
        {
            EntidadInfo entidad = EntidadInfo.Get(agente.OidEntidad, false);

            ExistsDirectory(ftp, entidad.Schema + "/" + agente.Codigo);

            return entidad.Schema + "/" + agente.Codigo;
        }

        public static bool CheckDuplicate(Agente agente, Documento doc)
        {
            DocumentoList documentos = DocumentoList.GetListByAgente(agente);
            foreach (DocumentoInfo obj in documentos)
            {
                if (obj.Nombre.Equals(doc.Nombre) && obj.Oid != doc.Oid)
                    return true;
            }
            return false;
        }

        #endregion

    }
}
