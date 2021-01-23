using BackEnd.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealState.DAL.Context;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BackEnd.DAL.Context
{
  public class BakEndContext : IdentityDbContext<ApplicationUser>, IBackEndContext
  {
    public BakEndContext(DbContextOptions<BakEndContext> options) : base(options)
    {
      
    }
  
    public DbSet<Category> Category { get; set; }
    public DbSet<Client> Client { get; set; }
    public DbSet<Company> Company { get; set; }
    public DbSet<Discount> Discount { get; set; } 
    public DbSet<Rating> Rating  { get; set; } 
    public DbSet<Purchases> Purchases { get; set; } 
    public DbSet<Product> Product{ get; set; }
    public DbSet<ProductImages> ProductImages{ get; set; }
    public DbSet<Review> Review { get; set; }
  }
}
