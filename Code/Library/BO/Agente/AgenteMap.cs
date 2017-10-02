using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace moleQule.Library.Hipatia
{
	[Serializable()]
	public class AgentMap : ClassMapping<AgentRecord>
	{
		public AgentMap()
		{
			Table("`HPAgent`");
			Lazy(true);

			Id(x => x.Oid, map => { map.Generator(Generators.Sequence, gmap => gmap.Params(new { sequence = "`HPAgent_OID_seq`" })); map.Column("`OID`"); });
			Property(x => x.OidEntidad, map => { map.Column("`OID_ENTIDAD`"); map.NotNullable(false); map.Length(32768); }); 
			Property(x => x.OidAgenteExt, map => { map.Column("`OID_AGENTE_EXT`"); map.NotNullable(false); map.Length(32768); }); 
			Property(x => x.Codigo, map => { map.Column("`CODIGO`"); map.NotNullable(false); map.Length(255); }); 
			Property(x => x.Serial, map => { map.Column("`SERIAL`"); map.Length(32768); }); 
			Property(x => x.Nombre, map => { map.Column("`NOMBRE`"); map.NotNullable(false); map.Length(255); }); 
			Property(x => x.Fecha, map => { map.Column("`FECHA`"); map.NotNullable(false); map.Length(32768); }); 
			Property(x => x.Observaciones, map => { map.Column("`OBSERVACIONES`"); map.NotNullable(false); map.Length(32768); });
		}
	}
}

