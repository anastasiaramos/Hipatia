using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Hipatia
{
	[Serializable()]
	public class DocumentTypeMap : ClassMapping<DocumentTypeRecord>
	{
		public DocumentTypeMap()
		{
			Table("`HPDocumentType`");
			Schema("\"COMMON\"");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`HPDocumentType_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.Valor, map => { map.Column("`VALOR`"); map.NotNullable(false); map.Length(255); });
			Property(x => x.UserCreated, map => { map.Column("`USER_CREATED`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}

