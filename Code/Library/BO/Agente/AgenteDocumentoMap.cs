using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Hipatia
{
	[Serializable()]
	public class AgentDocumentMap : ClassMapping<AgentDocumentRecord>
	{
		public AgentDocumentMap()
		{
			Table("`HPAgent_Document`");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`HPAgent_Document_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidAgente, map => { map.Column("`OID_AGENTE`"); map.NotNullable(false); map.Length(32768); });
			Property(x => x.OidDocumento, map => { map.Column("`OID_DOCUMENTO`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}

