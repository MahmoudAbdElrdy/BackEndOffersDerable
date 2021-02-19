using BackEnd.BAL.Interfaces;
using BackEnd.DAL.Context;
using BackEnd.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.BAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BakEndContext Context;


        public UnitOfWork(BakEndContext dbContext)
        {
            Context = dbContext;
        }

        public GenericRepository<T> Repository<T>() where T : class, new()
        {
            return new GenericRepository<T>(Context);
        }


        public virtual string Save()
        {
            var returnValue = "200";
            using (var dbContextTransaction = Context.Database.BeginTransaction())
            {
                try
                {
                    Context.SaveChanges();
                    dbContextTransaction.Commit();
                }

                catch (Exception e)
                {
                    //Log Exception Handling message                      
                    returnValue = "Error " + string.Format("{0} - {1} ", e.Message, e.InnerException != null ? e.InnerException.Message : "");
                    dbContextTransaction.Rollback();
                }
            }

            return returnValue;
        }
        public virtual async Task<int> SaveAsync()
        {
            int returnValue = 200;
            using (var dbContextTransaction = Context.Database.BeginTransaction())
            {
                try
                {
                    await Context.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (DbUpdateException ex)
                {
                    var sqlException = ex.GetBaseException() as SqlException;

                    if (sqlException != null)
                    {
                        var number = sqlException.Number;

                        if (number == 547)
                        {
                            returnValue = 501;

                        }
                        else
                            returnValue = 500;
                    }
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    returnValue = 500;
                    dbContextTransaction.Rollback();
                }
            }

            return returnValue;
        }

        private bool disposed = false;



        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public IGenericRepository<Category> Category { get { return new GenericRepository<Category>(Context); } }
        public IGenericRepository<ApplicationUser> ApplicationUser { get { return new GenericRepository<ApplicationUser>(Context); } }

        public IGenericRepository<Product> Prodcut { get { return new GenericRepository<Product>(Context); } }
        public IGenericRepository<ProductImages> ProductImages { get { return new GenericRepository<ProductImages>(Context); } }
        public IGenericRepository<Company> Company { get { return new GenericRepository<Company>(Context); } }
        public IGenericRepository<Discount> Discount { get { return new GenericRepository<Discount>(Context); } }
        public IGenericRepository<Rating> Rating { get { return new GenericRepository<Rating>(Context); } }
        public IGenericRepository<Client> Client { get { return new GenericRepository<Client>(Context); } }
        public IGenericRepository<Purchases> Purchases { get { return new GenericRepository<Purchases>(Context); } }
    }
}
