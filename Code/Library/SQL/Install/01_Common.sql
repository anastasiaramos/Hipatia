﻿SET search_path TO "COMMON";
-- HIPATIA MODULE COMMON SCHEMA SCRIPT

DROP TABLE IF EXISTS "HPDocumentType";
CREATE TABLE "HPDocumentType" ( 
	"OID" bigserial NOT NULL,
	"VALOR" character varying(255),
    "USER_CREATED" boolean,
	CONSTRAINT "PK_HPDocumentType" PRIMARY KEY ("OID") 
) WITHOUT OIDS;

ALTER TABLE "HPDocumentType" OWNER TO molAdmin;
GRANT ALL ON TABLE "HPDocumentType" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "HPEntity";
CREATE TABLE "HPEntity" ( 
	"OID" bigserial NOT NULL,
	"TIPO" character varying(255),
    "OBSERVACIONES" text,
    "COMPARTIDO" boolean DEFAULT false,
	CONSTRAINT "PK_HPEntity" PRIMARY KEY ("OID") 
) WITHOUT OIDS;

ALTER TABLE "HPEntity" OWNER TO molAdmin;
GRANT ALL ON TABLE "HPEntity" TO GROUP "MOLEQULE_ADMINISTRATOR";

DROP TABLE IF EXISTS "HPEntityType";
CREATE TABLE "HPEntityType" ( 
	"OID" bigserial NOT NULL,
	"VALOR" character varying(255) NOT NULL,
    "USER_CREATED" boolean,
    "COMMON_SCHEMA" boolean DEFAULT false,
	CONSTRAINT "PK_HPEntityType" PRIMARY KEY ("OID") ,
	CONSTRAINT "UQ_HPEntityType_VALUE" UNIQUE ("VALOR")
) WITHOUT OIDS;

ALTER TABLE "HPEntityType" OWNER TO molAdmin;
GRANT ALL ON TABLE "HPEntityType" TO GROUP "MOLEQULE_ADMINISTRATOR";

ALTER TABLE ONLY "HPEntity"
    ADD CONSTRAINT "FK_Entidad_EntityType" FOREIGN KEY ("TIPO") REFERENCES "HPEntityType"("VALOR") ON UPDATE CASCADE;
