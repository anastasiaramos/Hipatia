using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using Csla;
using moleQule.Library;
using moleQule.Library.CslaEx;

namespace moleQule.Library.Hipatia
{
	/// <summary>
	/// ReadOnly Child Business Object with ReadOnly Childs
	/// </summary>
	[Serializable()]
	public class DocumentoInfo : ReadOnlyBaseEx<DocumentoInfo, Documento>
	{
		#region Attributes

		protected DocumentBase _base = new DocumentBase();   

        private AgenteDocumentoList _agente_documentos = null;

		#endregion

        #region Properties

		public DocumentBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Nombre { get { return _base.Record.Nombre; } }
		public string Tipo { get { return _base.Record.Tipo; } }
		public DateTime Fecha { get { return _base.Record.Fecha; } }
		public DateTime FechaAlta { get { return _base.Record.FechaAlta; } }
		public DateTime ExpirationDate { get { return _base.Record.ExpirationDate; } }
		public string Ruta { get { return _base.Record.Ruta; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }

		public virtual AgenteDocumentoList AgenteDocumentos { get { return _agente_documentos; } }

        //NO ENLAZADOS
		public virtual bool Shared { get { return _base.Shared; } set { _base.NAgentes = 2; } }
		public virtual long OidDocumento { get { return _base.OidDocumento; } }
		public virtual string Entidad { get { return _base.Entidad; } }
		public virtual string Agente { get { return _base.Agente; } }
		public virtual int NAgentes { get { return _base.NAgentes; } }

        #endregion

        #region Business Methods

        public void CopyFrom(Documento source) { _base.CopyValues(source); }

		#endregion

		#region Factory Methods

		protected DocumentoInfo() { /* require use of factory methods */ }

		private DocumentoInfo(int session_code, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = session_code;
			Fetch(reader);
		}
		internal DocumentoInfo(Documento item, bool childs)
		{
			_base.CopyValues(item);			
			
			if (childs)
			{
				if (item.AgenteDocumentos != null) _agente_documentos = AgenteDocumentoList.GetChildList(item.AgenteDocumentos);				
			}
		}

		public static DocumentoInfo Get(long oid, bool childs = false)
		{
			CriteriaEx criteria = Documento.GetCriteria(Documento.OpenSession());
			
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = Documento.SELECT(oid);
				
			criteria.Childs = childs;
			DocumentoInfo obj = DataPortal.Fetch<DocumentoInfo>(criteria);
			Documento.CloseSession(criteria.SessionCode);
			return obj;
		}

		public static DocumentoInfo New(long oid = 0) { return new DocumentoInfo() { Oid = oid }; } 

		/// <summary>
		/// Copia los datos al objeto desde un IDataReader 
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static DocumentoInfo Get(int sessionCode, IDataReader reader, bool childs)
		{
			return new DocumentoInfo(sessionCode, reader, childs);
		}

		#endregion

		#region Data Access

		// called to retrieve data from db
		private void DataPortal_Fetch(CriteriaEx criteria)
		{
			try
			{
				_base.Record.Oid = 0;;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;

				if (nHMng.UseDirectSQL)
				{
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
					
					if (reader.Read())
						_base.CopyValues(reader);
					
                    if (Childs)
					{
						string query = string.Empty;

						query = AgenteDocumentoList.SELECT(this);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
                        _agente_documentos = AgenteDocumentoList.GetChildList(reader);												
                    }
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
			}
		}

		//called to copy data from IDataReader
		private void Fetch(IDataReader source)
		{
			try
			{
				_base.CopyValues(source);

				if (Childs)
				{
					string query = string.Empty;
					IDataReader reader;

					query = AgenteDocumentoList.SELECT(this);
                    reader = nHManager.Instance.SQLNativeSelect(query, Session());
                    _agente_documentos = AgenteDocumentoList.GetChildList(reader);
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}

		#endregion
	}
}



