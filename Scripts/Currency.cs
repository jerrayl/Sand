using Godot;
using System.Linq;

public partial class Currency : RichTextLabel
{
	private GlobalManager _global;
	
	public override void _Ready()
	{
		_global = GetNode<GlobalManager>("/root/GlobalManager");
	}

	public override void _Process(double delta)
	{
		Text = string.Join(" ", _global.Currencies.Select(kvp => $"{GetCurrencyName(kvp.Key)}: {kvp.Value}"));
	}
	
	public string GetCurrencyName(CurrencyType currencyType)
	{
		return _global.IdentifiedCurrencyTypes.Contains(currencyType)
		? Labels.AbsoluteCurrencyNames[currencyType]
		: Labels.MaskedCurrencyNames[currencyType];
	}
}
