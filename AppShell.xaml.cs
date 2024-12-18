using ShoppingList.Views;

namespace ShoppingList
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();
			Routing.RegisterRoute("addproduct", typeof(AddProductPage));
			Routing.RegisterRoute("addcategory", typeof(AddCategoryPage));
			Routing.RegisterRoute("categoriespage", typeof(CategoriesPage));
		}
	}
}
