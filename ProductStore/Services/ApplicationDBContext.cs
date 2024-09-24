using System;
using Microsoft.EntityFrameworkCore;
using ProductStore.Models;

namespace ProductStore.Services
{
	public class ApplicationDBContext : DbContext
	{

		public ApplicationDBContext(DbContextOptions options ) : base(options)
		{

		}
		public DbSet<Products> Products { get; set; } 
	}
}

