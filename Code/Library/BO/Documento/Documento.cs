using System;
using System.ComponentModel;
using System.Data;
using System.Reflection;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx;

namespace moleQule.Library.Hipatia
{
	[Serializable()]
	public class DocumentRecord : RecordBase
	{
		#region Attributes

		private string _codigo = string.Empty;
		private long _serial;
		private string _nombre = string.Empty;
		private string _tipo = string.Empty;
		private DateTime _fecha;
		private DateTime _fecha_alta;
		private DateTime _expiration_date;
		private string _ruta = string.Empty;
		private string _observaciones = string.Empty;

		#endregion

		#region Properties

		public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
		public virtual long Serial { get { return _serial; } set { _serial = value; } }
		public virtual string Nombre { get { return _nombre; } set { _nombre = value; } }
		public virtual string Tipo { get { return _tipo; } set { _tipo = value; } }
		public virtual DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
		public virtual DateTime FechaAlta { get { return _fecha_alta; } set { _fecha_alta = value; } }
		public virtual DateTime ExpirationDate { get { return _expiration_date; } set { _expiration_date = value; } }
		public virtual string Ruta { get { return _ruta; } set { _ruta = value; } }
		public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }

		#endregion

		#region Business Methods

		public DocumentRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_codigo = Format.DataReader.GetString(source, "CODIGO");
			_serial = Format.DataReader.GetInt64(source, "SERIAL");
			_nombre = Format.DataReader.GetString(source, "NOMBRE");
			_tipo = Format.DataReader.GetString(source, "TIPO");
			_fecha = Format.DataReader.GetDateTime(source, "FECHA");
			_fecha_alta = Format.DataReader.GetDateTime(source, "FECHA_ALTA");
			_expiration_date = Format.DataReader.GetDateTime(source, "EXPIRATION_DATE");
			_ruta = Format.DataReader.GetString(source, "RUTA");
			_observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");

		}

		public virtual void CopyValues(DocumentRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_codigo = source.Codigo;
			_serial = source.Serial;
			_nombre = source.Nombre;
			_tipo = source.Tipo;
			_fecha = source.Fecha;
			_fecha_alta = source.FechaAlta;
			_expiration_date = source.ExpirationDate;
			_ruta = source.Ruta;
			_observaciones = source.Observaciones;
		}

		#endregion
	}

	[Serializable()]
	public class DocumentBase
	{
		#region Attributes

		private DocumentRecord _record = new DocumentRecord();

		public long _estado = (long)EEstado.Created;
		public int _n_agentes = 0;
		public long _oid_documento;
		public string _entidad = string.Empty;
		public string _agente = string.Empty;

		#endregion

		#region Properties

		public DocumentRecord Record { get { return _record; } }

		public EEstado EEstado { get { return (EEstado)_estado; } }
		public string EstadoLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EEstado); } }
		public bool Shared { get { return _n_agentes > 1; } }
		public long OidDocumento { get { return _oid_documento; } }
		public string Entidad { get { return _entidad; } }
		public string Agente { get { return _agente; } }
		public int NAgentes { get { return _n_agentes; } set { _n_agentes = value; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);

			long query = Format.DataReader.GetInt64(source, "QUERY");

			if (query == 1)
			{
				long oid_agente = Format.DataReader.GetInt64(source, "OID_AGENTE");
				long oid_entidad = Format.DataReader.GetInt64(source, "OID_ENTIDAD");
				string oid = oid_agente.ToString("00000") + oid_entidad.ToString("00") + _record.Oid.ToString("00000");
                _record.Oid = Convert.ToInt64(oid);
                _entidad = Format.DataReader.GetString(source, "TIPO_ENTIDAD");
			}

			_oid_documento = Format.DataReader.GetInt64(source, "OID");
			_n_agentes = Format.DataReader.GetInt32(source, "SHARED");
		}
		public void CopyValues(Documento source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_n_agentes = source.NAgentes;
			_oid_documento = source.OidDocumento;
			_entidad = source.Entidad;
			_agente = source.Agente;
		}
		public void CopyValues(DocumentoInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);

			_n_agentes = source.NAgentes;
			_oid_documento = source.OidDocumento;
			_entidad = source.Entidad;
			_agente = source.Agente;
		}

		#endregion
	}
	
    /// <summary>
    /// Editable Root Business Object With Editable Child Collection
    /// Editable Child Business Object With Editable Child Collection
    /// </summary>
    [Serializable()]
    public class Documento : BusinessBaseEx<Documento>
    {
		#region Attributes

		protected DocumentBase _base = new DocumentBase();   

        private AgenteDocumentos _agente_documentos = AgenteDocumentos.NewChildList();

		#endregion

		#region Properties

		public DocumentBase Base { get { return _base; } }

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
		public virtual DateTime FechaAlta
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.FechaAlta;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.FechaAlta.Equals(value))
				{
					_base.Record.FechaAlta = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime ExpirationDate
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.ExpirationDate;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.ExpirationDate.Equals(value))
				{
					_base.Record.ExpirationDate = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Ruta
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Ruta;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Ruta.Equals(value))
				{
					_base.Record.Ruta = value;
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

        public virtual AgenteDocumentos AgenteDocumentos
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _agente_documentos;
            }

            set
            {
                _agente_documentos = value;
            }
        }

		//NO ENLAZADOS
		public virtual bool Shared { get { return _base.Shared; } }
		public virtual long OidDocumento { get { return _base.OidDocumento; } }
		public virtual string Entidad { get { return _base.Entidad; } }
		public virtual string Agente { get { return _base.Agente; } }
		public virtual int NAgentes { get { return _base.NAgentes; } }

        public override bool IsValid
        {
            get
            {
                return base.IsValid
                   && _agente_documentos.IsValid;
            }
        }
        public override bool IsDirty
        {
            get
            {
                return base.IsDirty
                   || _agente_documentos.IsDirty;
            }
        }

		#endregion

		#region Business Methods
		
		/// <summary>
        /// Clona la entidad y sus subentidades y las marca como nuevas
        /// </summary>
        /// <returns>Una entidad clon</returns>
        public virtual Documento CloneAsNew()
        {
            Documento clon = base.Clone();

            //Se definen el Oid y el Coidgo como nueva entidad
			clon.Base.Record.Oid = (long)(new Random()).Next();

            clon.Codigo = (0).ToString(Resources.Defaults.DOCUMENTO_CODE_FORMAT);
            clon.SessionCode = Documento.OpenSession();
            Documento.BeginTransaction(clon.SessionCode);

            clon.MarkNew();
            clon.AgenteDocumentos.MarkAsNew();

            return clon;
        }

		public virtual void GetNewCode()
		{
			// Obtenemos el último serial de servicio
			Serial = SerialInfo.GetNext(typeof(Documento));
			Codigo = "DOC#" + Serial.ToString(Resources.Defaults.DOCUMENTO_CODE_FORMAT);
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

        /// <summary>
        /// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
        /// Debería ser private para CSLA porque la creación requiere el uso de los factory methods,
        /// pero debe ser protected por exigencia de NHibernate
        /// y public para que funcionen los DataGridView
        /// </summary>
        protected Documento() 
        {
            //MarkAsChild();
			_base.Record.Oid = (long)(new Random()).Next();
        }
		private Documento(Documento source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
			SessionCode = source.SessionCode;
            Fetch(source);
        }
		private Documento(int sessionCode, IDataReader source, bool childs)
        {
            MarkAsChild();	
			Childs = childs;
			SessionCode = sessionCode;
            Fetch(source);
        }

		public virtual DocumentoInfo GetInfo(bool childs = false)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(
                  Resources.Messages.USER_NOT_ALLOWED);

            return new DocumentoInfo(this, childs);
        }

		public static Documento NewChild()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			Documento obj = DataPortal.Create<Documento>(new CriteriaCs(-1));
			obj.MarkAsChild();
			return obj;
		}

		internal static Documento GetChild(Documento source)
		{
			return new Documento(source, false);
		}
		internal static Documento GetChild(Documento source, bool childs)
		{
			return new Documento(source, childs);
		}
		internal static Documento GetChild(int sessionCode, IDataReader source) { return GetChild(sessionCode, source, false); }
		internal static Documento GetChild(int sessionCode, IDataReader source, bool childs) { return new Documento(sessionCode, source, childs); }

        #endregion

        #region Root Factory Methods

        public static Documento New(int sessionCode = -1)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);

			Documento obj = DataPortal.Create<Documento>(new CriteriaCs(-1));
			obj.SetSharedSession(sessionCode);
			return obj;
        }

		public new static Documento Get(string query, bool childs, int sessionCode = -1)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return BusinessBaseEx<Documento>.Get(query, childs, -1);
		}

		public static Documento Get(long oid, bool childs = true) { return Get(Documento.SELECT(oid), childs); }

        public static Documento Get(CriteriaEx criteria)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);

            Documento.BeginTransaction(criteria.Session);
            return DataPortal.Fetch<Documento>(criteria);
        }

        /// <summary>
        /// Borrado inmediato, no cabe "undo"
        /// (La funcion debe ser "estatica")
        /// </summary>
        /// <param name="oid"></param>
        public static void Delete(long oid)
        {
            if (!CanDeleteObject())
                throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);

            DataPortal.Delete(new CriteriaCs(oid));
        }

        /// <summary>
        /// Elimina todas los Documentos
        /// </summary>
        public static void DeleteAll()
        {
            //Iniciamos la conexion y la transaccion
            int sessCode = Documento.OpenSession();
            ISession sess = Documento.Session(sessCode);
            ITransaction trans = Documento.BeginTransaction(sessCode);

            try
            {
                sess.Delete("from DocumentRecord");
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null) trans.Rollback();
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }
            finally
            {
                Documento.CloseSession(sessCode);
            }
        }

        public override Documento Save()
        {
            // Por interfaz Root/Child
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

                // Update de las listas.
                _agente_documentos.Update(this);

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
				if (!SharedTransaction)
					if (CloseSessions) CloseSession(); 
					else BeginTransaction();
			}
        }

        #endregion

        #region Common Data Access

        [RunLocal()]
        private void DataPortal_Create(CriteriaCs criteria)
        {
            _base.Record.Oid = (long)(new Random()).Next();
            Fecha = DateTime.Today;
            FechaAlta = DateTime.Today;
			ExpirationDate = DateTime.MaxValue;
            _agente_documentos = AgenteDocumentos.NewChildList();
        }

		private void Fetch(IDataReader source)
		{
			_base.CopyValues(source);

			if (Childs)
			{
				string query = string.Empty;
				IDataReader reader;

				AgenteDocumento.DoLOCK(Session());
				query = AgenteDocumentos.SELECT(this);
				reader = nHManager.Instance.SQLNativeSelect(query, Session());
				_agente_documentos = AgenteDocumentos.GetChildList(reader);
			}

			MarkOld();
		}

		private void Fetch(Documento source)
		{
			_base.CopyValues(source);

			if (Childs)
			{
				string query = string.Empty;
				IDataReader reader;

				AgenteDocumento.DoLOCK(Session());
				query = AgenteDocumentos.SELECT(this);
				reader = nHManager.Instance.SQLNativeSelect(query, Session());
				_agente_documentos = AgenteDocumentos.GetChildList(reader);
			}

			MarkOld();
		}

		/// <summary>
		/// Inserta el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para insertar elementos<remarks/>
		internal void Insert(Documentos parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			parent.Session().Save(Base.Record);

			MarkOld();
		}

		/// <summary>
		/// Actualiza el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para actualizar elementos<remarks/>
		internal void Update(Documentos parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			SessionCode = parent.SessionCode;
			DocumentRecord obj = Session().Get<DocumentRecord>(Oid);
			obj.CopyValues(Base.Record);
			Session().Update(obj);

			MarkOld();
		}

		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(Documentos parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<DocumentRecord>(Oid));

			MarkNew();
		}

        #endregion

        #region Root Data Access

        // called to retrieve data from the database
        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            try
            {
				_base.Record.Oid = 0;;
                SessionCode = criteria.SessionCode;
                Childs = criteria.Childs;

                if (nHMng.UseDirectSQL)
                {
                    Documento.DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    if (reader.Read())
                        _base.CopyValues(reader);

                    if (Childs)
                    {
                        string query = string.Empty;

                        AgenteDocumento.DoLOCK(Session());
                        query = AgenteDocumentos.SELECT(this);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
                        _agente_documentos = AgenteDocumentos.GetChildList(reader);
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
              
				GetNewCode();
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
					DocumentRecord obj = Session().Get<DocumentRecord>(OidDocumento);
                    obj.CopyValues(Base.Record);
                    Session().Update(obj);
                }
                catch (Exception ex)
                {
					iQExceptionHandler.TreatException(ex);
                }
            }
        }

        // deferred deletion
        [Transactional(TransactionalTypes.Manual)]
        protected override void DataPortal_DeleteSelf()
        {
			DataPortal_Delete(new CriteriaCs(OidDocumento));
        }

        // inmediate deletion
        [Transactional(TransactionalTypes.Manual)]
        private void DataPortal_Delete(CriteriaCs criterio)
        {
            if (this.AgenteDocumentos.Count < 0)
                throw new iQException("Existe al menos un agente asociado con este documento. No se puede borrar.");

            try
            {
                //Iniciamos la conexion y la transaccion
                SessionCode = OpenSession();
                BeginTransaction();

                //Si no hay integridad referencial, aqui se deben borrar las listas hijo
                CriteriaEx criteria = GetCriteria();
                criteria.AddOidSearch(criterio.Oid);

                // Obtenemos el objeto
				DocumentRecord obj = (DocumentRecord)(criteria.UniqueResult());
				Session().Delete(Session().Get<DocumentRecord>(obj.Oid));

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

		#region SQL

		public new static string SELECT(long oid) { return SELECT(oid, true); }

		internal static string SELECT_BASE_FIELDS()
		{
			string query;

			query = "SELECT DISTINCT DC.*" +
					"       ,AD.\"SHARED\"";

			return query;
		}

		internal static string SELECT_FIELDS()
		{
			string query;

			query = SELECT_BASE_FIELDS() +
					"		,0 AS \"QUERY\"";

			return query;
		}

		internal static string SELECT_FIELDS_ENTIDAD()
		{
			string query;

			query = SELECT_BASE_FIELDS() +
					"		,AG.\"OID\" AS \"OID_AGENTE\"" +
					"		,AG.\"NOMBRE\" AS \"AGENTE\"" +
					"		,EN.\"OID\" AS \"OID_ENTIDAD\"" +
					"		,EN.\"TIPO\" AS \"TIPO_ENTIDAD\"" +
					"		,1 AS \"QUERY\"";

			return query;
		}

		public static string INNER(QueryConditions conditions)
		{
			string dc = nHManager.Instance.GetSQLTable(typeof(DocumentRecord));
			string ad = nHManager.Instance.GetSQLTable(typeof(AgentDocumentRecord));

			string query = @"
				FROM " + dc + @" AS DC
				INNER JOIN (SELECT AD1.""OID_DOCUMENTO"", COUNT(AD1.""OID_AGENTE"") AS ""SHARED"" 
							FROM " + ad + @"AS AD1
							GROUP BY AD1.""OID_DOCUMENTO"")
					AS AD ON AD.""OID_DOCUMENTO"" = DC.""OID""
				INNER JOIN " + ad + @" AS AD2 ON AD2.""OID_DOCUMENTO"" = DC.""OID""";

			return query + " " + conditions.ExtraJoin;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			string query = string.Empty;

			query += @"
				WHERE TRUE";

			if (conditions.Document != null) 
				query += @"
					AND DC.""OID"" = " + conditions.Document.Oid;
			if (conditions.Entity != null) 
				query += @"
					AND EN.""OID"" = " + conditions.Entity.Oid;
			if (conditions.Agent != null)
			{
				query += @"
					AND AG.""OID"" = " + conditions.Agent.Oid;
				query += @"
					AND AG.""OID_ENTIDAD"" = " + conditions.Agent.OidEntidad;
			}
			if (conditions.IHipatiaAgent != null)
			{
				query += @"
					AND AG.""OID_AGENTE_EXT"" = " + conditions.IHipatiaAgent.Oid;
				query += @"
					AND EN.""TIPO"" = '" + conditions.IHipatiaAgent.TipoEntidad.Name + "'";
			}
			return query;
		}

		internal static string SELECT(QueryConditions conditions, bool lockTable)
		{
			string query;

			query = SELECT_FIELDS() +
					INNER(conditions) +
					WHERE(conditions);

			query += @"
				ORDER BY DC.""NOMBRE""";

			//query += Common.EntityBase.LOCK("DC", lockTable);

			return query;
		}

		internal static string SELECT(long oid, bool lockTable)
		{
			string query = string.Empty;

			QueryConditions conditions = new QueryConditions { Document = DocumentoInfo.New(oid) };

			query = SELECT(conditions, lockTable);

			return query;
		}

		internal static string SELECT_BY_ENTIDAD(QueryConditions conditions, bool lockTable)
		{
			string ag = nHManager.Instance.GetSQLTable(typeof(AgentRecord));
			string en = nHManager.Instance.GetSQLTable(typeof(EntityRecord));

			conditions.ExtraJoin = 	 @"
				INNER JOIN " + ag + @" AS AG ON AD2.""OID_AGENTE"" = AG.""OID""
				INNER JOIN " + en + @" AS EN ON EN.""OID"" = AG.""OID_ENTIDAD""";

			string query = 
				SELECT_FIELDS_ENTIDAD() +
				INNER(conditions) +
				WHERE(conditions);

			query += Common.EntityBase.LOCK("DC", lockTable);

			return query;
		}

		#endregion
	}
}

