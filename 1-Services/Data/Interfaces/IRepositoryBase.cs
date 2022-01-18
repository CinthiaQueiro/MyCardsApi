using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Data.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
    }
}
