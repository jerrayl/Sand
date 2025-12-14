using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class ProcessingBlock : DraggableBlock
{
	[Export]
	public int ProductionSeconds { get; set; } = 15;
	
	private GlobalManager _global;
	private List<Slot> _slots = [];
	private Timer _timer;
	private int _countDown = -1;
	private ShaderMaterial _mat;
	private DraggableBlock _slottedBlock;
	
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
		if (_countDown == -1) 
		{
			CheckSlottedBlock();
			return;
		}
			
		_countDown -= 1;
		
		if (_countDown < 0)
		{
			_global.UpdateCurrency(CurrencyType.Sand, 1);
			_slottedBlock.QueueFree();
			_countDown = -1;
		}
	}
	private void CheckSlottedBlock()
	{
		var slot = _slots.Single();
		_slottedBlock = slot.Occupant;
		if (occupant is null)
		{
			return;
		}
		var slottedShapeType = _slottedBlock?.ShapeType;
		_slottedBlock.IsDraggable = false;
		_countDown = ProductionSeconds;
	}
}
