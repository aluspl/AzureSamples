using System;
using LifeLike.CloudService.CosmosDB;
using LifeLike.CloudService.SQLDB;
using LifeLike.Shared;
using LifeLike.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LifeLike.CloudService.SqlDB
{
    public class SQLUnitOfWork : IUnitOfWork 
    {

        public PortalContext CreateDbContext(string args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PortalContext>();
            optionsBuilder.UseSqlServer(args);
            return new PortalContext(optionsBuilder.Options);
        }
        public SQLUnitOfWork(IConfiguration configuration)
        {
            _configuration = configuration;
            _DbContext = CreateDbContext(_configuration["DB"]);
        }

        public IRepository<T> Get<T>() where T : class
        {
            IRepository<T> repo = new SqlDB<T>(_DbContext);

            return repo;
        }
        public Result Save()
        {
            try
            {
                return Result.Success;
            }
            catch (Exception)
            {
                return Result.Failed;
            }
        }
        private readonly IConfiguration _configuration;
        private readonly PortalContext _DbContext;

        public Provider Provider => Provider.EF;
    }
       
}