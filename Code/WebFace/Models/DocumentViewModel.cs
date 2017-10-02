using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Hipatia;
using moleQule.WebFace.Models;

namespace moleQule.WebFace.Hipatia.Models
{
	/// <summary>
	/// DocumentViewModel
	/// </summary>
	[Serializable()]
	public class DocumentViewModel : ViewModelBase<Documento, DocumentoInfo>, IViewModel
	{
		#region Attributes

		protected DocumentBase _base = new DocumentBase();

		#endregion	
	
		#region Properties

		[HiddenInput]
		public long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		
		[HiddenInput]
		public long OidDocument { get { return _base._oid_documento; } set { _base._oid_documento = value; } }

		[HiddenInput]
		public long OidAgent { get; set; }

		[Required]
		[Display(ResourceType = typeof(Resources.Labels), Name = "NAME")]
		public string Name { get { return _base.Record.Nombre; } set { _base.Record.Nombre = value; } }

		[Required]
		[Display(ResourceType = typeof(Resources.Labels), Name = "FILE_TYPE")]
		public EFile EFile{ get; set; }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[Display(ResourceType = typeof(Resources.Labels), Name = "REGISTRY_DATE")]
		public DateTime RegistryDate { get { return _base.Record.FechaAlta; } set { _base.Record.FechaAlta= value; } }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[Display(ResourceType = typeof(Resources.Labels), Name = "DATE")]
		public DateTime Date { get { return _base.Record.Fecha; } set { _base.Record.Fecha = value; } }

		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		[Display(ResourceType = typeof(Resources.Labels), Name = "EXPIRATION_DATE")]
		public DateTime ExpirationDate { get { return _base.Record.ExpirationDate; } set { _base.Record.ExpirationDate = value; } }

		[HiddenInput]
		public long Status { get { return _base._estado; } set { _base._estado = value; } }

		//UNLINKED PROPERTIES
		public virtual EEstado EStatus { get { return _base.EEstado; } set { _base._estado = (long)value; } }

        [HiddenInput]
        public int FileIndex { get { return (int)EFile; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "STATUS")]
		public virtual string StatusLabel { get { return _base.EstadoLabel; } set { } }

		public virtual string Path { get { return _base.Record.Ruta; } set { _base.Record.Ruta = value; } }
		public virtual string WebPath { get { return _base.Record.Ruta.Replace("\\", "/"); } }

		#endregion

		#region Business Methods

		public new void CopyFrom(Documento source)
		{
			if (source == null) return;
			_base.CopyValues(source);
		}
		public new void CopyFrom(DocumentoInfo source)
		{
			if (source == null) return;
			_base.CopyValues(source);
		}
		public new void CopyTo(Documento dest, HttpRequestBase request = null)
		{
			if (dest == null) return;

			base.CopyTo(dest, request);

			dest.Nombre = Name;
			dest.Fecha = Date;
			dest.FechaAlta = RegistryDate;
			dest.ExpirationDate = ExpirationDate;
			dest.Ruta = Path;
		}

		#endregion	

		#region Factory Methods

		public DocumentViewModel() { }

		public static DocumentViewModel New() 
		{
			DocumentViewModel obj = new DocumentViewModel();
			obj.CopyFrom(DocumentoInfo.New());
			obj.ExpirationDate = DateTime.Today;
			return obj;
		}
		public static DocumentViewModel New(Documento  source) { return New(source.GetInfo(false)); }
		public static DocumentViewModel New(DocumentoInfo source)
		{
			DocumentViewModel obj = new DocumentViewModel();
			obj.CopyFrom(source);
			return obj;
		}
		public static DocumentViewModel New(Documento source, IAgenteHipatia agent)
		{
			DocumentViewModel obj = new DocumentViewModel();
			obj.CopyFrom(source);
			obj.OidAgent = agent.Oid;
			return obj;
		}
		public static DocumentViewModel New(DocumentoInfo source, IAgenteHipatia agent)
		{
			DocumentViewModel obj = new DocumentViewModel();
			obj.CopyFrom(source);
			obj.OidAgent = agent.Oid;
			return obj;
		}

		public static DocumentViewModel Get(long oid, bool childs = false)
		{
			DocumentViewModel obj = new DocumentViewModel();
			DocumentoInfo delivery = DocumentoInfo.Get(oid, childs);
			obj.CopyFrom(delivery);

			/*if (childs)
				obj.Lines = DocumentoLineListViewModel.Get(delivery.Conceptos);*/

			return obj;
		}

		public static void Add(DocumentViewModel item)
		{
			Documento newItem = Documento.New();
			item.CopyTo(newItem);
			newItem.Save();
			item.CopyFrom(newItem);
		}
		public static void Edit(DocumentViewModel source, HttpRequestBase request = null)
		{
			Documento item = Documento.Get(source.Oid);
			source.CopyTo(item,request);
			item.Save();
		}
		public static void Remove(long oid)
		{
			Documento.Delete(oid);
		}
		
		#endregion
	}

	/// <summary>
	/// ViewModel List
	/// </summary>
	[Serializable()]
	public class DocumentListViewModel : List<DocumentViewModel>
	{
		#region Business Objects

		#endregion

		#region Business Methods

		public DocumentViewModel GetItemByName(string name)
		{
			return this.FirstOrDefault(x => x.Name == name);
		}

		#endregion	

		#region Factory Methods

		public DocumentListViewModel() { }

        public static DocumentListViewModel Get(IEnumerable<DocumentViewModel> source)
        {
            DocumentListViewModel list = new DocumentListViewModel();

            foreach (DocumentViewModel item in source)
                list.Add(item);

            return list;
        }

		public static DocumentListViewModel Get()
		{
			DocumentListViewModel list = new DocumentListViewModel();

			DocumentoList sourceList = DocumentoList.GetList();

			foreach (DocumentoInfo item in sourceList)
				list.Add(DocumentViewModel.New(item));

			return list;
		}
		public static DocumentListViewModel Get(DocumentoList sourceList)
		{
			DocumentListViewModel list = new DocumentListViewModel();

			foreach (DocumentoInfo item in sourceList)
				list.Add(DocumentViewModel.New(item));

			return list;
		}
		public static DocumentListViewModel Get(DocumentoList sourceList, IAgenteHipatia agent)
		{
			DocumentListViewModel list = new DocumentListViewModel();

			foreach (DocumentoInfo item in sourceList)
				list.Add(DocumentViewModel.New(item, agent));

			return list;
		}
		public static DocumentListViewModel Get(Documentos sourceList, IAgenteHipatia agent)
		{
			DocumentListViewModel list = new DocumentListViewModel();

			foreach (Documento item in sourceList)
				list.Add(DocumentViewModel.New(item, agent));

			return list;
		}

        public DocumentListViewModel GetSorted(string property)
        {
			switch (property)
			{
				case "FileIndex":
					return Get(from d in this
                               orderby d.FileIndex ascending
                               select d);

				default:
					return Get(from d in this
                               orderby d.Name ascending
                               select d);
			}
        }

		#endregion
	}
}
