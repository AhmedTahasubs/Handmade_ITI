using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandmadeITI.Data.Migrations
{
	/// <inheritdoc />
	public partial class startUpdatabase : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Category",
				columns: table => new
				{
					CategoryId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Category", x => x.CategoryId);
				});

			migrationBuilder.CreateTable(
				name: "User",
				columns: table => new
				{
					UserId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
					LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
					DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
					NationalId = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
					MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
					IsBlackListed = table.Column<bool>(type: "bit", nullable: false),
					Role = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_User", x => x.UserId);
				});

			migrationBuilder.CreateTable(
				name: "Cart",
				columns: table => new
				{
					CartId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserId = table.Column<int>(type: "int", nullable: false),
					TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
					IsCheckedOut = table.Column<bool>(type: "bit", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Cart", x => x.CartId);
					table.ForeignKey(
						name: "FK_Cart_User_UserId",
						column: x => x.UserId,
						principalTable: "User",
						principalColumn: "UserId",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Order",
				columns: table => new
				{
					OrderId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
					UserId = table.Column<int>(type: "int", nullable: false),
					ShippingAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
					OrderStatus = table.Column<int>(type: "int", nullable: false),
					PaymentStatus = table.Column<int>(type: "int", nullable: false),
					TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Order", x => x.OrderId);
					table.ForeignKey(
						name: "FK_Order_User_UserId",
						column: x => x.UserId,
						principalTable: "User",
						principalColumn: "UserId",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Product",
				columns: table => new
				{
					ProductId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
					Quantity = table.Column<int>(type: "int", nullable: false),
					IsApproved = table.Column<bool>(type: "bit", nullable: false),
					ImageUrls = table.Column<string>(type: "nvarchar(max)", nullable: false),
					CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
					SellerId = table.Column<int>(type: "int", nullable: false),
					CategoryId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Product", x => x.ProductId);
					table.ForeignKey(
						name: "FK_Product_Category_CategoryId",
						column: x => x.CategoryId,
						principalTable: "Category",
						principalColumn: "CategoryId",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Product_User_SellerId",
						column: x => x.SellerId,
						principalTable: "User",
						principalColumn: "UserId",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "CartItem",
				columns: table => new
				{
					CartItemId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					CartId = table.Column<int>(type: "int", nullable: false),
					ProductId = table.Column<int>(type: "int", nullable: false),
					Quantity = table.Column<int>(type: "int", nullable: false),
					UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_CartItem", x => x.CartItemId);
					table.ForeignKey(
						name: "FK_CartItem_Cart_CartId",
						column: x => x.CartId,
						principalTable: "Cart",
						principalColumn: "CartId",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_CartItem_Product_ProductId",
						column: x => x.ProductId,
						principalTable: "Product",
						principalColumn: "ProductId",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "OrderItem",
				columns: table => new
				{
					OrderItemId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					OrderId = table.Column<int>(type: "int", nullable: false),
					ProductId = table.Column<int>(type: "int", nullable: false),
					Quantity = table.Column<int>(type: "int", nullable: false),
					UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_OrderItem", x => x.OrderItemId);
					table.ForeignKey(
						name: "FK_OrderItem_Order_OrderId",
						column: x => x.OrderId,
						principalTable: "Order",
						principalColumn: "OrderId",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_OrderItem_Product_ProductId",
						column: x => x.ProductId,
						principalTable: "Product",
						principalColumn: "ProductId",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateTable(
				name: "Review",
				columns: table => new
				{
					ReviewId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					ProductId = table.Column<int>(type: "int", nullable: false),
					UserId = table.Column<int>(type: "int", nullable: false),
					Rating = table.Column<int>(type: "int", nullable: false),
					Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Review", x => x.ReviewId);
					table.ForeignKey(
						name: "FK_Review_Product_ProductId",
						column: x => x.ProductId,
						principalTable: "Product",
						principalColumn: "ProductId",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Review_User_UserId",
						column: x => x.UserId,
						principalTable: "User",
						principalColumn: "UserId",
						onDelete: ReferentialAction.Restrict);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Cart_UserId",
				table: "Cart",
				column: "UserId",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_CartItem_CartId",
				table: "CartItem",
				column: "CartId");

			migrationBuilder.CreateIndex(
				name: "IX_CartItem_ProductId",
				table: "CartItem",
				column: "ProductId");

			migrationBuilder.CreateIndex(
				name: "IX_Order_UserId",
				table: "Order",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_OrderItem_OrderId",
				table: "OrderItem",
				column: "OrderId");

			migrationBuilder.CreateIndex(
				name: "IX_OrderItem_ProductId",
				table: "OrderItem",
				column: "ProductId");

			migrationBuilder.CreateIndex(
				name: "IX_Product_CategoryId",
				table: "Product",
				column: "CategoryId");

			migrationBuilder.CreateIndex(
				name: "IX_Product_SellerId",
				table: "Product",
				column: "SellerId");

			migrationBuilder.CreateIndex(
				name: "IX_Review_ProductId",
				table: "Review",
				column: "ProductId");

			migrationBuilder.CreateIndex(
				name: "IX_Review_UserId",
				table: "Review",
				column: "UserId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "CartItem");

			migrationBuilder.DropTable(
				name: "OrderItem");

			migrationBuilder.DropTable(
				name: "Review");

			migrationBuilder.DropTable(
				name: "Cart");

			migrationBuilder.DropTable(
				name: "Order");

			migrationBuilder.DropTable(
				name: "Product");

			migrationBuilder.DropTable(
				name: "Category");

			migrationBuilder.DropTable(
				name: "User");
		}
	}
}
