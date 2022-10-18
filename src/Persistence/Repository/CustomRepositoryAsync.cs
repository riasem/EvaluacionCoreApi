using Ardalis.Specification.EntityFrameworkCore;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Persistence.Contexts;


namespace EvaluacionCore.Persistence.Repository
{
    public  class CustomRepositoryAsync<T> : RepositoryBase<T>,IRepositoryAsync<T> where T : class
    {
        private readonly ApplicationDbContext dbContext;

        public CustomRepositoryAsync(ApplicationDbContext dbContext): base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
