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
    public class TipoEntidades : BusinessListBaseEx<TipoEntidades, TipoEntidad>
    {
        #region Business Methods

        public TipoEntidad NewItem()
        {
            this.Add(TipoEntidad.NewChild());
            return this[Count - 1];
        }

        #endregion

        #region Authorization Rules

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

        #endregion

        #region Factory Methods

        private TipoEntidades() { }

        public static TipoEntidades NewList() { return new TipoEntidades(); }

        public static TipoEntidades GetList()
        {
            CriteriaEx criteria = TipoEntidad.GetCriteria(TipoEntidad.OpenSession());

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT();

            TipoEntidad.BeginTransaction(criteria.SessionCode);

            //No criteria. Retrieve all de List
            return DataPortal.Fetch<TipoEntidades>(criteria);
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
                    TipoEntidad.DoLOCK(AppContext.CommonSchema, Session());

                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                    {
                        this.AddItem(TipoEntidad.GetChild(reader));
                    }
                }
                else
                {
                    IList list = criteria.List();

                    foreach (TipoEntidad item in list)
                    {
                        //Bloqueamos todos los elementos de la lista
                        Session().Lock(Session().Get<EntityTypeRecord>(item.Oid), LockMode.UpgradeNoWait);
                        this.AddItem(TipoEntidad.GetChild(item));
                    }
                }

            }
            catch (NHibernate.ADOException)
            {
                if (Transaction() != null) Transaction().Rollback();
                throw new iQLockException(Resources.Messages.LOCK_ERROR);
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
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
            foreach (TipoEntidad obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
                // add/update any current child objects
                foreach (TipoEntidad obj in this)
                {
                    if (!Contains(obj))
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
			orders.NewOrder("Valor", ListSortDirection.Ascending, typeof(TipoEntidad));
			conditions.Orders = orders;
			return TipoEntidad.SELECT(conditions, true);
		}

		#endregion
    }
}
