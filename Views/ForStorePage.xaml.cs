using ShoppingList.Models;

namespace ShoppingList.Views;

public partial class ForStorePage : ContentPage
{
	private List<Models.Product> StoreProducts = [];
	public ForStorePage()
	{
		InitializeComponent();
		GetProducts();
	}

	private void Name_Clicked(object sender, EventArgs e)
	{
		productsCollection.ItemsSource = null;
		StoreProducts.Sort((x, y) => x.Name.CompareTo(y.Name));
		productsCollection.ItemsSource = StoreProducts;
    }

	private void Amount_Clicked(object sender, EventArgs e)
	{
		productsCollection.ItemsSource = null;
		StoreProducts = [.. StoreProducts.OrderBy(x => x.Amount)];
		productsCollection.ItemsSource = StoreProducts;
	}

	private void GetProducts()
	{
		StoreProducts.Clear();
		var allProducts = new AllProducts();
		allProducts.LoadProducts();

		foreach(var product in allProducts.Products)
		{
			if (!product.IsBought)
				StoreProducts.Add(product);
		}

		StoreProducts.Sort((x, y) => x.Category.CompareTo(y.Category));
		productsCollection.ItemsSource = null;
		productsCollection.ItemsSource = StoreProducts;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		GetProducts();
	}
}