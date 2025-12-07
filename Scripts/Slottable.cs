using Godot;
using System;

public partial class Slottable : DraggableShape
{
	public SnapSlot CurrentSlot { get; set; }
	
	[Export]
	public float GlobalSnapRadius = 32f;

	private Node2D _freeParent;

	public override void _Ready()
	{
		base._Ready();
		_freeParent = GetParent<Node2D>();
	}

	protected override void OnDragReleased()
	{
		base.OnDragReleased();
		TrySnapToNearestSlot();
	}

	private void TrySnapToNearestSlot()
	{
		SnapSlot closestSlot = null;
		float closestDistSq = GlobalSnapRadius * GlobalSnapRadius;
		float snapRadiusSq = closestDistSq;

		// Get all slots in the group "slot"
		var slotNodes = GetTree().GetNodesInGroup("Slot");

		foreach (Node node in slotNodes)
		{
			if (node is not SnapSlot slot)
				continue;
				
			if (!slot.Accepts(this))
				continue;

			float distSq = GlobalPosition.DistanceSquaredTo(slot.GlobalPosition);

			// Slot must be close enough AND either free or already holding us
			if (distSq < snapRadiusSq && distSq <= closestDistSq && (slot.IsFree || slot == CurrentSlot))
			{
				closestDistSq = distSq;
				closestSlot = slot;
			}
		}

		if (closestSlot != null)
		{
			SnapIntoSlot(closestSlot);
		}
		else
		{
			DetachFromSlot();
		}
	}
	
	private void SnapIntoSlot(SnapSlot newSlot)
	{
		// Free old slot, if any
		if (CurrentSlot != null && CurrentSlot != newSlot)
		{
			CurrentSlot.Occupant = null;
		}

		CurrentSlot = newSlot;
		CurrentSlot.Occupant = this;

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
			CurrentSlot.Occupant = null;
			CurrentSlot = null;
		}

		// Reparent back to "free" parent so it no longer follows the polygon
		if (_freeParent != null && GetParent() != _freeParent)
		{
			Reparent(_freeParent);
		}
	}
}
