/* UPDATE 3.12.0.0*/

SET SEARCH_PATH = "COMMON";

--AGENTE -> AGENTE_DOCUMENTO
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE") VALUES ('', '1', '', '1');
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE") VALUES ('', '2', '', '2');
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE") VALUES ('', '3', '', '3');
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE") VALUES ('', '4', '', '4');

--DOCUMENTO -> AGENTE_DOCUMENTO
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE") VALUES ('33', '1', '', '1');
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE") VALUES ('33', '2', '', '2');
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE") VALUES ('33', '3', '', '3');
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE") VALUES ('33', '4', '', '4');

--DOCUMENTO -> AGENTE
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE") VALUES ('33', '1', '', '1');
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE") VALUES ('33', '2', '', '2');
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE") VALUES ('33', '3', '', '3');
INSERT INTO "COMMON"."ItemMap" ("OID_ITEM","PRIVILEGE","OID_ASSOCIATE_ITEM","ASSOCIATE_PRIVILEGE") VALUES ('33', '4', '', '4');

SET SEARCH_PATH = "0001";



