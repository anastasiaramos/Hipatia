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
	public class TipodocumentoList : ReadOnlyListBaseEx<TipodocumentoList, TipodocumentoInfo>
	{		 
		#region Factory Methods

		private TipodocumentoList() { }
		
		private TipodocumentoList(IList<Tipodocumento> lista)
		{
            Fetch(lista);
        }
        private TipodocumentoList(IDataReader reader)
		{
			Fetch(reader);
		}
		
		/// <summary>
		/// Builds a TipodocumentoList
		/// </summary>
		/// <param name="list"></param>
		/// <returns>TipodocumentoList</returns>
		public static TipodocumentoList GetList(bool childs)
		{
			CriteriaEx criteria = Tipodocumento.GetCriteria(Tipodocumento.OpenSession());
            criteria.Childs = childs;
			
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT();		

			TipodocumentoList list = DataPortal.Fetch<TipodocumentoList>(criteria);

            CloseSession(criteria.SessionCode);
			return list;
		}

		/// <summary>
		/// Builds a TipodocumentoList
		/// </summary>
		/// <param name="list"></param>
		/// <returns>TipodocumentoList</returns>
		public static TipodocumentoList GetList()
		{ 
			return TipodocumentoList.GetList(true); 
		}

		/// <summary>
        /// Devuelve una lista de todos los elementos
        /// </summary>
        /// <returns>Lista de elementos</returns>
        public static TipodocumentoList GetList(CriteriaEx criteria)
        {
            return TipodocumentoList.RetrieveList(typeof(Tipodocumento), AppContext.CommonSchema, criteria);
        }
		
		/// <summary>
        /// Builds a TipodocumentoList from a IList<!--<TipodocumentoInfo>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>TipodocumentoList</returns>
        public static TipodocumentoList GetChildList(IList<TipodocumentoInfo> list)
        {
            TipodocumentoList flist = new TipodocumentoList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (TipodocumentoInfo item in list)
                    flist.Add(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }

        /// <summary>
        /// Builds a TipodocumentoList from IList<!--<Tipodocumento>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>TipodocumentoList</returns>
        public static TipodocumentoList GetChildList(IList<Tipodocumento> list) { return new TipodocumentoList(list); }

        public static TipodocumentoList GetChildList(IDataReader reader) { return new TipodocumentoList(reader); }

		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<TipodocumentoInfo> GetSortedList(string sortProperty,
																	ListSortDirection sortDirection)
		{
			SortedBindingList<TipodocumentoInfo> sortedList =
				new SortedBindingList<TipodocumentoInfo>(GetList());
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
		
		#endregion

		#region Data Access
		
		// called to copy objects data from list
        private void Fetch(IList<Tipodocumento> lista)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            foreach (Tipodocumento item in lista)
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
                this.Add(Tipodocumento.GetChild(reader).GetInfo());

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
					{
						this.AddItem(TipodocumentoInfo.Get(reader,Childs));
					}

					IsReadOnly = true;
				}
				else
				{
					IList list = criteria.List();

					if (list.Count > 0)
					{
						IsReadOnly = false;

						foreach (Tipodocumento item in list)
							this.Add(item.GetInfo());

						IsReadOnly = true;
					}
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

		internal static string SELECT() { return SELECT(new QueryConditions()); }
		internal static string SELECT(QueryConditions conditions)
		{
			OrderList orders = new OrderList();
			orders.NewOrder("Valor", ListSortDirection.Ascending, typeof(Tipodocumento));
			conditions.Orders = orders;
			return Tipodocumento.SELECT(conditions, false);
		}

		#endregion
	}
}

