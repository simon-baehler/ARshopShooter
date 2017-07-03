
using HoloToolkit.Unity.InputModule;
using RAIN.Entities;
using RAIN.Entities.Aspects;
using UnityEngine;


public class Adam :   HumanAI, IInputClickHandler
{
	private RAINAspect civil;
	
	private const float ANIM_SPEED = 0.5f;
	private const int ANIM_SPEED_RUN = 2;
	private const int NORMAL_SPEED = 1;
	private const int RUN_SPEED_MIN = 3;
	private const int RUN_SPEED_MAX = 6;
    
	private const float MASS_MIN = 1;
	private const float MASS_MAX = 10;

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
		tRig.AI.WorkingMemory.SetItem<float>("speed", 1);
		tRig.AI.WorkingMemory.SetItem<string>("state", "searchCivilian");
		tRig.AI.WorkingMemory.SetItem<string>("moveESC", "ESC");
		anim.SetFloat("Speed", ANIM_SPEED);

		// Creation of the Sensor
		tRig.AI.Body = gameObject;
		tRig.AI.Senses.AddSensor(createVisualSensor(true, "eyes", 120, new Vector3(0,1.6f ,0), true));

   
		//creation of the aspect
		tEntity = entity.GetComponentInChildren<EntityRig>();
		entity.GetComponentInChildren<EntityRig>().Entity.Form = gameObject;
		tEntity.Entity.AddAspect(createRAINAspect("aAdam"));
		if (NavTargetsGO == null)
		{
			NavTargetsGO =  GameObject.FindWithTag("ShoppingStops");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!isMoving())
		{
			anim.SetFloat("Speed", 0);
		}
		else
		{
			anim.SetFloat("Speed", 2);
		}

		switch (getState())
		{
			case "helping":
				tRig.AI.WorkingMemory.SetItem("speed",RUN_SPEED_MAX);
				break;
			case "searchCivilian":
				tRig.AI.WorkingMemory.SetItem("speed",RUN_SPEED_MAX);
				timeLeft -= Time.deltaTime;
				if ( timeLeft < 0 )
				{
					setState("run");
				}
				break;
		}
	}
	private void InSafeZone()
	{
		var randomDist = Random.Range(0.1f, 6);
		tRig.AI.Motor.CloseEnoughDistance = randomDist;
		setState("saved");
        
	}

	public void sayRassuring()
	{
		civil = tRig.AI.WorkingMemory.GetItem<RAINAspect>("moveTarget");
		civil.MountPoint.gameObject.GetComponent<Civillian>().setState("run");
		civil.MountPoint.gameObject. GetComponent<Animator>().SetBool("panic", false);
	}
	private void OnSelect()
	{
		if (getState() == "saved") return;
		setState("run");
	}

	public void OnInputClicked(InputClickedEventData eventData)
	{
		OnSelect();
	}
}
