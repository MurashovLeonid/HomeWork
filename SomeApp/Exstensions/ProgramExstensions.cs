using Quartz;
using SomeApp.Jobs;

namespace SomeApp.Exstensions
{
    public static class ProgramExstensions
    {
        public static void ConfigureQuartz(this IServiceCollection services)
        {
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                var createJobKey = new JobKey("CreateEntryRowsJob");
                var updateJobKey = new JobKey("UpdateEntryRowsJob");
                var deleteJobKey = new JobKey("DeleteEntryRowsJob");

                q.AddJob<CreateEntryRowsJob>(opts => opts.WithIdentity(createJobKey));
                q.AddJob<UpdateEntryRowsJob>(opts => opts.WithIdentity(updateJobKey));
                q.AddJob<DeleteEntryRowsJob>(opts => opts.WithIdentity(deleteJobKey));

                q.AddTrigger(opts => opts
                    .ForJob(createJobKey)
                    .WithIdentity("CreateEntryRowsJob-trigger")
                    .WithCronSchedule("0 */2 * ? * *")
                );
                q.AddTrigger(opts => opts
                    .ForJob(updateJobKey)
                    .WithIdentity("UpdateEntryRowsJob-trigger")
                    .WithCronSchedule("0 */2 * ? * *")
                );
                q.AddTrigger(opts => opts
                    .ForJob(deleteJobKey)
                    .WithIdentity("DeleteEntryRowsJob-trigger")
                    .WithCronSchedule("0 */2 * ? * *")
                );

                
               
            });
            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        }
    }
}
