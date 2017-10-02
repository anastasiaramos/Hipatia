using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using moleQule.Library;
using moleQule.Library.CslaEx;

namespace moleQule.Library.Hipatia
{
    /// <summary>
    /// Read Only Root Collection of Business Objects With Child Collection
    /// Read Only Child Collection of Business Objects With Child Collection
    /// </summary>
    [Serializable()]
    public class DocumentoList : ReadOnlyListBaseEx<DocumentoList, DocumentoInfo>
    {
        #region Factory Methods

        private DocumentoList() { }

        private DocumentoList(IDataReader reader)
        {
            Fetch(reader);
        }

        /// <summary>
        /// Retrieve the complete list from db
        /// </summary>
        /// <param name="get_childs">retrieving the childs</param>
        /// <returns>DocumentoList</returns>
        public static DocumentoList GetChildList(bool childs)
        {
            CriteriaEx criteria = Documento.GetCriteria(Documento.OpenSession());
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT();

            DocumentoList list = DataPortal.Fetch<DocumentoList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

        #endregion

        #region Root Factory Methods

		public static DocumentoList NewList() { return new DocumentoList(); }

		public static DocumentoList GetList() { return DocumentoList.GetList(true); }
        public static DocumentoList GetList(bool childs)
        {
            CriteriaEx criteria = Documento.GetCriteria(Documento.OpenSession());
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = DocumentoList.SELECT();

            DocumentoList list = DataPortal.Fetch<DocumentoList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

		public static DocumentoList GetListByAgente(Agente agente) { return GetListByAgente(agente.GetInfo(false)); }
        public static DocumentoList GetListByAgente(AgenteInfo agente)
        {
            CriteriaEx criteria = Documento.GetCriteria(Documento.OpenSession());
            criteria.Childs = true;

			QueryConditions conditions = new QueryConditions { Agent = agente };

            criteria.Query = DocumentoList.SELECT_BY_AGENTE(conditions);

            DocumentoList list = DataPortal.Fetch<DocumentoList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }
		public static DocumentoList GetListByAgente(IAgenteHipatia agente)
		{
			CriteriaEx criteria = Documento.GetCriteria(Documento.OpenSession());
			criteria.Childs = true;

			QueryConditions conditions = new QueryConditions { IHipatiaAgent = agente };

			criteria.Query = DocumentoList.SELECT_BY_AGENTE(conditions);

			DocumentoList list = DataPortal.Fetch<DocumentoList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}

		public static DocumentoList GetListByEntidad(EntidadInfo entidad)
		{
			CriteriaEx criteria = Documento.GetCriteria(Documento.OpenSession());
			criteria.Childs = false;
			
			QueryConditions conditions = new QueryConditions { Entity = entidad };
			
			criteria.Query = DocumentoList.SELECT_BY_ENTIDAD(conditions);

			DocumentoList list = DataPortal.Fetch<DocumentoList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}

        public static DocumentoList GetListExceptAgente(long oid_agente)
        {
            CriteriaEx criteria = Documento.GetCriteria(Documento.OpenSession());
            criteria.Childs = true;

            //No criteria. Retrieve all de List
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = DocumentoList.SELECT_EXCEPT_AGENTE(oid_agente);
            DocumentoList list = DataPortal.Fetch<DocumentoList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }
 
        public static DocumentoList GetList(CriteriaEx criteria)
        {
            return DocumentoList.RetrieveList(typeof(Documento), AppContext.ActiveSchema.Code, criteria);
        }
        public static DocumentoList GetList(IList<DocumentoInfo> list)
        {
            DocumentoList flist = new DocumentoList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (DocumentoInfo item in list)
                    flist.Add(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }

        /// <summary>
        /// Devuelve una lista ordenada de todos los elementos
        /// </summary>
        /// <param name="sortProperty">Campo de ordenación</param>
        /// <param name="sortDirection">Sentido de ordenación</param>
        /// <returns>Lista ordenada de elementos</returns>
        public static SortedBindingList<DocumentoInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection)
        {
            SortedBindingList<DocumentoInfo> sortedList = new SortedBindingList<DocumentoInfo>(GetList());

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
        public static SortedBindingList<DocumentoInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<DocumentoInfo> sortedList = new SortedBindingList<DocumentoInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }

        /// <summary>
        /// Builds a DocumentoList from a IList<!--<Documento>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>DocumentoList</returns>
        public static DocumentoList GetList(IList<Documento> list)
        {
            DocumentoList flist = new DocumentoList();

            if (list != null)
            {
                flist.IsReadOnly = false;

                foreach (Documento item in list)
                    flist.Add(item.GetInfo());

                flist.IsReadOnly = true;
            }

            return flist;
        }

        #endregion

        #region Child Factory Methods

        /// <summary>
        /// Default call for GetChildList(bool get_childs)
        /// </summary>
        /// <returns></returns>
        public static DocumentoList GetChildList()
        {
            return DocumentoList.GetChildList(true);
        }

        /// <summary>
        /// Builds a DocumentoList from a IList<!--<DocumentoInfo>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>DocumentoList</returns>
        public static DocumentoList GetChildList(IList<DocumentoInfo> list)
        {
            DocumentoList flist = new DocumentoList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (DocumentoInfo item in list)
                    flist.Add(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }

        /// <summary>
        /// Builds a DocumentoList from IList<!--<Documento>--> and retrieve DocumentoInfo Childs from DB
        /// </summary>
        /// <param name="list"></param>
        /// <returns>DocumentoList</returns>
        public static DocumentoList GetChildList(IList<Documento> list)
        {
            DocumentoList flist = new DocumentoList();

            if (list != null)
            {
                int sessionCode = Documento.OpenSession();
                CriteriaEx criteria = null;

                flist.IsReadOnly = false;

                foreach (Documento item in list)
                {
                    criteria = AgenteDocumento.GetCriteria(sessionCode);
                    criteria.AddEq("OidDocumento", item.Oid);
                    criteria.AddOrder("Codigo", true);

                    item.AgenteDocumentos = AgenteDocumentos.GetChildList(criteria.List<AgenteDocumento>());


                    flist.Add(item.GetInfo());
                }

                flist.IsReadOnly = true;

                Documento.CloseSession(sessionCode);
            }

            return flist;
        }

        public static DocumentoList GetChildList(IDataReader reader) { return new DocumentoList(reader); }

        #endregion

        #region Data Access

        // called to retrieve data from database
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
                        this.AddItem(DocumentoInfo.Get(SessionCode, reader, Childs));

                    IsReadOnly = true;

					if (criteria.PagingInfo != null)
					{
						reader = nHManager.Instance.SQLNativeSelect(Documento.SELECT_COUNT(criteria), criteria.Session);
						if (reader.Read()) criteria.PagingInfo.TotalItems = Format.DataReader.GetInt32(reader, "TOTAL_ROWS");
					}
                }
            }
            catch (Exception ex)
            {
				iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
            }

            this.RaiseListChangedEvents = true;
        }

        // called to retrieve data from db
        protected override void Fetch(string hql)
        {
            this.RaiseListChangedEvents = false;

            try
            {
                IList list = nHMng.HQLSelect(hql);

                if (list.Count > 0)
                {
                    IsReadOnly = false;

                    foreach (Documento item in list)
                        this.Add(item.GetInfo(false));

                    IsReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            this.RaiseListChangedEvents = true;
        }

        // called to retrieve data from db
        protected void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            try
            {
                IsReadOnly = false;

                while (reader.Read())
                    this.AddItem(DocumentoInfo.Get(SessionCode, reader, Childs));

                IsReadOnly = true;

            }
            catch (Exception ex)
            {
				iQExceptionHandler.TreatException(ex);
            }

            this.RaiseListChangedEvents = true;
        }

        #endregion

        #region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return Documento.SELECT(conditions, false); }
		
		public static string SELECT_BY_ENTIDAD(QueryConditions conditions)
		{
			string query = string.Empty;

			query = Documento.SELECT_BY_ENTIDAD(conditions, false);

			return query;
		}
		public static string SELECT_BY_ENTIDAD(EntidadInfo entidad)
		{
			string query = string.Empty;

			QueryConditions conditions = new QueryConditions { Entity = entidad };

			query = Documento.SELECT_BY_ENTIDAD(conditions, false);

			return query;
		}

		public static string SELECT_BY_AGENTE(QueryConditions conditions)
		{
			string query = string.Empty;

			query = Documento.SELECT_BY_ENTIDAD(conditions, false);

			return query;
		}
		public static string SELECT_BY_AGENTE(AgenteInfo agente)
		{
			string query = string.Empty;

			QueryConditions conditions = new QueryConditions { Agent = agente };

			query = Documento.SELECT_BY_ENTIDAD(conditions, false);

			return query;
		}
		
		public static string SELECT_EXCEPT_AGENTE(long oid_agente)
		{
			string query = string.Empty;
			string tabla = nHManager.Instance.GetSQLTable(typeof(DocumentRecord));
			string agente_documento = nHManager.Instance.GetSQLTable(typeof(AgentDocumentRecord));

			query = "SELECT DISTINCT D.*" +
					"		,0 AS \"QUERY\"" +
					" FROM " + tabla + " AS D" +
					" INNER JOIN " + agente_documento + " AS AD ON AD.\"OID_DOCUMENTO\" = D.\"OID\"" +
					" WHERE D.\"OID\" NOT IN (SELECT D1.\"OID\"" +
					"                         FROM " + tabla + " AS D1" +
					"                         INNER JOIN " + agente_documento + " AS AD1 ON AD1.\"OID_DOCUMENTO\" = D1.\"OID\"" +
					"                         WHERE AD1.\"OID_AGENTE\" = " + oid_agente + ")";

			return query;
		}		

        #endregion
    }
}



