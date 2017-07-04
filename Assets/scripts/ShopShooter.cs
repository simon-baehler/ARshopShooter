
using HoloToolkit.Unity.InputModule;
using RAIN.Entities;
using UnityEngine;

public class ShopShooter : HumanAI, IInputClickHandler
{    
    protected const int ANIM_SPEED = 1;
    protected const int NORMAL_SPEED = 2;
    protected const int RUN_SPEED = 4;
    protected const int NBR_POLICE_SEEN_FOR_SURRENDER = 3;
    
   
    // Use this for initialization
    private void Start()
    {
        
        GameObject entity = new GameObject("Entity");
        entity.transform.parent = gameObject.transform;
        entity.AddComponent<EntityRig>();
        init();
        tRig.AI.WorkingMemory.SetItem<float>("speed", NORMAL_SPEED);
        tRig.AI.WorkingMemory.SetItem<string>("state", "normal");
        anim.SetFloat("Speed", ANIM_SPEED);

        // Creation of the Sensor
        tRig.AI.Body = gameObject;
        tRig.AI.Senses.AddSensor(CreateVisualSensor(true, "eyes", 120, new Vector3(0,1.6f ,0), true));
        
        SetState("normal");
        tRig.AI.WorkingMemory.SetItem<int>("nbrSeenPoliceForSurr", NBR_POLICE_SEEN_FOR_SURRENDER);
        tRig.AI.WorkingMemory.SetItem<int>("speed", NORMAL_SPEED);
        oldLocation = transform.position;
        anim.SetFloat("Speed", ANIM_SPEED);
        
        tEntity = entity.GetComponentInChildren<EntityRig>();
        entity.GetComponentInChildren<EntityRig>().Entity.Form = gameObject;
        tEntity.Entity.AddAspect(CreateRainAspect("aShooter"));
    }

    // Update is called once per frame
    private void Update()
    {
        switch (GetState())
        {
            case "normal":
                anim.SetFloat("Speed", NORMAL_SPEED); 
                break;
            case "hidded":
                anim.SetFloat("Speed", 0);
                break;
            case "stopped":
                anim.SetFloat("Speed", 0);
                break;
            case "arrested":
                anim.SetFloat("Speed", tRig.AI.WorkingMemory.GetItem<int>("speed") * NORMAL_SPEED);
                tRig.AI.Motor.CloseEnoughDistance = 2.0f;
                break;
            case "hidding":
                anim.SetFloat("Speed", RUN_SPEED);
                break;
        }
    }
    /// <summary>
    /// Called when we do a tap movement on the hologram, this function will call OnSelect();
    /// </summary>
    /// <param name="eventData"></param>
    public void OnInputClicked(InputClickedEventData eventData)
    {
        OnSelect();
    }
    /// <summary>
    /// set the state to arrested
    /// </summary>
    private void OnSelect()
    {
        tRig.AI.WorkingMemory.SetItem<string>("state", "arrested");
    }
    /// <summary>
    /// Set the state to caught when the shooter enter in the safe zone
    /// </summary>
    private void InSafeZone()
    {
        SetState("caugth");
        anim.SetFloat("Speed", 0);
    }
}