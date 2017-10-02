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
	/// Read Only Child Collection of Business Objects
	/// </summary>
    [Serializable()]
	public class AgenteDocumentoList : ReadOnlyListBaseEx<AgenteDocumentoList, AgenteDocumentoInfo>
	{		 
		#region Factory Methods

		private AgenteDocumentoList() { }		
		private AgenteDocumentoList(IList<AgenteDocumento> lista)
		{
            Fetch(lista);
        }
        private AgenteDocumentoList(IDataReader reader)
		{
			Fetch(reader);
		}
		
		/// <summary>
		/// Builds a AgenteDocumentoList
		/// </summary>
		/// <param name="list"></param>
		/// <returns>AgenteDocumentoList</returns>
		public static AgenteDocumentoList GetList(bool childs)
		{
			CriteriaEx criteria = AgenteDocumento.GetCriteria(AgenteDocumento.OpenSession());
            criteria.Childs = childs;
						
			criteria.Query = SELECT();			

			AgenteDocumentoList list = DataPortal.Fetch<AgenteDocumentoList>(criteria);

            CloseSession(criteria.SessionCode);
			return list;
		}

        public static long CountAssociations(long oid)
        {
            CriteriaEx criteria = AgenteDocumento.GetCriteria(AgenteDocumento.OpenSession());
            criteria.Childs = false;

			QueryConditions conditions = new QueryConditions() { Document = DocumentoInfo.New(oid) };
            criteria.Query = AgenteDocumentos.SELECT(conditions);

            AgenteDocumentoList list = DataPortal.Fetch<AgenteDocumentoList>(criteria);

            CloseSession(criteria.SessionCode);
            return list.Count;
        }

		/// <summary>
		/// Builds a AgenteDocumentoList
		/// </summary>
		/// <param name="list"></param>
		/// <returns>AgenteDocumentoList</returns>
		public static AgenteDocumentoList GetList()
		{ 
			return AgenteDocumentoList.GetList(true); 
		}

		/// <summary>
        /// Devuelve una lista de todos los elementos
        /// </summary>
        /// <returns>Lista de elementos</returns>
        public static AgenteDocumentoList GetList(CriteriaEx criteria)
        {
            return AgenteDocumentoList.RetrieveList(typeof(AgenteDocumento), AppContext.ActiveSchema.Code, criteria);
        }
		
		/// <summary>
        /// Builds a AgenteDocumentoList from a IList<!--<AgenteDocumentoInfo>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>AgenteDocumentoList</returns>
        public static AgenteDocumentoList GetChildList(IList<AgenteDocumentoInfo> list)
        {
            AgenteDocumentoList flist = new AgenteDocumentoList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (AgenteDocumentoInfo item in list)
                    flist.Add(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }

        /// <summary>
        /// Builds a AgenteDocumentoList from IList<!--<AgenteDocumento>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>AgenteDocumentoList</returns>
        public static AgenteDocumentoList GetChildList(IList<AgenteDocumento> list) { return new AgenteDocumentoList(list); }

        public static AgenteDocumentoList GetChildList(IDataReader reader) { return new AgenteDocumentoList(reader); }

		
		#endregion

		#region Data Access
		
		// called to copy objects data from list
        private void Fetch(IList<AgenteDocumento> lista)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            foreach (AgenteDocumento item in lista)
                this.Add(item.GetInfo());

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }

        // called to copy objects data from list
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            while (reader.Read())
                this.Add(AgenteDocumento.GetChild(reader).GetInfo());

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }
		
		// called to retrieve data from db
		protected override void Fetch(CriteriaEx criteria)
		{
			this.RaiseListChangedEvents = false;

			SessionCode = criteria.SessionCode;
			Childs = criteria.Childs;

			try
			{
				if (nHMng.UseDirectSQL)
				{
					IDataReader reader = nHManager.Instance.SQLNativeSelect(criteria.Query, Session());

					IsReadOnly = false;

					while (reader.Read())
						this.AddItem(AgenteDocumentoInfo.Get(reader,Childs));

					IsReadOnly = true;
				}
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}

			this.RaiseListChangedEvents = true;
		}

		#endregion

		#region SQL

		public static string SELECT() { return AgenteDocumento.SELECT(new QueryConditions(), false); }
		public static string SELECT(QueryConditions conditions) { return AgenteDocumento.SELECT(conditions, false); }
		public static string SELECT(AgenteInfo source) { return SELECT(new QueryConditions { Agent = source }); }
		public static string SELECT(DocumentoInfo source) { return SELECT(new QueryConditions { Document = source }); }

		#endregion
	}
}

