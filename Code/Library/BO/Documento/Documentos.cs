using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx;

namespace moleQule.Library.Hipatia
{
	/// <summary>
	/// Editable Business Object With Childs Root Collection  
	/// </summary>
    [Serializable()]
	public class Documentos : BusinessListBaseEx<Documentos, Documento>
    {
		#region Business Methods
        
        public Documento NewItem()
        {
            this.AddItem(Documento.NewChild());
            return this[Count - 1];
        }

		public void SetNextCode(Documento item)
		{
			int index = this.IndexOf(item);

			if (index == 0)
			{
				item.GetNewCode();
				MaxSerial = item.Serial;
			}
			else
			{
				item.Serial = MaxSerial + 1;
				item.Codigo = "DOC#" + item.Serial.ToString(Resources.Defaults.DOCUMENTO_CODE_FORMAT);
				MaxSerial++;
			}
		}

        #endregion
		
		#region Common Factory Methods

		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
        private Documentos() { }

		#endregion		
		
		#region Child Factory Methods

		private Documentos(IList<Documento> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }
        private Documentos(int sessionCode, IDataReader reader, bool childs)
        {
            MarkAsChild();
            Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }

		public static Documentos NewChildList() { return new Documentos(); }

		public static Documentos GetChildList(IList<Documento> lista) 
		{
			Documentos list = new Documentos(lista);
			list.MarkAsChild();
			return list;
		}
		public static Documentos GetChildList(int sessionCode, IDataReader reader) { return GetChildList(sessionCode, reader, true); }
		public static Documentos GetChildList(int sessionCode, IDataReader reader, bool childs) { return new Documentos(sessionCode, reader, childs); }

		public static Documentos GetChildList(int sessionCode, List<long> oid_list, bool childs)
		{
			return GetChildList(sessionCode, Documento.SELECT(new QueryConditions { OidList = oid_list }, false), childs);
		}
		internal static Documentos GetChildList(int sessionCode, string query, bool childs)
		{
			if (!Documento.CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = Documento.GetCriteria(sessionCode);
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;

			return DataPortal.Fetch<Documentos>(criteria);
		}

		#endregion

		#region Root Factory Methods
		
        /// <summary>
        /// Crea una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static Documentos NewList(int sessionCode = -1) 
		{ 	
			if (!Documento.CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			Documentos list = new Documentos();
			list.SetSharedSession(sessionCode);
			return list;
		}

		public static Documentos GetList(bool childs= true) { return GetList(Documentos.SELECT(), childs); }
		public static Documentos GetList(QueryConditions conditions, bool childs)	 { return GetList(Documentos.SELECT(conditions), childs); }

		internal static Documentos GetList(string query, bool childs)
		{
            if (!Documento.CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = Documento.GetCriteria(Documento.OpenSession());
			criteria.Childs = childs;
			
            if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;
				
			Documento.BeginTransaction(criteria.SessionCode);

            return DataPortal.Fetch<Documentos>(criteria);
		}
		
        #endregion
		
		#region Root Data Access

        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            Fetch(criteria);
        }

        private void Fetch(CriteriaEx criteria)
        {
            try
            {
				this.RaiseListChangedEvents = false;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;

				if (nHMng.UseDirectSQL)
                {
                    Documento.DoLOCK(Session());

                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

					while (reader.Read())
					{
						Documento item = Documento.GetChild(SessionCode, reader, Childs);
						AddItem(item);
						if (item.Serial > MaxSerial) MaxSerial = item.Serial;
					}

					if (criteria.PagingInfo != null)
					{
						reader = nHManager.Instance.SQLNativeSelect(Documento.SELECT_COUNT(criteria), criteria.Session);
						if (reader.Read()) criteria.PagingInfo.TotalItems = Format.DataReader.GetInt32(reader, "TOTAL_ROWS");
					}
                }
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
            }
            finally
            {
                this.RaiseListChangedEvents = true;
            }
        }

        /// <summary>
        /// Realiza el Save de los objetos de la lista. Inserta, Actualiza o Borra en función
		/// de los flags de cada objeto de la lista
		/// </summary>
		/// <param name="reader">IDataReader origen</param>
        protected override void DataPortal_Update()
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (Documento obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
				// add/update any current child objects
				foreach (Documento obj in this)
				{
					if (obj.IsNew)
					{
						SetNextCode(obj);
						obj.Insert(this);
					}
					else
						obj.Update(this);
				}

                if (!SharedTransaction) Transaction().Commit();
            }
            catch (Exception ex)
            {
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
            }
            finally
            {
				if (!SharedTransaction) BeginTransaction();
                this.RaiseListChangedEvents = true;
            }
        }

        #endregion		

		#region Child Data Access

		// called to copy objects data from list and to retrieve child data from db
		private void Fetch(IList<Documento> lista)
		{
			this.RaiseListChangedEvents = false;

			foreach (Documento item in lista)
			{
				Documento obj = Documento.GetChild(item, Childs);
				this.AddItem(obj);
				if (item.Serial > MaxSerial) MaxSerial = item.Serial;
			}

			this.RaiseListChangedEvents = true;
		}

		// called to copy objects data from list and to retrieve child data from db
		private void Fetch(IDataReader reader)
		{
			this.RaiseListChangedEvents = false;

			while (reader.Read())
			{
				Documento item = Documento.GetChild(SessionCode, reader, Childs);
				this.AddItem(item);
				if (item.Serial > MaxSerial) MaxSerial = item.Serial;
			}

			this.RaiseListChangedEvents = true;
		}

		#endregion
			
        #region SQL

        public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return Documento.SELECT(conditions, true); }
		
		#endregion
    }
}

