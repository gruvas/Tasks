CREATE TABLE IF NOT EXISTS main."users" (
    "Id" serial4 NOT NULL PRIMARY KEY,
    "LastName" varchar(50) NULL,
    "FirstName" varchar(50) NULL,
    "Email" varchar(128) NULL
);

CREATE TABLE IF NOT EXISTS main."tasks"(
	"Id" serial4 NOT NULL PRIMARY KEY,
	"ContractorInitiatorId" int4 NULL,
	"Subject" varchar(255) NULL,
	"Description" text NULL,
	"CreatedDate" timestamptz DEFAULT now(),
    "ExpirationDate" timestamptz NULL
);

CREATE TABLE IF NOT EXISTS main."contractor_initiator"(
	"Id" serial4 NOT NULL PRIMARY KEY,
	"ContractorId" int4 NOT NULL REFERENCES main."users"("Id") ON DELETE CASCADE,
    "InitiatorId" int4 NOT NULL REFERENCES main."users"("Id") ON DELETE CASCADE
);

ALTER TABLE main."tasks" ADD CONSTRAINT "FK_tasks_contractor_initiator" FOREIGN KEY ("ContractorInitiatorId") REFERENCES main."contractor_initiator"("Id") ON DELETE CASCADE;

ALTER TABLE main."contractor_initiator" ADD CONSTRAINT "FK_contractor_initiator_contractor_id" FOREIGN KEY ("ContractorId") REFERENCES main."users"("Id") ON DELETE CASCADE;

ALTER TABLE main."contractor_initiator" ADD CONSTRAINT "FK_contractor_initiator_initiator_id" FOREIGN KEY ("InitiatorId") REFERENCES main."users"("Id") ON DELETE CASCADE;

