using System.Text;
using System.Threading.Tasks;
using SysCommon.Repository.Domain;
using Quartz;

namespace SysCommon.Service.Jobs
{
    public class SysLogJob : IJob
    {
        private SysLogApp _sysLogApp;
        private OpenJobService _openJobService;

        public SysLogJob(SysLogApp sysLogApp, OpenJobService openJobService)
        {
            _sysLogApp = sysLogApp;
            _openJobService = openJobService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var jobId = context.MergedJobDataMap.GetString(Define.JOBMAPKEY);
            //todo:这里可以加入自己的自动任务逻辑


            {
                var log = new SysLog
                {
                    TypeName = "定时任务"
                };
                _sysLogApp.Add(log);
            }
            _openJobService.RecordRun(jobId);
            return Task.Delay(1);
        }
    }
    public class SysLogJob1 : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}