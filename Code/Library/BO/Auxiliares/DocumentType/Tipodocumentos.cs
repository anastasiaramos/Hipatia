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
    /// Editable Root Collection
    /// </summary>
    [Serializable()]
    public class Tipodocumentos : BusinessListBaseEx<Tipodocumentos, Tipodocumento>
    {
        #region Business Methods

        public Tipodocumento NewItem()
        {
            this.NewItem(Tipodocumento.NewChild());
            return this[Count - 1];
        }

        public bool ExistOtherItem(Tipodocumento Tipodocumento)
        {
            foreach (Tipodocumento obj in this)
                if ((obj.Oid != Tipodocumento.Oid) && (obj.Valor == Tipodocumento.Valor))
                    return true;
            return false;
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

        private Tipodocumentos() { }

        public static Tipodocumentos NewList() { return new Tipodocumentos(); }

        public static Tipodocumentos GetList()
        {
            CriteriaEx criteria = Tipodocumento.GetCriteria(Tipodocumento.OpenSession());

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT();

            Tipodocumento.BeginTransaction(criteria.SessionCode);

            //No criteria. Retrieve all de List
            return DataPortal.Fetch<Tipodocumentos>(criteria);
        }

        #endregion

        #region Root Data Access

        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            Fetch(criteria);
        }

        private void Fetch(CriteriaEx criteria)
        {
            this.RaiseListChangedEvents = false;

            SessionCode = criteria.SessionCode;

            try
            {
                if (nHMng.UseDirectSQL)
                {
                    Tipodocumento.DoLOCK(AppContext.CommonSchema, Session());

                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                        this.AddItem(Tipodocumento.GetChild(reader));
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

        protected override void DataPortal_Update()
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (Tipodocumento obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
                // add/update any current child objects
                foreach (Tipodocumento obj in this)
                {
                    if (!ExistOtherItem(obj))
                    {
                        if (obj.IsNew)
                            obj.Insert(this);
                        else
                            obj.Update(this);
                    }
                }

                Transaction().Commit();
            }
            catch (Exception ex)
            {
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                BeginTransaction();
                this.RaiseListChangedEvents = true;
            }
        }

        #endregion

		#region SQL

		internal static string SELECT() { return SELECT(new QueryConditions()); }
		internal static string SELECT(QueryConditions conditions)
		{
			OrderList orders = new OrderList();
			orders.NewOrder("Valor", ListSortDirection.Ascending, typeof(Tipodocumento));
			conditions.Orders = orders;
			return Tipodocumento.SELECT(conditions, true);
		}

		#endregion
    }
}
