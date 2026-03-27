using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace InventoryManagementSystem
{
	// ===================== MODELS =====================

	
	
	



	// ===================== MAIN PROGRAM =====================

	class Program
	{
		static List<Category> categories = new List<Category>();
		static List<Supplier> suppliers = new List<Supplier>();
		static List<Product> products = new List<Product>();
		static List<TransactionRecord> transactions = new List<TransactionRecord>();
		static List<User> users = new List<User>();

		static int categoryIdCounter = 1;
		static int supplierIdCounter = 1;
		static int productIdCounter = 1;
		static int transactionIdCounter = 1;
		static int userIdCounter = 1;

		static User currentUser = null;

		static void Main(string[] args)
		{
			Console.OutputEncoding = System.Text.Encoding.UTF8;

			CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-PH");
			CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-PH");

			SeedData();

			Console.Clear();
			PrintHeader("CLI-BASED INVENTORY MANAGEMENT SYSTEM");

			if (!Login())
			{
				Console.WriteLine("Too many failed attempts. Exiting...");
				return;
			}

			MainMenu();
		}

		// ===================== SEED DATA =====================

		static void SeedData()
		{
			users.Add(new User(userIdCounter++, "admin", "admin123", "Admin"));
			users.Add(new User(userIdCounter++, "staff", "staff123", "Staff"));

			categories.Add(new Category(categoryIdCounter++, "Electronic", "Electronic devices and accessories"));
			categories.Add(new Category(categoryIdCounter++, "Office Supplies", "Office and stationery items"));

			suppliers.Add(new Supplier(supplierIdCounter++, "TechWorld Inc.", "09171234567", "Makati City"));
			suppliers.Add(new Supplier(supplierIdCounter++, "OfficeDepot PH", "09281234567", "Quezon City"));

			products.Add(new Product(productIdCounter++, "USB Flash Drive 32GB", "High-speed USB 3.0 flash drive", 299.00m, 50, 10, 1, 1));
			products.Add(new Product(productIdCounter++, "Ballpen Box (12pcs)", "Black ink ballpen set", 85.00m, 100, 20, 2, 2));
			products.Add(new Product(productIdCounter++, "HDMI Cable 1.5m", "Full HD HDMI cable", 199.00m, 8, 10, 1, 1));
		}

		// ===================== AUTHENTICATION =====================

		static bool Login()
		{
			int attempts = 0;
			while (attempts < 3)
			{
				Console.WriteLine("\n--- LOGIN ---");
				Console.Write("Username: ");
				string username = Console.ReadLine()?.Trim();
				Console.Write("Password: ");
				string password = ReadPassword();

				User found = users.FirstOrDefault(u => u.Username == username);
				if (found != null && found.CheckPassword(password))
				{
					currentUser = found;
					Console.WriteLine($"\nWelcome, {currentUser.Username}! ({currentUser.Role})");
					return true;
				}

				attempts++;
				Console.WriteLine($"Invalid credentials. {3 - attempts} attempt(s) remaining.");
			}
			return false;
		}

		static string ReadPassword()
		{
			string pass = "";
			ConsoleKeyInfo key;
			do
			{
				key = Console.ReadKey(true);
				if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
				{
					pass += key.KeyChar;
					Console.Write("*");
				}
				else if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
				{
					pass = pass.Substring(0, pass.Length - 1);
					Console.Write("\b \b");
				}
			} while (key.Key != ConsoleKey.Enter);
			Console.WriteLine();
			return pass;
		}

		// ===================== MENUS =====================

		static void MainMenu()
		{
			while (true)
			{
				Console.Clear();
				PrintHeader("MAIN MENU");
				Console.WriteLine("  [1] Category Management");
				Console.WriteLine("  [2] Supplier Management");
				Console.WriteLine("  [3] Product Management");
				Console.WriteLine("  [4] Inventory Operations");
				Console.WriteLine("  [5] Reports");
				Console.WriteLine("  [0] Logout & Exit");
				Console.Write("\nEnter choice: ");

				string choice = Console.ReadLine();
				switch (choice)
				{
					case "1": CategoryMenu(); break;
					case "2": SupplierMenu(); break;
					case "3": ProductMenu(); break;
					case "4": InventoryMenu(); break;
					case "5": ReportsMenu(); break;
					case "0":
						Console.WriteLine("\nGoodbye!");
						return;
					default:
						PrintError("Invalid choice. Press any key to continue...");
						Console.ReadKey();
						break;
				}
			}
		}

		static void CategoryMenu()
		{
			while (true)
			{
				Console.Clear();
				PrintHeader("CATEGORY MANAGEMENT");
				Console.WriteLine("  [1] Add Category");
				Console.WriteLine("  [2] View All Categories");
				Console.WriteLine("  [0] Back");
				Console.Write("\nEnter choice: ");
				string choice = Console.ReadLine();
				switch (choice)
				{
					case "1": AddCategory(); break;
					case "2": ViewCategories(); break;
					case "0": return;
					default:
						PrintError("Invalid choice.");
						Pause();
						break;
				}
			}
		}

		static void SupplierMenu()
		{
			while (true)
			{
				Console.Clear();
				PrintHeader("SUPPLIER MANAGEMENT");
				Console.WriteLine("  [1] Add Supplier");
				Console.WriteLine("  [2] View All Suppliers");
				Console.WriteLine("  [0] Back");
				Console.Write("\nEnter choice: ");
				string choice = Console.ReadLine();
				switch (choice)
				{
					case "1": AddSupplier(); break;
					case "2": ViewSuppliers(); break;
					case "0": return;
					default:
						PrintError("Invalid choice.");
						Pause();
						break;
				}
			}
		}

		static void ProductMenu()
		{
			while (true)
			{
				Console.Clear();
				PrintHeader("PRODUCT MANAGEMENT");
				Console.WriteLine("  [1] Add Product");
				Console.WriteLine("  [2] View All Products");
				Console.WriteLine("  [3] Search Product");
				Console.WriteLine("  [4] Update Product");
				Console.WriteLine("  [5] Delete Product");
				Console.WriteLine("  [0] Back");
				Console.Write("\nEnter choice: ");
				string choice = Console.ReadLine();
				switch (choice)
				{
					case "1": AddProduct(); break;
					case "2": ViewAllProducts(); break;
					case "3": SearchProduct(); break;
					case "4": UpdateProduct(); break;
					case "5": DeleteProduct(); break;
					case "0": return;
					default:
						PrintError("Invalid choice.");
						Pause();
						break;
				}
			}
		}

		static void InventoryMenu()
		{
			while (true)
			{
				Console.Clear();
				PrintHeader("INVENTORY OPERATIONS");
				Console.WriteLine("  [1] Restock Product");
				Console.WriteLine("  [2] Deduct Stock");
				Console.WriteLine("  [0] Back");
				Console.Write("\nEnter choice: ");
				string choice = Console.ReadLine();
				switch (choice)
				{
					case "1": RestockProduct(); break;
					case "2": DeductStock(); break;
					case "0": return;
					default:
						PrintError("Invalid choice.");
						Pause();
						break;
				}
			}
		}

		static void ReportsMenu()
		{
			while (true)
			{
				Console.Clear();
				PrintHeader("REPORTS");
				Console.WriteLine("  [1] View Transaction History");
				Console.WriteLine("  [2] Show Low-Stock Items");
				Console.WriteLine("  [3] Total Inventory Value");
				Console.WriteLine("  [0] Back");
				Console.Write("\nEnter choice: ");
				string choice = Console.ReadLine();
				switch (choice)
				{
					case "1": ViewTransactionHistory(); break;
					case "2": ShowLowStockItems(); break;
					case "3": ShowTotalInventoryValue(); break;
					case "0": return;
					default:
						PrintError("Invalid choice.");
						Pause();
						break;
				}
			}
		}

		// ===================== CATEGORY OPERATIONS =====================

		static void AddCategory()
		{
			Console.Clear();
			PrintHeader("ADD CATEGORY");
			try
			{
				Console.Write("Category Name: ");
				string name = Console.ReadLine()?.Trim();
				if (string.IsNullOrWhiteSpace(name)) throw new Exception("Category name cannot be empty.");

				if (categories.Any(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
					throw new Exception("A category with that name already exists.");

				Console.Write("Description: ");
				string desc = Console.ReadLine()?.Trim();
				if (string.IsNullOrWhiteSpace(desc)) throw new Exception("Description cannot be empty.");

				categories.Add(new Category(categoryIdCounter++, name, desc));
				PrintSuccess($"Category '{name}' added successfully!");
			}
			catch (Exception ex)
			{
				PrintError($"Error: {ex.Message}");
			}
			Pause();
		}

		static void ViewCategories()
		{
			Console.Clear();
			PrintHeader("ALL CATEGORIES");
			if (categories.Count == 0)
			{
				Console.WriteLine("No categories found.");
			}
			else
			{
				foreach (var c in categories)
					Console.WriteLine("  " + c);
			}
			Pause();
		}

		// ===================== SUPPLIER OPERATIONS =====================

		static void AddSupplier()
		{
			Console.Clear();
			PrintHeader("ADD SUPPLIER");
			try
			{
				Console.Write("Supplier Name: ");
				string name = Console.ReadLine()?.Trim();
				if (string.IsNullOrWhiteSpace(name)) throw new Exception("Supplier name cannot be empty.");

				Console.Write("Contact Info (11 digits): ");
				string contact = Console.ReadLine()?.Trim();

				if (string.IsNullOrWhiteSpace(contact))
					throw new Exception("Contact info cannot be empty.");
				if (!contact.StartsWith("09") || contact.Length != 11 || !contact.All(char.IsDigit))
					throw new Exception("Contact number must start with '09' and be 11 digits.");



				Console.Write("Address: ");
				string address = Console.ReadLine()?.Trim();
				if (string.IsNullOrWhiteSpace(address)) throw new Exception("Address cannot be empty.");

				suppliers.Add(new Supplier(supplierIdCounter++, name, contact, address));
				PrintSuccess($"Supplier '{name}' added successfully!");
			}
			catch (Exception ex)
			{
				PrintError($"Error: {ex.Message}");
			}
			Pause();
		}

		static void ViewSuppliers()
		{
			Console.Clear();
			PrintHeader("ALL SUPPLIERS");
			if (suppliers.Count == 0)
			{
				Console.WriteLine("No suppliers found.");
			}
			else
			{
				foreach (var s in suppliers)
					Console.WriteLine("  " + s);
			}
			Pause();
		}

		// ===================== PRODUCT OPERATIONS =====================

		static void AddProduct()
		{
			Console.Clear();
			PrintHeader("ADD PRODUCT");
			try
			{
				if (categories.Count == 0) throw new Exception("Please add a category first.");
				if (suppliers.Count == 0) throw new Exception("Please add a supplier first.");

				Console.Write("Product Name: ");
				string name = Console.ReadLine()?.Trim();
				if (string.IsNullOrWhiteSpace(name)) throw new Exception("Product name cannot be empty.");

				Console.Write("Description: ");
				string desc = Console.ReadLine()?.Trim();

				Console.Write("Price: ");
				if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price < 0)
					throw new Exception("Invalid price.");

				Console.Write("Quantity: ");
				if (!int.TryParse(Console.ReadLine(), out int qty) || qty < 0)
					throw new Exception("Invalid quantity.");

				Console.Write("Low Stock Threshold: ");
				if (!int.TryParse(Console.ReadLine(), out int threshold) || threshold < 0)
					throw new Exception("Invalid threshold.");

				Console.WriteLine("\nAvailable Categories:");
				foreach (var c in categories) Console.WriteLine("  " + c);
				Console.Write("Category ID: ");
				if (!int.TryParse(Console.ReadLine(), out int catId) || categories.All(c => c.Id != catId))
					throw new Exception("Invalid Category ID.");

				Console.WriteLine("\nAvailable Suppliers:");
				foreach (var s in suppliers) Console.WriteLine("  " + s);
				Console.Write("Supplier ID: ");
				if (!int.TryParse(Console.ReadLine(), out int supId) || suppliers.All(s => s.Id != supId))
					throw new Exception("Invalid Supplier ID.");

				var product = new Product(productIdCounter++, name, desc, price, qty, threshold, catId, supId);
				products.Add(product);
				LogTransaction(product.Id, product.Name, "ADD", qty, qty);
				PrintSuccess($"Product '{name}' added successfully! (ID: {product.Id})");
			}
			catch (Exception ex)
			{
				PrintError($"Error: {ex.Message}");
			}
			Pause();
		}

		static void ViewAllProducts()
		{
			Console.Clear();
			PrintHeader("ALL PRODUCTS");
			if (products.Count == 0)
			{
				Console.WriteLine("No products found.");
			}
			else
			{
				PrintProductTable(products);
			}
			Pause();
		}

		static void SearchProduct()
		{
			Console.Clear();
			PrintHeader("SEARCH PRODUCT");
			try
			{
				Console.Write("Enter name or ID to search: ");
				string query = Console.ReadLine()?.Trim().ToLower();
				if (string.IsNullOrWhiteSpace(query))
					throw new Exception("Search query cannot be empty.");

				List<Product> results;

				if (int.TryParse(query, out int searchId))
				{
					// Exact search by ID only
					results = products.Where(p => p.Id == searchId).ToList();
				}
				else
				{
					// Search by name only
					results = products.Where(p => p.Name.ToLower().Contains(query)).ToList();
				}

				if (results.Count == 0)
				{
					Console.WriteLine("No products found matching your search.");
				}
				else
				{
					Console.WriteLine($"\n{results.Count} result(s) found:\n");
					PrintProductTable(results);
				}
			}
			catch (Exception ex)
			{
				PrintError($"Error: {ex.Message}");
			}
			Pause();
		}

		static void UpdateProduct()
		{
			Console.Clear();
			PrintHeader("UPDATE PRODUCT");
			try
			{
				ViewAllProducts();
				Console.Write("Enter Product ID to update: ");
				if (!int.TryParse(Console.ReadLine(), out int id))
					throw new Exception("Invalid ID.");

				var product = products.FirstOrDefault(p => p.Id == id);
				if (product == null) throw new Exception("Product not found.");

				Console.WriteLine($"\nEditing: {product.Name}");
				Console.WriteLine("(Press Enter to keep current value)\n");

				Console.Write($"Name [{product.Name}]: ");
				string name = Console.ReadLine()?.Trim();
				if (!string.IsNullOrWhiteSpace(name)) product.Name = name;

				Console.Write($"Description [{product.Description}]: ");
				string desc = Console.ReadLine()?.Trim();
				if (!string.IsNullOrWhiteSpace(desc)) product.Description = desc;

				Console.Write($"Price [{product.Price:C}]: ");
				string priceInput = Console.ReadLine()?.Trim();
				if (!string.IsNullOrWhiteSpace(priceInput))
				{
					if (!decimal.TryParse(priceInput, out decimal price) || price < 0)
						throw new Exception("Invalid price.");
					product.Price = price;
				}

				Console.Write($"Low Stock Threshold [{product.LowStockThreshold}]: ");
				string threshInput = Console.ReadLine()?.Trim();
				if (!string.IsNullOrWhiteSpace(threshInput))
				{
					if (!int.TryParse(threshInput, out int thresh) || thresh < 0)
						throw new Exception("Invalid threshold.");
					product.LowStockThreshold = thresh;
				}

				LogTransaction(product.Id, product.Name, "UPDATE", 0, product.Quantity);
				PrintSuccess($"Product ID {id} updated successfully!");
			}
			catch (Exception ex)
			{
				PrintError($"Error: {ex.Message}");
			}
			Pause();
		}

		static void DeleteProduct()
		{
			Console.Clear();
			PrintHeader("DELETE PRODUCT");
			try
			{
				ViewAllProducts();
				Console.Write("Enter Product ID to delete: ");
				if (!int.TryParse(Console.ReadLine(), out int id))
					throw new Exception("Invalid ID.");

				var product = products.FirstOrDefault(p => p.Id == id);
				if (product == null) throw new Exception("Product not found.");

				Console.Write($"Are you sure you want to delete '{product.Name}'? (yes/no): ");
				string confirm = Console.ReadLine()?.Trim().ToLower();
				if (confirm != "yes")
				{
					Console.WriteLine("Deletion cancelled.");
					Pause();
					return;
				}

				LogTransaction(product.Id, product.Name, "DELETE", -product.Quantity, 0);
				products.Remove(product);
				PrintSuccess($"Product '{product.Name}' deleted successfully!");
			}
			catch (Exception ex)
			{
				PrintError($"Error: {ex.Message}");
			}
			Pause();
		}

		// ===================== INVENTORY OPERATIONS =====================

		static void RestockProduct()
		{
			Console.Clear();
			PrintHeader("RESTOCK PRODUCT");
			try
			{
				ViewAllProducts();
				Console.Write("Enter Product ID to restock: ");
				if (!int.TryParse(Console.ReadLine(), out int id))
					throw new Exception("Invalid ID.");

				var product = products.FirstOrDefault(p => p.Id == id);
				if (product == null) throw new Exception("Product not found.");

				Console.Write($"Current Quantity: {product.Quantity}\nQuantity to Add: ");
				if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
					throw new Exception("Quantity must be a positive number.");

				product.Quantity += qty;
				LogTransaction(product.Id, product.Name, "RESTOCK", qty, product.Quantity);
				PrintSuccess($"Restocked '{product.Name}'. New quantity: {product.Quantity}");
			}
			catch (Exception ex)
			{
				PrintError($"Error: {ex.Message}");
			}
			Pause();
		}

		static void DeductStock()
		{
			Console.Clear();
			PrintHeader("DEDUCT STOCK");
			try
			{
				ViewAllProducts();
				Console.Write("Enter Product ID to deduct from: ");
				if (!int.TryParse(Console.ReadLine(), out int id))
					throw new Exception("Invalid ID.");

				var product = products.FirstOrDefault(p => p.Id == id);
				if (product == null) throw new Exception("Product not found.");

				Console.Write($"Current Quantity: {product.Quantity}\nQuantity to Deduct: ");
				if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
					throw new Exception("Quantity must be a positive number.");

				if (qty > product.Quantity)
					throw new Exception($"Insufficient stock. Available: {product.Quantity}");

				product.Quantity -= qty;
				LogTransaction(product.Id, product.Name, "DEDUCT", -qty, product.Quantity);
				PrintSuccess($"Deducted {qty} from '{product.Name}'. New quantity: {product.Quantity}");

				if (product.IsLowStock)
					PrintError($"⚠ WARNING: '{product.Name}' is now LOW STOCK ({product.Quantity} remaining)!");
			}
			catch (Exception ex)
			{
				PrintError($"Error: {ex.Message}");
			}
			Pause();
		}

		// ===================== REPORTS =====================

		static void ViewTransactionHistory()
		{
			Console.Clear();
			PrintHeader("TRANSACTION HISTORY");
			if (transactions.Count == 0)
			{
				Console.WriteLine("No transactions recorded.");
			}
			else
			{
				foreach (var t in transactions)
					Console.WriteLine("  " + t);
			}
			Pause();
		}

		static void ShowLowStockItems()
		{
			Console.Clear();
			PrintHeader("LOW STOCK ITEMS");
			var lowStock = products.Where(p => p.IsLowStock).ToList();
			if (lowStock.Count == 0)
			{
				PrintSuccess("All products are sufficiently stocked.");
			}
			else
			{
				Console.WriteLine($"  {lowStock.Count} low-stock item(s) found:\n");
				PrintProductTable(lowStock);
			}
			Pause();
		}

		static void ShowTotalInventoryValue()
		{
			Console.Clear();
			PrintHeader("TOTAL INVENTORY VALUE");
			if (products.Count == 0)
			{
				Console.WriteLine("No products in inventory.");
			}
			else
			{
				decimal total = 0;
				Console.WriteLine($"  {"ID",-5} {"Name",-30} {"Price",10} {"Qty",8} {"Total Value",14}");
				Console.WriteLine(new string('-', 72));
				foreach (var p in products)
				{
					Console.WriteLine($"  {p.Id,-5} {p.Name,-30} {p.Price,10:C} {p.Quantity,8} {p.TotalValue,14:C}");
					total += p.TotalValue;
				}
				Console.WriteLine(new string('-', 72));
				Console.WriteLine($"  {"GRAND TOTAL",-47} {total,14:C}");
			}
			Pause();
		}

		// ===================== HELPERS =====================

		static void LogTransaction(int productId, string productName, string type, int change, int after)
		{
			transactions.Add(new TransactionRecord(
				transactionIdCounter++, productId, productName, type, change, after, currentUser.Username));
		}

		static void PrintProductTable(List<Product> list)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine($"  {"ID",-5} {"Name",-25} {"Price",10} {"Qty",6} {"Stock",10} {"Category",-15} {"Supplier",-20}");
			Console.WriteLine(new string('-', 100));
			Console.ResetColor();

			foreach (var p in list)
			{
				string stockStatus = p.IsLowStock ? "LOW" : "OK";

				if (p.IsLowStock)
					Console.ForegroundColor = ConsoleColor.Red;
				else
					Console.ForegroundColor = ConsoleColor.Green;

				Console.WriteLine($"  {p.Id,-5} {p.Name,-25} {p.Price,10:C} {p.Quantity,6} {stockStatus,10} " +
					$"{categories.FirstOrDefault(c => c.Id == p.CategoryId)?.Name,-15} " +
					$"{suppliers.FirstOrDefault(s => s.Id == p.SupplierId)?.Name,-20}");

				Console.ResetColor();
			}
		}

		static void PrintHeader(string title)
		{
			Console.Clear();

			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine(new string('=', 70));

			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine($"   {title.ToUpper()}");

			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine(new string('=', 70));

			Console.ResetColor();
			Console.WriteLine();
		}

		static void PrintSuccess(string msg)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"\n✔ {msg}");
			Console.ResetColor();
		}

		static void PrintError(string msg)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine($"\n✘ {msg}");
			Console.ResetColor();
		}

		static void Pause()
		{
			Console.WriteLine("\nPress any key to continue...");
			Console.ReadKey();
		}
	}
}