--ALTER TABLE main."tasks"
--ADD CONSTRAINT "fk_contractor"
--FOREIGN KEY ("ContractorId")
--REFERENCES main."users"("Id");

--ALTER TABLE main."tasks"
--ADD CONSTRAINT "fk_initiator"
--FOREIGN KEY ("InitiatorId")
--REFERENCES main."users"("Id");

--ALTER TABLE main."users"
--ADD CONSTRAINT "fk_created_tasks"
--FOREIGN KEY ("CreatedTasksId")
--REFERENCES main."tasks"("Id");

--ALTER TABLE main."users"
--ADD CONSTRAINT "fk_assigned_task"
--FOREIGN KEY ("AssignedTasksId")
--REFERENCES main."tasks"("Id");