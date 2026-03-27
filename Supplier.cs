using System;

namespace InventoryManagementSystem
{
	class Supplier
	{
		public int Id { get; private set; }
		public string Name { get; set; }
		public string ContactInfo { get; set; }
		public string Address { get; set; }

		public Supplier(int id, string name, string contactInfo, string address)
		{
			Id = id;
			Name = name;
			ContactInfo = contactInfo;
			Address = address;
		}

		public override string ToString()
		{
			return $"[{Id}] {Name} | Contact: {ContactInfo} | Address: {Address}";
		}
	}
}