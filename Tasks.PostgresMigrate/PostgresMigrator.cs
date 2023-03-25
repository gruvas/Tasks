﻿using DbUp;

namespace Tasks.PostgresMigrate
{
    public class PostgresMigrator
    {
        public static void Migrate(string connectionString)
        {
            AppContext.SetSwitch("Npgsql.Enable Legacy TimestampBehavior", true);
            var upgrader = DeployChanges.To
            .PostgresqlDatabase(connectionString)
            //.JournalToPostgresqlTable("main", "users")
            //.JournalToPostgresqlTable("main", "__SchemaVersions")
            .JournalToPostgresqlTable("protection", "__SchemaVersions")


            //.WithScriptsFromFileSystem(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sctipts"))
            //.WithScriptsFromFileSystem(Path.Combine("Tasks.PostgresMigrate/Scripts"))
            //.WithScriptsFromFileSystem(Path.Combine("..\\..\\Tasks\\Tasks.PostgresMigrate\\Scripts"))
            //.WithScriptsFromFileSystem(Path.Combine("Tasks.PostgresMigrate/Scripts"))
            .WithScriptsFromFileSystem(Path.Combine("../Tasks.PostgresMigrate/Scripts"))


            .WithVariable("BODY", "$BODY$")
            .WithExecutionTimeout(TimeSpan.FromSeconds(60 * 5))
            .Build();
            var result = upgrader.PerformUpgrade();
            if (!result.Successful)
                throw new Exception("Migrate error: ", result.Error);
        }
    }
}