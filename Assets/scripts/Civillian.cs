using System.Collections;
using HoloToolkit.Unity.InputModule;
using RAIN.Entities;
using RAIN.Entities.Aspects;
using RAIN.Perception.Sensors;
using UnityEngine;


public class Civillian : HumanAI, IInputClickHandler
{
    private const float ANIM_SPEED = 0.5f;
    private const int ANIM_SPEED_RUN = 2;
    private const int NORMAL_SPEED = 1;
    private const int RUN_SPEED_MIN = 3;
    private const int RUN_SPEED_MAX = 6;
    
    private const float MASS_MIN = 1;
    private const float MASS_MAX = 10;

    private int randomSpeed;

    // Use this for initialization
    private void Start()
    {
       
        randomSpeed = Random.Range(RUN_SPEED_MIN,RUN_SPEED_MAX);
        
        //Adding gameObject Named Entity
        GameObject entity = new GameObject("Entity");
        entity.transform.parent = gameObject.transform;
        entity.AddComponent<EntityRig>();
        
        //Initialisation
        init();
        rigidbody.mass = Random.Range(MASS_MIN, MASS_MAX);
        tRig.AI.WorkingMemory.SetItem<float>("speed", 1);
        tRig.AI.WorkingMemory.SetItem<string>("state", "panic");
        tRig.AI.WorkingMemory.SetItem<string>("moveESC", "ESC");
        anim.SetFloat("Speed", ANIM_SPEED);

        // Creation of the Sensor
        tRig.AI.Body = gameObject;
        tRig.AI.Senses.AddSensor(createVisualSensor(true, "eyes", 120, new Vector3(0,1.6f ,0), true));

   
        //creation of the aspect
        tEntity = entity.GetComponentInChildren<EntityRig>();
        entity.GetComponentInChildren<EntityRig>().Entity.Form = gameObject;
        tEntity.Entity.AddAspect(createRAINAspect("aCivil"));
        if (NavTargetsGO == null)
        {
            NavTargetsGO =  GameObject.FindWithTag("ShoppingStops");
        }
    }

    // Update is called once per frame
    private void Update()
    {
        switch (getState())
        {
            case "normal":
                //print("normal");
                tRig.AI.WorkingMemory.SetItem<float>("speed", NORMAL_SPEED);
                anim.SetFloat("Speed", ANIM_SPEED);
                if (!isMoving())
                {
                    anim.SetFloat("Speed", 0);
                }
                else
                {
                    anim.SetFloat("Speed", ANIM_SPEED);
                }
                break;
            case "panic":
                //print("panic");
                anim.SetBool("panic", true);
                break;
            case "run":
                anim.SetFloat("Speed", ANIM_SPEED_RUN);
                tRig.AI.WorkingMemory.SetItem<float>("speed", randomSpeed);
                break;
            case "saved":        
                if (!isMoving())
                {
                    anim.SetFloat("Speed", 0);
                }
                else
                {
                    anim.SetFloat("Speed", ANIM_SPEED);
                    tRig.AI.WorkingMemory.SetItem<float>("speed", NORMAL_SPEED);
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

    private void OnPolice()
    {
        if (tRig.AI.WorkingMemory.GetItem("varPlayer") == null ||
            (getState() == "panic" || getState() == "dead" || getState() == "saved")) return;
        setState("run");
    }

    private void OnSelect()
    {
        if (getState() == "panic")
        {
            anim.SetBool("panic", false);
            setState("run");
        }
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        OnSelect();
    }
}