using System;
using System.Collections.Generic;
using System.Linq;
using EmployeeTaskManagement.Models;
using LiteDB;

namespace EmployeeTaskManagement.Services
{
    public class EmployeeService : IDisposable
    {
        private readonly LiteDatabase _database;
        private readonly LiteCollection<EmployeeViewModel> _collection;
        public EmployeeService(LiteDatabase database)
        {
            _database = database;
            _collection = _database.GetCollection<EmployeeViewModel>();
            _collection.EnsureIndex(x => x.Id);
        }

        public EmployeeViewModel[] List()
        {
            var data = _collection.FindAll().ToList();

            return data.ToArray();
        }
        public EmployeeViewModel Get(Guid id)
        {
            return _collection.FindById(id);
        }

        public Guid Create(EmployeeViewModel model)
        {
            model.Id = Guid.NewGuid();
            _collection.Insert(model);
            return model.Id;
        }

        public void Update(EmployeeViewModel model)
        {
            _collection.Update(model);
        }

        public void Delete(Guid id)
        {
            _collection.Delete(x => x.Id == id);
        }
        public void Dispose()
        {
            _database?.Dispose();
        }
    }
}