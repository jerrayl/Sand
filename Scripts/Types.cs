using System.Collections.Generic;

public enum ShapeType
{
	S,
	G,
	B,
	C,
	H
}


public enum CurrencyType
{
	Sand,
	Words,
	Insight
}

public static class Labels {
	public static readonly Dictionary<CurrencyType, string> MaskedCurrencyNames = new() { 
		{ CurrencyType.Sand, "S" },
		{ CurrencyType.Words, "W" },
		{ CurrencyType.Insight, "I" }
	};

	public static readonly Dictionary<CurrencyType, string> AbsoluteCurrencyNames = new() { 
		{ CurrencyType.Sand, "Sand" },
		{ CurrencyType.Words, "Words" },
		{ CurrencyType.Insight, "Insight" }
	};
}
