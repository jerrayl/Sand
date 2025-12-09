using Godot;
using System;

public partial class Slottable : DraggableBlock
{
	public Slot CurrentSlot { get; set; }
	
	[Export]
	public float GlobalSnapRadius = 32f;

	private Node2D _freeParent;

	public override void _Ready()
	{
		base._Ready();
		_freeParent = GetParent<Node2D>();
	}
	
	protected override void OnDragStarted()
	{
		base.OnDragStarted();
		DetachFromSlot();
	}

	protected override void OnDragReleased()
	{
		base.OnDragReleased();
		TrySnapToNearestSlot();
	}

	private void TrySnapToNearestSlot()
	{
		Slot closestSlot = null;
		float closestDistSq = GlobalSnapRadius * GlobalSnapRadius;
		float snapRadiusSq = closestDistSq;

		// Get all slots in the group "slot"
		var slotNodes = GetTree().GetNodesInGroup("Slot");

		foreach (Node node in slotNodes)
		{
			if (node is not Slot slot)
				continue;
				
			if (!slot.Accepts(this))
				continue;

			float distSq = GlobalPosition.DistanceSquaredTo(slot.GlobalPosition);

			// Slot must be close enough AND either free or already holding us
			if (distSq < snapRadiusSq && distSq <= closestDistSq && (slot.Occupant is null || slot == CurrentSlot))
			{
				closestDistSq = distSq;
				closestSlot = slot;
			}
		}

		if (closestSlot != null)
		{
			SnapIntoSlot(closestSlot);
		}
	}
	
	private void SnapIntoSlot(Slot newSlot)
	{
		CurrentSlot = newSlot;

		if (GetParent() != CurrentSlot)
		{
			Reparent(CurrentSlot);
		}

		// Local position relative to slot
		Position = Vector2.Zero;
	}

	private void DetachFromSlot()
	{
		if (CurrentSlot != null)
		{
			CurrentSlot = null;
		}

		// Reparent back to "free" parent so it no longer follows the polygon
		if (_freeParent != null && GetParent() != _freeParent)
		{
			Reparent(_freeParent);
		}
	}
}
