using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Hipatia
{
	[Serializable()]
	public class DocumentMap : ClassMapping<DocumentRecord>
	{
		public DocumentMap()
		{
			Table("`HPDocument`");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`HPDocument_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.NotNullable(false); map.Length(255); }); 
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); map.Length(32768); }); 
			Property(x => x.Nombre, map => { map.Column("`NOMBRE`"); map.NotNullable(false); map.Length(255); }); 
			Property(x => x.Tipo, map => { map.Column("`TIPO`"); map.NotNullable(false); map.Length(255); }); 
			Property(x => x.Fecha, map => { map.Column("`FECHA`"); map.NotNullable(false); }); 
			Property(x => x.FechaAlta, map => { map.Column("`FECHA_ALTA`"); map.NotNullable(false);  });
			Property(x => x.ExpirationDate, map => { map.Column("`EXPIRATION_DATE`"); map.NotNullable(false); }); 
			Property(x => x.Ruta, map => { map.Column("`RUTA`"); map.NotNullable(false); map.Length(1024); }); 
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); }); 
		}
	}
}

