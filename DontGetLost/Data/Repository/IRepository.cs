using CSharpFunctionalExtensions;
using LiteDB;
using System.Collections.Generic;

namespace DontGetLost.Contracts
{
    public interface IRepository<T>
    {
        Result<int> Create(T entity);
        Result<bool> Delete(int id);
        Result<IEnumerable<T>> FindAll();
        Result<IEnumerable<T>> FindAllWhere(BsonExpression predicate);
        Result<bool> Update(T entity);
    }
}
