using System;
using System.Collections.Generic;
using Godot;

public partial class LocationBlock : DraggableBlock
{
	[Export]
	public int ProductionSeconds { get; set; } = 30;
	[Export]
	public PackedScene BScene { get; set; }
	[Export]
	public PackedScene HScene { get; set; }
	[Export]
	public Slot SpawnSlot { get; set; }
	[Export]
	public Node2D RootNode { get; set; }

	private GlobalManager _global;
	private List<Slot> _slots = [];
	private Timer _timer;
	private int _countDown = -1;
	private ShaderMaterial _mat;
	
	public override void _Ready()
	{
		_slots = Helpers.GetChildrenOfType<Slot>(this);
		_mat = GetNode<Polygon2D>("Polygon2D").Material as ShaderMaterial;
		_global = GetNode<GlobalManager>("/root/GlobalManager");
		
		_timer = new Timer
		{
			Name = "BehaviorTimer",
			WaitTime = 1.0f,
			Autostart = true,
			OneShot = false
		};
		AddChild(_timer);
		_timer.Timeout += OnTimerTimeout;
	}
	
	private void OnTimerTimeout()
	{
		UpdateCountDown();
		
		float t =  _countDown == -1 
			? 0 
			: 1f - ((float)_countDown / ProductionSeconds);
	
		_mat.SetShaderParameter("fill_amount", t);
	}
	
	private void UpdateCountDown() 
	{
		var sBlockCount = 0;
		
		foreach (var slot in _slots)
		{
			var occupant = slot.Occupant;
			if (occupant is null)
			{
				continue;	
			}
			
			var shapeType = occupant.ShapeType;
			if (shapeType == ShapeType.B || shapeType == ShapeType.H)
			{
				_countDown = -1;
				return;
			}

			if (shapeType == ShapeType.S)
			{
				sBlockCount++;
			}
		}

		if (sBlockCount == 0)
		{
			_countDown = -1;
			return;
		}

		if (_countDown == -1)
		{
			_countDown = ProductionSeconds;
		}

		_countDown -= sBlockCount;

		if (_countDown < 0)
		{
			SpawnBlock();
			_countDown = -1;
		}
	}
	
	private void SpawnBlock()
	{
		var scene = _global.ActivatedGBlock && new Random().NextDouble() < 0.5 ? HScene : BScene;
		var block = scene.Instantiate<Slottable>();
		RootNode.AddChild(block);
		block.GlobalPosition = SpawnSlot.GlobalPosition;
		block.CurrentSlot = SpawnSlot;
		block.Reparent(SpawnSlot);
	}
}
