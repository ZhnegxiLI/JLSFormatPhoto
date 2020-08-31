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
            optionsBuilder.UseSqlServer("Data Source=blog.db"); //todo place correcte connection string 
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<ProductPhotoPath> ProductPhotoPath { get; set; }
        public DbSet<ReferenceItem> ReferenceItem { get; set; }
    }

           

}
    
