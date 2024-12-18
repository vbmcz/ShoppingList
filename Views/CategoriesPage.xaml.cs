using ShoppingList.Models;

namespace ShoppingList.Views;

public partial class CategoriesPage : ContentPage
{
	readonly AllCategories allCategories = new();
	public CategoriesPage()
	{
		InitializeComponent();
		categories.ItemsSource = allCategories.Categories;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		categories.ItemsSource = allCategories.Categories;
	}
}