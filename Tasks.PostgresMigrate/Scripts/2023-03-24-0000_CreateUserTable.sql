CREATE TABLE IF NOT EXISTS main."users" (
    "Id" serial4 NOT NULL PRIMARY KEY,
    "LastName" varchar(50) NULL,
    "FirstName" varchar(50) NULL,
    "Email" varchar(128) NULL
);

CREATE TABLE IF NOT EXISTS main."tasks" (
    "Id" serial4 NOT NULL PRIMARY KEY,
    "Subject" varchar(255) NULL,
    "Description" text NULL,
    "ContractorId" int4 NULL,
    "InitiatorId" int4 NULL,
    "CreatedDate" timestamptz DEFAULT now(),
    "ExpirationDate" timestamptz NULL,
    "UserTaskId" int4 NULL
);

CREATE TABLE IF NOT EXISTS main."users_tasks" (
    "Id" serial4 NOT NULL PRIMARY KEY,
    "UserId" int4 NOT NULL,
    "TaskId" int4 NOT NULL,
    CONSTRAINT "fk_users_tasks_user"
        FOREIGN KEY ("UserId")
        REFERENCES main."users"("Id"),
    CONSTRAINT "fk_users_tasks_task"
        FOREIGN KEY ("TaskId")
        REFERENCES main."tasks"("Id")
);

ALTER TABLE main."tasks" ADD CONSTRAINT "fk_tasks_user_task" FOREIGN KEY ("UserTaskId") REFERENCES main."users_tasks"("Id");


INSERT INTO main."users" ("LastName", "FirstName", "Email") VALUES ('Doe', 'John', 'john.doe@example.com');
INSERT INTO main."users" ("LastName", "FirstName", "Email") VALUES ('Smith', 'Jane', 'jane.smith@example.com');

INSERT INTO main."tasks" ("Subject", "Description", "ContractorId", "InitiatorId") VALUES ('Do something', 'Do something important', 1, 2);
INSERT INTO main."tasks" ("Subject", "Description", "ContractorId", "InitiatorId") VALUES ('Do something else', 'Do something else important', 2, 1);

INSERT INTO main."users_tasks" ("UserId", "TaskId") VALUES (1, 1);
INSERT INTO main."users_tasks" ("UserId", "TaskId") VALUES (2, 2);

ALTER TABLE main."tasks"
ADD CONSTRAINT "fk_tasks_contractor"
    FOREIGN KEY ("ContractorId")
    REFERENCES main."users"("Id");

ALTER TABLE main."tasks"
ADD CONSTRAINT "fk_tasks_initiator"
    FOREIGN KEY ("InitiatorId")
    REFERENCES main."users"("Id");

ALTER TABLE main."users_tasks"
ADD CONSTRAINT "fk_users_tasks_created_tasks"
    FOREIGN KEY ("TaskId")
    REFERENCES main."tasks"("Id")
    ON DELETE CASCADE;

ALTER TABLE main."users_tasks"
ADD CONSTRAINT "fk_users_tasks_assigned_tasks"
    FOREIGN KEY ("UserId")
    REFERENCES main."users"("Id")
    ON DELETE CASCADE;