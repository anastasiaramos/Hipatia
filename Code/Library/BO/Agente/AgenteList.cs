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
	/// ReadOnly Root Collection of Business Objects With Child Collection
	/// </summary>
    [Serializable()]
	public class AgenteList : ReadOnlyListBaseEx<AgenteList, AgenteInfo>
	{	
		#region Business Methods
			
		#endregion
		
        #region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private AgenteList() {}
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private AgenteList(IList<Agente> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private AgenteList(IDataReader reader, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(reader);
        }
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private AgenteList(IList<AgenteInfo> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }
		
		#endregion

		#region Factory Methods

		public static AgenteList NewList() { return new AgenteList(); }

        public static AgenteList GetList() { return AgenteList.GetList(true); }
		public static AgenteList GetList(bool childs)
		{
			CriteriaEx criteria = Agente.GetCriteria(Agente.OpenSession());
			criteria.Childs = childs;
			
			//No criteria. Retrieve all de List
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = AgenteList.SELECT();
			
			AgenteList list = DataPortal.Fetch<AgenteList>(criteria);
			
			CloseSession(criteria.SessionCode);
			return list;
		}

        public static AgenteList GetListByEntidad(EntidadInfo entidad)
        {
            CriteriaEx criteria = Agente.GetCriteria(Agente.OpenSession());
            criteria.Childs = false;

            //No criteria. Retrieve all de List
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = AgenteList.SELECT(entidad);

            AgenteList list = DataPortal.Fetch<AgenteList>(criteria);

            CloseSession(criteria.SessionCode);
            return list;
        }

		/// <summary>
		/// Devuelve una lista de todos los elementos
		/// </summary>
		/// <returns>Lista de elementos</returns>
		public static AgenteList GetList(CriteriaEx criteria)
		{
			return AgenteList.RetrieveList(typeof(Agente), AppContext.ActiveSchema.Code, criteria);
		}	
		public static AgenteList GetList(IList<AgenteInfo> list)
		{
			AgenteList flist = new AgenteList();
			
			if (list.Count > 0)
			{
				flist.IsReadOnly = false;
				
				foreach (AgenteInfo item in list)
					flist.Add(item);
				
				flist.IsReadOnly = true;
			}
			
			return flist;
		}
		public static AgenteList GetList(IList<Agente> list)
		{
			AgenteList flist = new AgenteList();

			if (list != null)
			{
				flist.IsReadOnly = false;

				foreach (Agente item in list)
					flist.Add(item.GetInfo());

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
		public static SortedBindingList<AgenteInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<AgenteInfo> sortedList = new SortedBindingList<AgenteInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<AgenteInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<AgenteInfo> sortedList = new SortedBindingList<AgenteInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
		
		#endregion

        #region Child Factory Methods

        /// <summary>
        /// Construye la lista
        /// </summary>
        /// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
        /// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
        public static AgenteList GetChildList(IList<Agente> list) { return new AgenteList(list, false); }
        public static AgenteList GetChildList(IList<Agente> list, bool retrieve_childs) { return new AgenteList(list, retrieve_childs); }
        public static AgenteList GetChildList(IDataReader reader) { return new AgenteList(reader, false); }
        public static AgenteList GetChildList(IDataReader reader, bool retrieve_childs) { return new AgenteList(reader, retrieve_childs); }
        public static AgenteList GetChildList(IList<AgenteInfo> list) { return new AgenteList(list, false); }

        #endregion

        #region Common Data Access

        /// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
        /// </summary>
        /// <param name="lista">IList origen</param>
        private void Fetch(IList<Agente> lista)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            foreach (Agente item in lista)
                this.AddItem(item.GetInfo(Childs));

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }

        /// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
        /// </summary>
        /// <param name="reader">IDataReader origen</param>
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            while (reader.Read())
                this.AddItem(AgenteInfo.GetChild(SessionCode, reader, Childs));

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }

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
						this.AddItem(AgenteInfo.Get(SessionCode, reader, Childs));

					IsReadOnly = true;

					if (criteria.PagingInfo != null)
					{
						reader = nHManager.Instance.SQLNativeSelect(Agente.SELECT_COUNT(criteria), criteria.Session);
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
				
		#endregion

		#region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return Agente.SELECT(conditions, true); }
		public static string SELECT(EntidadInfo source) { return Agente.SELECT(new QueryConditions() { Entity = source }, true); }

        #endregion
	}
}

