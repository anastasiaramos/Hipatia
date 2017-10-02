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
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class Entidades : BusinessListBaseEx<Entidades, Entidad>
    {
        #region Business Methods
		
        public Entidad NewItem(Agente parent)
        {
            //this.NewItem(Entidad.NewChild(parent));
            return this[Count - 1];
        }
		
        public Entidad GetItem(string tipo)
        {
            foreach (Entidad obj in this)
                if (obj.Tipo == tipo)
                    return obj;
            return null;
        }

        #endregion

        #region Factory Methods

        private Entidades()
        {
            MarkAsChild();
        }
        private Entidades(IList<Entidad> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }
        private Entidades(IDataReader reader, bool childs)
        {
            Childs = childs;
            Fetch(reader);
        }

        public static Entidades NewChildList() { return new Entidades(); }

        public static Entidades GetChildList(IList<Entidad> lista) { return new Entidades(lista); }
        public static Entidades GetChildList(IDataReader reader, bool childs) { return new Entidades(reader, childs); }
        public static Entidades GetChildList(IDataReader reader) { return GetChildList(reader, true); }

        #endregion

        #region Child Data Access

        // called to copy objects data from list
        private void Fetch(IList<Entidad> lista)
        {
            /*this.RaiseListChangedEvents = false;

            foreach (Entidad item in lista)
                this.Add(Entidad.GetChild(SessionCode, item));

            this.RaiseListChangedEvents = true;*/
        }

        private void Fetch(IDataReader reader)
        {
			this.RaiseListChangedEvents = false;

			while (reader.Read())
				this.Add(Entidad.GetChild(SessionCode, reader));

            this.RaiseListChangedEvents = true;
        }
		
        internal void Update(Agente parent)
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
           /* foreach (Entidad obj in DeletedList)
                obj.DeleteSelf(parent);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            foreach (Entidad obj in this)
            {
                if (obj.IsNew)
                    obj.Insert(parent);
                else
                    obj.Update(parent);
            }*/
			
            this.RaiseListChangedEvents = true;
        }		

        #endregion

		#region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return Entidad.SELECT(conditions, true); }

		#endregion
    }
}
