	using System;
	using System.Linq;
	using System.Collections.Generic;
	using Godot;

	public partial class GlobalManager : Node
	{
		// Permanent State
		public Dictionary<CurrencyType, int> Currencies { get; private set; } = new();
		public HashSet<CurrencyType> IdentifiedCurrencyTypes { get; set; } = new();	
		public HashSet<int> UnlockedDialogs { get; set; } = new();
		public bool ActivatedGBlock = false;
		
		// Transient State
		public string DisplayText { get; set; } = null;
		
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
		
		public string GetCurrencyName(CurrencyType currencyType)
		{
			return IdentifiedCurrencyTypes.Contains(currencyType)
			? Labels.AbsoluteCurrencyNames[currencyType]
			: Labels.MaskedCurrencyNames[currencyType];
		}
	}
