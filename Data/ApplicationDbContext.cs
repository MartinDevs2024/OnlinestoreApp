using EcommerceApp.Models;
using EcommerceApp.Models.Comments;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }
		public DbSet<Post> Posts { get; set; }
        public DbSet<MainComment> MainComments { get; set; }
		public DbSet<SubComment> SubComments { get; set; }
		public DbSet<MyMessage> MyMessages { get; set; }

	}
}
