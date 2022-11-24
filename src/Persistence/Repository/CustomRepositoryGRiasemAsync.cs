using Ardalis.Specification.EntityFrameworkCore;
using EvaluacionCore.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Persistence.Contexts;

namespace Workflow.Persistence.Repository
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
