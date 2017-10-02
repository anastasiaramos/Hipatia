using System;
using System.Collections.Generic;

using moleQule.Library;
using moleQule.Library.Common;

namespace moleQule.Library.Hipatia
{
	#region Querys

	public class QueryConditions : moleQule.Library.QueryConditions
    {
        public AgenteInfo Agent = null;
		public AgenteDocumentoInfo AgentDocument = null;
        public DocumentoInfo Document = null;
		public EntidadInfo Entity = null;
		public IAgenteHipatia IHipatiaAgent = null;

		public EEstado[] Status = null;
    }

	public delegate string SelectCaller(QueryConditions conditions);

	#endregion

	#region Enums

	public class EnumText<T> : EnumTextBase<T>
    {
        public static ComboBoxList<T> GetList()
        {
            return GetList(Resources.Enums.ResourceManager);
        }

        public static ComboBoxList<T> GetList(bool empty_value)
        {
            return GetList(Resources.Enums.ResourceManager, empty_value);
        }

        public static ComboBoxList<T> GetList(bool empty_value, bool special_values)
        {
            return GetList(Resources.Enums.ResourceManager, empty_value, special_values);
        }

		public static ComboBoxList<T> GetList(bool empty_value, bool special_values, bool all_value)
		{
			return GetList(Resources.Enums.ResourceManager, empty_value, special_values, all_value);
		}

		public static ComboBoxList<T> GetList(T[] list)
		{
			return GetList(list, false);
		}

		public static ComboBoxList<T> GetList(T[] list, bool empty_value)
		{
			return GetList(Resources.Enums.ResourceManager, list, empty_value);
		}

        public static string GetLabel(object value)
        {
            return GetLabel(Resources.Enums.ResourceManager, value);
        }

		public static string GetPrintLabel(object value)
		{
			return GetPrintLabel(Resources.Enums.ResourceManager, value);
		}
    }

	public static class EnumConvert
	{
		/*public static ETipoAcreedor ToETipoAcreedor(ETipoEntidad source)
		{
			switch (source)
			{
				case ETipoEntidad.Acreedor: return ETipoAcreedor.Acreedor;
				case ETipoEntidad.Proveedor: return ETipoAcreedor.Proveedor;
				case ETipoEntidad.Naviera: return ETipoAcreedor.Naviera;
				case ETipoEntidad.TransportistaOrigen: return ETipoAcreedor.TransportistaOrigen;
				case ETipoEntidad.TransportistaDestino: return ETipoAcreedor.TransportistaDestino;
				case ETipoEntidad.Despachante: return ETipoAcreedor.Despachante;
				case ETipoEntidad.Instructor: return ETipoAcreedor.Instructor;
			}

			return ETipoAcreedor.Todos;
		}*/
	}

	#endregion
}
