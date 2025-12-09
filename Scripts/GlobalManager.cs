	using System;
	using System.Linq;
	using System.Collections.Generic;
	using Godot;

	public partial class GlobalManager : Node
	{
		public Dictionary<CurrencyType, int> Currencies { get; private set; } = new();
		public HashSet<CurrencyType> IdentifiedCurrencyTypes { get; set; } = new();	
		
		public override void _Ready()
		{
			Currencies = Enum.GetValues(typeof(CurrencyType))
			   .Cast<CurrencyType>()
			   .ToDictionary(x => x, x => 0);
		}
		
		public void UpdateCurrency(CurrencyType type, int change)
		{
			Currencies[type] += change;
		}
	}
