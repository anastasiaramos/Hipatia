using System;
using System.Collections.Generic;
using System.Data;

using Csla;
using moleQule.Library;
using moleQule.Library.CslaEx; 
using NHibernate;

namespace moleQule.Library.Hipatia
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class Agentes : BusinessListBaseEx<Agentes, Agente>
    {
        #region Business Methods

        public Agente NewItem(Entidad parent)
        {
            this.AddItem(Agente.NewChild(parent));
            return this[Count - 1];
        }

        #endregion

        #region Factory Methods

        private Agentes()
        {
            MarkAsChild();
        }
        private Agentes(IList<Agente> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }
        private Agentes(IDataReader reader, bool childs)
        {
            Childs = childs;
            Fetch(reader);
        }

        public static Agentes NewChildList() { return new Agentes(); }

        public static Agentes GetChildList(IList<Agente> lista) { return new Agentes(lista); }
        public static Agentes GetChildList(IDataReader reader, bool childs) { return new Agentes(reader, childs); }
        public static Agentes GetChildList(IDataReader reader) { return GetChildList(reader, true); }

        #endregion

        #region Child Data Access

        // called to copy objects data from list
        private void Fetch(IList<Agente> lista)
        {
            this.RaiseListChangedEvents = false;

            foreach (Agente item in lista)
                this.AddItem(Agente.GetChild(item));

            this.RaiseListChangedEvents = true;
        }

        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(Agente.GetChild(reader));

            this.RaiseListChangedEvents = true;
        }

        internal void Update(Entidad parent)
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (Agente obj in DeletedList)
                obj.DeleteSelf(parent);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            foreach (Agente obj in this)
            {
                if (obj.IsNew)
                    obj.Insert(parent);
                else
                    obj.Update(parent);
            }

            this.RaiseListChangedEvents = true;
        }

        #endregion

		#region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return Agente.SELECT(conditions, true); }
		public static string SELECT(Entidad source) { return Agente.SELECT(new QueryConditions() { Entity = source.GetInfo(false) }, true); }

		#endregion
    }
}

