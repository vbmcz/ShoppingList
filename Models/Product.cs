using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Models
{
	internal class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Unit { get; set; }
		public int Amount { get; set; }
		public string Category { get; set; }
		public bool IsBought { get; set; }
		public bool IsOptional { get; set; }
	}
}
