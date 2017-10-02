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
	public class EntidadInfo : ReadOnlyBaseEx<EntidadInfo>
	{
		#region Attributes

		protected EntityBase _base = new EntityBase();

		private AgenteList _agentes = null;

		#endregion

		#region Properties

		public EntityBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public string Tipo { get { return _base.Record.Tipo; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
		public bool Compartido { get { return _base.Record.Compartido; } }

		public virtual AgenteList Agentes { get { return _agentes; } }

		public string Schema { get { return Compartido ? AppContext.CommonSchema : Convert.ToInt32(AppContext.ActiveSchema.Code).ToString("0000"); } }

		#endregion

		#region Business Methods
		
		#endregion		
		
		#region Common Factory Methods

		private EntidadInfo(int session_code, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = session_code;
			Fetch(reader);
		}

		public static EntidadInfo GetChild(int session_code, IDataReader reader, bool childs)
		{
			return new EntidadInfo(session_code, reader, childs);
		}

		public static EntidadInfo New(long oid = 0) { return new EntidadInfo() { Oid = oid }; }

		#endregion

		#region Factory Methods

		private EntidadInfo() { /* require use of factory methods */ }
		
		internal EntidadInfo(Entidad item)
			: this(item, false) {}
		
		internal EntidadInfo(Entidad item, bool childs)
		{
            _base.CopyValues(item);

			if (childs)
			{
                _agentes = (item.Agentes != null) ? AgenteList.GetChildList(item.Agentes) : null;
			}
		}
		
		/// <summary>
		/// Devuelve un EntidadInfo tras consultar la base de datos
		/// </summary>
		/// <param name="oid"></param>
		/// <returns></returns>
		public static EntidadInfo Get(long oid) { return Get(oid, false); }
		public static EntidadInfo Get(long oid, bool childs)
		{
			CriteriaEx criteria = Entidad.GetCriteria(Entidad.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = Entidad.SELECT(oid, false);
			
			EntidadInfo obj = DataPortal.Fetch<EntidadInfo>(criteria);
			
			Entidad.CloseSession(criteria.SessionCode);

			return obj;
		}
		
        public static EntidadInfo Get(Type tipo, bool childs = false)
        {
            CriteriaEx criteria = Entidad.GetCriteria(Entidad.OpenSession());
			criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = EntidadInfo.SELECT(tipo);

            EntidadInfo obj = DataPortal.Fetch<EntidadInfo>(criteria);
            
			Entidad.CloseSession(criteria.SessionCode);
            
			return obj;
        }

 		#endregion
		 
		#region Data Access
		 
		// called to retrieve data from db
		private void DataPortal_Fetch(CriteriaEx criteria)
        {
            _base.Record.Oid = 0;
			SessionCode = criteria.SessionCode;
			Childs = criteria.Childs;

			try
			{
				if (nHMng.UseDirectSQL)
				{
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
					if (reader.Read())
						_base.CopyValues(reader);
					
                    if (Childs)
					{
                        string query = string.Empty;

                        query = AgenteList.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
                        _agentes = AgenteList.GetChildList(reader); 
                    }
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
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

                    query = AgenteList.SELECT(this);
                    reader = nHMng.SQLNativeSelect(query, Session());
                    _agentes = AgenteList.GetChildList(reader); 
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}
		
		#endregion

        #region SQL

        public static string SELECT(Type tipo)
        {
            string te = nHManager.Instance.GetSQLTable(typeof(EntityRecord));
            string query;

            query = "SELECT E.*" +
                    " FROM " + te + " AS E" +
                    " WHERE E.\"TIPO\" = '" + tipo.Name + "';";

            return query;
        }

        #endregion
    }
}



