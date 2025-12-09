using Godot;
using System;

public partial class DraggableBlock : Node2D
{
	[Export]
	public ShapeType ShapeType { get; set; }
	
	private bool _isDragging = false;
	private Vector2 _dragOffset;

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseButtonEvent)
		{
			if (mouseButtonEvent.ButtonIndex == MouseButton.Left)
			{
				if (mouseButtonEvent.Pressed)
				{
					var mousePos = GetGlobalMousePosition();

					if (IsMouseOver(mousePos) && IsTopMostAt(mousePos))
					{
						_isDragging = true;
						_dragOffset = GlobalPosition - mousePos;
						OnDragStarted();
					}
				}
				else
				{
					_isDragging = false;
					OnDragReleased();
				}
			}
		}

		if (@event is InputEventMouseMotion && _isDragging)
		{
			var mousePos = GetGlobalMousePosition();
			GlobalPosition = mousePos + _dragOffset;
		}
	}
	
	private bool IsMouseOver(Vector2 globalMousePos)
	{
		return TryPolygonCheck(globalMousePos) || TryColorRectCheck(globalMousePos);
	}
	
	private bool TryPolygonCheck(Vector2 globalMousePos)
	{
		var poly = GetNodeOrNull<Polygon2D>("Polygon2D");
		if (poly == null)
			return false;

		var localPoint = poly.ToLocal(globalMousePos);
		return Geometry2D.IsPointInPolygon(localPoint, poly.Polygon);
	}
	
	private bool TryColorRectCheck(Vector2 globalMousePos)
	{
		var cr = GetNodeOrNull<ColorRect>("ColorRect");
		if (cr == null)
			return false;

		Rect2 rect = new Rect2(cr.GlobalPosition, cr.Size);
		return rect.HasPoint(globalMousePos);
	}
	
	private bool IsTopMostAt(Vector2 globalMousePos)
	{
		var nodes = GetTree().GetNodesInGroup("Draggable");

		DraggableBlock topBlock = null;
		int topZ = int.MinValue;

		foreach (Node node in nodes)
		{
			if (node is not DraggableBlock shape)
				continue;

			if (!shape.IsMouseOver(globalMousePos))
				continue;

			if (shape.ZIndex > topZ)
			{
				topZ = shape.ZIndex;
				topBlock = shape;
			}
		}

		return topBlock == this;
	}

	protected virtual void OnDragStarted()
	{
	}

	protected virtual void OnDragReleased()
	{
	}
	
	public override void _Ready()
	{
		AddToGroup("Draggable");
	}
}
