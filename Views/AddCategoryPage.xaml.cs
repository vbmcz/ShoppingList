using System.Xml.Linq;
using ShoppingList.Models;
namespace ShoppingList.Views;

public partial class AddCategoryPage : ContentPage
{
	public AddCategoryPage()
	{
		InitializeComponent();
	}

	private async void Add_Clicked(object sender, EventArgs e)
	{
		var doc = XDocument.Load(Models.AllCategories.filePath);

		foreach (var node in doc.Descendants())
		{
			if (node.Name.ToString() == "CategoryName" && node.Value.ToLower() == CategoryNameEntry.Text.ToLower())
			{
				ErrorLabel.Text = "Category name already in use!";
				return;
			}
		}

		if (CategoryNameEntry.Text.Length < 0)
		{
			ErrorLabel.Text = "Invalid category name!";
			return;
		}

		int tempID = 0;

		foreach (var node in doc.Descendants())
		{
			if (node.Name.ToString() == "CategoryID")
				tempID = int.Parse(node.Value);
		}

		var category = new Category()
		{
			Id = ++tempID,
			Name = CategoryNameEntry.Text,
		};

		var cat = new XElement("Category", [
			new XElement("CategoryID", category.Id),
			new XElement("CategoryName", category.Name)
		]);

		doc.Element("Categories").Add(cat);
		doc.Save(Models.AllCategories.filePath);
		await Shell.Current.GoToAsync("..");
	}
}