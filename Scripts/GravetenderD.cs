using Godot;
using System;
using System.Collections.Generic;

public partial class GravetenderD : Node2D
{
	private const int DialogId = 1;
	private GlobalManager _global;
	private List<Button> _buttons;
	
	public override void _Ready()
	{
		_global = GetNode<GlobalManager>("/root/GlobalManager");
		
		_buttons = Helpers.GetChildrenOfType<Button>(this);
		_buttons[0].Pressed += OnButton1Pressed;
		_buttons[1].Pressed += OnButton2Pressed;
		_buttons[2].Pressed += OnButton3Pressed;
		
		_buttons[0].MouseExited  += OnButton1MouseExited;
		
		_buttons[0].Text = $"Talk (5 {_global.GetCurrencyName(CurrencyType.Sand)})";
		
		UpdateButtonLabels();
	}
	
	private void UpdateButtonLabels() 
	{
		if (_global.UnlockedDialogs.Contains(DialogId))
		{
			_buttons[0].Text = "Greeted World";
		}
	}

	private void OnButton1Pressed()
	{
		if (_global.UnlockedDialogs.Contains(DialogId)) {
			_global.DisplayText = Labels.Dialogs[DialogId];
		}
		
		_global.Currencies[CurrencyType.Sand] += 5;
		if (_global.Currencies[CurrencyType.Sand] >= 5) 
		{
			_global.Currencies[CurrencyType.Sand] -= 5;
		}
		_global.UnlockedDialogs.Add(DialogId);
		UpdateButtonLabels();
		_global.DisplayText = Labels.Dialogs[DialogId];
	}
	
	private void OnButton1MouseExited()
	{
		_global.DisplayText = null;
	}
	
	private void OnButton2Pressed()
	{
		_global.ActivatedGBlock = true;
	}	
	
	private void OnButton3Pressed()
	{
		GD.Print("Button 3 clicked!"); 
	}
}
