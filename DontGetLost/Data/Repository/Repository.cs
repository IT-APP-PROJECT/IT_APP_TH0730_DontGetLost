﻿using CSharpFunctionalExtensions;
using DontGetLost.Contracts;
using LiteDB;
using System;
using System.Collections.Generic;

namespace DontGetLost.Repository
{
    public sealed class Repository<T> : IRepository<T>
    {
        private readonly LiteDatabase m_database;
        private ILiteCollection<T> Collection => m_database.GetCollection<T>();

        public Repository(LiteDatabase database)
        {
            m_database = database;
        }

        public Result<IEnumerable<T>> FindAll() => Guard(Collection.FindAll);

        public Result<IEnumerable<T>> FindAllWhere(BsonExpression predicate)
            => Guard(() => Collection.Find(predicate));

        public Result<int> Create(T entity)
            => Guard(() => Collection.Insert(entity).AsInt32);

        public Result<bool> Update(T entity)
            => Guard(() => Collection.Update(entity));

        public Result<bool> Delete(int id)
            => Guard(() => Collection.Delete(id));

        private Result<K> Guard<K>(Func<K> func)
        {
            K result;
            try
            {
                result = func();
                return Result.Success(result);
            }
            catch (Exception ex)
            {
                return Result.Failure<K>(ex.Message);
            }
        }
    }
}
