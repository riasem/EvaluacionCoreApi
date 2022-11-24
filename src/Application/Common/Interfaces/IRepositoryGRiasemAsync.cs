using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluacionCore.Application.Common.Interfaces
{
    public interface IRepositoryGRiasemAsync<T> : IRepositoryBase<T> where T : class
    {

    }

    public interface IReadRepositoryGRiasemAsync<T> : IReadRepositoryBase<T> where T : class
    {

    }
}
