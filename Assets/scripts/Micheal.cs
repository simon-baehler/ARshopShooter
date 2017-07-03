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
		tRig.AI.Senses.AddSensor(createVisualSensor(true, "eyes", 120, new Vector3(0,1.6f ,0), true));

   
		//creation of the aspect
		tEntity = entity.GetComponentInChildren<EntityRig>();
		entity.GetComponentInChildren<EntityRig>().Entity.Form = gameObject;
		tEntity.Entity.AddAspect(createRAINAspect("aMicheal"));
		if (NavTargetsGO == null)
		{
			NavTargetsGO =  GameObject.FindWithTag("ShoppingStops");
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (getState() != "helping") return;
		if (other.gameObject.layer != AILayerID) return;
		other.gameObject.SendMessage("OnInDanger");
		print("OnTriggerEnter");
	}

	private void OnTriggerStay(Collider other)
	{
		if (getState() != "helping") return;
		if (other.gameObject.layer != AILayerID || other.name == "shooter") return;
		other.gameObject.SendMessage("OnInDanger");
		print("OnTriggerStay");
	}

	// Update is called once per frame
	 void Update ()
	 {
		 anim.SetFloat("Speed", !isMoving() ? 0 : ANIM_SPEED);
		 switch (getState())
		{
			case "helping":
				print(timeLeft);
				tRig.AI.WorkingMemory.SetItem("speed",RUN_SPEED_MAX);
				timeLeft -= Time.deltaTime;
				if ( timeLeft < 0 )
				{
					setState("run");
				}
				break;
		}
	 }
}
