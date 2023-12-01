using Hangfire;
using Microsoft.Extensions.Logging;
using ZAM.Core.Application.Tahoma.Commands;
using ZAM.Core.Application.Tahoma.Services;

namespace ZAM.Core.Application.Tahoma.CommandHandlers;

internal sealed class RepeatedActionHandler : IRequestHandler<RepeatedAction>
{
    private const string CRON_REPEAT_EVERY_MINUTE = "*/1 * * * *";
    private readonly ILogger<RepeatedActionHandler> logger;
    private readonly IRecurringJobManager recurringJob;
    private readonly ITahomaService tahomaService;

    public RepeatedActionHandler(IRecurringJobManager recurringJob, ILogger<RepeatedActionHandler> logger, ITahomaService tahomaService)
    {
        this.recurringJob = recurringJob;
        this.logger = logger;
        this.tahomaService = tahomaService;
    }

    public Task Handle(RepeatedAction request, CancellationToken cancellationToken)
    {
        var jobId = Guid.NewGuid().ToString();

        this.logger.LogInformation("Scheduling job Id = {jobId} with interval {interval}", jobId, request.IntervalInMinutes);

        this.recurringJob.AddOrUpdate(jobId, () => Method(request.Label),
            CRON_REPEAT_EVERY_MINUTE);

        //this.recurringJob.AddOrUpdate<ITahomaService>(jobId,
        //    s => s.SendTahomaAction(request.Label, request.Command, request.DeviceUrl), 
        //    CRON_REPEAT_EVERY_MINUTE);

        return Task.CompletedTask;
    }

    public void Method(string label)
    {
        //TODO
    }
}
