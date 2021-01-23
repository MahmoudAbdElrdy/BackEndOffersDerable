using BackEnd.BAL.Repository;
using BackEnd.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.BAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        string Save();

        GenericRepository<T> Repository<T>() where T : class, new();

        Task<int> SaveAsync();
        IGenericRepository<Category> Category { get; }
        IGenericRepository<ApplicationUser> ApplicationUser{ get; }
    }
}
