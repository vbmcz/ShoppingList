using ShoppingList.Models;
using ShoppingList.Views;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ShoppingList
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			BindingContext = new Models.AllProducts();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			productsCollection.ItemsSource = new AllProducts().Products;
		}

        private async void Add_Clicked(object sender, EventArgs e)
		{
			await Shell.Current.GoToAsync("addproduct");
		}

		private async void Add_Cat_Clicked(object sender, EventArgs e)
		{
			await Shell.Current.GoToAsync("addcategory");
		}

		private async void Export_Clicked(object sender, EventArgs e)
		{
			string fileContent = File.ReadAllText(Models.AllProducts.filePath);

			File.WriteAllText(@"C:\Users\Komputer PC\Desktop\ShoppingList\ExportFolder\exportedData.xml", fileContent);

			await DisplayAlert("Success", "Succesfully exported data to: 'ExportsFolder\\exportedData.xml'!", "Ok");

		}

		private async void Import_Clicked(object sender, EventArgs e)
		{
			var result = await FilePicker.PickAsync(new PickOptions()
			{
				PickerTitle = "Pick file to import from",
			});

			if (result == null)
				return;

			File.WriteAllText(Models.AllProducts.filePath ,File.ReadAllText(result.FullPath.ToString()).ToString());
			await DisplayAlert("Success", $"Successfully imported data from {result.FullPath.ToString()}!", "Ok");

			var products = new AllProducts();
			productsCollection.ItemsSource = products.Products;
		}
	}

}
