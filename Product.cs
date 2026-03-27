using System;

namespace InventoryManagementSystem
{
	class Product
	{
		public int Id { get; private set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
		public int LowStockThreshold { get; set; }
		public int CategoryId { get; set; }
		public int SupplierId { get; set; }

		public Product(int id, string name, string description, decimal price, int quantity, int lowStockThreshold, int categoryId, int supplierId)
		{
			Id = id;
			Name = name;
			Description = description;
			Price = price;
			Quantity = quantity;
			LowStockThreshold = lowStockThreshold;
			CategoryId = categoryId;
			SupplierId = supplierId;
		}

		public decimal TotalValue => Price * Quantity;

		public bool IsLowStock => Quantity <= LowStockThreshold;

		public override string ToString()
		{
			return $"[{Id}] {Name} | Price: {Price:C} | Qty: {Quantity} | Category ID: {CategoryId} | Supplier ID: {SupplierId}";
		}
	}
}