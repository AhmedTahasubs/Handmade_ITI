﻿using HandmadeITI.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HandmadeITI.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		public DbSet<Product> Product { get; set; } = default!;
		public DbSet<Category> Category { get; set; } = default!;
		public DbSet<Order> Order { get; set; } = default!;
		public DbSet<Cart> Cart { get; set; } = default!;
		public DbSet<Review> Review { get; set; } = default!;
		public DbSet<CartItem> CartItem { get; set; } = default!;
		public DbSet<OrderItem> OrderItem { get; set; } = default!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// حل مشكلة multiple cascade paths
			modelBuilder.Entity<CartItem>()
				.HasOne(ci => ci.Product)
				.WithMany()
				.HasForeignKey(ci => ci.ProductId)
				.OnDelete(DeleteBehavior.Restrict); // أو DeleteBehavior.NoAction

			// لو حبيت تتحكم في العلاقات الأخرى برضو
			modelBuilder.Entity<CartItem>()
				.HasOne(ci => ci.Cart)
				.WithMany(c => c.CartItems)
				.HasForeignKey(ci => ci.CartId)
				.OnDelete(DeleteBehavior.Cascade); // دي ممكن تفضل موجودة


			// الباقي زي ما هو
			// Fix OrderItem conflict
			modelBuilder.Entity<OrderItem>()
				.HasOne(oi => oi.Product)
				.WithMany()
				.HasForeignKey(oi => oi.ProductId)
				.OnDelete(DeleteBehavior.Restrict); // No cascade to avoid multiple paths

			modelBuilder.Entity<OrderItem>()
				.HasOne(oi => oi.Order)
				.WithMany(o => o.OrderItems)
				.HasForeignKey(oi => oi.OrderId)
				.OnDelete(DeleteBehavior.Cascade);
			modelBuilder.Entity<Review>()
	.HasOne(r => r.User)
	.WithMany(u => u.Reviews)
	.HasForeignKey(r => r.UserId)
	.OnDelete(DeleteBehavior.Restrict); // بدل Cascade

			modelBuilder.Entity<Review>()
				.HasOne(r => r.Product)
				.WithMany(p => p.Reviews)
				.HasForeignKey(r => r.ProductId)
				.OnDelete(DeleteBehavior.Cascade); // نحتفظ بالـ cascade هنا مثلاً


			base.OnModelCreating(modelBuilder);
		}

	}
}
