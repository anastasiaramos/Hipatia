using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.Library;
using moleQule.Library.CslaEx;
using NHibernate;

namespace moleQule.Library.Hipatia
{
    /// <summary>
    /// ReadOnly Root Business Object with ReadOnly Childs
    /// </summary>
    [Serializable()]
	public class AgenteInfo : ReadOnlyBaseEx<AgenteInfo, Agente>
    {
        #region Attributes

		protected AgentBase _base = new AgentBase();

        // Esta lista se mantiene para facilitar las busquedas
        private AgenteDocumentoList _agente_documentos = null;

        #endregion

        #region Properties

		public AgentBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidEntidad { get { return _base.Record.OidEntidad; } }
		public long OidAgenteExt { get { return _base.Record.OidAgenteExt; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Nombre { get { return _base.Record.Nombre; } }
		public DateTime Fecha { get { return _base.Record.Fecha; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }

        public virtual AgenteDocumentoList AgenteDocumentos { get { return _agente_documentos; } }

        //NO ENLAZADAS
        public string Entidad { get { return _base.Entidad; } }

		#endregion

		#region Business Methods

		public void CopyFrom(Agente source) { _base.CopyValues(source); }

        #endregion

        #region Common Factory Methods

        		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected AgenteInfo() { /* require use of factory methods */ }
        private AgenteInfo(int session_code, IDataReader reader, bool retrieve_childs)
		{
			Childs = retrieve_childs;
			SessionCode = session_code;
			Fetch(reader);
		}

        /// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> a partir de un <see cref="IDataReader"/>
        /// </summary>
        /// <param name="reader"><see cref="IDataReader"/> con los datos del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
        /// <remarks>
        /// NO OBTIENE los datos de los hijos. Para ello utiliza GetChild(IDataReader reader, bool retrieve_childs)
        /// La utiliza la ReadOnlyListBaseEx correspondiente para montar la lista
        /// <remarks/>
        public static AgenteInfo GetChild(int session_code, IDataReader reader)
        {
            return GetChild(session_code, reader, false);
        }
        public static AgenteInfo GetChild(int session_code, IDataReader reader, bool retrieve_childs)
        {
            return new AgenteInfo(session_code, reader, retrieve_childs);
        }

		public static AgenteInfo New(long oid = 0) { return new AgenteInfo() { Oid = oid }; }

        #endregion

        #region Root Factory Methods

        /// <summary>
        /// Contructor de AgenteInfo a partir de un Agente
        /// No copia los hijos
        /// </summary>
        /// <param name="item"></param>
		internal AgenteInfo(Agente item)
			: this(item, false) { }

        internal AgenteInfo(Agente item, bool childs)
        {
            _base.CopyValues(item);
            
			if (childs)
			{
				_agente_documentos = (item.Documentos != null) ? AgenteDocumentoList.GetChildList(item.Documentos) : null;
			}
        }

		public new static AgenteInfo Get(string query, bool childs = false)
		{
			if (!Agente.CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return ReadOnlyBaseEx<AgenteInfo, Agente>.Get(query, childs);
		}

        public static AgenteInfo Get(long oid, bool childs = true) { return Get(Agente.SELECT(oid), childs); }
		public static AgenteInfo Get(Type entityType, long oidHipatiaAgent, bool childs = false)
		{
			EntidadInfo entidad = EntidadInfo.Get(entityType);

			if (entidad.Oid != 0)
			{
				HipatiaAgentBase hipatiaAgent = new HipatiaAgentBase();
 				hipatiaAgent.Oid = oidHipatiaAgent;

				QueryConditions conditions = new QueryConditions()
				{
					Entity = entidad,
					IHipatiaAgent = hipatiaAgent
				};

				return Get(Agente.SELECT(conditions, false), childs);
			}

			return null;
		}
		public static AgenteInfo Get(Type entityType, IAgenteHipatia hipatiaAgent, bool childs = false)
		{
			EntidadInfo entidad = EntidadInfo.Get(entityType);

			if (entidad.Oid != 0)
			{
				QueryConditions conditions = new QueryConditions()
				{
					Entity = entidad,
					IHipatiaAgent = hipatiaAgent
				};

				AgenteInfo obj = Get(Agente.SELECT(conditions, false), childs);

				if (obj == null)
					throw new HipatiaException(String.Format(Resources.Messages.AGENTE_NOT_FOUND, hipatiaAgent.NombreHipatia), HipatiaCode.NO_AGENTE);

				return obj;
			}
			else
				throw new HipatiaException(Resources.Messages.ENTIDAD_NOT_FOUND + entityType.ToString(), HipatiaCode.NO_ENTIDAD);
		}

        public static AgenteInfo Get(int sessionCode, IDataReader reader, bool childs)
        {
            return new AgenteInfo(sessionCode, reader, childs);
        }

        #endregion

        #region Data Access

        // called to retrieve data from db
        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            try
            {
				_base.Record.Oid = 0;
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

                        query =  AgenteDocumentoList.SELECT(this);
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
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }
        }

        #endregion
    }
}



