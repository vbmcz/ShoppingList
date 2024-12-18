using ShoppingList.Models;
using System.Xml.Linq;

namespace ShoppingList.Controls;

public partial class StoreReadyProductContentView : ContentView
{
	public static readonly BindableProperty ProductNameProperty = BindableProperty.Create(nameof(ProductName), typeof(string), typeof(StoreReadyProductContentView), string.Empty, defaultBindingMode: BindingMode.OneWay, propertyChanged: OnPropertyChangeName);
	public static readonly BindableProperty ProductUnitProperty = BindableProperty.Create(nameof(ProductUnit), typeof(string), typeof(StoreReadyProductContentView), string.Empty, propertyChanged: OnPropertyChangeUnit);
	public static readonly BindableProperty ProductAmountProperty = BindableProperty.Create(nameof(ProductAmount), typeof(int), typeof(StoreReadyProductContentView), 0, propertyChanged: OnPropertyChangeAmount);
	public static readonly BindableProperty ProductCategoryProperty = BindableProperty.Create(nameof(ProductCategory), typeof(string), typeof(StoreReadyProductContentView), string.Empty, propertyChanged: OnPropertyChangeCategory);
	public static readonly BindableProperty ProductIDProperty = BindableProperty.Create(nameof(ProductID), typeof(int), typeof(StoreReadyProductContentView), 0, propertyChanged: OnPropertyChangeID);
	public static readonly BindableProperty ProductIsBoughtProperty = BindableProperty.Create(nameof(ProductIsBought), typeof(bool), typeof(StoreReadyProductContentView), false, propertyChanged: OnPropertyChangeIsBought);
	public static readonly BindableProperty ProductIsOptionalProperty = BindableProperty.Create(nameof(ProductIsOptional), typeof(bool), typeof(StoreReadyProductContentView), false, propertyChanged: OnPropertyChangeIsOptional);

	public int ProductID
	{
		get => (int)GetValue(ProductIDProperty);
		set => SetValue(ProductIDProperty, value);
	}

	public string ProductName
	{
		get => (string)GetValue(ProductNameProperty);
		set => SetValue(ProductNameProperty, value);
	}

	public string ProductUnit
	{
		get => (string)GetValue(ProductUnitProperty);
		set => SetValue(ProductUnitProperty, value);
	}

	public int ProductAmount
	{
		get => (int)GetValue(ProductAmountProperty);
		set => SetValue(ProductAmountProperty, value);
	}

	public string ProductCategory
	{
		get => (string)GetValue(ProductCategoryProperty);
		set => SetValue(ProductCategoryProperty, value);
	}

	public bool ProductIsBought
	{
		get => (bool)GetValue(ProductIsBoughtProperty);
		set => SetValue(ProductIsBoughtProperty, value);
	}

	public bool ProductIsOptional
	{
		get => (bool)GetValue(ProductIsOptionalProperty);
		set => SetValue(ProductIsOptionalProperty, value);
	}

	private static void OnPropertyChangeID(BindableObject bindable, object oldValue, object newValue)
	{
		var control = (StoreReadyProductContentView)bindable;
		control.ProductID = (int)newValue;
	}
	private static void OnPropertyChangeName(BindableObject bindable, object oldValue, object newValue)
	{
		var control = (StoreReadyProductContentView)bindable;
		control.ProductName = (string)newValue;
	}
	private static void OnPropertyChangeUnit(BindableObject bindable, object oldValue, object newValue)
	{
		var control = (StoreReadyProductContentView)bindable;
		control.ProductUnit = (string)newValue;
	}
	private static void OnPropertyChangeAmount(BindableObject bindable, object oldValue, object newValue)
	{
		var control = (StoreReadyProductContentView)bindable;
		control.ProductAmount = (int)newValue;
	}
	private static void OnPropertyChangeCategory(BindableObject bindable, object oldValue, object newValue)
	{
		var control = (StoreReadyProductContentView)bindable;
		control.ProductCategory = (string)newValue;
	}
	private static void OnPropertyChangeIsBought(BindableObject bindable, object oldValue, object newValue)
	{
		var control = (StoreReadyProductContentView)bindable;
		control.ProductIsBought = (bool)newValue;
	}
	private static void OnPropertyChangeIsOptional(BindableObject bindable, object oldValue, object newValue)
	{
		var control = (StoreReadyProductContentView)bindable;
		control.ProductIsOptional = (bool)newValue;
	}

	public StoreReadyProductContentView()
	{
		InitializeComponent();
	}
	private void Bought_Changed(object sender, CheckedChangedEventArgs e)
	{
		var doc = XDocument.Load(Models.AllProducts.filePath);
		bool found = false;
		foreach (var node in doc.Descendants())
		{
			if (node.Name.ToString().Equals("ProductID") && node.Value.ToString().Equals(ProductID.ToString()))
			{
				found = true;
				continue;
			}
			if (found && node.Name.ToString().Equals("ProductBought"))
			{
				node.Value = ProductIsBought.ToString();
				doc.Save(Models.AllProducts.filePath);
				break;
			}
		}
		var node_ = doc.Descendants("Product").FirstOrDefault(p => p.Element("ProductID")?.Value == ProductID.ToString());

		node_.Remove();
		doc.Root.Add(node_);
		doc.Save(Models.AllProducts.filePath);

		var _ = new AllProducts();
		this.IsVisible = false;
	}

	private void Optional_Changed(object sender, CheckedChangedEventArgs e)
	{
		var doc = XDocument.Load(Models.AllProducts.filePath);
		bool found = false;
		foreach (var node in doc.Descendants())
		{
			if (node.Name.ToString().Equals("ProductID") && node.Value.ToString().Equals(ProductID.ToString()))
			{
				found = true;
				continue;
			}
			if (found && node.Name.ToString().Equals("ProductOptional"))
			{
				node.Value = ProductIsOptional.ToString();
				doc.Save(Models.AllProducts.filePath);
				break;
			}
		}
		var _ = new AllProducts();
	}
}