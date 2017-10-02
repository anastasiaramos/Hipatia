using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using Csla.Validation;
using moleQule.Library;
using moleQule.Library.CslaEx;
using moleQule.Library.Common;
using NHibernate;

namespace moleQule.Library.Hipatia
{
	[Serializable()]
	public class DocumentTypeRecord : RecordBase
	{
		#region Attributes

		private string _valor = string.Empty;
		private bool _user_created = false;

		#endregion

		#region Properties

		public virtual string Valor { get { return _valor; } set { _valor = value; } }
		public virtual bool UserCreated { get { return _user_created; } set { _user_created = value; } }

		#endregion

		#region Business Methods

		public DocumentTypeRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_valor = Format.DataReader.GetString(source, "VALOR");
			_user_created = Format.DataReader.GetBool(source, "USER_CREATED");

		}
		public virtual void CopyValues(DocumentTypeRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_valor = source.Valor;
			_user_created = source.UserCreated;
		}

		#endregion
	}

	[Serializable()]
	public class DocumentTypeBase
	{
		#region Attributes

		private DocumentTypeRecord _record = new DocumentTypeRecord();

		#endregion

		#region Properties

		public DocumentTypeRecord Record { get { return _record; } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);
		}
		internal void CopyValues(Tipodocumento source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);
		}
		internal void CopyValues(TipodocumentoInfo source)
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
	public class Tipodocumento : BusinessBaseEx<Tipodocumento>
	{
		#region Attributes

		protected DocumentTypeBase _base = new DocumentTypeBase();

		#endregion

		#region Properties

		public DocumentTypeBase Base { get { return _base; } }

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
		public virtual string Valor
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Valor;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Valor.Equals(value))
				{
					_base.Record.Valor = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual bool UserCreated
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.UserCreated;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.UserCreated.Equals(value))
				{
					_base.Record.UserCreated = value;
					PropertyHasChanged();
				}
			}
		}

		#endregion
		 
	    #region Validation Rules
		 
		//región a rellenar si hay campos requeridos o claves ajenas
		
		protected override void AddBusinessRules()
        {	
			ValidationRules.AddRule(CommonRules.StringRequired, "Valor");
			
			//Agregar otras reglas de validación
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
		 
		#region Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate
		/// y public para que funcionen los DataGridView
		/// </summary>
		public Tipodocumento() 
		{ 
			MarkAsChild();
			_base.Record.Oid = (long)(new Random()).Next();
			//Rellenar si hay más campos que deban ser inicializados aquí
		}			
		private Tipodocumento(Tipodocumento source)
		{
			MarkAsChild();
			Fetch(source);
		}		
		private Tipodocumento(IDataReader reader)
		{
			MarkAsChild();
			Fetch(reader);
		}
		
		//Por cada padre que tenga la clase
		public static Tipodocumento NewChild()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);
			
			return new Tipodocumento();
		}		
		
		internal static Tipodocumento GetChild(Tipodocumento source)
		{
			return new Tipodocumento(source);
		}		
		internal static Tipodocumento GetChild(IDataReader reader)
		{
			return new Tipodocumento(reader);
		}
		
		public virtual TipodocumentoInfo GetInfo() { return new TipodocumentoInfo(this); }
			
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
		public override Tipodocumento Save()
		{
			throw new iQException(Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
		}		
			
		#endregion
		 
		#region Child Data Access
		 
		private void Fetch(Tipodocumento source)
		{
			_base.CopyValues(source);
			MarkOld();
		}
		
		private void Fetch(IDataReader reader)
		{
			_base.CopyValues(reader);
			MarkOld();
		}		
		
		internal void Insert(Tipodocumentos parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			try
			{	
				parent.Session().Save(Base.Record);

				MarkOld();
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}				
		}

		internal void Update(Tipodocumentos parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;			
			
			try
			{
				SessionCode = parent.SessionCode;
				DocumentTypeRecord obj = Session().Get<DocumentTypeRecord>(Oid);
				obj.CopyValues(Base.Record);
				Session().Update(obj);

				MarkOld();
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}		
		}

		internal void DeleteSelf(Tipodocumentos parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<DocumentTypeRecord>(Oid));

				MarkNew(); 
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}
		
		#endregion	

		#region SQL

		internal static Dictionary<String, ForeignField> ForeignFields()
		{
			return new Dictionary<String, ForeignField>() { };
		}

		//public new static string SELECT(long oid) { return SELECT(oid, true); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }

		internal static string SELECT_FIELDS()
		{
			string query;

			query = @"
				SELECT DT.*";

			return query;
		}

		internal static string JOIN(QueryConditions conditions)
		{
			string pu = nHManager.Instance.GetSQLTable(typeof(DocumentTypeRecord));

			string query;

			query = @"
				FROM " + pu + @" AS DT";

			return query + " " + conditions.ExtraJoin;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			string query;

			query = @" 
				WHERE " + FilterMng.GET_FILTERS_SQL(conditions.Filters, "DT", ForeignFields());

			query += Common.EntityBase.STATUS_LIST_CONDITION(conditions.Status, "DT");
			query += Common.EntityBase.GET_IN_LIST_CONDITION(conditions.OidList, "DT");

			/*if (conditions.Municipio != null)
				query += @"
					AND P.""OID"" = " + conditions.Municipio.Oid;*/

			return query + " " + conditions.ExtraWhere;
		}

		internal static string SELECT(QueryConditions conditions, bool lockTable)
		{
			string query =
				SELECT_FIELDS() +
				JOIN(conditions) +
				WHERE(conditions);
			
			if (conditions != null)
			{
				query += ORDER(conditions.Orders, "DT", ForeignFields());
				query += LIMIT(conditions.PagingInfo);
			}

			query += Common.EntityBase.LOCK("DT", lockTable);

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

		/*internal static string SELECT(long oid, bool lockTable)
		{
			return SELECT(new QueryConditions { Municipio = MunicipioInfo.New(oid) }, lockTable);
		}*/

		#endregion

	}
}

