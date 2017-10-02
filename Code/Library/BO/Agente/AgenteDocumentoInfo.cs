using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx;

namespace moleQule.Library.Hipatia
{
	/// <summary>
	/// ReadOnly Child Business Object
    /// </summary>
	[Serializable()]
	public class AgenteDocumentoInfo : ReadOnlyBaseEx<AgenteDocumentoInfo, AgenteDocumento>
	{	
		#region Attributes

		protected AgentDocumentBase _base = new AgentDocumentBase();

        #endregion

        #region Properties

		public AgentDocumentBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidAgente { get { return _base.Record.OidAgente; } }
		public long OidDocumento { get { return _base.Record.OidDocumento; } }

        #endregion

        #region Business Methods
		
		#endregion		 

		#region Factory Methods
		 
		protected AgenteDocumentoInfo() { /* require use of factory methods */ }
		private AgenteDocumentoInfo(IDataReader reader, bool childs)
		{
			Childs = childs;
			Fetch(reader);
		}			
		internal AgenteDocumentoInfo(AgenteDocumento source)
		{
			_base.CopyValues(source);		
		}

		/// <summary>
		/// Copia los datos al objeto desde un IDataReader 
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static AgenteDocumentoInfo Get(IDataReader reader, bool childs)
		{
			return new AgenteDocumentoInfo(reader, childs);
		}

		public static AgenteDocumentoInfo New(long oid = 0) { return new AgenteDocumentoInfo() { Oid = oid }; }

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
				iQExceptionHandler.TreatException(ex);
			}
		}
					
		#endregion		
	}
}

