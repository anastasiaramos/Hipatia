using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using Csla.Validation;
using moleQule.Library;
using moleQule.Library.CslaEx;
using NHibernate;

namespace moleQule.Library.Hipatia
{
	/// <summary>
	/// ReadOnly Child Business Object
    /// </summary>
	[Serializable()]
	public class TipodocumentoInfo : ReadOnlyBaseEx<TipodocumentoInfo, Tipodocumento>
	{	
		#region Attributes

		protected DocumentTypeBase _base = new DocumentTypeBase();
		
		#endregion

        #region Properties

		public DocumentTypeBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public string Valor { get { return _base.Record.Valor; } }
		public bool UserCreated { get { return _base.Record.UserCreated; } }

        #endregion

        #region Business Methods
			
		#endregion		 

		#region Factory Methods
		 
		protected TipodocumentoInfo() { /* require use of factory methods */ }
		private TipodocumentoInfo(IDataReader reader, bool childs)
		{
			Childs = childs;
			Fetch(reader);
		}
			
		internal TipodocumentoInfo(Tipodocumento source)
		{
			_base.CopyValues(source);		
		}

		/// <summary>
		/// Copia los datos al objeto desde un IDataReader 
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static TipodocumentoInfo Get(IDataReader reader, bool childs)
		{
			return new TipodocumentoInfo(reader, childs);
		}
		
		#endregion		 
		 
		#region Data Access
		 
		//called to copy data from IDataReader
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
		}
					
		#endregion		
	}
}

