using System;
using System.Runtime.CompilerServices;
using HoloToolkit.Unity.InputModule;
using RAIN.Entities;
using UnityEngine;
using Random = UnityEngine.Random;

public class Micheal : HumanAI, IInputClickHandler {
	
	private float timeLeft = 10f;


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
		tEntity.Entity.AddAspect(CreateRainAspect("aMicheal"));
		if (NavTargetsGO == null)
		{
			NavTargetsGO =  GameObject.FindWithTag("ShoppingStops");
		}
	}


	// Update is called once per frame
	 void Update ()
	 {
		 anim.SetFloat("Speed", !IsMoving() ? 0 : ANIM_SPEED_RUN);
		 switch ((EnumState.EStates)Enum.Parse(typeof( EnumState.EStates), GetState()))
		{
			case EnumState.EStates.Helping:
				if (timeLeft > 0)
				{
					anim.SetBool("Shout", true);
					tRig.AI.WorkingMemory.SetItem("speed", RUN_SPEED_MAX);
					timeLeft -= Time.deltaTime;
				}
				if ( timeLeft < 0 )
				{
					anim.SetBool("Shout", false);
					
				}
				if (timeLeft < 0 && !anim.GetCurrentAnimatorStateInfo(0).IsName("Shout"))
				{
					SetState(EnumState.EStates.Run);
					gameObject.GetComponents<Collider>()[0].enabled = false;
				}
				break;
				default:
					anim.SetBool("Shout", false);
					break;

		}
	 }
	/// <summary>
	/// When a IA ENTER in the collider, say him he is in danger
	/// </summary>
	private void OnTriggerEnter(Collider other)
	{
		if (GetState() != "helping") return;
		if(other.gameObject.GetComponent<Civilian>() != null)
			other.gameObject.SendMessage("OnInDanger");
	}
	/// <summary>
	/// When a IA IS in the collider, say him he is in danger
	/// </summary>
	private void OnTriggerStay(Collider other)
	{
		if (GetState() != EnumState.EStates.Helping.ToString()) return;
		if(other.gameObject.GetComponent<Civilian>() != null)
			other.gameObject.SendMessage("OnInDanger");
	}


	
}
