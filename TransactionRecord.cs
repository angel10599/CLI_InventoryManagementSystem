using System;

namespace InventoryManagementSystem
{
	class TransactionRecord
	{
		public int Id { get; private set; }
		public int ProductId { get; private set; }
		public string ProductName { get; private set; }
		public string TransactionType { get; private set; } // "ADD", "RESTOCK", "DEDUCT", "UPDATE", "DELETE"
		public int QuantityChanged { get; private set; }
		public int QuantityAfter { get; private set; }
		public string PerformedBy { get; private set; }
		public DateTime Timestamp { get; private set; }

		public TransactionRecord(int id, int productId, string productName, string transactionType,
			int quantityChanged, int quantityAfter, string performedBy)
		{
			Id = id;
			ProductId = productId;
			ProductName = productName;
			TransactionType = transactionType;
			QuantityChanged = quantityChanged;
			QuantityAfter = quantityAfter;
			PerformedBy = performedBy;
			Timestamp = DateTime.Now;
		}

		public override string ToString()
		{
			return $"[{Id}] {Timestamp:yyyy-MM-dd HH:mm:ss} | {TransactionType} | Product: {ProductName} (ID:{ProductId}) | Change: {QuantityChanged:+#;-#;0} | After: {QuantityAfter} | By: {PerformedBy}";
		}
	}
}