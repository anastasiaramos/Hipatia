using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx;

namespace moleQule.Library.Hipatia
{
	[Serializable()]
	public class EntityRecord : RecordBase
	{
		#region Attributes

		private string _tipo = string.Empty;
		private string _observaciones = string.Empty;
		private bool _compartido = false;

		#endregion

		#region Properties

		public virtual string Tipo { get { return _tipo; } set { _tipo = value; } }
		public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
		public virtual bool Compartido { get { return _compartido; } set { _compartido = value; } }

		#endregion

		#region Business Methods

		public EntityRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_tipo = Format.DataReader.GetString(source, "TIPO");
			_observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
			_compartido = Format.DataReader.GetBool(source, "COMPARTIDO");

		}
		public virtual void CopyValues(EntityRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_tipo = source.Tipo;
			_observaciones = source.Observaciones;
			_compartido = source.Compartido;
		}

		#endregion
	}

	[Serializable()]
	public class EntityBase
	{
		#region Attributes

		private EntityRecord _record = new EntityRecord();

		#endregion

		#region Properties

		public EntityRecord Record { get { return _record; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);
		}
		internal void CopyValues(Entidad source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);
		}
		internal void CopyValues(EntidadInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);
		}

		#endregion
	}

	/// <summary>
	/// Editable Root Business Object With Editable Child Collection
	/// </summary>
    [Serializable()]
	public class Entidad : BusinessBaseEx<Entidad>
	{	 
		#region Attributes

		protected EntityBase _base = new EntityBase();

		private Agentes _agentes = Agentes.NewChildList();

		#endregion

		#region Properties

		public EntityBase Base { get { return _base; } }

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
		public virtual string Tipo
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Tipo;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Tipo.Equals(value))
				{
					_base.Record.Tipo = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Observaciones
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Observaciones;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Observaciones.Equals(value))
				{
					_base.Record.Observaciones = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual bool Compartido
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Compartido;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Compartido.Equals(value))
				{
					_base.Record.Compartido = value;
					PropertyHasChanged();
				}
			}
		}
        
		public virtual Agentes Agentes
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _agentes;
            }

            set
            {
                _agentes = value;
            }

        }

		public override bool IsValid
		{
			get { return base.IsValid && _agentes.IsValid; }
		}		
		public override bool IsDirty
		{
			get { return base.IsDirty || _agentes.IsDirty; }
		}

		#endregion

		#region Business Methods
		
		public virtual Entidad CloneAsNew()
		{
			Entidad clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			Random rd = new Random();
			clon.Base.Record.Oid = rd.Next();
			
			clon.SessionCode = Entidad.OpenSession();
			Entidad.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
            clon.Agentes.MarkAsNew();
			
			return clon;
		}			

		#endregion

        #region Validation Rules

        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CheckValidation, "Oid");
        }

        private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
        {
            return true;
        }
		 
		#endregion
		 
		#region Autorization Rules
		 
		public static bool CanAddObject()
		{
			return AutorizationRulesControler.CanAddObject(Resources.SecureItems.DOCUMENTO);
		}
		
		public static bool CanGetObject()
		{
			return AutorizationRulesControler.CanGetObject(Resources.SecureItems.DOCUMENTO);
		}
		
		public static bool CanDeleteObject()
		{
			return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.DOCUMENTO);
		}
		
		public static bool CanEditObject()
		{
			return AutorizationRulesControler.CanEditObject(Resources.SecureItems.DOCUMENTO);
		}
		 
		#endregion

		#region Common Factory Methods

		private Entidad(int sessionCode, IDataReader reader)
		{
			SessionCode = sessionCode;
			Fetch(reader);
		}

		internal static Entidad GetChild(int session_code, IDataReader reader)
		{
			return new Entidad(session_code, reader);
		}

		public virtual EntidadInfo GetInfo() { return GetInfo(true); }
		public virtual EntidadInfo GetInfo(bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);

			return new EntidadInfo(this, childs);
		}

		#endregion

		#region Root Factory Methods

		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New
		/// Debería ser private para CSLA porque la creación requiere el uso de los factory methods,
		/// pero es protected por exigencia de NHibernate.
		/// </summary>
		protected Entidad() {}
			
		public static Entidad New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<Entidad>(new CriteriaCs(-1));
		}

		public static Entidad Get(long oid, bool childs = false)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);
			
			CriteriaEx criteria = Entidad.GetCriteria(Entidad.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = Entidad.SELECT(oid, true);
			
			Entidad.BeginTransaction(criteria.Session);
			
			return DataPortal.Fetch<Entidad>(criteria);
		}

		/// <summary>
		/// Borrado inmediato, no cabe "undo"
		/// (La función debe ser "estática")
		/// </summary>
		/// <param name="oid"></param>
		public static void Delete(long oid)
		{
			if (!CanDeleteObject())
				throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);
			
			DataPortal.Delete(new CriteriaCs(oid));
		}
		
		/// <summary>
		/// Elimina todos los Entidad. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = Entidad.OpenSession();
			ISession sess = Entidad.Session(sessCode);
			ITransaction trans = Entidad.BeginTransaction(sessCode);
			
			try
			{
                sess.Delete("from EntityRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			finally
			{
				Entidad.CloseSession(sessCode);
			}
		}
		
		public override Entidad Save()
		{
			// Por la posible doble interfaz Root/Child
			if (IsChild) throw new iQException(Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
			
			if (IsDeleted && !CanDeleteObject())
				throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);
			else if (IsNew && !CanAddObject())
				throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);
			else if (!CanEditObject())
				throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);

            try
            {
                ValidationRules.CheckRules();
            }
            catch (iQValidationException ex)
            {
                iQExceptionHandler.TreatException(ex);
                return this;
            }

            try
            {
                base.Save();

                _agentes.Update(this);

				if (!SharedTransaction) Transaction().Commit();
				return this;
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
				return this;
			}
			finally
			{
				if (!SharedTransaction) if (CloseSessions) CloseSession(); 
				else BeginTransaction();
			}
		}
				
		#endregion
		
		#region Common Data Access
		 
		[RunLocal()]
		private void DataPortal_Create(CriteriaCs criteria)
		{
			_base.Record.Oid = (long)(new Random()).Next();
		}

        //Fetch independiente de DataPortal para generar un Entidad a partir de un IDataReader
        private void Fetch(IDataReader source)
        {
            try
            {
                _base.CopyValues(source);

                if (Childs)
                {
                    if (nHMng.UseDirectSQL)
                    {
                        Agente.DoLOCK(Session());
                        string query = Agentes.SELECT(this);
                        IDataReader reader = nHMng.SQLNativeSelect(query, Session());
                        _agentes = Agentes.GetChildList(reader, false);
                    }
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }
        }
 
		#endregion
		 
		#region Root Data Access
		 
		private void DataPortal_Fetch(CriteriaEx criteria)
		{
			try
			{
				_base.Record.Oid = 0;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;
				
				if (nHMng.UseDirectSQL)
				{
					Entidad.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
					
					if (reader.Read())
						_base.CopyValues(reader);
					
					if (Childs)
					{
                        Agente.DoLOCK(Session());
                        string query = Agentes.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
                        _agentes = Agentes.GetChildList(reader, false);
 					}
				}
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
			}
		}
		 
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Insert()
		{
			try
            {
                if (!SharedTransaction)
                {
                    if (SessionCode < 0) SessionCode = OpenSession();
                    BeginTransaction();
                }
				//si hay codigo o serial, hay que obtenerlos aquí
				
				Session().Save(Base.Record);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}
		
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Update()
		{
			if (IsDirty)
			{
				try
				{
					EntityRecord obj = Session().Get<EntityRecord>(Oid);
					obj.CopyValues(Base.Record);
					Session().Update(obj);
				}
				catch (Exception ex)
				{
					throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
				}
			}
		}
		
		//Deferred deletion
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_DeleteSelf()
		{
			DataPortal_Delete(new CriteriaCs(Oid));
		}
		
		[Transactional(TransactionalTypes.Manual)]
		private void DataPortal_Delete(CriteriaCs criteria)
		{
			try
			{
				// Iniciamos la conexion y la transaccion
				SessionCode = OpenSession();
				BeginTransaction();
					
				//Si no hay integridad referencial, aquí se deben borrar las listas hijo
				CriteriaEx criterio = GetCriteria();
				criterio.AddOidSearch(criteria.Oid);
				Session().Delete((EntityRecord)(criterio.UniqueResult()));
				Transaction().Commit();
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			finally
			{
				CloseSession();
			}
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
				SELECT EN.*";

			return query;
		}

		internal static string INNER(QueryConditions conditions)
		{
			string en = nHManager.Instance.GetSQLTable(typeof(EntityRecord));

			string query;

			query = @"
				FROM " + en + @" AS EN";

			return query + " " + conditions.ExtraJoin;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			string query;

			query = @" 
				WHERE " + FilterMng.GET_FILTERS_SQL(conditions.Filters, "EN", ForeignFields());

//            query += @" 
//				AND (EN.""DATE"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";

			query += Common.EntityBase.STATUS_LIST_CONDITION(conditions.Status, "EN");
			query += Common.EntityBase.GET_IN_LIST_CONDITION(conditions.OidList, "EN");

			if (conditions.Entity != null)
				query += @"
					AND EN.""OID"" = " + conditions.Entity.Oid;

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
				query += ORDER(conditions.Orders, "EN", ForeignFields());
				query += LIMIT(conditions.PagingInfo);
			}

			query += Common.EntityBase.LOCK("EN", lockTable);

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
			return SELECT(new QueryConditions { Entity = EntidadInfo.New(oid) }, lockTable);
		}

		#endregion
	}
}

