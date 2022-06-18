﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevInSales.Migrations
{
    public partial class Authorization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Order_Product_OrderProductId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Order_Product_OrderProductId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_OrderProductId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Order_OrderProductId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderProductId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "OrderProductId",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Order_Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Order_Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "shipping_Company",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "delivery_Date",
                table: "Delivery",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "Complement",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Profile",
                columns: new[] { "id", "name" },
                values: new object[] { 2, "Usuário" });

            migrationBuilder.InsertData(
                table: "Profile",
                columns: new[] { "id", "name" },
                values: new object[] { 3, "Gerente" });

            migrationBuilder.InsertData(
                table: "Profile",
                columns: new[] { "id", "name" },
                values: new object[] { 4, "Administrador" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "id", "birth_date", "email", "name", "password", "ProfileId" },
                values: new object[] { 5, new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "joao@mail.com", "João Usuário", "joao@123", 2 });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "id", "birth_date", "email", "name", "password", "ProfileId" },
                values: new object[] { 6, new DateTime(2000, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "maria@mail.com", "Maria Gerente", "maria@123", 3 });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "id", "birth_date", "email", "name", "password", "ProfileId" },
                values: new object[] { 7, new DateTime(2000, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "jose@mail.com", "José Administrador", "jose@123", 4 });

            migrationBuilder.CreateIndex(
                name: "IX_Order_Product_OrderId",
                table: "Order_Product",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Product_ProductId",
                table: "Order_Product",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Product_Order_OrderId",
                table: "Order_Product",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Product_Product_ProductId",
                table: "Order_Product",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Product_Order_OrderId",
                table: "Order_Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Product_Product_ProductId",
                table: "Order_Product");

            migrationBuilder.DropIndex(
                name: "IX_Order_Product_OrderId",
                table: "Order_Product");

            migrationBuilder.DropIndex(
                name: "IX_Order_Product_ProductId",
                table: "Order_Product");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Profile",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Profile",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Profile",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Order_Product");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Order_Product");

            migrationBuilder.DropColumn(
                name: "shipping_Company",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "OrderProductId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderProductId",
                table: "Order",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "delivery_Date",
                table: "Delivery",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Complement",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_OrderProductId",
                table: "Product",
                column: "OrderProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderProductId",
                table: "Order",
                column: "OrderProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Order_Product_OrderProductId",
                table: "Order",
                column: "OrderProductId",
                principalTable: "Order_Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Order_Product_OrderProductId",
                table: "Product",
                column: "OrderProductId",
                principalTable: "Order_Product",
                principalColumn: "Id");
        }
    }
}
