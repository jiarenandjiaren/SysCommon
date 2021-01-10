using Enyim.Caching;
using Infrastructure.Extensions.AutofacManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
using SysCommon.Repository.Core;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace SysCommon
{
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
                        && x.BaseType == (typeof(BaseEntity)))
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
