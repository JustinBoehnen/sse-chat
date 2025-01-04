var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.SseChat_ApiService>("apiservice");

builder.AddProject<Projects.SseChat_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
