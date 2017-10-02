SET search_path TO "0001";
-- HIPATIA MODULE DETAIL SCHEMA SCRIPT

DROP TABLE IF EXISTS "HPAgent";
CREATE TABLE "HPAgent" 
( 
	"OID" bigserial NOT NULL,
	"OID_ENTIDAD" bigint,
    "OID_AGENTE_EXT" bigint,
    "CODIGO" character varying(255),
    "SERIAL" bigint NOT NULL,
    "NOMBRE" character varying(255),
    "FECHA" timestamp without time zone,
    "OBSERVACIONES" text,
	CONSTRAINT "PK_Agent" PRIMARY KEY ("OID"),
	CONSTRAINT "UQ_Agent_SERIAL" UNIQUE ("SERIAL")
) WITHOUT OIDS;

ALTER TABLE "HPAgent" OWNER TO moladmin;
GRANT ALL ON TABLE "HPAgent" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "HPAgent_Document";
CREATE TABLE "HPAgent_Document" 
( 
	"OID" bigserial NOT NULL,
	"OID_AGENTE" bigint,
    "OID_DOCUMENTO" bigint,
	CONSTRAINT "PK_HPAgent_Document" PRIMARY KEY ("OID")
) WITHOUT OIDS;

ALTER TABLE "HPAgent_Document" OWNER TO moladmin;
GRANT ALL ON TABLE "HPAgent_Document" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "HPDocument";
CREATE TABLE "HPDocument" 
( 
	"OID" bigserial NOT NULL,
	"CODIGO" character varying(255),
    "SERIAL" bigint NOT NULL,
    "NOMBRE" character varying(255),
    "TIPO" character varying(255),
    "FECHA" timestamp without time zone,
    "FECHA_ALTA" timestamp without time zone,
	"EXPIRATION_DATE" timestamp without time zone,
    "RUTA" character varying(1024),
    "OBSERVACIONES" text,
	CONSTRAINT "PK_HPDocument" PRIMARY KEY ("OID"),
	CONSTRAINT "UQ_Documento_SERIAL" UNIQUE ("SERIAL")
) WITHOUT OIDS;

ALTER TABLE "HPDocument" OWNER TO moladmin;
GRANT ALL ON TABLE "HPDocument" TO GROUP "MOLEQULE_ADMINISTRATOR";

-- FOREIGN KEYS

ALTER TABLE ONLY "HPAgent_Document"
    ADD CONSTRAINT "FK_HPAgent_Document_Agent" FOREIGN KEY ("OID_AGENTE") REFERENCES "HPAgent"("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "HPAgent_Document"
    ADD CONSTRAINT "FK_HPAgent_Document_Document" FOREIGN KEY ("OID_DOCUMENTO") REFERENCES "HPDocument"("OID") ON UPDATE CASCADE ON DELETE CASCADE;

ALTER TABLE ONLY "HPAgent"
    ADD CONSTRAINT "FK_HPAgent_Entity" FOREIGN KEY ("OID_ENTIDAD") REFERENCES "COMMON"."HPEntity"("OID") ON UPDATE CASCADE;
