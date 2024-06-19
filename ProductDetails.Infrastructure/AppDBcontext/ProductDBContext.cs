using Microsoft.EntityFrameworkCore;
using ProductDetails.Domain.Entities;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductDetails.Infrastructure.ProductDetails
{
    public class ProductDBContext : DbContext
    {
        public ProductDBContext(DbContextOptions<ProductDBContext> options) : base(options)
        {

        }
        public DbSet<ProductInfo> ProductInfo { get; set; }

       
    }
}
