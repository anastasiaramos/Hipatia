/* UPDATE 4.1.1.2*/

SET SEARCH_PATH = "COMMON";

UPDATE "Variable" SET "VALUE" = '4.1.1.2' WHERE "NAME" = 'HIPATIA_DB_VERSION';

SET SEARCH_PATH = "0001";

ALTER TABLE "HPDocument" ADD COLUMN "EXPIRATION_DATE" timestamp without time zone;


