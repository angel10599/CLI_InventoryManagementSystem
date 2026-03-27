using System;

namespace InventoryManagementSystem
{
	class Category
	{
		public int Id { get; private set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public Category(int id, string name, string description)
		{
			Id = id;
			Name = name;
			Description = description;
		}

		public override string ToString()
		{
			return $"[{Id}] {Name} - {Description}";
		}
	}
}