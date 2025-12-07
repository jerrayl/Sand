using Godot;
using Godot.Collections;
using System;

public partial class SnapSlot : Marker2D
{
	public DraggableShape Occupant { get; set; }

	public bool IsFree => Occupant == null;
	
	[Export]
	public Array<ShapeType> AllowedShapes { get; set; } = [];
	
	public bool Accepts(DraggableShape shape)
	{
		return AllowedShapes.Contains(shape.ShapeType);
	}
	
	 public override void _Ready()
	{
		AddToGroup("Slot");
	}
}
