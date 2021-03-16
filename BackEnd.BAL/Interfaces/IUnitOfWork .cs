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
        IGenericRepository<Product> Prodcut { get; }
        IGenericRepository<ProductImages> ProductImages { get; }
        IGenericRepository<Company> Company { get; } 
        IGenericRepository<Discount> Discount { get; }  
        IGenericRepository<Rating> Rating{ get; }  
        IGenericRepository<Client> Client { get; }  
        IGenericRepository<Purchases> Purchases{ get; }  
        IGenericRepository<ProductFavourite> ProductFavourite { get; }  
        IGenericRepository<tblCities> Cities { get; }  
        IGenericRepository<NotificationClient> NotificationClient { get; }  
       
    }
}
