using System;
using System.Collections.Generic;
using System.Linq;
using AppCore.Interfaces;
using AppCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
         private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public IList<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }


        public T GetBy(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public T Add(User source, T entity)
        {
            var tracked = _context.Set<T>().Add(entity);
            _context.SaveChanges();

            var log = new DbLog();
            log.ExecUserId = source.Id;
            log.Action = ACTION.ADD;

            this.Logging(log, tracked.Entity);

            return tracked.Entity;
        }

        public void Update(User source, T entity)
        {
            
            _context.Set<T>().Update(entity);
            _context.SaveChanges();

            if(source != null){
                var log = new DbLog();
                log.ExecUserId = source.Id;
                log.Action = ACTION.UPDATE;

                this.Logging(log, entity);
            }
        }

        public void Delete(User source, T entity)
        {
            var log = new DbLog();
            log.ExecUserId = source.Id;
            log.Action = ACTION.DELETE;

            this.Logging(log, entity);

            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public bool Exists(int id)
        {
            return this.GetBy(id) != null;
        }

        private void Logging(DbLog log, T entity)
        {
            var check = log.GetActionTarget(typeof(T));
            if(check){
                log.ExecDate = DateTime.Now;
                
                if(typeof(T).Equals(typeof(Comment))){
                    var temp = entity as Comment;
                    log.TargetId = temp.ToDoTaskId;
                    log.TargetName = temp.ToDoTask.Title;
                }else if(typeof(T).Equals(typeof(User))){
                    var temp = entity as User;
                    log.TargetId = temp.Id;
                    log.TargetName = temp.Name;
                }else{
                    var temp = entity as ToDoTask;
                    log.TargetId = temp.Id;
                    log.TargetName = temp.Title;
                }
            }
            _context.Set<DbLog>().Add(log);
            _context.SaveChanges();
        }

    }
}