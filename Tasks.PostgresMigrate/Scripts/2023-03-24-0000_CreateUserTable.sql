CREATE TABLE IF NOT EXISTS main."users" (
    "Id" serial4 NOT NULL,
    "LastName" varchar(50) NULL,
    "FirstName" varchar(50) NULL,
    "Email" varchar(128) NULL
);

CREATE TABLE IF NOT EXISTS main."tasks"(
	"Id" serial4 NOT NULL,
	"Subject" varchar(255) NULL,
	"Description" text NULL,
	"Contractorid" int4 NULL,
	"Initiatorid" int4 NULL,
	"CreatedDate" timestamptz DEFAULT now(),
    "ExpirationDate" timestamptz NULL
);