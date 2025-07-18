﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HandmadeITI.Data.Migrations
{
	/// <inheritdoc />
	public partial class ExtendUsersTable : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "Role",
				table: "User");

			migrationBuilder.AddColumn<DateTime>(
				name: "CreatedOn",
				table: "AspNetUsers",
				type: "datetime2",
				nullable: false,
				defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

			migrationBuilder.AddColumn<string>(
				name: "FullName",
				table: "AspNetUsers",
				type: "nvarchar(100)",
				maxLength: 100,
				nullable: false,
				defaultValue: "");

			migrationBuilder.AddColumn<bool>(
				name: "IsDeleted",
				table: "AspNetUsers",
				type: "bit",
				nullable: false,
				defaultValue: false);

			migrationBuilder.AddColumn<DateTime>(
				name: "LastUpdatedOn",
				table: "AspNetUsers",
				type: "datetime2",
				nullable: false,
				defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "CreatedOn",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "FullName",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "IsDeleted",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "LastUpdatedOn",
				table: "AspNetUsers");

			migrationBuilder.AddColumn<int>(
				name: "Role",
				table: "User",
				type: "int",
				nullable: false,
				defaultValue: 0);
		}
	}
}
