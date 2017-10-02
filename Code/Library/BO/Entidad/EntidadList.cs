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
	public class EntidadList : ReadOnlyListBaseEx<EntidadList, EntidadInfo>
	{	
		#region Business Methods

		#endregion
		 
		#region Factory Methods
		 
		private EntidadList() {}
		
		/// <summary>
		/// Retrieve the complete list from db
		/// </summary>
		/// <param name="get_childs">retrieving the childs</param>
		/// <returns></returns>
		public static EntidadList GetList(bool childs = true)
		{
			CriteriaEx criteria = Entidad.GetCriteria(Entidad.OpenSession());
			criteria.Childs = childs;
			
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = EntidadList.SELECT();

			EntidadList list = DataPortal.Fetch<EntidadList>(criteria);
			
			CloseSession(criteria.SessionCode);
			return list;
		}
			
		/// <summary>
		/// Devuelve una lista de todos los elementos
		/// </summary>
		/// <returns>Lista de elementos</returns>
		public static EntidadList GetList(CriteriaEx criteria)
		{
			return EntidadList.RetrieveList(typeof(Entidad), AppContext.ActiveSchema.Code, criteria);
		}
		public static EntidadList GetList(IList<EntidadInfo> list)
		{
			EntidadList flist = new EntidadList();
			
			if (list.Count > 0)
			{
				flist.IsReadOnly = false;
				
				foreach (EntidadInfo item in list)
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
		public static SortedBindingList<EntidadInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<EntidadInfo> sortedList = new SortedBindingList<EntidadInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
		
		/// <summary>
        /// Devuelve una lista ordenada de todos los elementos y sus hijos
        /// </summary>
        /// <param name="sortProperty">Campo de ordenación</param>
        /// <param name="sortDirection">Sentido de ordenación</param>
        /// <param name="childs">Traer hijos</param>
        /// <returns>Lista ordenada de elementos</returns>
        public static SortedBindingList<EntidadInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<EntidadInfo> sortedList = new SortedBindingList<EntidadInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
		
		/// <summary>
        /// Builds a EntidadList from a IList<!--<Entidad>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>EntidadList</returns>
        public static EntidadList GetList(IList<Entidad> list)
        {
            EntidadList flist = new EntidadList();

            if (list != null)
            {
                flist.IsReadOnly = false;

                foreach (Entidad item in list)
                    flist.Add(item.GetInfo());

                flist.IsReadOnly = true;
            }

            return flist;
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

                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query); 
						
					IsReadOnly = false;
						
					while (reader.Read())
						this.AddItem(EntidadInfo.GetChild(SessionCode, reader, Childs));

					IsReadOnly = true;

					if (criteria.PagingInfo != null)
					{
						reader = nHManager.Instance.SQLNativeSelect(Entidad.SELECT_COUNT(criteria), criteria.Session);
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
		public static string SELECT(QueryConditions conditions) { return Entidad.SELECT(conditions, false); }

		#endregion		
	}
}

