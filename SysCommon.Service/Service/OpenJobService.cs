using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using Microsoft.Extensions.Logging;
using SysCommon.Service.Interface;
using SysCommon.Service.Jobs;
using SysCommon.Service.Request;
using SysCommon.Service.Response;
using SysCommon.Repository.Domain;
using SysCommon.Repository.Interface;
using Quartz;


namespace SysCommon.Service
{
    public class OpenJobService : BaseService<OpenJob>
    {
        private SysLogApp _sysLogApp;
        private IScheduler _scheduler;
        private ILogger<OpenJobService> _logger;

        /// <summary>
        /// 加载列表
        /// </summary>
        public TableData Load(QueryOpenJobListReq request)
        {
            var result = new TableData();
            var objs = Repository.Find(null);
            if (!string.IsNullOrEmpty(request.key))
            {
                objs = objs.Where(u => u.Id.Contains(request.key));
            }

            result.data = objs.OrderBy(u => u.Id)
                .Skip((request.page - 1) * request.limit)
                .Take(request.limit);
            result.count = objs.Count();
            return result;
        }

        public void Add(AddOrUpdateOpenJobReq req)
        {
            var obj = req.MapTo<OpenJob>();
            //todo:补充或调整自己需要的字段
            obj.CreateDate = DateTime.Now;
            var user = _auth.GetCurrentUser().User;
            obj.CreateId = user.Id;
            Repository.Add(obj);
        }

        public void Update(AddOrUpdateOpenJobReq obj)
        {
            var user = _auth.GetCurrentUser().User;
            UnitWork.Update<OpenJob>(u => u.Id == obj.Id, u => new OpenJob
            {
                JobName = obj.JobName,
                JobType = obj.JobType,
                JobCall = obj.JobCall,
                JobCallParams = obj.JobCallParams,
                Cron = obj.Cron,
                Status = obj.Status,
                Description = obj.Description,
                UpdateDate = DateTime.Now,
                UpdateId = user.Id,
                //todo:补充或调整自己需要的字段
            });
        }

        #region 定时任务运行相关操作

        /// <summary>
        /// 返回系统的job接口
        /// </summary>
        /// <returns></returns>
        public List<string> QueryLocalHandlers()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces()
                    .Contains(typeof(IJob))))
                .ToArray();
            return types.Select(u => u.FullName).ToList();
        }

        public void ChangeJobStatus(ChangeJobStatusReq req)
        {
            var job = Repository.FindSingle(u => u.Id == req.Id);
            if (job == null)
            {
                throw new Exception("任务不存在");
            }



            if (req.Status == 0) //停止
            {
                TriggerKey triggerKey = new TriggerKey(job.Id);
                // 停止触发器
                _scheduler.PauseTrigger(triggerKey);
                // 移除触发器
                _scheduler.UnscheduleJob(triggerKey);
                // 删除任务
                _scheduler.DeleteJob(new JobKey(job.Id));
            }
            else  //启动
            {
                var jobBuilderType = typeof(JobBuilder);
                var method = jobBuilderType.GetMethods().FirstOrDefault(
                        x => x.Name.Equals("Create", StringComparison.OrdinalIgnoreCase) &&
                             x.IsGenericMethod && x.GetParameters().Length == 0)
                    ?.MakeGenericMethod(Type.GetType(job.JobCall));

                var jobBuilder = (JobBuilder)method.Invoke(null, null);

                IJobDetail jobDetail = jobBuilder.WithIdentity(job.Id).Build();
                jobDetail.JobDataMap[Define.JOBMAPKEY] = job.Id;  //传递job信息
                ITrigger trigger = TriggerBuilder.Create()
                    .WithCronSchedule(job.Cron)
                    .WithIdentity(job.Id)
                    .StartNow()
                    .Build();
                _scheduler.ScheduleJob(jobDetail, trigger);
            }


            var user = _auth.GetCurrentUser().User;

            job.Status = req.Status;
            job.UpdateDate = DateTime.Now;
            job.UpdateId = user.Id;
            Repository.Update(job);
        }

        /// <summary>
        /// 记录任务运行结果
        /// </summary>
        /// <param name="jobId"></param>
        public void RecordRun(string jobId)
        {
            var job = Repository.FindSingle(u => u.Id == jobId);
            if (job == null)
            {
                _sysLogApp.Add(new SysLog
                {
                    TypeName = "定时任务",
                    TypeId = "AUTOJOB",
                    Content = $"未能找到定时任务：{jobId}"
                });
                return;
            }

            job.RunCount++;
            job.LastRunTime = DateTime.Now;
            Repository.Update(job);

            _sysLogApp.Add(new SysLog
            {
                CreateName = "Quartz",
                CreateId = "Quartz",
                TypeName = "定时任务",
                TypeId = "AUTOJOB",
                Content = $"运行了自动任务：{job.JobName}"
            });
            _logger.LogInformation($"运行了自动任务：{job.JobName}");
        }

        #endregion


        public OpenJobService(IUnitWork unitWork, IRepository<OpenJob> repository,
            IAuth auth, SysLogApp sysLogApp, IScheduler scheduler, ILogger<OpenJobService> logger) : base(unitWork, repository, auth)
        {
            _sysLogApp = sysLogApp;
            _scheduler = scheduler;
            _logger = logger;
        }

    }
}