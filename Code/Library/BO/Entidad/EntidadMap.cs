using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Hipatia
{
	[Serializable()]
	public class EntityMap : ClassMapping<EntityRecord>
	{
		public EntityMap()
		{
			Table("`HPEntity`");
			Schema("\"COMMON\"");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`HPEntity_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.Tipo, map => { map.Column("`TIPO`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.Compartido, map => { map.Column("`COMPARTIDO`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}

