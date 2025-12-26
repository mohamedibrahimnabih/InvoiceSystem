var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.InvoiceSystem_Web>("web");

builder.Build().Run();
