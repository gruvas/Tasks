CREATE TABLE IF NOT EXISTS main."users" (
    "Id" serial4 NOT NULL PRIMARY KEY,
    "LastName" varchar(50) NULL,
    "FirstName" varchar(50) NULL,
    "Email" varchar(128) NULL
);

CREATE TABLE IF NOT EXISTS main."tasks"(
	"Id" serial4 NOT NULL,
	"ContractorInitiatorId" int4 NULL,
	"Subject" varchar(255) NULL,
	"Description" text NULL,
	"CreatedDate" timestamptz DEFAULT now(),
    "ExpirationDate" timestamptz NULL
);

CREATE TABLE IF NOT EXISTS main."contractor_initiator"(
	"Id" serial4 NOT NULL,
	"ContractorId" int4 NOT NULL,
    "InitiatorId" int4 NOT NULL
);