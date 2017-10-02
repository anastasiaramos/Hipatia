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
	public class TipoEntidadList : ReadOnlyListBaseEx<TipoEntidadList, TipoEntidadInfo>
    {
        #region Business Methods

        public TipoEntidadInfo GetItem(string valor)
        {
            foreach (TipoEntidadInfo obj in this)
                if (obj.Valor == valor)
                    return obj;
            return null;
        }

        #endregion

        #region Factory Methods

        private TipoEntidadList() { }
		
		private TipoEntidadList(IList<TipoEntidad> lista)
		{
            Fetch(lista);
        }

        private TipoEntidadList(IDataReader reader)
		{
			Fetch(reader);
		}
		
		/// <summary>
		/// Builds a TipoEntidadList
		/// </summary>
		/// <param name="list"></param>
		/// <returns>TipoEntidadList</returns>
		public static TipoEntidadList GetList(bool childs)
		{
			CriteriaEx criteria = TipoEntidad.GetCriteria(TipoEntidad.OpenSession());
            criteria.Childs = childs;			
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT();			

			TipoEntidadList list = DataPortal.Fetch<TipoEntidadList>(criteria);

            CloseSession(criteria.SessionCode);
			return list;
		}

		/// <summary>
		/// Builds a TipoEntidadList
		/// </summary>
		/// <param name="list"></param>
		/// <returns>TipoEntidadList</returns>
		public static TipoEntidadList GetList()
		{ 
			return TipoEntidadList.GetList(true); 
		}

		/// <summary>
        /// Devuelve una lista de todos los elementos
        /// </summary>
        /// <returns>Lista de elementos</returns>
        public static TipoEntidadList GetList(CriteriaEx criteria)
        {
            return TipoEntidadList.RetrieveList(typeof(TipoEntidad), AppContext.CommonSchema, criteria);
        }
		
		/// <summary>
        /// Builds a TipoEntidadList from a IList<!--<TipoEntidadInfo>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>TipoEntidadList</returns>
        public static TipoEntidadList GetChildList(IList<TipoEntidadInfo> list)
        {
            TipoEntidadList flist = new TipoEntidadList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (TipoEntidadInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }

        /// <summary>
        /// Builds a TipoEntidadList from IList<!--<TipoEntidad>-->
        /// </summary>
        /// <param name="list"></param>
        /// <returns>TipoEntidadList</returns>
        public static TipoEntidadList GetChildList(IList<TipoEntidad> list) { return new TipoEntidadList(list); }

        public static TipoEntidadList GetChildList(IDataReader reader) { return new TipoEntidadList(reader); }

		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<TipoEntidadInfo> GetSortedList(string sortProperty,
																	ListSortDirection sortDirection)
		{
			SortedBindingList<TipoEntidadInfo> sortedList =
				new SortedBindingList<TipoEntidadInfo>(GetList());
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
		
		#endregion

		#region Data Access
		
		// called to copy objects data from list
        private void Fetch(IList<TipoEntidad> lista)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            foreach (TipoEntidad item in lista)
                this.AddItem(item.GetInfo());

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }

        // called to copy objects data from list
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            while (reader.Read())
                this.AddItem(TipoEntidad.GetChild(reader).GetInfo());

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
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query);

					IsReadOnly = false;

					while (reader.Read())
					{
						this.AddItem(TipoEntidadInfo.Get(reader,Childs));
					}

					IsReadOnly = true;
				}
				else
				{
					IList list = criteria.List();

					if (list.Count > 0)
					{
						IsReadOnly = false;

						foreach (TipoEntidad item in list)
							this.AddItem(item.GetInfo());

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
			orders.NewOrder("Valor", ListSortDirection.Ascending, typeof(TipoEntidad));
			conditions.Orders = orders;
			return TipoEntidad.SELECT(conditions, false);
		}

		#endregion
	}
}

