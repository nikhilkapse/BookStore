﻿using Book.DataAccess.Data;
using Book.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Book.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> DbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.DbSet = _db.Set<T>(); // this is required to access or assign methods to generic type received as T because 
            // _db.Categories = DbSet  // let's assume we have Categories table then with generic interface we cannot do _db.Categories.Add(obj)
        }
        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = DbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = DbSet;
            return query.ToList();  
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity); 
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }
    }
}
