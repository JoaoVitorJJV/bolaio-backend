using FutebolWorker;
using MassTransit;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddMassTransit(x => {
    x.UsingRabbitMq((context, cfg) => {
        cfg.Host("localhost", "/");
    });
});

builder.Services.AddHttpClient();
builder.Services.AddHostedService<FutebolWorkerService>();

var host = builder.Build();
host.Run();
