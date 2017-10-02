using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Hipatia
{
	[Serializable()]
	public class EntityTypeMap : ClassMapping<EntityTypeRecord>
	{
		public EntityTypeMap()
		{
			Table("`HPEntityType`");
			Schema("\"COMMON\"");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`HPEntityType_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.Valor, map => { map.Column("`VALOR`"); map.Length(255); });
			Property(x => x.UserCreated, map => { map.Column("`USER_CREATED`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.CommonSchema, map => { map.Column("`COMMON_SCHEMA`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}

