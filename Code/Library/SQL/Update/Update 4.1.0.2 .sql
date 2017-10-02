/* UPDATE 4.1.0.2*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '4.1.0.2' WHERE "NAME" = 'HIPATIA_DB_VERSION';

ALTER TABLE "Entidad" RENAME TO "HPEntity";
ALTER TABLE "TIPOENTIDAD" RENAME TO "HPEntityType";
ALTER TABLE "TIPODOCUMENTO" RENAME TO "HPDocumentType";

ALTER TABLE "Entidad_OID_seq" RENAME TO "HPEntity_OID_seq";
ALTER TABLE "TIPOENTIDAD_OID_seq" RENAME TO "HPEntityType_OID_seq";
ALTER TABLE "TIPODOCUMENTO_OID_seq" RENAME TO "HPDocumentType_OID_seq";

ALTER TABLE "HPEntity"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"COMMON"."HPEntity_OID_seq"'::text)::regclass);
ALTER TABLE "HPEntityType"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"COMMON"."HPEntityType_OID_seq"'::text)::regclass);
ALTER TABLE "HPDocumentType"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"COMMON"."HPDocumentType_OID_seq"'::text)::regclass);

SET SEARCH_PATH = "0001";

ALTER TABLE "Agente" RENAME TO "HPAgent";
ALTER TABLE "Agente_Documento" RENAME TO "HPAgent_Document";
ALTER TABLE "Documento" RENAME TO "HPDocument";

ALTER TABLE "Agente_OID_seq" RENAME TO "HPAgent_OID_seq";
ALTER TABLE "Agente_Documento_OID_seq" RENAME TO "HPAgent_Document_OID_seq";
ALTER TABLE "Documento_OID_seq" RENAME TO "HPDocument_OID_seq";

ALTER TABLE "HPAgent"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."HPAgent_OID_seq"'::text)::regclass);
ALTER TABLE "HPAgent_Document"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."HPAgent_Document_OID_seq"'::text)::regclass);
ALTER TABLE "HPDocument"
	ALTER COLUMN "OID" SET DEFAULT nextval(('"0001"."HPDocument_OID_seq"'::text)::regclass);

