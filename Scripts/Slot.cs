using Godot;
using Godot.Collections;
using System;
using System.Linq;

public partial class Slot : Marker2D
{
	public DraggableBlock Occupant => GetChildren().OfType<DraggableBlock>().FirstOrDefault();

	public bool IsFree => Occupant == null;
	
	[Export]
	public Array<ShapeType> AllowedShapes { get; set; } = [];
	
	public bool Accepts(DraggableBlock block)
	{
		return AllowedShapes.Contains(block.ShapeType);
	}
	
	 public override void _Ready()
	{
		AddToGroup("Slot");
	}
}
