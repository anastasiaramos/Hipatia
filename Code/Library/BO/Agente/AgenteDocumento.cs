using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using Csla.Validation;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 
using NHibernate;

namespace moleQule.Library.Hipatia
{
	[Serializable()]
	public class AgentDocumentRecord : RecordBase
	{
		#region Attributes

		private long _oid_agente;
		private long _oid_documento;

		#endregion

		#region Properties

		public virtual long OidAgente { get { return _oid_agente; } set { _oid_agente = value; } }
		public virtual long OidDocumento { get { return _oid_documento; } set { _oid_documento = value; } }

		#endregion

		#region Business Methods

		public AgentDocumentRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_agente = Format.DataReader.GetInt64(source, "OID_AGENTE");
			_oid_documento = Format.DataReader.GetInt64(source, "OID_DOCUMENTO");

		}
		public virtual void CopyValues(AgentDocumentRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_agente = source.OidAgente;
			_oid_documento = source.OidDocumento;
		}

		#endregion
	}

	[Serializable()]
	public class AgentDocumentBase
	{
		#region Attributes

		private AgentDocumentRecord _record = new AgentDocumentRecord();

		#endregion

		#region Properties

		public AgentDocumentRecord Record { get { return _record; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);
		}
		internal void CopyValues(AgenteDocumento source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);
		}
		internal void CopyValues(AgenteDocumentoInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);
		}

		#endregion
	}

	/// <summary>
	/// Editable Child Business Object
	/// </summary>
    [Serializable()]
	public class AgenteDocumento : BusinessBaseEx<AgenteDocumento>
	{
		#region Attributes

		protected AgentDocumentBase _base = new AgentDocumentBase();

		#endregion

		#region Properties

		public AgentDocumentBase Base { get { return _base; } }

		public override long Oid
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Oid;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.Oid.Equals(value))
				{
					_base.Record.Oid = value;
					//PropertyHasChanged();
				}
			}
		}
		public virtual long OidAgente
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidAgente;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.OidAgente.Equals(value))
				{
					_base.Record.OidAgente = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidDocumento
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidDocumento;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.OidDocumento.Equals(value))
				{
					_base.Record.OidDocumento = value;
					PropertyHasChanged();
				}
			}
		}			
			
		#endregion
		 
	    #region Validation Rules
		 
		//región a rellenar si hay campos requeridos o claves ajenas
		
		//Descomentar en caso de existir reglas de validación
		/*protected override void AddBusinessRules()
        {	
			//Agregar reglas de validación
        }*/
		
		#endregion
		 
		#region Authorization Rules
		 
		public static bool CanAddObject()
		{
			return Documento.CanAddObject();
		}
		
		public static bool CanGetObject()
		{
			return Documento.CanGetObject();
		}
		
		public static bool CanDeleteObject()
		{
			return Documento.CanDeleteObject();
		}
		
		public static bool CanEditObject()
		{
			return Documento.CanEditObject();
		}
		 
		#endregion
		 
		#region Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate
		/// y public para que funcionen los DataGridView
		/// </summary>
		public AgenteDocumento() 
		{ 
			MarkAsChild();
			_base.Record.Oid = (long)(new Random()).Next();
		}			
		private AgenteDocumento(AgenteDocumento source)
		{
			MarkAsChild();
			Fetch(source);
		}		
		private AgenteDocumento(IDataReader reader)
		{
			MarkAsChild();
			Fetch(reader);
		}
		
		//Por cada padre que tenga la clase
		public static AgenteDocumento NewChild()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);
			
			return new AgenteDocumento();
		}		
		public static AgenteDocumento NewChild(Documento parent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);
			
			AgenteDocumento obj = new AgenteDocumento();
            obj.OidDocumento = parent.Oid;			
			return obj;
        }
        public static AgenteDocumento NewChild(Agente parent)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);

            AgenteDocumento obj = new AgenteDocumento();
			obj.OidAgente = parent.Oid;
            return obj;
        }
		public static AgenteDocumento NewChild(Agente agent, Documento doc)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);

			AgenteDocumento obj = new AgenteDocumento();
			obj.OidAgente = agent.Oid;
			obj.OidDocumento = doc.Oid;

			return obj;
		}

		internal static AgenteDocumento GetChild(AgenteDocumento source)
		{
			return new AgenteDocumento(source);
		}		
		internal static AgenteDocumento GetChild(IDataReader reader)
		{
			return new AgenteDocumento(reader);
		}
		
		public virtual AgenteDocumentoInfo GetInfo() {	return new AgenteDocumentoInfo(this); }
			
		/// <summary>
		/// Borrado aplazado, es posible el undo 
		/// (La función debe ser "no estática")
		/// </summary>
		public override void Delete()
		{
			if (!CanDeleteObject())
				throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);			
				
			MarkDeleted();
		}
		
		/// <summary>
		/// No se debe utilizar esta función para guardar. Hace falta el padre.
		/// Utilizar Insert o Update en sustitución de Save.
		/// </summary>
		/// <returns></returns>
		public override AgenteDocumento Save()
		{
			throw new iQException(Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
		}
					
		#endregion
		 
		#region Child Data Access
		 
		private void Fetch(AgenteDocumento source)
		{
			_base.CopyValues(source);
			MarkOld();
		}		
		private void Fetch(IDataReader reader)
		{
			_base.CopyValues(reader);
			MarkOld();
		}

        //internal void DeleteSelf(Agente parent)
        //{
        //    // if we're not dirty then don't update the database
        //    if (!this.IsDirty) return;

        //    // if we're new then don't update the database
        //    if (this.IsNew) return;
			
        //    try
        //    {
        //        SessionCode = parent.SessionCode;
        //        Session().Delete(Session().Get<AgenteDocumento>(Oid));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
        //    }
		
        //    MarkNew(); 
        //}
		
		internal void Insert(Documento parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

            OidDocumento = parent.Oid;

			try
			{
				SessionCode = parent.SessionCode;
				parent.Session().Save(Base.Record);
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			
			MarkOld();
		}
        internal void Insert(Agente parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            OidAgente = parent.Oid;

            try
            {
				SessionCode = parent.SessionCode;
                parent.Session().Save(Base.Record);
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            MarkOld();
        }

        internal void Update(Documento parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

            OidDocumento = parent.Oid;
			
			try
			{
                SessionCode = parent.SessionCode;
				AgentDocumentRecord obj = Session().Get<AgentDocumentRecord>(Oid);
				obj.CopyValues(Base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			
			MarkOld();
		}
        internal void Update(Agente parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            OidAgente = parent.Oid;

            try
            {
                SessionCode = parent.SessionCode;
				AgentDocumentRecord obj = Session().Get<AgentDocumentRecord>(Oid);
                obj.CopyValues(Base.Record);
                Session().Update(obj);
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            MarkOld();
        }

		internal void DeleteSelf(Documento parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<AgentDocumentRecord>(Oid));
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
		
			MarkNew(); 
		}
        internal void DeleteSelf(Agente parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            try
            {
                SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<AgentDocumentRecord>(Oid));
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            MarkNew();
        }		
		
		#endregion	

		#region SQL

		internal static Dictionary<String, ForeignField> ForeignFields()
		{
			return new Dictionary<String, ForeignField>() { };
		}

		public new static string SELECT(long oid) { return SELECT(oid, true); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }

		internal static string SELECT_FIELDS()
		{
			string query;

			query = @"
				SELECT AG.*";

			return query;
		}

		internal static string INNER(QueryConditions conditions)
		{
			string ag = nHManager.Instance.GetSQLTable(typeof(AgentDocumentRecord));

			string query;

			query = @"
				FROM " + ag + @" AS AG";

			return query + " " + conditions.ExtraJoin;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			string query;

			query = @" 
				WHERE " + FilterMng.GET_FILTERS_SQL(conditions.Filters, "SU", ForeignFields());

			query += Common.EntityBase.GET_IN_LIST_CONDITION(conditions.OidList, "AG");

			if (conditions.AgentDocument != null)
				query += @"
					AND AG.""OID"" = " + conditions.AgentDocument.Oid;

			if (conditions.Agent != null)
				query += @"
					AND AG.""OID_AGENTE"" = " + conditions.Agent.Oid;

			if (conditions.Document != null)
				query += @"
					AND AG.""OID_DOCUMENTO"" = " + conditions.Document.Oid;

			return query + " " + conditions.ExtraWhere;
		}

		internal static string SELECT(QueryConditions conditions, bool lockTable)
		{
			string query;

			query =
				SELECT_FIELDS() +
				INNER(conditions) +
				WHERE(conditions);

			if (conditions != null)
			{
				query += ORDER(conditions.Orders, "AG", ForeignFields());
				query += LIMIT(conditions.PagingInfo);
			}

			query += Common.EntityBase.LOCK("AG", lockTable);

			return query;
		}

		public static string SELECT(CriteriaEx criteria, bool lockTable)
		{
			QueryConditions conditions = new QueryConditions
			{
				PagingInfo = criteria.PagingInfo,
				Filters = criteria.Filters,
				Orders = criteria.Orders
			};
			return SELECT(conditions, lockTable);
		}

		internal static string SELECT(long oid, bool lockTable)
		{
			return SELECT(new QueryConditions { AgentDocument = AgenteDocumentoInfo.New(oid) }, lockTable);
		}

		#endregion
	}
}

