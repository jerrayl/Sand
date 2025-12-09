using Godot;
using System;

public partial class DraggableBlock : Node2D
{
    /*
	[Export]
	public float MoveSpeed = 150f; // units per second

	[Export]
	public float PickupRadius = 16f; // how close we need to get to pick up
    // Optional: where to hold the picked-up block relative to this JobBlock
    [Export]
    public Vector2 CarryOffset = new Vector2(0, -24);

    private DraggableShape _targetBlock;
    private bool _isCarrying = false;

    public override void _Ready()
    {
        // Find the nearest B block when we spawn
        _targetBlock = FindNearestBlockOfType(ShapeType.B);
    }

    public override void _Process(double delta)
    {
        if (_isCarrying)
        {
            // Already carrying a block → keep it attached and stop moving
            UpdateCarriedBlockTransform();
            return;
        }

        if (_targetBlock == null || !IsInstanceValid(_targetBlock))
        {
            // No valid target → stop moving
            return;
        }

        // Move towards the target
        MoveTowardsTarget((float)delta);
    }

    private void MoveTowardsTarget(float delta)
    {
        Vector2 targetPos = _targetBlock.GlobalPosition;
        Vector2 myPos = GlobalPosition;

        Vector2 toTarget = targetPos - myPos;
        float dist = toTarget.Length();

        if (dist <= PickupRadius)
        {
            // We've reached the block → pick it up
            PickUpBlock(_targetBlock);
            return;
        }

        if (dist > 0.001f)
        {
            Vector2 dir = toTarget / dist;
            GlobalPosition += dir * MoveSpeed * delta;
        }
    }

    private void PickUpBlock(DraggableShape block)
    {
        if (block == null || !IsInstanceValid(block))
            return;

        _isCarrying = true;

        // Reparent the block under this JobBlock so it moves with us
        if (block.GetParent() != this)
        {
            block.Reparent(this); // Godot 4

            // If you're on Godot 3, use:
            // var parent = block.GetParent();
            // parent.RemoveChild(block);
            // AddChild(block);
        }

        // Place it at the carry offset
        block.Position = CarryOffset;
    }

    private void UpdateCarriedBlockTransform()
    {
        // If you want the carried block to always sit at CarryOffset,
        // this is technically redundant since it's set once in PickUpBlock,
        // but you can keep it in case you animate things.
        foreach (Node child in GetChildren())
        {
            if (child is DraggableShape shape && shape.ShapeType == ShapeType.B)
            {
                shape.Position = CarryOffset;
            }
        }
    }

    private DraggableShape FindNearestBlockOfType(ShapeType type)
    {
        DraggableShape nearest = null;
        float nearestDistSq = float.PositiveInfinity;

        Vector2 myPos = GlobalPosition;

        var nodes = GetTree().GetNodesInGroup("draggable_shape");
        foreach (Node node in nodes)
        {
            if (node == this)
                continue;

            if (node is not DraggableShape shape)
                continue;

            if (shape.ShapeType != type)
                continue;

            // Optional: skip blocks that are already being carried or slotted
            // if (shape is DraggableSquare sq && sq.CurrentSlot != null) continue;

            float distSq = myPos.DistanceSquaredTo(shape.GlobalPosition);
            if (distSq < nearestDistSq)
            {
                nearestDistSq = distSq;
                nearest = shape;
            }
        }

        return nearest;
    }
    */
}
