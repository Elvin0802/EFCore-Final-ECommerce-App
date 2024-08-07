﻿namespace ECommerceApp.Models.EFCore;

public class Product
{
	public int ProductId { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public decimal Price { get; set; }
	public int Quantity { get; set; }
	public int CategoryId { get; set; }
	public Category Category { get; set; }
}