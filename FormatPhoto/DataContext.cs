using JLSDataModel.Models;
using JLSDataModel.Models.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FormatPhoto
{

    public class DataContext : DbContext
    {

        public DataContext() 
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=JlsDATA;User Id=dev-sql;Password=abcd+1234"); //todo place correcte connection string 
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<ProductPhotoPath> ProductPhotoPath { get; set; }
        public DbSet<ReferenceItem> ReferenceItem { get; set; }
    }

           

}
    
