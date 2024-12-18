using ShoppingList.Models;
using System.Xml;
using System.Xml.Linq;

namespace ShoppingList.Controls;

public partial class ProductContentView : ContentView
{
	public static readonly BindableProperty ProductNameProperty = BindableProperty.Create(nameof(ProductName), typeof(string), typeof(ProductContentView), string.Empty,defaultBindingMode:BindingMode.OneWay, propertyChanged:OnPropertyChangeName);
	public static readonly BindableProperty ProductUnitProperty = BindableProperty.Create(nameof(ProductUnit), typeof(string), typeof(ProductContentView), string.Empty, propertyChanged:OnPropertyChangeUnit);
	public static readonly BindableProperty ProductAmountProperty = BindableProperty.Create(nameof(ProductAmount), typeof(int), typeof(ProductContentView), 0, propertyChanged:OnPropertyChangeAmount);
	public static readonly BindableProperty ProductCategoryProperty = BindableProperty.Create(nameof(ProductCategory), typeof(string), typeof(ProductContentView), string.Empty, propertyChanged:OnPropertyChangeCategory);
	public static readonly BindableProperty ProductIDProperty = BindableProperty.Create(nameof(ProductID), typeof(int), typeof(ProductContentView), 0, propertyChanged:OnPropertyChangeID);
	public static readonly BindableProperty ProductIsBoughtProperty = BindableProperty.Create(nameof(ProductIsBought), typeof(bool), typeof(ProductContentView), false, propertyChanged:OnPropertyChangeIsBought);
	public static readonly BindableProperty ProductIsOptionalProperty = BindableProperty.Create(nameof(ProductIsOptional), typeof(bool), typeof(ProductContentView), false, propertyChanged:OnPropertyChangeIsOptional);
	private bool isBought, isOptional;

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
		var control = (ProductContentView)bindable;
		control.ProductID = (int)newValue;
	}
	private static void OnPropertyChangeName(BindableObject bindable, object oldValue, object newValue)
	{
		var control = (ProductContentView)bindable;
		control.ProductName = (string)newValue;
	}
	private static void OnPropertyChangeUnit(BindableObject bindable, object oldValue, object newValue)
	{
		var control = (ProductContentView)bindable;
		control.ProductUnit = (string)newValue;
	}
	private static void OnPropertyChangeAmount(BindableObject bindable, object oldValue, object newValue)
	{
		var control = (ProductContentView)bindable;
		control.ProductAmount = (int)newValue;
	}
	private static void OnPropertyChangeCategory(BindableObject bindable, object oldValue, object newValue)
	{
		var control = (ProductContentView)bindable;
		control.ProductCategory = (string)newValue;
	}
	private static void OnPropertyChangeIsBought(BindableObject bindable, object oldValue, object newValue)
	{
		var control = (ProductContentView)bindable;
		control.ProductIsBought = (bool)newValue;
	}
	private static void OnPropertyChangeIsOptional(BindableObject bindable, object oldValue, object newValue)
	{
		var control = (ProductContentView)bindable;
		control.ProductIsOptional = (bool)newValue;
	}

	public ProductContentView()
	{
		InitializeComponent();
	}
	private void Increment_Clicked(object sender, EventArgs e)
	{

		if (ProductAmount + 1 > int.MaxValue)
			ProductAmount = 0;
		else
			ProductAmount++;

		var doc = XDocument.Load(Models.AllProducts.filePath);
		bool productFound = false;
		foreach (var node in doc.Descendants())
		{
			if (node.Name.ToString().Equals("ProductID") && node.Value.ToString().Equals(ProductID.ToString()))
			{
				productFound = true;
				continue;
			}

			if (productFound && node.Name.ToString().Equals("ProductAmount"))
			{
				node.SetValue(ProductAmount); 
				doc.Save(Models.AllProducts.filePath);
				break;
			}
		}
	}

	private void Decrement_Clicked(object sender, EventArgs e)
	{
		if (ProductAmount > 0)
			ProductAmount--;
		else
			return;
        var doc = XDocument.Load(Models.AllProducts.filePath);
        bool productFound = false;
        foreach (var node in doc.Descendants())
        {
            if (node.Name.ToString().Equals("ProductID") && node.Value.ToString().Equals(ProductID.ToString()))
            {
                productFound = true;
                continue;
            }

            if (productFound && node.Name.ToString().Equals("ProductAmount"))
            {
                node.SetValue(ProductAmount);
                doc.Save(Models.AllProducts.filePath);
                break;
            }
        }
    }

    private async void Delete_Clicked(object sender, EventArgs e)
    {
        var doc = XDocument.Load(Models.AllProducts.filePath);
        foreach (var node in doc.Descendants())
        {
            if(node.Name.ToString().Equals("ProductID") && node.Value.ToString().Equals(ProductID.ToString()))
            {
                node.Parent?.Remove();
				doc.Save(Models.AllProducts.filePath);
				break;
            }
        }
		this.IsVisible = false;
    }

    private void Bought_Changed(object sender, CheckedChangedEventArgs e)
    {
		CheckBox checkBox = (CheckBox)sender;
		isBought = !isBought;
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

		if (checkBox.IsChecked)
			this.Opacity = 0.5;
		else
			this.Opacity = 1;
	}

	private void Entry_Changed(object sender, TextChangedEventArgs e)
	{
		var entry = (Entry)sender;
		var doc = XDocument.Load(Models.AllProducts.filePath);
		bool found = false;
		foreach (var node in doc.Descendants())
		{
			if (node.Name.ToString().Equals("ProductID") && node.Value.ToString().Equals(ProductID.ToString()))
			{
				found = true;
				continue;
			}
			if (found && node.Name.ToString().Equals("ProductAmount"))
			{
				node.Value = entry.Text;
				doc.Save(Models.AllProducts.filePath);
				break;
			}
		}
	}

	private void Optional_Changed(object sender, CheckedChangedEventArgs e)
	{
		CheckBox checkbox = (CheckBox)sender;
		isOptional = !isOptional;
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

		if(checkbox.IsChecked)
			this.BackgroundColor = Color.Parse("Blue");
		else
			this.BackgroundColor = null;
	}
}