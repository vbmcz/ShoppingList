using ShoppingList.Models;
using System.Xml.Linq;

namespace ShoppingList.Views;

public partial class AddProductPage : ContentPage
{
	public AddProductPage()
	{
		InitializeComponent();

		ProductCategoryPicker.SetBinding(Picker.ItemsSourceProperty, "Categories");
		ProductCategoryPicker.ItemDisplayBinding = new Binding("Name");
    }


	private async void Add_Clicked(object sender, EventArgs e)
	{
		var doc = XDocument.Load(Models.AllProducts.filePath);

		foreach (var node in doc.Descendants())
		{
			if (node.Name.ToString() == "ProductName" && node.Value == ProductNameEntry.Text)
			{
				ErrorLabel.Text = "Product name already in use!";
				return;
			}
		}

		if (checkForNull(ProductNameEntry))
		{
			ErrorLabel.Text = "Set name of the product!";
			return;
		}

		int? temp = null;
		try
		{
			temp = int.Parse(ProductAmountEntry.Text);
		}
		catch (Exception)
		{
			ErrorLabel.Text = "Invalid product amount!";
			return;
		}

		if (checkForNullPicker(ProductUnitPicker))
		{
			ErrorLabel.Text = "Select unit of the product!";
			return;
		}

		if (checkForNullPicker(ProductCategoryPicker))
		{
			ErrorLabel.Text = "Select category of the product!";
			return;
		}

		int tempId = 0;

		foreach (var node in doc.Descendants())
		{
			if(node.Name.ToString() == "ProductID")
				tempId = int.Parse(node.Value);
		}

		var product = new Models.Product()
		{
			Id = ++tempId,
			Name = ProductNameEntry.Text,
			Amount = int.Parse(ProductAmountEntry.Text),
			Unit = ProductUnitPicker.SelectedItem.ToString(),
			Category = ProductCategoryPicker.Items[ProductCategoryPicker.SelectedIndex].ToString()
		};

		var prod = new XElement("Product", [
			new XElement("ProductID", product.Id),
			new XElement("ProductName", product.Name),
			new XElement("ProductAmount", product.Amount),
			new XElement("ProductUnit", product.Unit),
			new XElement("ProductBought", false),
			new XElement("ProductOptional", false),
			new XElement("ProductCategory", product.Category),
		]);

		doc.Element("Products").Add(prod);
		doc.Save(Models.AllProducts.filePath);
        await Shell.Current.GoToAsync("..");
	}

	private bool checkForNull(Entry entry)
	{
		if (entry.Text == null)
		{
			return true;
		}
		return false;
	}

	private bool checkForNullPicker(Picker picker)
	{
		return picker.SelectedItem == null;
	}
}