using System.Runtime.CompilerServices;
using HoloToolkit.Unity.InputModule;
using RAIN.Entities;
using UnityEngine;

public class Micheal : HumanAI, IInputClickHandler {

	private const float ANIM_SPEED = 0.5f;
	private const int ANIM_SPEED_RUN = 2;
	private const int NORMAL_SPEED = 1;
	private const int RUN_SPEED_MIN = 3;
	private const int RUN_SPEED_MAX = 6;
    
	private const float MASS_MIN = 1;
	private const float MASS_MAX = 10;

	private float timeLeft = 30f;
	
	private int AILayerID;


	
	// Use this for initialization
	void Start () {
		AILayerID = LayerMask.NameToLayer("IA");
		//Adding gameObject Named Entity
		GameObject entity = new GameObject("Entity");
		entity.transform.parent = gameObject.transform;
		entity.AddComponent<EntityRig>();
        
		//Initialisation
		init();
		rigidbody.mass = Random.Range(MASS_MIN, MASS_MAX);
		tRig.AI.WorkingMemory.SetItem<float>("speed", 1);
		tRig.AI.WorkingMemory.SetItem<string>("state", "normal");
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
		 anim.SetFloat("Speed", !IsMoving() ? 0 : ANIM_SPEED);
		 switch (GetState())
		{
			case "helping":
				anim.SetBool("Shout", true);
				tRig.AI.WorkingMemory.SetItem("speed",RUN_SPEED_MAX);
				timeLeft -= Time.deltaTime;
				if ( timeLeft < 0 )
				{
					SetState("run");
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
		if (other.gameObject.layer != AILayerID) return;
		other.gameObject.SendMessage("OnInDanger");
	}
	/// <summary>
	/// When a IA IS in the collider, say him he is in danger
	/// </summary>
	private void OnTriggerStay(Collider other)
	{
		if (GetState() != "helping") return;
		if (other.gameObject.layer != AILayerID || other.name == "shooter") return;
		other.gameObject.SendMessage("OnInDanger");
	}

	
}
