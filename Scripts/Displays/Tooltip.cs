using Godot;
using System.Linq;

public partial class Tooltip : ColorRect
{
	private GlobalManager _global;
	private RichTextLabel _label;
	
	public override void _Ready()
	{
		_global = GetNode<GlobalManager>("/root/GlobalManager");
		_label = GetNode<RichTextLabel>("RichTextLabel");
	}

	public override void _Process(double delta)
	{
		if (_global.DisplayText is null)
		{
			Visible = false;
			return;
		}
		
		Visible = true;
		_label.Text = _global.DisplayText;
	}
}
