using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryGateway.Domain.Repository;

public interface IBaseRepository<T>
{
    void Add(T entity);
    void AddRange(List<T> entity);
    void Update(T entity);
    void UpdateRange(List<T> entity);
    void Delete(T entity);
    void DeleteRange(List<T> entity);
}
