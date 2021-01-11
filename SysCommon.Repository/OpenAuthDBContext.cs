using System;
using System.Reflection;
using System.Runtime.Loader;
using Infrastructure.Extensions.AutofacManager;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SysCommon.Repository.Core;
using Microsoft.Extensions.DependencyModel;

namespace SysCommon.Repository
{

    //public partial class SysCommonDBContext : DbContext
    //{

    //    private ILoggerFactory _LoggerFactory;

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        optionsBuilder.EnableSensitiveDataLogging (true);  //允许打印参数
    //        optionsBuilder.UseLoggerFactory (_LoggerFactory);

    //        base.OnConfiguring (optionsBuilder);
    //    }

    //    public SysCommonDBContext(DbContextOptions<SysCommonDBContext> options, ILoggerFactory loggerFactory)
    //        : base(options)
    //    {
    //        _LoggerFactory = loggerFactory;
    //    }

    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        modelBuilder.Entity<DataPrivilegeRule>()
    //            .HasKey(c => new { c.Id });
    //    }

    //    public virtual DbSet<Application> Applications { get; set; }
    //    public virtual DbSet<Category> Categories { get; set; }
    //    public virtual DbSet<CategoryType> CategoryTypes { get; set; }
    //    public virtual DbSet<FlowInstance> FlowInstances { get; set; }
    //    public virtual DbSet<FlowInstanceOperationHistory> FlowInstanceOperationHistorys { get; set; }
    //    public virtual DbSet<FlowInstanceTransitionHistory> FlowInstanceTransitionHistorys { get; set; }
    //    public virtual DbSet<FlowScheme> FlowSchemes { get; set; }
    //    public virtual DbSet<Form> Forms { get; set; }
    //    public virtual DbSet<Module> Modules { get; set; }
    //    public virtual DbSet<ModuleElement> ModuleElements { get; set; }
    //    public virtual DbSet<Org> Orgs { get; set; }
    //    public virtual DbSet<Relevance> Relevances { get; set; }
    //    public virtual DbSet<Resource> Resources { get; set; }
    //    public virtual DbSet<Role> Roles { get; set; }
    //    public virtual DbSet<User> Users { get; set; }
    //    public virtual DbSet<UploadFile> UploadFiles { get; set; }

    //    public virtual DbSet<FrmLeaveReq> FrmLeaveReqs { get; set; }

    //    public virtual DbSet<SysLog> SysLogs { get; set; }

    //    public virtual DbSet<SysMessage> SysMessages { get; set; }

    //    public virtual DbSet<DataPrivilegeRule> DataPrivilegeRules { get; set; }

    //    public virtual DbSet<WmsInboundOrderDtbl> WmsInboundOrderDtbls { get; set; }
    //    public virtual DbSet<WmsInboundOrderTbl> WmsInboundOrderTbls { get; set; }
    //    public virtual DbSet<OpenJob> OpenJobs { get; set; }
    //    public virtual DbSet<BuilderTable> BuilderTables { get; set; }
    //    public virtual DbSet<BuilderTableColumn> BuilderTableColumns { get; set; }

    //    //非数据库表格
    //    public virtual DbQuery<SysTableColumn> SysTableColumns { get; set; }

    //}




    public partial class SysCommonDBContext : DbContext, IDependency
    {
        public SysCommonDBContext(DbContextOptions<SysCommonDBContext> options)
            : base(options)
        {
        }

        public SysCommonDBContext()
        {
        }
        public override void Dispose()
        {
            base.Dispose();
        }
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                throw (ex.InnerException as Exception ?? ex);
            }
        }
        public override DbSet<TEntity> Set<TEntity>()
        {
            return base.Set<TEntity>();
        }
        //public DbSet<TEntity> Set<TEntity>(bool trackAll = false) where TEntity : class
        //{
        //    return base.Set<TEntity>();
        //}
        /// <summary>
        /// 设置跟踪状态
        /// </summary>
        public bool QueryTracking
        {
            set
            {
                this.ChangeTracker.QueryTrackingBehavior =
                       value ? QueryTrackingBehavior.TrackAll
                       : QueryTrackingBehavior.NoTracking;
            }
        }
        /// <summary>
        /// 数据库配置
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string connectionString = DBServerProvider.GetConnectionString(null);
            //if (Const.DBType.Name == Enums.DbCurrentType.MySql.ToString())
            //{
            //    optionsBuilder.UseMySql(connectionString);
            //}
            //else if (Const.DBType.Name == Enums.DbCurrentType.PgSql.ToString())
            //{
            //    optionsBuilder.UseNpgsql(connectionString);
            //}
            //else
            //{
            //    optionsBuilder.UseSqlServer(connectionString);
            //}
            ////默认禁用实体跟踪
            //optionsBuilder = optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            ////var loggerFactory = new LoggerFactory();
            ////loggerFactory.AddProvider(new EFLoggerProvider());
            ////  optionsBuilder.UseLoggerFactory(loggerFactory);
            //base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Type type = null;
            try
            {
                //获取所有类库
                var compilationLibrary = DependencyContext
                    .Default
                    .CompileLibraries
                    .Where(x => !x.Serviceable && x.Type != "package" && x.Type == "project");
                foreach (var _compilation in compilationLibrary)
                {
                    //加载指定类
                    AssemblyLoadContext.Default
                    .LoadFromAssemblyName(new AssemblyName(_compilation.Name))
                    .GetTypes()
                    .Where(x =>
                        x.GetTypeInfo().BaseType != null
                        && x.BaseType == (typeof(Entity))|| x.BaseType == (typeof(TreeEntity)))
                        .ToList().ForEach(t =>
                        {
                            modelBuilder.Entity(t);
                            //  modelBuilder.Model.AddEntityType(t);
                        });
                }
                //modelBuilder.AddEntityConfigurationsFromAssembly(GetType().Assembly);
                base.OnModelCreating(modelBuilder);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
