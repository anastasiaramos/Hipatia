using System;
using System.Collections.Generic;
using System.ComponentModel;

using Csla;
using moleQule.Library.CslaEx;

namespace moleQule.Library.Hipatia
{
    /// <summary>
    /// Interfaz genérica para cualquier entidad que quiera integrarse en Hipatia
    /// </summary>
    public interface IAgenteHipatia
    {
        long Oid { get; }
        string IDHipatia { get; }
		Type TipoEntidad { get; }
        string NombreHipatia { get; }
        string ObservacionesHipatia { get; }
    }

    /// <summary>
	/// ReadOnly Collection of IAgenteHipatia
	/// </summary>
    [Serializable()]
    public class IAgenteHipatiaList : SortedBindingList<IAgenteHipatia>
    {
        public IAgenteHipatiaList(IList<IAgenteHipatia> list)
            : base(list) {}

		public static IAgenteHipatiaList GetList(IList<IAgenteHipatia> list)
		{
			IAgenteHipatiaList flist = new IAgenteHipatiaList(new List<IAgenteHipatia>());

			if (list.Count > 0)
			{
				foreach (IAgenteHipatia item in list)
					flist.Add(item);
			}

			return flist;
		}

        /// <summary>
        /// Devuelve una lista ordenada y filtrada a partir de los datos de la lista
        /// actual
        /// </summary>
        /// <param name="criteria">Filtro</param>
        /// <param name="sortProperty">Campo de ordenación</param>
        /// <param name="sortDirection">Sentido de ordenación</param>
        /// <returns>Lista ordenada</returns>
        public IAgenteHipatiaList GetSortedSubList( FCriteria criteria,
                                                    string sortProperty,
                                                    ListSortDirection sortDirection)
        {

            IAgenteHipatiaList sortedList = new IAgenteHipatiaList(new List<IAgenteHipatia>());

            if (this.Count == 0) return sortedList;

            PropertyDescriptor property = TypeDescriptor.GetProperties(this[0]).Find(criteria.GetProperty(), false);

            switch (criteria.Operation)
            {
                case Operation.StartsWith:
                    {
                        foreach (IAgenteHipatia item in this)
                        {
                            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(item))
                                if (prop.Name == property.Name)
                                {
                                    object value = prop.GetValue(item);
                                    if (value.ToString().ToLower().StartsWith(criteria.GetValue().ToString().ToLower()))
                                        sortedList.Add(item);
                                    break;
                                }
                        }
                    } break;

                case Operation.Equal:
                    {
                        foreach (IAgenteHipatia item in this)
                        {
                            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(item))
                                if (prop.Name == property.Name)
                                {
                                    object value = prop.GetValue(item);
                                    if (criteria.Equal(value))
                                        sortedList.Add(item);
                                    break;
                                }
                        }
                    } break;

                case Operation.Less:
                    {
                        foreach (IAgenteHipatia item in this)
                        {
                            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(item))
                                if (prop.Name == property.Name)
                                {
                                    object value = prop.GetValue(item);
                                    if (criteria.Less(value))
                                        sortedList.Add(item);
                                    break;
                                }
                        }
                    } break;

                case Operation.LessOrEqual:
                    {
                        foreach (IAgenteHipatia item in this)
                        {
                            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(item))
                                if (prop.Name == property.Name)
                                {
                                    object value = prop.GetValue(item);
                                    if (criteria.LessOrEqual(value))
                                        sortedList.Add(item);
                                    break;
                                }
                        }
                    } break;

                case Operation.Greater:
                    {
                        foreach (IAgenteHipatia item in this)
                        {
                            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(item))
                                if (prop.Name == property.Name)
                                {
                                    object value = prop.GetValue(item);
                                    if (criteria.Greater(value))
                                        sortedList.Add(item);
                                    break;
                                }
                        }
                    } break;

                case Operation.GreaterOrEqual:
                    {
                        foreach (IAgenteHipatia item in this)
                        {
                            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(item))
                                if (prop.Name == property.Name)
                                {
                                    object value = prop.GetValue(item);
                                    if (criteria.GreaterOrEqual(value))
                                        sortedList.Add(item);
                                    break;
                                }
                        }
                    } break;

                default:
                    {
                        foreach (IAgenteHipatia item in this)
                        {
                            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(item))
                                if (prop.Name == property.Name)
                                {
                                    object value = prop.GetValue(item);
                                    if (value.ToString().ToLower().Contains(criteria.GetValue().ToString().ToLower()))
                                        sortedList.Add(item);
                                    break;
                                }
                        }
                    } break;
            }

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }

		/// <summary>
		/// Devuelve una lista a partir de los datos de la lista actual
		/// </summary>
		/// <param name="criteria">Filtro (Insensitive)</param>
		/// <returns>Lista</returns>
		public SortedBindingList<IAgenteHipatia> GetSortedSubList(FCriteria criteria, List<string> properties_list)
		{
			List<IAgenteHipatia> list = new List<IAgenteHipatia>();
			SortedBindingList<IAgenteHipatia> sortedList = new SortedBindingList<IAgenteHipatia>(list);

			if (this.Count == 0) return sortedList;

			PropertyDescriptor property = null;

			if (criteria.GetProperty() != null)
				property = TypeDescriptor.GetProperties(this[0]).Find(criteria.GetProperty(), false);
			else
				property = null;

			Type type = typeof(IAgenteHipatia);
			System.Reflection.PropertyInfo prop = null;

			switch (criteria.Operation)
			{
				case Operation.Equal:
					{
						foreach (IAgenteHipatia item in this)
						{
							foreach (string propName in properties_list)
							{
								prop = type.GetProperty(propName);

								if (prop == null) continue;

								//Buscamos en una propiedad en concreto
								if (property != null)
								{
									if (prop.Name == property.Name)
									{
										object value = prop.GetValue(item, null);
										if (value == null) break;
										if (value.ToString().ToLower().Equals(criteria.GetValue().ToString().ToLower()))
											sortedList.Add(item);
										break;
									}
								}
								//Buscamos en todas las propiedades de la lista
								else
								{
									object value = prop.GetValue(item, null);
									if (value == null) continue;
									if (value.ToString().ToLower().Equals(criteria.GetValue().ToString().ToLower()))
									{
										sortedList.Add(item);
										break;
									}
								}
							}
						}
					} break;

				case Operation.StartsWith:
					{
						foreach (IAgenteHipatia item in this)
						{
							foreach (string propName in properties_list)
							{
								prop = type.GetProperty(propName);

								if (prop == null) continue;

								//Buscamos en una propiedad en concreto
								if (property != null)
								{
									if (prop.Name == property.Name)
									{
										object value = prop.GetValue(item, null);
										if (value == null) break;
										if (value.ToString().ToLower().StartsWith(criteria.GetValue().ToString().ToLower()))
											sortedList.Add(item);
										break;
									}
								}
								//Buscamos en todas las propiedades de la lista
								else
								{
									object value = prop.GetValue(item, null);
									if (value == null) continue;
									if (value.ToString().ToLower().StartsWith(criteria.GetValue().ToString().ToLower()))
									{
										sortedList.Add(item);
										break;
									}
								}
							}
						}
					} break;

				case Operation.Less:
					{
						foreach (IAgenteHipatia item in this)
						{
							foreach (string propName in properties_list)
							{
								prop = type.GetProperty(propName);

								if (prop == null) continue;

								//Buscamos en una propiedad en concreto
								if (property != null)
								{
									if (prop.Name == property.Name)
									{
										object value = prop.GetValue(item, null);
										if (value == null) break;
										if (criteria.Less(value))
											sortedList.Add(item);
										break;
									}
								}
								//Buscamos en todas las propiedades de la lista
								else
								{
									object value = prop.GetValue(item, null);
									if (value == null) continue;
									if (criteria.Less(value))
									{
										sortedList.Add(item);
										break;
									}
								}
							}
						}
					} break;

				case Operation.LessOrEqual:
					{
						foreach (IAgenteHipatia item in this)
						{
							foreach (string propName in properties_list)
							{
								prop = type.GetProperty(propName);

								if (prop == null) continue;

								//Buscamos en una propiedad en concreto
								if (property != null)
								{
									if (prop.Name == property.Name)
									{
										object value = prop.GetValue(item, null);
										if (value == null) break;
										if (criteria.LessOrEqual(value))
											sortedList.Add(item);
										break;
									}
								}
								//Buscamos en todas las propiedades de la lista
								else
								{
									object value = prop.GetValue(item, null);
									if (value == null) continue;
									if (criteria.LessOrEqual(value))
									{
										sortedList.Add(item);
										break;
									}
								}
							}
						}
					} break;

				case Operation.Greater:
					{
						foreach (IAgenteHipatia item in this)
						{
							foreach (string propName in properties_list)
							{
								prop = type.GetProperty(propName);

								if (prop == null) continue;

								//Buscamos en una propiedad en concreto
								if (property != null)
								{
									if (prop.Name == property.Name)
									{
										object value = prop.GetValue(item, null);
										if (value == null) break;
										if (criteria.Greater(value))
											sortedList.Add(item);
										break;
									}
								}
								//Buscamos en todas las propiedades de la lista
								else
								{
									object value = prop.GetValue(item, null);
									if (value == null) continue;
									if (criteria.Greater(value))
									{
										sortedList.Add(item);
										break;
									}
								}
							}
						}
					} break;

				case Operation.GreaterOrEqual:
					{
						foreach (IAgenteHipatia item in this)
						{
							foreach (string propName in properties_list)
							{
								prop = type.GetProperty(propName);

								if (prop == null) continue;

								//Buscamos en una propiedad en concreto
								if (property != null)
								{
									if (prop.Name == property.Name)
									{
										object value = prop.GetValue(item, null);
										if (value == null) break;
										if (criteria.GreaterOrEqual(value))
											sortedList.Add(item);
										break;
									}
								}
								//Buscamos en todas las propiedades de la lista
								else
								{
									object value = prop.GetValue(item, null);
									if (value == null) continue;
									if (criteria.GreaterOrEqual(value))
									{
										sortedList.Add(item);
										break;
									}
								}
							}
						}
					} break;

				case Operation.Contains:
				default:
					{
						foreach (IAgenteHipatia item in this)
						{
							foreach (string propName in properties_list)
							{
								prop = type.GetProperty(propName);

								if (prop == null) continue;

								//Buscamos en una propiedad en concreto
								if (property != null)
								{
									if (prop.Name == property.Name)
									{
										object value = prop.GetValue(item, null);
										if (value == null) break;
										if (value.ToString().ToLower().Contains(criteria.GetValue().ToString().ToLower()))
											sortedList.Add(item);
										break;
									}
								}
								//Buscamos en todas las propiedades de la lista
								else
								{
									object value = prop.GetValue(item, null);
									if (value == null) continue;
									if (value.ToString().ToLower().Contains(criteria.GetValue().ToString().ToLower()))
									{
										sortedList.Add(item);
										break;
									}
								}
							}
						}
					} break;
			}

			return sortedList;
		}
    
	}

	public class HipatiaAgentBase : IAgenteHipatia
	{
		public long Oid { get; set; }
		public string IDHipatia { get; set; }
		public Type TipoEntidad { get; set; }
		public string NombreHipatia { get; set; }
		public string ObservacionesHipatia { get; set; }
	}
}

