IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<PostgresDatabaseResource> database = builder.AddPostgres("database")
    .WithPgAdmin()
    .WithDataVolume()
    .WithEnvironment("TZ", "Asia/Ho_Chi_Minh")
    .AddDatabase("nontitle-db");

builder.AddProject<Projects.Nontitle_API>("nontitle-api")
    .WithReference(database)
    .WaitFor(database);

await builder.Build().RunAsync();
