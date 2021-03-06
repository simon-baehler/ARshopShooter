﻿using System;
using HoloToolkit.Unity.InputModule;
using RAIN.Entities;
using RAIN.Entities.Aspects;
using UnityEngine;
using Random = UnityEngine.Random;

public class Adam :   HumanAI, IInputClickHandler
{
	private RAINAspect civil;
	private float timeLeft = 30f;

	// Use this for initialization
	void Start () {

		//Adding gameObject Named Entity
		GameObject entity = new GameObject("Entity");
		entity.transform.parent = gameObject.transform;
		entity.AddComponent<EntityRig>();
        
		//Initialisation
		init();
		rigidbody.mass = Random.Range(MASS_MIN, MASS_MAX);
		tRig.AI.WorkingMemory.SetItem<float>("speed", NORMAL_SPEED);
		tRig.AI.WorkingMemory.SetItem<string>("state", EnumState.EStates.Normal.ToString());
		tRig.AI.WorkingMemory.SetItem<string>("moveESC", "ESC");
		anim.SetFloat("Speed", ANIM_SPEED);

		// Creation of the Sensor
		tRig.AI.Body = gameObject;
		tRig.AI.Senses.AddSensor(CreateVisualSensor(true, "eyes", 120, new Vector3(0,1.6f ,0), true));

   
		//creation of the aspect
		tEntity = entity.GetComponentInChildren<EntityRig>();
		entity.GetComponentInChildren<EntityRig>().Entity.Form = gameObject;
		tEntity.Entity.AddAspect(CreateRainAspect("aAdam"));
		if (NavTargetsGO == null)
		{
			NavTargetsGO =  GameObject.FindWithTag("ShoppingStops");
		}
	}
	
	// Update is called once per frame
	void Update () {
		switch ((EnumState.EStates)Enum.Parse(typeof( EnumState.EStates), GetState()))
		{
			case EnumState.EStates.Normal:
				tRig.AI.WorkingMemory.SetItem<float>("speed", NORMAL_SPEED);
				anim.SetFloat("Speed", ANIM_SPEED);
				anim.SetFloat("Speed", !IsMoving() ? 0 : ANIM_SPEED);
				break;
			case EnumState.EStates.Helping:
				tRig.AI.WorkingMemory.SetItem("speed",RUN_SPEED_MAX);
				break;
			case EnumState.EStates.SearchCivilian:
				tRig.AI.WorkingMemory.SetItem("speed",RUN_SPEED_MAX);
				anim.SetFloat("Speed", !IsMoving() ? 0 : RUN_SPEED_MAX);
				timeLeft -= Time.deltaTime;
				if ( timeLeft < 0 )
				{
					SetState(EnumState.EStates.Run);
				}
				break;
			case EnumState.EStates.Run:
				anim.SetFloat("Speed", !IsMoving() ? 0 : RUN_SPEED_MAX);
				break;			
			case EnumState.EStates.Saved: 
				anim.SetFloat("Speed", !IsMoving() ? 0 : ANIM_SPEED);
				tRig.AI.WorkingMemory.SetItem<float>("speed", NORMAL_SPEED);
				break;
		}
	}
	/// <summary>
	/// set the state of the target to run, used in the custom action script
	/// </summary>
	public void SayReassuring()
	{
		civil = tRig.AI.WorkingMemory.GetItem<RAINAspect>("moveTarget");
		civil.MountPoint.gameObject.GetComponent<Civilian>().SetState(EnumState.EStates.Run);
		civil.MountPoint.gameObject. GetComponent<Animator>().SetBool("panic", false);
	}
	/// <summary>
	///  When we do the tap movement on a hologram
	/// </summary>
	/// <param name="eventData"></param>
	public void OnInputClicked(InputClickedEventData eventData)
	{
		OnSelect();
	}
	/// <summary>
	/// set the state to run if he is not saved
	/// </summary>
	private void OnSelect()
	{
		if (GetState() == EnumState.EStates.Saved.ToString()) return;
		SetState(EnumState.EStates.Run);
	}



}
