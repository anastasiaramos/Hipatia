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
using moleQule.Library.Common;

namespace moleQule.Library.Hipatia
{
	[Serializable()]
	public class AgentRecord : RecordBase
	{
		#region Attributes

		private long _oid_entidad;
		private long _oid_agente_ext;
		private string _codigo = string.Empty;
		private long _serial;
		private string _nombre = string.Empty;
		private DateTime _fecha;
		private string _observaciones = string.Empty;

		#endregion

		#region Properties

		public virtual long OidEntidad { get { return _oid_entidad; } set { _oid_entidad = value; } }
		public virtual long OidAgenteExt { get { return _oid_agente_ext; } set { _oid_agente_ext = value; } }
		public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
		public virtual long Serial { get { return _serial; } set { _serial = value; } }
		public virtual string Nombre { get { return _nombre; } set { _nombre = value; } }
		public virtual DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
		public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }

		#endregion

		#region Business Methods

		public AgentRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_entidad = Format.DataReader.GetInt64(source, "OID_ENTIDAD");
			_oid_agente_ext = Format.DataReader.GetInt64(source, "OID_AGENTE_EXT");
			_codigo = Format.DataReader.GetString(source, "CODIGO");
			_serial = Format.DataReader.GetInt64(source, "SERIAL");
			_nombre = Format.DataReader.GetString(source, "NOMBRE");
			_fecha = Format.DataReader.GetDateTime(source, "FECHA");
			_observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");

		}
		public virtual void CopyValues(AgentRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_entidad = source.OidEntidad;
			_oid_agente_ext = source.OidAgenteExt;
			_codigo = source.Codigo;
			_serial = source.Serial;
			_nombre = source.Nombre;
			_fecha = source.Fecha;
			_observaciones = source.Observaciones;
		}

		#endregion
	}

	[Serializable()]
	public class AgentBase
	{
		#region Attributes

		private AgentRecord _record = new AgentRecord();

		private string _entidad = string.Empty;

		#endregion

		#region Properties

		public AgentRecord Record { get { return _record; } }

		public virtual string Entidad { get { return _entidad; } set { _entidad = value; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);

			_entidad = Format.DataReader.GetString(source, "ENTIDAD");
		}
		internal void CopyValues(Agente source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_entidad = source.Entidad;
		}
		internal void CopyValues(AgenteInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_entidad = source.Entidad;
		}

		#endregion
	}

	/// <summary>
	/// Editable Root Business Object With Editable Child Collection
    /// Editable Child Business Object With Editable Child Collection
	/// </summary>
    [Serializable()]
	public class Agente : BusinessBaseEx<Agente>
	{	 
		#region Attributes

		protected AgentBase _base = new AgentBase();
		
		private AgenteDocumentos _documentos = AgenteDocumentos.NewChildList();

		#endregion

		#region Properties

		public AgentBase Base { get { return _base; } }

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
		public virtual long OidEntidad
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidEntidad;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidEntidad.Equals(value))
				{
					_base.Record.OidEntidad = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidAgenteExt
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidAgenteExt;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.OidAgenteExt.Equals(value))
				{
					_base.Record.OidAgenteExt = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Codigo
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Codigo;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Codigo.Equals(value))
				{
					_base.Record.Codigo = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long Serial
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Serial;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Serial.Equals(value))
				{
					_base.Record.Serial = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Nombre
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Nombre;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Nombre.Equals(value))
				{
					_base.Record.Nombre = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime Fecha
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Fecha;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Fecha.Equals(value))
				{
					_base.Record.Fecha = value;
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

		public virtual AgenteDocumentos Documentos
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
				return _documentos;
			}
		}

        //NO ENLAZADAS
		public virtual string Entidad { get { return _base.Entidad; } set { _base.Entidad = value; } }
	 
		public override bool IsValid
		{
			get { return base.IsValid
						 && _documentos.IsValid ; }
		}
		public override bool IsDirty
		{
			get { return base.IsDirty
						 || _documentos.IsDirty ; }
		}

		#endregion

		#region Business Methods

		/// <summary>
		/// Clona la entidad y sus subentidades y las marca como nuevas
		/// </summary>
		/// <returns>Una entidad clon</returns>			
		public virtual Agente CloneAsNew()
		{
			Agente clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			clon.Base.Record.Oid = (long)(new Random()).Next();
			
			clon.Codigo = (0).ToString(Resources.Defaults.AGENTE_CODE_FORMAT);
			clon.SessionCode = Agente.OpenSession();
			Agente.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			clon.Documentos.MarkAsNew();
			
			return clon;
		}

		public virtual void CopyFrom(EntidadInfo entity, IAgenteHipatia agent)
		{
			OidEntidad = entity.Oid;
			OidAgenteExt = agent.Oid;
			Nombre = agent.NombreHipatia;
			Fecha = DateTime.Today;
			Entidad = entity.Tipo;
		}

		/// <summary>
        /// Devuelve el siguiente Serial de Agente
        /// </summary>
        /// <returns></returns>
        private static Int64 GetNewSerial()
        {
            // Obtenemos la lista de clientes ordenados por serial
            SortedBindingList<AgenteInfo> Agentes =
                AgenteList.GetSortedList("Serial", ListSortDirection.Ascending);

            // Obtenemos el último serial de servicio
            Int64 lastcode;

            if (Agentes.Count > 0)
                lastcode = Agentes[Agentes.Count - 1].Serial;
            else
                lastcode = 0;

            lastcode++;
            return lastcode;
        }

        public virtual string GetCode()
        {
            return "GD#" + OidEntidad.ToString("00") + OidAgenteExt.ToString("00000");
        }
        public static string GetCode(EntidadInfo entidad, IAgenteHipatia agente_h)
        {
            return "GD#" + entidad.Oid.ToString("00") + agente_h.Oid.ToString("00000");
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

        #region Common Factory Methods

        /// <summary>
        /// NO UTILIZAR DIRECTAMENTE. LAS UTILIZAN LAS FUNCIONES DE CREACION DE LISTAS
        /// </summary>
        private Agente(Agente source, bool childs)
        {
            MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
		private Agente(IDataReader source, bool childs)
        {
            MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
		private Agente(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}

        public static Agente NewChild()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

            return DataPortal.Create<Agente>(new CriteriaCs(-1));
        }

        internal static Agente GetChild(Agente source)
        {
            return new Agente(source, false);
        }
        internal static Agente GetChild(IDataReader source)
        {
            return new Agente(source, false);
        }
		internal static Agente GetChild(IDataReader source, bool childs)
        {
			return new Agente(source, childs);
        }
		internal static Agente GetChild(int sessionCode, IDataReader reader, bool childs = false)
		{
			return new Agente(sessionCode, reader, childs);
		}

        #endregion

		#region Root Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New
		/// Debería ser private para CSLA porque la creación requiere el uso de los factory methods,
		/// pero es protected por exigencia de NHibernate.
		/// </summary>
		protected Agente() {}		

		public static Agente New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<Agente>(new CriteriaCs(-1));
		}
		public static Agente New(Type entityType, IAgenteHipatia agent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);

			EntidadInfo entity = EntidadInfo.Get(entityType);
			if (entity.Oid != 0)
			{
				Agente obj = DataPortal.Create<Agente>(new CriteriaCs(-1));
				obj.CopyFrom(entity, agent);
				return obj;
			}
			else
				throw new HipatiaException(Resources.Messages.ENTIDAD_NOT_FOUND + entityType.ToString(), HipatiaCode.NO_ENTIDAD);	
		}

		public new static Agente Get(string query, bool childs, int sessionCode = -1)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return BusinessBaseEx<Agente>.Get(query, childs, -1);
		}

		public static Agente Get(long oid, bool childs = true) { return Get(SELECT(oid), childs); }
		public static Agente Get(Type entityType, long oidHipatiaAgent, bool childs = false)
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

				return Get(SELECT(conditions, true), childs);
			}

			return null;
		}
        public static Agente Get(Type entityType, IAgenteHipatia hipatiaAgent, bool childs = false)
        {
			EntidadInfo entidad = EntidadInfo.Get(entityType);

            if (entidad.Oid != 0)
            {
				QueryConditions conditions = new QueryConditions()
				{
					Entity = entidad,
					IHipatiaAgent = hipatiaAgent
				};

				Agente obj = Get(SELECT(conditions, false), childs);

				if (obj == null)
					throw new HipatiaException(String.Format(Resources.Messages.AGENTE_NOT_FOUND, hipatiaAgent.NombreHipatia), HipatiaCode.NO_AGENTE);

				return obj;
            }
            else
				throw new HipatiaException(Resources.Messages.ENTIDAD_NOT_FOUND + entityType.ToString(), HipatiaCode.NO_ENTIDAD);
        }

		public virtual AgenteInfo GetInfo(bool childs = true) { return new AgenteInfo(this, childs); }
				
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
		/// Elimina todos los Agente. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = Agente.OpenSession();
			ISession sess = Agente.Session(sessCode);
			ITransaction trans = Agente.BeginTransaction(sessCode);
			
			try
			{
                sess.Delete("from AgentRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			finally
			{
				Agente.CloseSession(sessCode);
			}
		}
		
		public override Agente Save()
		{
			// Por la posible doble interfaz Root/Child
			if (IsChild) 
				throw new iQException(Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
			
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

                _documentos.Update(this);

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

        public static bool Exists(string type, long oid, out long agente_oid, out long entity_type_oid)
        {
            AgenteList lista = AgenteList.GetList(false);
            EntidadList ent_lista = EntidadList.GetList(false);
            long ent_oid = -1;
            agente_oid = -1;
            entity_type_oid = -1;

            foreach (EntidadInfo e in ent_lista)
                if (e.Tipo.Equals(type))
                    ent_oid = e.Oid;

            if (ent_oid == -1) return false;

            entity_type_oid = ent_oid;

            foreach (AgenteInfo a in lista)
                if (a.Codigo.Equals("GD#" + ent_oid.ToString() + oid.ToString()))
                {
                    agente_oid = a.Oid;
                    return true;
                }

            return false;
        }
	
		#endregion

        #region Child Factory Methods

        /// <summary>
        /// NO UTILIZAR DIRECTAMENTE. LAS UTILIZAN LAS FUNCIONES DE CREACION DE LAS LISTAS
        /// </summary>
        private Agente(Entidad parent)
        {
            OidEntidad = parent.Oid;
            MarkAsChild();
        }

        /// <summary>
        /// Crea un nuevo objeto hijo
        /// </summary>
        /// <param name="parent">Objeto padre</param>
        /// <returns>Nuevo objeto creado</returns>
        internal static Agente NewChild(Entidad parent)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

            return new Agente(parent);
        }
        
        /// <summary>
        /// Borrado aplazado, es posible el undo 
        /// (La función debe ser "no estática")
        /// </summary>
        public override void Delete()
        {
            if (!CanDeleteObject())
                throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

            MarkDeleted();
        }

        #endregion

		#region Common Data Access
		 
		[RunLocal()]
		private void DataPortal_Create(CriteriaCs criteria)
		{
			_base.Record.Oid = (long)(new Random()).Next();
            Serial = GetNewSerial();
            Codigo = GetCode();
			
			_documentos = AgenteDocumentos.NewChildList();
		}

        /// <summary>
        /// Construye el objeto y se encarga de obtener los
        /// hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="source">Objeto fuente</param>
        private void Fetch(Agente source)
        {
            try
            {
                SessionCode = source.SessionCode;
                _base.CopyValues(source);
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            MarkOld();
        }
 
        /// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">DataReader fuente</param>
        private void Fetch(IDataReader source)
        {
            try
            {
                _base.CopyValues(source);
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            MarkOld();
        }

        /// <summary>
        /// Inserta el registro en la base de datos
        /// </summary>
        /// <param name="parent">Lista padre</param>
        /// <remarks>La utiliza la BusinessListBaseEx correspondiente para insertar elementos<remarks/>
        internal void Insert(Agentes parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            try
            {
                ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

                parent.Session().Save(Base.Record);
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            MarkOld();
        }

        /// <summary>
        /// Actualiza el registro en la base de datos
        /// </summary>
        /// <param name="parent">Lista padre</param>
        /// <remarks>La utiliza la BusinessListBaseEx correspondiente para actualizar elementos<remarks/>
        internal void Update(Agentes parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            try
            {
                ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

                SessionCode = parent.SessionCode;
				AgentRecord obj = Session().Get<AgentRecord>(Oid);
                obj.CopyValues(Base.Record);
                Session().Update(obj);
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            MarkOld();
        }

        /// <summary>
        /// Borra el registro de la base de datos
        /// </summary>
        /// <param name="parent">Lista padre</param>
        /// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
        internal void DeleteSelf(Agentes parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            try
            {
                SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<AgentRecord>(Oid));
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            MarkNew();
        }

		#endregion
		 
		#region Root Data Access
		 
		private void DataPortal_Fetch(CriteriaEx criteria)
		{
			try
			{
				_base.Record.Oid = 0;;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;
				
				if (nHMng.UseDirectSQL)
				{
					Agente.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
					
					if (reader.Read())
						_base.CopyValues(reader);
					
					if (Childs)
					{
						string query = string.Empty;
						
						Documento.DoLOCK(Session());

                        query = AgenteDocumentos.SELECT(this);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
                        _documentos = AgenteDocumentos.GetChildList(reader);						
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
				Serial = GetNewSerial();
                Codigo = GetCode();
				Session().Save(Base.Record);
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
		}
		
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Update()
		{
			if (IsDirty)
			{
				try
				{
					AgentRecord obj = Session().Get<AgentRecord>(Oid);
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
				Session().Delete((AgentRecord)(criterio.UniqueResult()));
				Transaction().Commit();
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				CloseSession();
			}
		}
				
		#endregion

        #region Child Data Access

        /// <summary>
        /// Inserta un registro en la base de datos
        /// </summary>
        /// <param name="parent">Objeto padre</param>
        internal void Insert(Entidad parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            //Debe obtener la sesion del padre pq el objeto es padre a su vez
            SessionCode = parent.SessionCode;

            OidEntidad = parent.Oid;
            Codigo = GetCode();

            try
            {
                ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

                parent.Session().Save(Base.Record);
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            MarkOld();
        }

        /// <summary>
        /// Actualiza un registro en la base de datos
        /// </summary>
        /// <param name="parent">Objeto padre</param>
        internal void Update(Entidad parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            //Debe obtener la sesion del padre pq el objeto es padre a su vez
            SessionCode = parent.SessionCode;

            OidEntidad = parent.Oid;

            try
            {
                ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

				AgentRecord obj = parent.Session().Get<AgentRecord>(Oid);
                obj.CopyValues(Base.Record);
                parent.Session().Update(obj);
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            MarkOld();
        }

        /// <summary>
        /// Borra un registro de la base de datos.
        /// </summary>
        /// <param name="parent">Objeto padre</param>
        /// <remarks>Borrado inmediato<remarks/>
        internal void DeleteSelf(Entidad parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            try
            {
                SessionCode = parent.SessionCode;
                Session().Delete(Session().Get<AgentRecord>(Oid));
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
			return new Dictionary<String, ForeignField>()
            {
                { 
                    "Entidad", 
                    new ForeignField() { 
                        Property = "Entidad", 
                        TableAlias = "EN", 
                        Column = nHManager.Instance.GetTableColumn(typeof(EntityRecord), "Tipo")
                    } 
                },
            };
		}

		public new static string SELECT(long oid) { return SELECT(oid, true); }

		internal static string SELECT_FIELDS()
        {
            string query;

			query = 
				@"SELECT AG.* 
						,EN.""TIPO"" AS ""ENTIDAD""";

            return query;
        }

		public static string INNER(QueryConditions conditions)
		{
			string ta = nHManager.Instance.GetSQLTable(typeof(AgentRecord));
			string te = nHManager.Instance.GetSQLTable(typeof(EntityRecord));
			
			string query = @"
				FROM " + ta + @" AS AG 
				INNER JOIN " + te + " AS EN ON AG.\"OID_ENTIDAD\" = EN.\"OID\"";

			return query + " " + conditions.ExtraJoin;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			string query = @"
				WHERE " + FilterMng.GET_FILTERS_SQL(conditions.Filters, "AG", ForeignFields());

			query += Common.EntityBase.GET_IN_LIST_CONDITION(conditions.OidList, "EN");

			if (conditions.Agent != null) 
				query += @"
					AND AG.""OID"" = " + conditions.Agent.Oid;
			if (conditions.Entity != null) 
				query += @"
					AND AG.""OID_ENTIDAD"" = " + conditions.Entity.Oid;
			if (conditions.IHipatiaAgent != null) 
				query += @"
					AND AG.""OID_AGENTE_EXT"" = " + conditions.IHipatiaAgent.Oid;

			return query + " " + conditions.ExtraWhere;
		}
        
		public static string SELECT(QueryConditions conditions, bool lockTable)
		{
			string query =
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
			string query = string.Empty;

			QueryConditions conditions = new QueryConditions { Agent = AgenteInfo.New(oid) };

			query = SELECT(conditions, lockTable);

			return query;
		}

		public static string SELECT_COUNT() { return SELECT_COUNT(new QueryConditions()); }
		public static string SELECT_COUNT(QueryConditions conditions)
		{
			string query;

			query = @"
                SELECT COUNT(*) AS ""TOTAL_ROWS""" +
				SELECT(conditions, false);

			return query;
		}

        #endregion
	}
}

