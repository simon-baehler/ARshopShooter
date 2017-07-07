using System;
using HoloToolkit.Unity.InputModule;
using Polarith.AI.Move;
using RAIN.Entities;
using UnityEngine;
using Random = UnityEngine.Random;


public class Civilian : HumanAI, IInputClickHandler
{

    private int randomSpeed;

    // Use this for initialization
    private void Start()
    {
       
        randomSpeed = Random.Range(RUN_SPEED_MIN,RUN_SPEED_MAX);
        
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

        // Creation of the Sensor
        tRig.AI.Body = gameObject;
        tRig.AI.Senses.AddSensor(CreateVisualSensor(true, "eyes", 120, new Vector3(0,1.6f ,0), true));

   
        //creation of the aspect
        tEntity = entity.GetComponentInChildren<EntityRig>();
        entity.GetComponentInChildren<EntityRig>().Entity.Form = gameObject;
        tEntity.Entity.AddAspect(CreateRainAspect("aCivil"));
        if (NavTargetsGO == null)
        {
            NavTargetsGO =  GameObject.FindWithTag("ShoppingStops");
        }
    }

    // Update is called once per frame
    private void Update()
    {
        switch ((EnumState.EStates)Enum.Parse(typeof( EnumState.EStates), GetState()))
        {
            case EnumState.EStates.Normal:
                //print("normal");
                tRig.AI.WorkingMemory.SetItem<float>("speed", NORMAL_SPEED);
                anim.SetFloat("Speed", ANIM_SPEED);
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
                if (!IsMoving())
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

    /// <summary>
    /// Method called by Micheal when the IA enter in the danger zone
    /// </summary>
    private void OnInDanger()
    {
        if (GetState() == EnumState.EStates.Panic.ToString() && GetState() == EnumState.EStates.Saved.ToString()) return;
        SetState(EnumState.EStates.Run);
    }
    /// <summary>
    /// TO DO
    /// </summary>
    private void OnPolice()
    {
        if (GetState() == EnumState.EStates.Saved.ToString()) return;
        print("lol");
        if(isFocused)
            SetState(EnumState.EStates.Run);
    }

    /// <summary>
    /// Set the stat to run if the current state is normal or panic
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