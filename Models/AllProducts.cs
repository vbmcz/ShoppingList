using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace ShoppingList.Models
{
	internal class AllProducts
	{
		// Dla lapka:
		//public static string filePath = @"C:\Users\Alan Szymczyk\Desktop\ShoppingList\Resources\Raw\ProductsStorage.xml";

		// Dla kompa:
		public static string filePath = @"C:\Users\Komputer PC\Desktop\ShoppingList\Resources\Raw\ProductsStorage.xml";
		public bool isVisible = false;
		public ObservableCollection<Product> Products { get; set; } = [];

		public AllProducts() =>
			LoadProducts();


		public async void LoadProducts()
		{
			if (Products.Count != 0)
				Products.Clear();

			XDocument doc = XDocument.Load(filePath);
			Product product = new();

			foreach (var node in doc.Descendants())
			{
				switch (node.Name.ToString())
				{
					case "ProductID":
						product.Id = int.Parse(node.Value); 
						break;
					case "ProductName":
						product.Name = node.Value;
						break;
					case "ProductAmount":
						product.Amount = int.Parse(node.Value);
						break;
					case "ProductUnit":
						product.Unit = node.Value;
						break;
					case "ProductBought":
						product.IsBought = node.Value.ToString() == "True";
						break;
					case "ProductOptional":
						product.IsOptional = node.Value.ToString() == "True";
						break;
					case "ProductCategory":
						product.Category = node.Value;
						break;
					default:
						break;
				}
				if (!string.IsNullOrEmpty(product.Category))
				{
					Products.Add(product);
					isVisible = true;
					product = new();
				}
			}

		}

		public static implicit operator List<object>(AllProducts v)
		{
			throw new NotImplementedException();
		}
	}
}
