using Ardalis.Specification.EntityFrameworkCore;
using EvaluacionCore.Application.Common.Interfaces;
using EvaluacionCore.Persistence.Contexts;

namespace EvaluacionCore.Persistence.Repository
{
    public class CustomRepositoryGRiasemAsync<T> : RepositoryBase<T>, IRepositoryGRiasemAsync<T> where T : class
    {
        private readonly ApplicationDbGRiasemContext dbContext;

        public CustomRepositoryGRiasemAsync(ApplicationDbGRiasemContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
