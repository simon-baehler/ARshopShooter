﻿using UnityEngine; 
using System.Collections.Generic;
using RAIN.Core; 
using RAIN.Action; 
using RAIN.Entities.Aspects; 
//source :http://legacy.rivaltheory.com/forums/topic/ai-collision-solutions/page/2/


[RAINAction] 
public class PlayerAvoidance : RAINAction 
{ 
	private const float MINIMUM_DISTANCE = 10f;

	private Vector3 _target; 
	private IList<RAINAspect> _targetsToAvoid;
	private GameObject parent;

	public PlayerAvoidance() 
	{ 
		actionName = "PlayerAvoidance"; 
	}

	public override void Start(AI ai)
	{
		parent = ai.Body;
		_targetsToAvoid = new List<RAINAspect>();
		if (ai.WorkingMemory.GetItem<RAINAspect>("varAvoid") != null)
		{
			if(!_targetsToAvoid.Contains(ai.WorkingMemory.GetItem<RAINAspect>("varAvoid")))
				_targetsToAvoid.Add(ai.WorkingMemory.GetItem<RAINAspect>("varAvoid"));
		}
		base.Start(ai); 
	}

	public override ActionResult Execute(AI ai) 
	{ 
		if(_targetsToAvoid != null) 
		{ 
			foreach(var aspect in _targetsToAvoid) 
			{ 
				if(isToClose(ai,aspect)) 
				{ 
					doAvoidance(ai, aspect); 
				} 
			} 
		}
		return ActionResult.SUCCESS; 
	}
	#region 
	private void doAvoidance(AI ai, RAINAspect aspect) 
	{ 
		var direction = ai.Kinematic.Position - aspect.Position; 
		direction.Normalize(); 
		ai.Motor.MoveTo(ai.Kinematic.Position + direction); 
	}

	private bool isToClose(AI ai, RAINAspect aspect) 
	{ 
		var dist = Vector3.Distance(ai.Kinematic.Position, aspect.Position);
		if(dist <= MINIMUM_DISTANCE) 
			return true;
		return false; 
	} 
	#endregion 
}