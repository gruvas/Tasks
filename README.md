# Tasks

В данном приложении реализовано создание и изменение пользователей и задач.

База данных устроена из трех тиблиц: users, tasks, contractor_initiator. Cвязи данных таблиц представлены на рисунке ниже. 

<img src="https://raw.githubusercontent.com/gruvas/img/main/db_schema.png">

Подробнее о связах: поле ContractorInitiatorId в таблице tasks связано с полем Id в таблице contractor_initiator. Поле ContractorId в таблице contractor_initiator связано с полем Id в таблице users. Поле InitiatorId в таблице contractor_initiator связано с полем Id в таблице users. К каждой связе добавлено каскадное удаление.



