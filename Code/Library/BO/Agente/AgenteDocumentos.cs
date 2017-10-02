using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using NHibernate;

using Csla;
using moleQule.Library;
using moleQule.Library.CslaEx; 

namespace moleQule.Library.Hipatia
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class AgenteDocumentos : BusinessListBaseEx<AgenteDocumentos, AgenteDocumento>
    {
        #region Business Methods

		public AgenteDocumento NewItem(Agente parent, Documento doc)
		{
			this.AddItem(AgenteDocumento.NewChild(parent, doc));
			return this[Count - 1];
		}

        public AgenteDocumento NewItem(Documento parent)
        {
            this.AddItem(AgenteDocumento.NewChild(parent));
            return this[Count - 1];
        }

        public AgenteDocumento NewItem(Agente parent)
        {
            this.AddItem(AgenteDocumento.NewChild(parent));
            return this[Count - 1];
        }

        public void RemoveAgent(DocumentoInfo doc_parent)
        {
            foreach (AgenteDocumento item in this)
            {
                if (item.OidDocumento == doc_parent.OidDocumento)
                {
                    this.Remove(item);
                    return;
                }
            }
        }

        #endregion

        #region Factory Methods

        private AgenteDocumentos()
        {
            MarkAsChild();
        }

        private AgenteDocumentos(IList<AgenteDocumento> lista)
        {
            MarkAsChild();
            Fetch(lista);
        }

        private AgenteDocumentos(IDataReader reader, bool childs)
        {
            Childs = childs;
            Fetch(reader);
        }

        public static AgenteDocumentos NewChildList() { return new AgenteDocumentos(); }

        public static AgenteDocumentos GetChildList(IList<AgenteDocumento> lista) { return new AgenteDocumentos(lista); }

        public static AgenteDocumentos GetChildList(IDataReader reader, bool childs) { return new AgenteDocumentos(reader, childs); }

        public static AgenteDocumentos GetChildList(IDataReader reader) { return GetChildList(reader, true); }

        #endregion

        #region Child Data Access

        // called to copy objects data from list
        private void Fetch(IList<AgenteDocumento> lista)
        {
            this.RaiseListChangedEvents = false;

            foreach (AgenteDocumento item in lista)
                this.AddItem(AgenteDocumento.GetChild(item));

            this.RaiseListChangedEvents = true;
        }

        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(AgenteDocumento.GetChild(reader));

            this.RaiseListChangedEvents = true;
        }
		
        internal void Update(Documento parent)
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (AgenteDocumento obj in DeletedList)
                obj.DeleteSelf(parent);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            foreach (AgenteDocumento obj in this)
            {
                if (obj.IsNew)
                    obj.Insert(parent);
                else
                    obj.Update(parent);
            }
			
            this.RaiseListChangedEvents = true;
        }

        internal void Update(Agente parent)
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (AgenteDocumento obj in DeletedList)
            {
                try
                {
                    obj.DeleteSelf(parent);
                }
                catch { continue; }
            }

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            foreach (AgenteDocumento obj in this)
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

		public static string SELECT(QueryConditions conditions) { return AgenteDocumento.SELECT(conditions, true); }
		public static string SELECT(Agente source) { return SELECT(new QueryConditions { Agent = source.GetInfo(false) }); }
		public static string SELECT(Documento source) { return SELECT(new QueryConditions { Document = source.GetInfo(false) }); }

		#endregion
    }
}
