using System;
using HoloToolkit.Unity.InputModule;
using RAIN.Entities;
using UnityEngine;
using Random = UnityEngine.Random;


public class Civilian : HumanAI, IInputClickHandler
{
    public GameObject aimSeekList;
    private int randomSpeed;
    /*private AIMContext AimContext;
    private AIMSeek AimSeek;
    private AIMSimpleController AimSimpleController;*/

    // Use this for initialization
    private void Start()
    {
        randomSpeed = Random.Range(RUN_SPEED_MIN,RUN_SPEED_MAX);
        if(aimSeekList == null)
            aimSeekList = GameObject.Find("civils");
        //Adding gameObject Named Entity
        GameObject entity = new GameObject("Entity");
        entity.tag = "aCivil";
        entity.transform.parent = gameObject.transform;
        entity.AddComponent<EntityRig>();
        
        //Initialisation
        init();
        rigidbody.mass = Random.Range(MASS_MIN, MASS_MAX);
        tRig.AI.WorkingMemory.SetItem<float>("speed", NORMAL_SPEED);
        tRig.AI.WorkingMemory.SetItem<string>("state", EnumState.EStates.Normal.ToString());
        tRig.AI.WorkingMemory.SetItem<string>("moveESC", "ESC");
        anim.SetFloat("Speed", ANIM_SPEED);

        //attaching the gameObject to the AI
        tRig.AI.Body = gameObject;
        // Creation of the Sensor
        tRig.AI.Senses.AddSensor(CreateVisualSensor(true, "eyes", 120, new Vector3(0,1.6f ,0), true, 10, Color.green));
        //tRig.AI.Senses.AddSensor(CreateVisualSensor(true, "avoid", 30, new Vector3(0,1.6f ,0), true, 5, Color.red));

   
        //creation of the aspect
        tEntity = entity.GetComponentInChildren<EntityRig>();
        entity.GetComponentInChildren<EntityRig>().Entity.Form = gameObject;
        tEntity.Entity.AddAspect(CreateRainAspect("aCivil"));
        if (NavTargetsGO == null)
        {
            NavTargetsGO =  GameObject.FindWithTag("ShoppingStops");
        }
       
        /*AimContext = gameObject.GetComponent<AIMContext>();

        AimSeek = gameObject.AddComponent<AIMSeek>();
        AimSeek.SteeringBehaviour.TargetObjective = 1;
        for (int i = 0; i < aimSeekList.transform.childCount; i++)
        {
            if(gameObject != aimSeekList.transform.GetChild(i).gameObject && aimSeekList.transform.GetChild(i).gameObject.active == true)
                AimSeek.GameObjects.Add(aimSeekList.transform.GetChild(i).gameObject);
        }
        AimSeek.RadiusSteeringBehaviour.OuterRadius = 5;
        AimSimpleController = gameObject.AddComponent<AIMSimpleController>();
        gameObject.GetComponent<AIMSimpleController>().Speed = 2;*/
        

    }
    // Update is called once per frame
    private void Update()
    {
        switch ((EnumState.EStates)Enum.Parse(typeof( EnumState.EStates), GetState()))
        {
            case EnumState.EStates.Normal:
                tRig.AI.WorkingMemory.SetItem<float>("speed", NORMAL_SPEED/2);
                anim.SetFloat("Speed", !IsMoving() ? 0 : ANIM_SPEED);
                break;
            case EnumState.EStates.Panic:
                //print("panic");
                anim.SetBool("panic", true);
                break;
            case EnumState.EStates.Run:
                anim.SetFloat("Speed", ANIM_SPEED_RUN);
                tRig.AI.WorkingMemory.SetItem<float>("speed", randomSpeed);
                break;
            case EnumState.EStates.Saved:  
                anim.SetFloat("Speed", !IsMoving() ? 0 : ANIM_SPEED);
                tRig.AI.WorkingMemory.SetItem<float>("speed", NORMAL_SPEED);
                break;
        }
    }

    /// <summary>
    /// Method called by Micheal when the IA enter in the danger zone
    /// </summary>
    private void OnInDanger()
    {
        if (GetState() == EnumState.EStates.Panic.ToString() && GetState() == EnumState.EStates.Saved.ToString()) return;
        SetState(EnumState.EStates.Run);
    }
    /// <summary>
    /// Method called by the KeywordHandler when the player say "Police", this method set the state of the civilian
    /// to "Run" if he is not saved. The civilian must be focused
    /// </summary>
    private void OnPolice()
    {
        if (GetState() == EnumState.EStates.Saved.ToString()) return;
        if(isFocused)
            SetState(EnumState.EStates.Run);
    }

    /// <summary>
    /// Set the state to run if the current state is normal or panic
    /// </summary>
    private void OnSelect()
    {
        if (GetState() != EnumState.EStates.Panic.ToString() &&
            GetState() != EnumState.EStates.Normal.ToString()) return;
        anim.SetBool("panic", false);
        SetState(EnumState.EStates.Run);
    }

    /// <summary>
    /// Called when we do a tap movement on the hologram, this function will call OnSelect();
    /// </summary>
    /// <param name="eventData"></param>
    public void OnInputClicked(InputClickedEventData eventData)
    {
        OnSelect();
    }
}