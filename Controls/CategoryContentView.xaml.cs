using Microsoft.Maui.Layouts;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ShoppingList.Controls;

public partial class CategoryContentView : ContentView
{
	List<Models.Product> products = [];
	public bool isVisible = false;
	public static readonly BindableProperty CategoryNameProperty = BindableProperty.Create(nameof(CategoryName), typeof(string), typeof(CategoryContentView), string.Empty,defaultBindingMode:BindingMode.OneWay, propertyChanged:OnPropertyChangeName);

	public string CategoryName
	{
		get => (string)GetValue(CategoryNameProperty);
		set => SetValue(CategoryNameProperty, value);
	}

	private static void OnPropertyChangeName(BindableObject bindable, object oldValue, object newValue)
	{
		var control = (CategoryContentView)bindable;
		control.CategoryName = (string)newValue;
	}

	public CategoryContentView()
	{
		InitializeComponent();
	}

	private void Toggle_Products(object sender, EventArgs e)
	{
		isVisible = !isVisible;
		categoriesCollection.IsVisible = isVisible;
		

		Models.AllProducts allProducts = new();

		foreach (var product in allProducts.Products)
		{
			if(product.Category.ToLower().ToString().Equals(CategoryName.ToLower().ToString()))
			{
				products.Add(product);
			}
		}

		if(products.Count > 0) 
			categoriesCollection.ItemsSource = products;
		else
		{
			categoriesCollection.ItemsSource = null;
			categoriesCollection.EmptyView = "No products linked to this category!";
		}
	}
}