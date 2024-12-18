using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShoppingList.Models
{
	internal class AllCategories
	{
		// Dla lapka:
		public static string filePath = @"C:\Users\Alan Szymczyk\source\repos\ShoppingList\Resources\Raw\CategoriesStorage.xml";

		// Dla kompa:
		//public static string filePath = @"C:\Users\Komputer PC\Desktop\ShoppingList\Resources\Raw\CategoriesStorage.xml";
		public ObservableCollection<Category> Categories { get; set; } = [];

		public AllCategories() =>
			LoadCategories();

		public async void LoadCategories()
		{
			if (Categories.Count != 0)
				Categories.Clear();

			XDocument doc = XDocument.Load(filePath);
			Category category = new();

			foreach (var node in doc.Descendants())
			{
				if(node.Name.ToString() == "CategoryName")
				{
					category.Name = node.Value;
					Categories.Add(category);
					category = new();
				}
			}
		}
	}
}
